using System;
using System.Collections.Generic;

namespace CM.JsonTools
{
    public class JsonReader : IDisposable
    {
        #region LocalVariables
        private NodeManager nodeManager;
        private bool disposedValue;
        private bool allowNulls;
        #endregion

        #region PassedDeligates
        public delegate object DeligateMakeObject(string inpName, object inpObject, string inpPath);
        public delegate object DeligateCloseObject(string inpName, object inpObject, string inpPath);
        public delegate object DeligateMakeArray(string inpName, object inpObject, string inpPath);
        public delegate object DeligateCloseArray(string inpName, object inpObject, string inpPath);
        public delegate object DeligateSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetDouble(string inpName, double inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetInteger(string inpName, int inpValue, object inpObject, string inpPath);
        public delegate object DeligateSetLongInt(string inpName, long inpValuem, object inpObject, string inpPath);
        public delegate object DeligateSetString(string inpName, string inpValue, object inpObject, string inpPath);

        readonly DeligateMakeObject makeObject;
        readonly DeligateCloseObject closeObject;
        readonly DeligateMakeArray makeArray;
        readonly DeligateCloseArray closeArray;
        readonly DeligateSetBoolean setBoolean;
        readonly DeligateSetDecimal setDecimal;
        readonly DeligateSetDouble setDouble;
        readonly DeligateSetInteger setInteger;
        readonly DeligateSetLongInt setLongInt;
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
                          DeligateSetString inpSetString) : this(inpMakeObject, inpCloseObject,
                              inpMakeArray, inpCloseArray, inpSetBoolean, inpSetDecimal,
                              inpSetInteger, inpSetString, true)
        {
            try
            {
            }
            catch
            {
                throw;
            }
        }

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
        /// <param name="inpAllowNulls">Boolean, indicates if null values can be loaded. Defaults to true</param>
        public JsonReader(DeligateMakeObject inpMakeObject,
                          DeligateCloseObject inpCloseObject,
                          DeligateMakeArray inpMakeArray,
                          DeligateCloseArray inpCloseArray,
                          DeligateSetBoolean inpSetBoolean,
                          DeligateSetDecimal inpSetDecimal,
                          DeligateSetInteger inpSetInteger,
                          DeligateSetString inpSetString,
                          bool inpAllowNulls)
        {
            try
            {
                makeObject = inpMakeObject;
                closeObject = inpCloseObject;
                makeArray = inpMakeArray;
                closeArray = inpCloseArray;
                setBoolean = inpSetBoolean;
                setDecimal = inpSetDecimal;
                setDouble = FakeDoubleProcessing;
                setInteger = inpSetInteger;
                setLongInt = FakeLongIntProcessing;
                setString = inpSetString;
                allowNulls = inpAllowNulls;
            }
            catch
            {
                throw;
            }
        }

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
                          DeligateSetDouble inpSetDouble,
                          DeligateSetInteger inpSetInteger,
                          DeligateSetLongInt inpSetLongInt,
                          DeligateSetString inpSetString) : this(inpMakeObject, inpCloseObject,
                              inpMakeArray, inpCloseArray, inpSetBoolean, inpSetDecimal,
                              inpSetDouble, inpSetInteger, inpSetLongInt, inpSetString, true)
        {
            try
            {
            }
            catch
            {
                throw;
            }
        }

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
                          DeligateSetDouble inpSetDouble,
                          DeligateSetInteger inpSetInteger,
                          DeligateSetLongInt inpSetLongInt,
                          DeligateSetString inpSetString,
                          bool inpAllowNulls) : this(inpMakeObject, inpCloseObject,
                              inpMakeArray, inpCloseArray, inpSetBoolean, inpSetDecimal,
                              inpSetInteger, inpSetString, inpAllowNulls)
        {
            try
            {
                setDouble = inpSetDouble;
                setLongInt = inpSetLongInt;
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
                                Type = LongCheck(Type, Value);

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

                            // If we finde a '.' in an integer, change it to a double (or decimal if it is too big).
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
                                        Type = NodeManager.DataType.Double;
                                        // If we've too many decimal places to fit in a double, use a decimal.
                                        if (Value.Split('.')[1].Length > 9)
                                        {
                                            Type = NodeManager.DataType.Decimal;
                                        }
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
                    try     // Catch any errors in converting the data types.
                    {
                        switch (CurrentNode.dataType)
                        {
                            case NodeManager.DataType.None:
                                // Ignore these, they have no value
                                break;
                            case NodeManager.DataType.Top:
                                // This is just the top level to ensure that each node has a parent
                                break;
                            case NodeManager.DataType.Array:
                                inpTempObject = makeArray(CurrentNode.fieldName, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.ArrayClose:
                                inpTempObject = closeArray(CurrentNode.fieldName, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.Object:
                                inpTempObject = makeObject(CurrentNode.fieldName, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.ObjectClose:
                                inpTempObject = closeObject(CurrentNode.fieldName, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.Boolean:
                                bool fieldBoolValue = Convert.ToBoolean(CurrentNode.fieldValue);
                                inpTempObject = setBoolean(CurrentNode.fieldName, fieldBoolValue, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.Double:
                                double fieldDoubleValue = Convert.ToDouble(CurrentNode.fieldValue);
                                inpTempObject = setDouble(CurrentNode.fieldName, fieldDoubleValue, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.Decimal:
                                decimal fieldDecimalValue = Convert.ToDecimal(CurrentNode.fieldValue);
                                inpTempObject = setDecimal(CurrentNode.fieldName, fieldDecimalValue, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.Integer:
                                int fieldIntegerValue = Convert.ToInt32(CurrentNode.fieldValue);
                                inpTempObject = setInteger(CurrentNode.fieldName, fieldIntegerValue, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.LongInt:
                                long fieldLongIntValue = Convert.ToInt64(CurrentNode.fieldValue);
                                inpTempObject = setLongInt(CurrentNode.fieldName, fieldLongIntValue, inpTempObject, CurrentNode.nodePath);
                                break;
                            case NodeManager.DataType.String:
                                inpTempObject = setString(CurrentNode.fieldName, CurrentNode.fieldValue, inpTempObject, entry.Value.nodePath);
                                break;
                        }
                    }
                    // If we've passed the wrong data type into a deligate, then treat it as a string instead.
                    catch (System.FormatException e)
                    {
                        if (e.Message == "Input string was not in a correct format.")
                        {
                            inpTempObject = setString(CurrentNode.fieldName, CurrentNode.fieldValue, inpTempObject, entry.Value.nodePath);
                        }
                        else
                        {
                            e.Data.Add("UserMessage", $"An error occurred while processing {CurrentNode.fieldName}");
                            throw e;
                        }
                    }
                    catch
                    {
                        throw;
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
                // We've finished reading the text. If there are no speach marks, but the value is not actually a number.
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
        /// <summary>
        /// Is this node an integer, or is it too big for that?
        /// </summary>
        /// <param name="inpType">Pass Node.type in here.</param>
        /// <param name="inpValue">Pass Node.value in here.</param>
        /// <returns>Returns Node.type, updated a suitable number value, if appropriate.</returns>
        private NodeManager.DataType LongCheck(NodeManager.DataType inpType, string inpValue)
        {
            try
            {
                // We've finished reading the text. If the value is a number, but too big to hold in an Int.
                if (inpType == NodeManager.DataType.Integer)
                {
                    // If the value is less than 10 characters long, it won't exceed the Int size.
                    if (inpValue.Length > 9)
                    {
                        // If the value is more than 18 characters long, it may blow the Long Int, so use a Double. God help us if it exceeds that!
                        if (inpValue.Length > 18)
                        {
                            if (Convert.ToDouble(inpValue) >= long.MaxValue)
                            {
                                inpType = NodeManager.DataType.Double;
                            }
                        }
                        // It fits in a Long, so use it.
                        else
                        {
                            if (Convert.ToInt64(inpValue) >= int.MaxValue)
                            {
                                inpType = NodeManager.DataType.LongInt;
                            }
                        }
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

        #region FakeDeligates
        /// <summary>
        /// If no deligate has been provided for Doubles, pass them to the Decimal deligate.
        /// </summary>
        /// <param name="inpName">Name of Node</param>
        /// <param name="inpValue">The value (as a double) for the node.</param>
        /// <param name="inpTempObject">The object that we're building up.</param>
        /// <param name="inpPath">Full path of Node</param>
        /// <returns>The object that we're building up.</returns>
        private object FakeDoubleProcessing(string inpName, double inpValue, object inpTempObject, string inpPath)
        {
            try
            {
                // Run the decimal value supplied by the calling program.
                inpTempObject = setDecimal(inpName, (decimal)inpValue, inpTempObject, inpPath);
                return inpTempObject;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// If no deligate has been provided for Long Ints, pass them to the Integer deligate.
        /// </summary>
        /// <param name="inpName">Name of Node</param>
        /// <param name="inpValue">The value (as a ulong) for the node.</param>
        /// <param name="inpTempObject">The object that we're building up.</param>
        /// <param name="inpPath">Full path of Node</param>
        /// <returns>The object that we're building up.</returns>
        private object FakeLongIntProcessing(string inpName, long inpValue, object inpTempObject, string inpPath)
        {
            try
            {
                // Error if the value is too big.
                if (inpValue < int.MaxValue)
                {
                    // Run the integer version supplied by the calling program.
                    inpTempObject = setInteger(inpName, (int)inpValue, inpTempObject, inpPath);
                    return inpTempObject;
                }
                else
                {
                    throw new InvalidCastException("Your integer it too large to process, you will need a LongInt deligate.");
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
