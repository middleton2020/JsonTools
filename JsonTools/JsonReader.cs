using System;
using System.Collections.Generic;

namespace CM.JsonTools
{
    public class JsonReader
    {
        #region LocalVariables
        private NodeManager nodeManager;
        #endregion

        #region PassedDeligates
        public delegate object DeligateMakeClass(string inpName, object inpObject, string inpPath);
        public delegate object DeligateCloseClass(string inpName, object inpObject, string inpPath);
        public delegate object DeligateMakeArray(string inpName, object inpObject, string inpPath);
        public delegate object DeligateCloseArray(string inpName, object inpObject, string inpPath);
        public delegate object DeligateSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetInteger(string inpName, int inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetString(string inpName, string inpValue, object inpObject, string inpPath);

        DeligateMakeClass makeClass;
        DeligateCloseClass closeClass;
        DeligateMakeArray makeArray;
        DeligateCloseArray closeArray;
        DeligateSetBoolean setBoolean;
        DeligateSetDecimal setDecimal;
        DeligateSetInteger setInteger;
        DeligateSetString setString;
        #endregion

        #region Constructors
        /// <summary>
        /// Set up the deligates passed from the calling program. We'll use these to build a class from our JSON nodes.
        /// </summary>
        /// <param name="inpMakeClass">Method to create a new class</param>
        /// <param name="inpCloseClass">Method to close that class</param>
        /// <param name="inpMakeArray">Method to create a new array</param>
        /// <param name="inpCloseArray">Method to close that array</param>
        /// <param name="inpSetBoolean">Method to set boolean fields</param>
        /// <param name="inpSetDecimal">Method to set decimal fields</param>
        /// <param name="inpSetInteger">Method to set integer fields</param>
        /// <param name="inpSetString">Method to set string fields</param>
        public JsonReader(DeligateMakeClass inpMakeClass,
                          DeligateCloseClass inpCloseClass,
                          DeligateMakeArray inpMakeArray,
                          DeligateCloseArray inpCloseArray,
                          DeligateSetBoolean inpSetBoolean,
                          DeligateSetDecimal inpSetDecimal,
                          DeligateSetInteger inpSetInteger,
                          DeligateSetString inpSetString)
        {
            try
            {
                makeClass = inpMakeClass;
                closeClass = inpCloseClass;
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
                NodeManager.node ParentNode = nodeManager.addNode(NodeManager.DataType.Top, "", "", 0);
                Boolean InString = false;
                NodeManager.node currentNode = null;

                // Break the JSON up into an array of characters.
                char[] byCharacters = inpJsonObject.ToCharArray();

                // Then step through it, character by character.
                foreach (char letter in byCharacters)
                {
                    // Rebuild any quoited strings to make use them properly.
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

                                currentNode = nodeManager.addNode(Type, Name, Value, Parent);
                                Mode = NodeManager.DataMode.Name;
                                Name = "";
                                Value = "";
                                break;

                            // Start processing a JSON group.
                            case '{':
                                Type = NodeManager.DataType.Object;
                                Value = "";
                                currentNode = nodeManager.addNode(Type, Name, Value, Parent);
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

                                currentNode = nodeManager.addNode(Type, Name, Value, Parent);
                                Type = NodeManager.DataType.ObjectClose;
                                Name = "";
                                Value = "";
                                break;

                            // Start processing an array of data.
                            case '[':
                                Type = NodeManager.DataType.Array;
                                Value = "";
                                currentNode = nodeManager.addNode(Type, Name, Value, Parent);
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

                                currentNode = nodeManager.addNode(Type, Name, Value, Parent);
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
                        nodeManager.addNode(Type, Name, Value, Parent);
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
                foreach (KeyValuePair<int, NodeManager.node> entry in nodeManager.nodeArray)
                {
                    NodeManager.node CurrentNode = entry.Value;
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
                            inpTempObject = makeClass(entry.Value.fieldName, inpTempObject, entry.Value.nodePath);
                            break;
                        case NodeManager.DataType.ObjectClose:
                            inpTempObject = closeClass(entry.Value.fieldName, inpTempObject, entry.Value.nodePath);
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
    }
}
