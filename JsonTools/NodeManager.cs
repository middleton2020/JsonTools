using System;
using System.Collections.Generic;
using System.Text;

namespace CM.JsonTools
{
    class NodeManager : IDisposable
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
            Deleted,
            Array,
            ArrayClose,
            Object,
            ObjectClose,
            Boolean,
            Decimal,
            Double,
            Integer,
            LongInt,
            String,
            Top
        }

        #region SubClasses
        /// <summary>
        /// A node of the JSON holds a single data item, be it a 'class', an array or a field.
        /// </summary>
        public class Node
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
            public Node(int inpInstance,
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
        public Dictionary<int, Node> nodeArray = new Dictionary<int, Node>();
        // Counter of what the next index number for the node.
        int nodeInstanceCounter = 1;
        private bool disposedValue;
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
        public Node AddNode(DataType inpType, string inpName, string inpValue, int inpParent)
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
                    Node NodeEntry = new Node(nodeInstanceCounter, inpName, inpValue, inpType, inpParent);
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
        /// Delete all nodes in the submitted list.
        /// </summary>
        /// <param name="deleteList">List<int> of instances to  be deleted.</int></param>
        public void DeleteNodeList(List<int> deleteList)
        {
            try
            {
                // Mark fields that we want to delete as deleted.
                foreach (int entry in deleteList)
                {
                    nodeArray[entry].dataType = DataType.Deleted;
                }
                // Rebuild the array, excluding the nodes marked as deleted.
                RenumberNodes();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Once we've marked nodes as deleted, re-generate the list without those nodes.
        /// </summary>
        public void RenumberNodes()
        {
            try
            {
                // Initial variables.
                Dictionary<int, Node> tempNodeArray = new Dictionary<int, Node>();
                int newCounter = 1;
                int currentParent = 0;

                // Copy the nodes that we're not deleting to a new array and re-number them.
                foreach (KeyValuePair<int, Node> entry in nodeArray)
                {
                    if (entry.Value.dataType == DataType.Deleted)
                    {
                        // This is deleted, so don't copy it.
                    }
                    else
                    {
                        tempNodeArray.Add(newCounter, entry.Value);
                        // Set the new parent number.
                        tempNodeArray[newCounter].parentNode = currentParent;
                        if (tempNodeArray[newCounter].dataType == DataType.Array ||
                           tempNodeArray[newCounter].dataType == DataType.Object ||
                           tempNodeArray[newCounter].dataType == DataType.Top)
                        {
                            currentParent += 1;
                        }
                        else if (tempNodeArray[newCounter].dataType == DataType.ArrayClose ||
                                 tempNodeArray[newCounter].dataType == DataType.ObjectClose)
                        {
                            currentParent = tempNodeArray[currentParent].parentNode;
                        }
                        newCounter += 1;
                    }
                }

                // Copy the new array over the original one.
                nodeArray.Clear();
                nodeArray = tempNodeArray;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Does the specified instance belong to an existing node?
        /// </summary>
        /// <param name="inpInstance">Int, instance to check.</param>
        public void ValidateInstance(int inpInstance)
        {
            try
            {
                // Make sure that the instance is within the node array's length.
                if (inpInstance > nodeArray.Count - 1)
                {
                    Messages.NoNode(inpInstance);
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

        #region EnquiryMethods
        /// <summary>
        /// Search the node list and return the index of any the match the specified name.
        /// </summary>
        /// <param name="inpName">String, name to search by.</param>
        /// <returns>Int array of node instances selected.</returns>
        public int[] FindNodeByName(string inpName)
        {
            List<int> foundInstances = new List<int>();

            // Step therough the nodes. Any that match the
            // specified path are added to the results list.
            foreach (KeyValuePair<int, NodeManager.Node> entry in nodeArray)
            {
                if (entry.Value.fieldName == inpName)
                {
                    foundInstances.Add(entry.Key);
                }
            }

            return foundInstances.ToArray();
        }
        /// <summary>
        /// Search the node list and return the index of any the match the specified path.
        /// </summary>
        /// <param name="inpPath">String, path to search by.</param>
        /// <returns>Int array of node instances selected.</returns>
        public int[] FindNodeByPath(string inpPath)
        {
            List<int> foundInstances = new List<int>();

            // Step therough the nodes. Any that match the
            // specified path are added to the results list.
            foreach (KeyValuePair<int, NodeManager.Node> entry in nodeArray)
            {
                if (entry.Value.nodePath == inpPath)
                {
                    foundInstances.Add(entry.Key);
                }
            }

            return foundInstances.ToArray();
        }

        /// <summary>
        /// Return a list of child nodes.
        /// </summary>
        /// <param name="inpInstance">List<int> of instances to check.</int></param>
        /// <returns>List<int> of child nodes.</int></returns>
        public List<int> ListNodeChildren(List<int> inpInstance)
        {
            // Step through the nodes. Ignore any closing nodes and
            // count any the have the specified node as a parent or grandparent.
            foreach (KeyValuePair<int, NodeManager.Node> entry in nodeArray)
            {
                if (inpInstance.Contains(entry.Value.parentNode) &&
                    !inpInstance.Contains(entry.Value.instance))
                {
                    inpInstance.Add(entry.Value.instance);
                }
            }

            return inpInstance;
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
                    nodeArray.Clear();
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
        ~NodeManager()
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
