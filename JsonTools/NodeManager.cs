﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CM.JsonTools
{
    class NodeManager
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
            ArrayClose,
            Object,
            ObjectClose,
            Boolean,
            Decimal,
            Integer,
            String,
            Top
        }

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
            public string nodePath;

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
                fieldName = inpName.Trim();
                fieldValue = inpValue.Trim();
                dataType = inpType;
                parentNode = inpParent;
            }
        }
        #endregion

        #region LocalVariables
        // List of nodes that we've created.
        public Dictionary<int, node> nodeArray = new Dictionary<int, node>();
        // Counter of what the next index number for the node.
        int nodeInstanceCounter = 1;
        #endregion

        #region Methods
        /// <summary>
        /// Add a new node to the array.
        /// </summary>
        /// <param name="inpType">DataType Enum. Type of node</param>
        /// <param name="inpName">String, Name of node</param>
        /// <param name="inpValue">String, Value of node</param>
        /// <param name="inpParent">Int, instance number for the node that 'contains' this node</param>
        /// <returns>The new node object</returns>
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
                    NodeEntry.nodePath = BuildPath(NodeEntry.instance);
                    return NodeEntry;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Calculate the path for this node (paren.child.child.variable).
        /// </summary>
        /// <param name="inpInstance">Instance (integetr) to build the path for.</param>
        /// <returns>String of path.</returns>
        private string BuildPath(int inpInstance)
        {
            try
            {
                string nodePath = "";

                // Instance 0 returns a blank path.
                if (inpInstance > 0)
                {
                    // Call self to get parent's path.
                    nodePath = BuildPath(nodeArray[inpInstance].parentNode);
                    // Always end with the current field's name.
                    string tempFieldName = nodeArray[inpInstance].fieldName;
                    if (tempFieldName != "")
                    {
                        // If path already blank, just return the field name.
                        if (nodePath == "")
                        {
                            nodePath = tempFieldName;
                        }
                        // If no field name, then nothing to add.
                        else if (tempFieldName == "")
                        {
                            // nodePath = nodePath;
                        }
                        // Otherwise add the name to the end of the path.
                        else
                        {
                            nodePath = nodePath + "." + tempFieldName;
                        }
                    }
                }

                return nodePath;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
