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

        public void AddNode(string inpName, DateTime inpValue)
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
                inpName, inpValue.ToString("yyyy-MM-dd"), currentParent);
            }
            catch
            {
                throw;
            }
        }

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

        public void DeleteNode()
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

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
                                    +  ":" + CurrentNode.fieldValue.ToLower();
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

    }
}
