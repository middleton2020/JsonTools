using System;
using System.Collections.Generic;

namespace CM.JsonTools
{
    public class JsonReader
    {
        public enum DataMode
        {
            None,
            Name,
            Value
        }
        public enum DataType
        {
            None,
            Array,
            ArrayEnd,
            Class,
            ClassEnd,
            Boolean,
            Decimal,
            Integer,
            String
        }

        #region LocalVariables
        // List of nodes that we've created.
        Dictionary<int, node> nodeArray = new Dictionary<int, node>();
        // Counter of what the next index number for the node.
        int nodeInstanceCounter = 1;
        #endregion

        #region PassedDeligates
        public delegate void DeligateMakeClass(string inpName);
        public delegate void DeligateCloseClass(string inpName);
        public delegate void DeligateMakeArray(string inpName);
        public delegate void DeligateCloseArray(string inpName);
        public delegate void DeligateSetBoolean(string inpName, bool inpValue);
        public delegate void DeligateSetDecimal(string inpName, decimal inpValue);
        public delegate void DeligateSetInteger(string inpName, int inpValue);
        public delegate void DeligateSetString(string inpName, string inpValue);

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
            makeClass = inpMakeClass;
            closeClass = inpCloseClass;
            makeArray = inpMakeArray;
            closeArray = inpCloseArray;
            setBoolean = inpSetBoolean;
            setDecimal = inpSetDecimal;
            setInteger = inpSetInteger;
            setString = inpSetString;
        }
        #endregion

        #region SubClasses
        /// <summary>
        /// A node of the JSON holds a single data item, be it a 'class', an array or a field.
        /// </summary>
        public class node
        {
            public int instance;
            public string fieldName;
            public string fieldValue;
            public DataType dataType;
            public int parentNode;

            /// <summary>
            /// Constructor, populating the relevant values.
            /// </summary>
            /// <param name="inpInstance">Int, unique number for the node.</param>
            /// <param name="inpName">String, the name of the node element.</param>
            /// <param name="inpValue">String, the value of the node element.</param>
            /// <param name="inpType">DataType enum, what is the data type?</param>
            /// <param name="inpParent">Int, instance number for the node that 'contains' this node.</param>
            public node(int inpInstance,
                string inpName,
                string inpValue,
                DataType inpType,
                int inpParent)
            {
                instance = inpInstance;
                fieldName = inpName;
                fieldValue = inpValue;
                dataType = inpType;
                parentNode = inpParent;
            }
        }
        #endregion

        #region Methods
        public void ReadJson(string inpJsonObject)
        {
            try
            {
                BreakIntoNodes(inpJsonObject);
                BuildClass();
            }
            catch
            {
                throw;
            }
        }

        private void BuildClass()
        {
            try
            {
                foreach (KeyValuePair<int, node> entry in nodeArray)
                {
                    node CurrentNode = entry.Value;
                    switch (CurrentNode.dataType)
                    {
                        case DataType.None:
                            // Ignore these, they have no value
                            break;
                        case DataType.Array:
                            makeArray(entry.Value.fieldName);
                            break;
                        case DataType.ArrayEnd:
                            closeArray(entry.Value.fieldName);
                            break;
                        case DataType.Class:
                            makeClass(entry.Value.fieldName);
                            break;
                        case DataType.ClassEnd:
                            closeClass(entry.Value.fieldName);
                            break;
                        case DataType.Boolean:
                            bool fieldBoolValue = Convert.ToBoolean(entry.Value.fieldValue);
                            setBoolean(entry.Value.fieldName, fieldBoolValue);
                            break;
                        case DataType.Decimal:
                            decimal fieldDecimalValue = Convert.ToDecimal(entry.Value.fieldValue);
                            setDecimal(entry.Value.fieldName, fieldDecimalValue);
                            break;
                        case DataType.Integer:
                            int fieldIntegerValue = Convert.ToInt32(entry.Value.fieldValue);
                            setInteger(entry.Value.fieldName, fieldIntegerValue);
                            break;
                        case DataType.String:
                            setString(entry.Value.fieldName, entry.Value.fieldValue);
                            break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add a new node to the array.
        /// </summary>
        /// <param name="inpType">DataType Enum. Type of node</param>
        /// <param name="inpName">String, Name of node</param>
        /// <param name="inpValue">String, Value of node</param>
        /// <param name="inpParent">Int, instance number for the node that 'contains' this node</param>
        /// <returns></returns>
        public node addNode(DataType inpType, string inpName, string inpValue, int inpParent)
        {
            try
            {
                if (inpType == DataType.None)
                {
                    // If no data type has been provided, then return the current node.
                    return nodeArray[nodeInstanceCounter - 1];
                }
                else
                {
                    // Create a new node, add it to the nodeArray and return the new node.
                    node NodeEntry = new node(nodeInstanceCounter, inpName, inpValue, inpType, inpParent);
                    nodeInstanceCounter += 1;
                    nodeArray.Add(NodeEntry.instance, NodeEntry);
                    return NodeEntry;
                }
            }
            catch
            {
                throw;
            }
        }

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
                DataMode Mode = DataMode.Name;
                DataType Type = DataType.Array;
                string Name = "Root";
                string Value = "";
                // Control variables
                int Parent = 1;
                node ParentNode = addNode(DataType.Array, "TopLevel", "", 0);
                Boolean InString = false;
                node currentNode = null;

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
                            if (Mode == DataMode.Name)
                            {
                                Name += letter;
                            }
                            else if (Mode == DataMode.Value)
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
                                if (Mode == DataMode.Value)
                                {
                                    Type = DataType.String;
                                }
                                else
                                {
                                    Mode = DataMode.Name;
                                }
                                InString = true;
                                break;

                            // The colon indicates that we're moving from name to value.
                            case ':':
                                Mode = DataMode.Value;
                                Value = "";
                                break;

                            // Commas separate the elements (i.e. nodes).
                            case ',':
                                // We've finished reading the text. If there are no speach marks, but the value is
                                if (Type == DataType.Integer)
                                {
                                    if (Value == Boolean.TrueString ||
                                        Value == Boolean.FalseString)
                                    {
                                        Type = DataType.Boolean;
                                    }
                                }
                                currentNode = addNode(Type, Name, Value, Parent);
                                Mode = DataMode.Name;
                                Name = "";
                                Value = "";
                                break;

                            // Start processing a JSON group.
                            case '{':
                                Type = DataType.Class;
                                Value = "";
                                currentNode = addNode(Type, Name, Value, Parent);
                                Parent = currentNode.instance;
                                ParentNode = nodeArray[Parent];
                                Mode = DataMode.Name;
                                Type = DataType.Integer;
                                Name = "";
                                break;

                            // and close that JSON group.
                            case '}':
                                currentNode = addNode(Type, Name, Value, Parent);
                                Type = DataType.ClassEnd;
                                Name = "";
                                Value = "";
                                break;

                            // Start processing an array of data.
                            case '[':
                                Type = DataType.Array;
                                Value = "";
                                currentNode = addNode(Type, Name, Value, Parent);
                                Parent = currentNode.instance;
                                ParentNode = nodeArray[Parent];
                                Mode = DataMode.Name;
                                Type = DataType.Integer;
                                Name = "";
                                break;

                            // Finish processing the array.
                            case ']':
                                currentNode = addNode(Type, Name, Value, Parent);
                                Type = DataType.ArrayEnd;
                                Name = "";
                                Value = "";
                                break;

                            // If we finde a '.' in an integer, change it to a decimal.
                            case '.':
                                if (Mode == DataMode.Name)
                                {
                                    Name += letter;
                                }
                                else if (Mode == DataMode.Value)
                                {
                                    Value += letter;
                                    if (Type == DataType.Integer)
                                    {
                                        Type = DataType.Decimal;
                                    }
                                }
                                break;

                            // Any other character is just added to the current details.
                            default:
                                if (Mode == DataMode.Name)
                                {
                                    Name += letter;
                                }
                                else if (Mode == DataMode.Value)
                                {
                                    Value += letter;
                                }
                                break;
                        }
                    }

                    // If we're closing a JSON element or Array, process that before moving onto the next letter.
                    if (Type == DataType.ClassEnd || Type == DataType.ArrayEnd)
                    {
                        ParentNode = nodeArray[Parent];
                        Name = ParentNode.fieldName;
                        addNode(Type, Name, Value, Parent);
                        Parent = ParentNode.parentNode;
                        Mode = DataMode.Name;
                        Type = DataType.None;
                    }
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
