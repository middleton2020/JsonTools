using System;
using System.Collections.Generic;
using System.Text;

namespace CM.JsonTools
{
    public class JsonWriter
    {
        #region LocalVariables
        private NodeManager nodeManager;
        private int currentParent = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Preprare the JsonWriter to recieve instructions.
        /// </summary>
        public JsonWriter()
        {
            try
            {
                // Prepare the node tools/storage.
                nodeManager = new NodeManager();
                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.Top,
                        "", "", 0);
                currentParent = currentNode.instance;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region StructureNodes
        /// <summary>
        /// Create a node to open an object. Object will have no name.
        /// </summary>
        public void OpenObject()
        {
            try
            {
                OpenObject("");
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Create a node to open an object.
        /// </summary>
        /// <param name="inpName">Name to give to the array object.</param>
        public void OpenObject(string inpName)
        {
            try
            {
                if (inpName == null)
                {
                    throw new ArgumentException($"You cannot supply a null value for a name.", nameof(inpName));
                }

                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.Object,
                inpName, "", currentParent);
                currentParent = currentNode.instance;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create a node to close an object.
        /// </summary>
        public void CloseObject()
        {
            try
            {
                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.ObjectClose,
                "", "", currentParent);
                currentParent = nodeManager.nodeArray[currentNode.parentNode].parentNode;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create a node to open an array. Array will have no name.
        /// </summary>
        public void OpenArray()
        {
            try
            {
                OpenArray("");
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Create a node to open an array.
        /// </summary>
        /// <param name="inpName">Name to give to the array object.</param>
        public void OpenArray(string inpName)
        {
            try
            {
                if (inpName == null)
                {
                    throw new ArgumentException($"You cannot supply a null value for a name.", nameof(inpName));
                }

                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.Array,
                inpName, "", currentParent);
                currentParent = currentNode.instance;

                //currentNode = nodeManager.addNode(Type, Name, Value, Parent);
                //Parent = currentNode.instance;
                //ParentNode = nodeManager.nodeArray[Parent];
                //Mode = NodeManager.DataMode.Name;
                //Type = NodeManager.DataType.Integer;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create a node to close an array.
        /// </summary>
        public void CloseArray()
        {
            try
            {
                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.ArrayClose,
                    "", "", currentParent);
                currentParent = nodeManager.nodeArray[currentNode.parentNode].parentNode;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region PropertyNodes
        /// <summary>
        /// Add a property node with a type of boolean.
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">Boolean holding the value of the node.</param>
        public void AddNode(string inpName, bool inpValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inpName))
                {
                    throw new ArgumentException($"You must supply a name for a property node.", nameof(inpName));
                }

                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.Boolean,
                inpName, inpValue.ToString(), currentParent);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add a property node with a type of date (saves as a string).
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">DateTime holding the value of the node.</param>
        public void AddNode(string inpName, DateTime inpValue)
        {
            try
            {
                // Set a default format for just the date.
                string DateTimeFormat = "yyyy-MM-dd";
                AddNode(inpName, inpValue, DateTimeFormat);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Add a property node with a type of date (saves as a string).
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">DateTime holding the value of the node.</param>
        /// <param name="inpFormat">String specifying the format to apply to the date.</param>
        public void AddNode(string inpName, DateTime inpValue, string inpFormat)
        {
            try
            {
                // Validate tyhe inputs.
                if (string.IsNullOrWhiteSpace(inpName))
                {
                    throw new ArgumentException($"You must supply a name for a property node.", nameof(inpName));
                }
                if (inpValue == null)
                {
                    throw new ArgumentException($"You cannot write a null value to the {inpName} node.", nameof(inpValue));
                }

                // Create the node.
                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.String,
                inpName, inpValue.ToString(inpFormat), currentParent);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Add a property node with a type of date (saves as a string).
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">DateTime holding the value of the node.</param>
        /// <param name="inpShowTime">Pass true to add the time to the display format.</param>
        public void AddNode(string inpName, DateTime inpValue, bool inpShowTime)
        {
            try
            {
                // Set a default format for just the date.
                string DateTimeFormat = "yyyy-MM-dd";
                if (inpShowTime)
                {
                    // Set a default format for just the date and time.
                    DateTimeFormat += "THH:mm:ss.fff";
                }
                AddNode(inpName, inpValue, DateTimeFormat);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Add a property node with a type of date (saves as a string).
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">DateTime holding the value of the node.</param>
        /// <param name="inpShowTime">Pass true to add the time to the display format.</param>
        /// <param name="inpShowTimeZone">Pass true to add teh time-zone to the display format.</param>
        public void AddNode(string inpName, DateTime inpValue, bool inpShowTime, bool inpShowTimeZone)
        {
            try
            {
                // Set a default format for just the date.
                string DateTimeFormat = "yyyy-MM-dd";
                if (inpShowTime)
                {
                    // Set a default format for just the date and time.
                    DateTimeFormat += "THH:mm:ss.fff";
                }
                if (inpShowTimeZone)
                {
                    // Set a default format for just the date, time and time-zone.
                    DateTimeFormat += "+zzz";
                }

                AddNode(inpName, inpValue, DateTimeFormat);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add a property node with a type of decimal.
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">Decimal holding the value of the node.</param>
        public void AddNode(string inpName, decimal inpValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inpName))
                {
                    throw new ArgumentException($"You must supply a name for a property node.", nameof(inpName));
                }

                AddNode(inpName, inpValue, 2);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Add a property node with a type of decimal.
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">Double holding the value of the node.</param>
        public void AddNode(string inpName, double inpValue)
        {
            try
            {
                AddNode(inpName, (decimal)inpValue);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add a property node with a type of decimal.
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">Decimal holding the value of the node.</param>
        /// <param name="inpDecimalPlaces">Number of decimal places to store to.</param>
        public void AddNode(string inpName, decimal inpValue, int inpDecimalPlaces)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inpName))
                {
                    throw new ArgumentException($"You must supply a name for a property node.", nameof(inpName));
                }

                string format;
                if (inpDecimalPlaces >= 0)
                {
                    format = "N" + inpDecimalPlaces.ToString();
                }
                else
                {
                    throw new ArgumentException($"'{nameof(inpDecimalPlaces)}' cannot be negative.", nameof(inpDecimalPlaces));
                }
                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.Decimal,
                        inpName, inpValue.ToString(format), currentParent);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add a property node with a type of integer.
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">Integer holding the value of the node.</param>
        public void AddNode(string inpName, int inpValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inpName))
                {
                    throw new ArgumentException($"You must supply a name for a property node.", nameof(inpName));
                }

                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.Integer,
                inpName, inpValue.ToString(), currentParent);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add a property node with a type of string.
        /// </summary>
        /// <param name="inpName">Name to give the node.</param>
        /// <param name="inpValue">String holding the value of the node.</param>
        public void AddNode(string inpName, string inpValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inpName))
                {
                    throw new ArgumentException($"You must supply a name for a property node.", nameof(inpName));
                }
                if (inpValue == null)
                {
                    throw new ArgumentException($"You cannot write a null value to the {inpName} node.", nameof(inpValue));
                }

                NodeManager.node currentNode = nodeManager.addNode(NodeManager.DataType.String,
                inpName, inpValue, currentParent);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region DeleteNodes
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpInstance">Int of the instance to delete.</param>
        public void DeleteNode(int inpInstance)
        {
            try
            {
                DeleteNode(inpInstance, "", false, false, false);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpInstance">Int of the instance to delete.</param>
        /// <param name="inpRecursive">Pass true to delete this node and all the nodes that it parents.</param>
        public void DeleteNode(int inpInstance, bool inpRecursive)
        {
            try
            {
                DeleteNode(inpInstance, "", false, inpRecursive, false);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpName">Name or Path to identify the node(s) that we are deleting.</param>
        public void DeleteNode(string inpName)
        {
            try
            {
                DeleteNode(0, inpName, false, false, false);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpName">Name or Path to identify the node(s) that we are deleting.</param>
        /// <param name="inpFindByPath">Pass true if the inpName contains a full path, false if it is just a name.</param>
        public void DeleteNode(string inpName, bool inpFindByPath)
        {
            try
            {
                DeleteNode(0, inpName, inpFindByPath, false, false);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpName">Name or Path to identify the node(s) that we are deleting.</param>
        /// <param name="inpFindByPath">Pass true if the inpName contains a full path, false if it is just a name.</param>
        /// <param name="inpRecursive">Pass true to delete this node and all the nodes that it parents.</param>
        public void DeleteNode(string inpName, bool inpFindByPath, bool inpRecursive)
        {
            try
            {
                DeleteNode(0, inpName, inpFindByPath, inpRecursive, false);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpName">Name or Path to identify the node(s) that we are deleting.</param>
        /// <param name="inpFindByPath">Pass true if the inpName contains a full path, false if it is just a name.</param>
        /// <param name="inpRecursive">Pass true to delete this node and all the nodes that it parents.</param>
        /// <param name="inpMultiple">Pass true to delete all nodes matching the name/path.</param>
        public void DeleteNode(string inpName, bool inpFindByPath, bool inpRecursive, bool inpMultiple)
        {
            try
            {
                DeleteNode(0, inpName, inpFindByPath, inpRecursive, inpMultiple);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the specified node(s).
        /// </summary>
        /// <param name="inpInstance">Int of the instance to delete.</param>
        /// <param name="inpName">Name or Path to identify the node(s) that we are deleting.</param>
        /// <param name="inpFindByPath">Pass true if the inpName contains a full path, false if it is just a name.</param>
        /// <param name="inpRecursive">Pass true to delete this node and all the nodes that it parents.</param>
        /// <param name="inpMultiple">Pass true to delete all nodes matching the name/path.</param>
        public void DeleteNode(int inpInstance, string inpName, bool inpFindByPath, bool inpRecursive, bool inpMultiple)
        {
            try
            {
                // Get records by index or path or by name.
                int[] instanceList;

                if (inpInstance > 0)
                {
                    nodeManager.validateInstance(inpInstance);
                    instanceList = new int[1];
                    instanceList[0] = inpInstance;
                    inpName = inpInstance.ToString();
                }
                else if (inpFindByPath)
                {
                    instanceList = nodeManager.findNodeByPath(inpName);
                }
                else
                {
                    instanceList = nodeManager.findNodeByName(inpName);
                }

                ValidateNumberDeleted(inpName, instanceList, inpMultiple);

                // Convert array to a list.
                List<int> deleteList = new List<int>(instanceList);
                // Add any child nodes to the list.
                deleteList = nodeManager.listNodeChildren(deleteList);

                // Not recursive delete and there are objects inside this one.
                if (!inpRecursive)
                {
                    ValidateNotRecursive(inpName, deleteList, instanceList);
                }

                // We've passed all validation, so delete the node(s).
                nodeManager.deleteNodeList(deleteList);
                //DeleteNode(instanceList, inpRecursive);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validate that we've not got zero record, or unwanted multiple records.
        /// </summary>
        /// <param name="inpName">Name or index of the problem node.</param>
        /// <param name="inpRequestArray">Int Array of records that we've asked to delete.</param>
        /// <param name="inpMultiple">Pass true to delete all nodes matching the name/path.</param>
        private void ValidateNumberDeleted(string inpName, int[] inpRequestArray, bool inpMultiple)
        {
            try
            {
                // No records have been found.
                if (inpRequestArray.Length == 0)
                {
                    throw new ArgumentOutOfRangeException($"No rercords found for '{inpName}'");
                }
                if (!inpMultiple)
                {
                    // We're not processing multiple nodes, so throw an error.
                    if (inpRequestArray.Length > 1)
                    {
                        throw new ArgumentOutOfRangeException($"Multiple records exist for '{inpName}'. Use multiple handler.");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Check that none of the nodes we're deleting has a child node.
        /// </summary>
        /// <param name="inpName">Name or index of the problem node.</param>
        /// <param name="inpDelList">List<int> of all nodes to be deleted.</int></param>
        /// <param name="inpRequestArray">Int Array of records that we've asked to delete.</param>
        private void ValidateNotRecursive(string inpName, List<int> inpDelList, int[] inpRequestArray)
        {
            try
            {
                if (inpDelList.Count > inpRequestArray.Length)
                {
                    throw new ArgumentException($"'{inpName}' has child values. Use recursive handler.");
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region OutputJsonData
        /// <summary>
        /// Read all the nodes and create a JSON from them.
        /// </summary>
        /// <returns>The Json string.</returns>
        public string WriteJson()
        {
            try
            {
                string newJson = "";

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
                            newJson += AddComma(newJson);
                            newJson += CurrentNode.fieldName == "" ? "" : "\"" + CurrentNode.fieldName + "\":";
                            newJson += "[";

                            break;
                        case NodeManager.DataType.ArrayClose:
                            newJson += "]";

                            break;
                        case NodeManager.DataType.Object:
                            newJson += AddComma(newJson);
                            newJson += CurrentNode.fieldName == "" ? "" : "\"" + CurrentNode.fieldName + "\":";
                            newJson += "{";

                            break;
                        case NodeManager.DataType.ObjectClose:
                            newJson += "}";

                            break;
                        case NodeManager.DataType.Boolean:
                            newJson += AddComma(newJson);
                            newJson += "\"" + CurrentNode.fieldName + "\""
                                    + ":" + CurrentNode.fieldValue.ToLower();
                            break;
                        case NodeManager.DataType.Decimal:
                            newJson += AddComma(newJson);
                            newJson += "\"" + CurrentNode.fieldName + "\""
                                    + ":" + CurrentNode.fieldValue;
                            break;
                        case NodeManager.DataType.Integer:
                            newJson += AddComma(newJson);
                            newJson += "\"" + CurrentNode.fieldName + "\""
                                    + ":" + CurrentNode.fieldValue;
                            break;
                        case NodeManager.DataType.String:
                            newJson += AddComma(newJson);
                            newJson += "\"" + CurrentNode.fieldName + "\""
                                    + ":" + "\"" + CurrentNode.fieldValue + "\"";
                            break;
                    }
                }

                return newJson;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Decide if we need to add a comma to the JSON string.
        /// </summary>
        /// <param name="inpCurrentString">JSON string that we're building.</param>
        /// <returns>inpTempString, with the comma added.</returns>
        private string AddComma(string inpCurrentString)
        {
            string ReturnString = "";
            if (!inpCurrentString.EndsWith("{") &&
                !inpCurrentString.EndsWith("[") &&
                inpCurrentString != "")
            {
                ReturnString = ",";
            }
            return ReturnString;
        }
        #endregion
    }
}
