using System;
using System.Collections.Generic;

namespace CM.JsonTools
{
    public class JsonReader: IDisposable
    {
        #region LocalVariables
        private NodeManager nodeManager;
        private bool disposedValue;
        #endregion

        #region PassedDeligates
        public delegate object DeligateMakeObject(string inpName, object inpObject, string inpPath);
        public delegate object DeligateCloseObject(string inpName, object inpObject, string inpPath);
        public delegate object DeligateMakeArray(string inpName, object inpObject, string inpPath);
        public delegate object DeligateCloseArray(string inpName, object inpObject, string inpPath);
        public delegate object DeligateSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetInteger(string inpName, int inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetString(string inpName, string inpValue, object inpObject, string inpPath);

        readonly DeligateMakeObject makeObject;
        readonly DeligateCloseObject closeObject;
        readonly DeligateMakeArray makeArray;
        readonly DeligateCloseArray closeArray;
        readonly DeligateSetBoolean setBoolean;
        readonly DeligateSetDecimal setDecimal;
        readonly DeligateSetInteger setInteger;
        readonly DeligateSetString setString;
        #endregion

        #region Constructors
        /// <summary>
        /// Set up the deligates passed from the calling program. We'll use these to build a class from our JSON nodes.
        /// </summary>
        /// <param name="inpMakeObject">Method to create a new class</param>
        /// <param name="inpCloseObject">Method to close that class</param>
        /// <param name="inpMakeArray">Method to create a new array</param>
        /// <param name="inpCloseArray">Method to close that array</param>
        /// <param name="inpSetBoolean">Method to set boolean fields</param>
        /// <param name="inpSetDecimal">Method to set decimal fields</param>
        /// <param name="inpSetInteger">Method to set integer fields</param>
        /// <param name="inpSetString">Method to set string fields</param>
        public JsonReader(DeligateMakeObject inpMakeObject,
                          DeligateCloseObject inpCloseObject,
                          DeligateMakeArray inpMakeArray,
                          DeligateCloseArray inpCloseArray,
                          DeligateSetBoolean inpSetBoolean,
                          DeligateSetDecimal inpSetDecimal,
                          DeligateSetInteger inpSetInteger,
                          DeligateSetString inpSetString)
        {
            try
            {
                makeObject = inpMakeObject;
                closeObject = inpCloseObject;
                makeArray = inpMakeArray;
                closeArray = inpCloseArray;
                setBoolean = inpSetBoolean;
                setDecimal = inpSetDecimal;
                setInteger = inpSetInteger;
                setString = inpSetString;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Read in the JSON object and return the data in the specified output. Uses to deligates to build the output.
        /// </summary>
        /// <param name="inpJsonObject">The JSON string to examine and split.</param>
        /// <param name="inpTempObject">The object that we are updating. This must be initialised before being passed in.</param>
        /// <returns>inpTempObject once all data has been written to it.</returns>
        public object ReadJson(string inpJsonObject, object inpTempObject)
        {
            try
            {
                // Prepare the node tools/storage.
                nodeManager = new NodeManager();
                // Populate the Node records.
                BreakIntoNodes(inpJsonObject);
                // Read the Node records to build the requested output.
                inpTempObject = OutputData(inpTempObject);

                return inpTempObject;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Split the JSON into a series of nodes, from which we can build our data classes.
        /// </summary>
        /// <param name="inpJsonObject">The JSON to read, passed as a string</param>
        private void BreakIntoNodes(string inpJsonObject)
        {
            try
            {
                // The escape character so that we can by-pass it.
                const char escapeCharacter = (char)92;

                // Default the variables.
                NodeManager.DataMode Mode = NodeManager.DataMode.Name;
                NodeManager.DataType Type = NodeManager.DataType.Object;
                string Name = "";
                string Value = "";
                // Control variables
                int Parent = 1;
                NodeManager.Node ParentNode = nodeManager.AddNode(NodeManager.DataType.Top, "", "", 0);
                Boolean InString = false;
                NodeManager.Node currentNode = null;

                // Break the JSON up into an array of characters.
                char[] byCharacters = inpJsonObject.ToCharArray();

                // Then step through it, character by character.
                foreach (char letter in byCharacters)
                {
                    // Rebuild any quoted strings to make use them properly.
                    if (InString == true)
                    {
                        // Close the string.
                        if (letter == '"')
                        {
                            InString = false;
                        }
                        else
                        {
                            if (Mode == NodeManager.DataMode.Name)
                            {
                                Name += letter;
                            }
                            else if (Mode == NodeManager.DataMode.Value)
                            {
                                Value += letter;
                            }
                        }
                    }
                    else
                    {
                        switch (letter)
                        {
                            case escapeCharacter:
                                // We simply skip over escape characters.
                                break;

                            // Open a string.
                            case '"':
                                if (Mode == NodeManager.DataMode.Value)
                                {
                                    Type = NodeManager.DataType.String;
                                }
                                else
                                {
                                    Mode = NodeManager.DataMode.Name;
                                }
                                InString = true;
                                break;

                            // The colon indicates that we're moving from name to value.
                            case ':':
                                Mode = NodeManager.DataMode.Value;
                                Type = NodeManager.DataType.Integer;    // Default to this.
                                Value = "";
                                break;

                            // Commas separate the elements (i.e. nodes).
                            case ',':
                                // We've finished reading the text. If there are no speach marks, but the value is
                                Type = BooleanCheck(Type, Value);

                                currentNode = nodeManager.AddNode(Type, Name, Value, Parent);
                                Mode = NodeManager.DataMode.Name;
                                Name = "";
                                Value = "";
                                break;

                            // Start processing a JSON group.
                            case '{':
                                Type = NodeManager.DataType.Object;
                                Value = "";
                                currentNode = nodeManager.AddNode(Type, Name, Value, Parent);
                                Parent = currentNode.instance;
                                ParentNode = nodeManager.nodeArray[Parent];
                                Mode = NodeManager.DataMode.Name;
                                Type = NodeManager.DataType.Integer;
                                Name = "";
                                break;

                            // and close that JSON group.
                            case '}':
                                // We've finished reading the text. If there are no speach marks, but the value is
                                Type = BooleanCheck(Type, Value);

                                currentNode = nodeManager.AddNode(Type, Name, Value, Parent);
                                Type = NodeManager.DataType.ObjectClose;
                                Name = "";
                                Value = "";
                                break;

                            // Start processing an array of data.
                            case '[':
                                Type = NodeManager.DataType.Array;
                                Value = "";
                                currentNode = nodeManager.AddNode(Type, Name, Value, Parent);
                                Parent = currentNode.instance;
                                ParentNode = nodeManager.nodeArray[Parent];
                                Mode = NodeManager.DataMode.Name;
                                Type = NodeManager.DataType.Integer;
                                Name = "";
                                break;

                            // Finish processing the array.
                            case ']':
                                // We've finished reading the text. If there are no speach marks, but the value is
                                Type = BooleanCheck(Type, Value);

                                currentNode = nodeManager.AddNode(Type, Name, Value, Parent);
                                Type = NodeManager.DataType.ArrayClose;
                                Name = "";
                                Value = "";
                                break;

                            // If we finde a '.' in an integer, change it to a decimal.
                            case '.':
                                if (Mode == NodeManager.DataMode.Name)
                                {
                                    Name += letter;
                                }
                                else if (Mode == NodeManager.DataMode.Value)
                                {
                                    Value += letter;
                                    if (Type == NodeManager.DataType.Integer)
                                    {
                                        Type = NodeManager.DataType.Decimal;
                                    }
                                }
                                break;

                            // Any other character is just added to the current details.
                            default:
                                if (Mode == NodeManager.DataMode.Name)
                                {
                                    Name += letter;
                                }
                                else if (Mode == NodeManager.DataMode.Value)
                                {
                                    Value += letter;
                                }
                                break;
                        }
                    }

                    // If we're closing a JSON element or Array, process that before moving onto the next letter.
                    if (Type == NodeManager.DataType.ObjectClose || Type == NodeManager.DataType.ArrayClose)
                    {
                        ParentNode = nodeManager.nodeArray[Parent];
                        Name = ParentNode.fieldName;
                        nodeManager.AddNode(Type, Name, Value, Parent);
                        Parent = ParentNode.parentNode;
                        Mode = NodeManager.DataMode.Name;
                        Type = NodeManager.DataType.None;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Read the Node data and export it to the inpTempObject using the deligates to apply the right structure.
        /// </summary>
        /// <param name="inpTempObject">The object that we are populating.</param>
        /// <returns>inpTempObject, now full of data.</returns>
        private object OutputData(object inpTempObject)
        {
            try
            {
                foreach (KeyValuePair<int, NodeManager.Node> entry in nodeManager.nodeArray)
                {
                    NodeManager.Node CurrentNode = entry.Value;
                    switch (CurrentNode.dataType)
                    {
                        case NodeManager.DataType.None:
                            // Ignore these, they have no value
                            break;
                        case NodeManager.DataType.Top:
                            // This is just the top level to ensure that each node has a parent
                            break;
                        case NodeManager.DataType.Array:
                            inpTempObject = makeArray(entry.Value.fieldName, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.ArrayClose:
                            inpTempObject = closeArray(entry.Value.fieldName, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.Object:
                            inpTempObject = makeObject(entry.Value.fieldName, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.ObjectClose:
                            inpTempObject = closeObject(entry.Value.fieldName, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.Boolean:
                            bool fieldBoolValue = Convert.ToBoolean(entry.Value.fieldValue);
                            inpTempObject = setBoolean(entry.Value.fieldName, fieldBoolValue, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.Decimal:
                            decimal fieldDecimalValue = Convert.ToDecimal(entry.Value.fieldValue);
                            inpTempObject = setDecimal(entry.Value.fieldName, fieldDecimalValue, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.Integer:
                            int fieldIntegerValue = Convert.ToInt32(entry.Value.fieldValue);
                            inpTempObject = setInteger(entry.Value.fieldName, fieldIntegerValue, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.String:
                            inpTempObject = setString(entry.Value.fieldName, entry.Value.fieldValue, inpTempObject, entry.Value.nodePath);
                            break;
                    }
                }
                return inpTempObject;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Is this node a boolean or an integer?
        /// </summary>
        /// <param name="inpType">Pass Node.type in here.</param>
        /// <param name="inpValue">Pass Node.value in here.</param>
        /// <returns>Returns Node.type, updated as a boolean if appropriate.</returns>
        private NodeManager.DataType BooleanCheck(NodeManager.DataType inpType, string inpValue)
        {
            try
            {
                // We've finished reading the text. If there are no speach marks, but the value is
                if (inpType == NodeManager.DataType.Integer)
                {
                    if (inpValue.ToLower() == Boolean.TrueString.ToLower() ||
                        inpValue.ToLower() == Boolean.FalseString.ToLower())
                    {
                        inpType = NodeManager.DataType.Boolean;
                    }
                }
                return inpType;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region DisposalMethods
        /// <summary>
        /// Clear down eresources that are using lots of memory.
        /// </summary>
        /// <param name="disposing">Boolean, is this being automatially disposed of, or explicityl disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    nodeManager.Dispose();
                }

                // Note disposing has been done.
                disposedValue = true;
            }
        }

        /// <summary>
        /// Finalizer for the NodeManager.
        /// </summary>
        // Use C# finalizer syntax for finalization code.
        // This finalizer will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide finalizer in types derived from this class.
        ~JsonReader()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(disposing: false) is optimal in terms of
            // readability and maintainability.
            Dispose(disposing: false);
        }

        /// <summary>
        /// Active removal of unwanted objects.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SuppressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
