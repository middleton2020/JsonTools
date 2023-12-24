using System;
using System.Collections.Generic;
using System.Text;

namespace CM.JsonTools
{
    class Messages
    {
        public Messages() { }

        #region CreateNodeMessages
        /// <summary>
        /// Name cannot accept a null value.
        /// </summary>
        /// <param name="inpParamName">Parameter name for message.</param>
        /// <returns>Error message text.</returns>
        public static void NullName(string inpParamName)
        {
            throw new ArgumentException($"You cannot supply a null value for a name.", inpParamName);
        }

        /// <summary>
        /// This property cannot accept a null value.
        /// </summary>
        /// <param name="inpName">Name of the field.</param>
        /// <param name="inpParamName">Parameter name for message.</param>
        public static void NullValue(string inpName, string inpParamName)
        {
            throw new ArgumentException($"You cannot write a null value to the {inpName} node.", inpParamName);
        }

        /// <summary>
        /// Property name cannot be blank.
        /// </summary>
        /// <param name="inpParamName">Parameter name for message.</param>
        public static void PropertyName(string inpParamName)
        {
            throw new ArgumentException($"You must supply a name for a property node.", inpParamName);
        }
        #endregion

        #region FindNodeMessages
        /// <summary>
        /// Requested Node does not exist.
        /// </summary>
        /// <param name="inpInstance">Int, Node instance number</param>
        public static void NoNode(int inpInstance)
        {
            throw new IndexOutOfRangeException($"There is no node {inpInstance}");
        }

        /// <summary>
        /// No Nodes found with the requested name.
        /// </summary>
        /// <param name="inpParamName">Name of Node.</param>
        public static void NoRecords(string inpParamName)
        {
            throw new ArgumentOutOfRangeException($"No rercords found for '{inpParamName}'");
        }

        /// <summary>
        /// Multiple Nodes found with the requested name, but multi-processing off.
        /// </summary>
        /// <param name="inpParamName">Name of Node.</param>
        public static void MultipleRecords(string inpParamName)
        {
            throw new ArgumentOutOfRangeException($"Multiple records exist for '{inpParamName}'. Use multiple handler.");
        }

        /// <summary>
        /// Node has children, but child processing is switched off.
        /// </summary>
        /// <param name="inpParamName">Name of Node.</param>
        public static void ChildRecords(string inpParamName)
            {
            throw new ArgumentException($"'{inpParamName}' has child values. Use recursive handler.");
    }
        #endregion

        #region NumberMessages
        /// <summary>
        /// Number provided cannot be negative.
        /// </summary>
        /// <param name="inpParamName">Parameter name for message.</param>
        public static void NegativeValue(string inpParamName)
        {
            throw new ArgumentException($"'{inpParamName}' cannot be negative.", inpParamName);
        }
        #endregion
    }
}
