using Microsoft.VisualStudio.TestTools.UnitTesting;
using CM.JsonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CM.JsonTools.Tests
{
    [TestClass()]
    public class JsonReaderTests
    {
        #region MakeStringTests
        /// <summary>
        /// A very simple test, just strings. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void SimpleMakeJsonTest()
        {
            InitializeText();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + "}";
            string resultJson = "";

            JsonReader testReader = new JsonReader(makeObject,
                                                    closeObject,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultJson = (string)testReader.ReadJson(testJson, "");

            testReader.Dispose();

            Assert.AreEqual(testJson, resultJson, false);
        }

        /// <summary>
        /// A moderately complex test, all data types. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void BasicMakeJsonTest()
        {
            InitializeText();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";
            string resultJson = "";

            JsonReader testReader = new JsonReader(makeObject,
                                                    closeObject,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultJson = (string)testReader.ReadJson(testJson, "");

            testReader.Dispose();

            Assert.AreEqual(testJson, resultJson, false);
        }

        /// <summary>
        /// A very simple test, just strings. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void ComplexMakeJsonTest()
        {
            InitializeText();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"items\":"
                            + "["
                            + "{"
                            + "\"name\":\"Box 17\""
                            + "}"
                            + ","
                            + "{"
                            + "\"name\":\"22 Sphere\""
                            + "}"
                            + ","
                            + "{"
                            + "\"name\":\"Alphabet\""
                            + "}"
                            + "]"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";
            string resultJson = "";

            JsonReader testReader = new JsonReader(makeObject,
                                                    closeObject,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultJson = (string)testReader.ReadJson(testJson, "");

            testReader.Dispose();

            Assert.AreEqual(testJson, resultJson, false);
        }
        #endregion

        #region MakeClassTests
        /// <summary>
        /// A very simple test, just strings. The output is written to a Class (see below).
        /// </summary>
        [TestMethod()]
        public void SimpleMakeClassTest()
        {
            InitializeClass();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + "}";
            Order resultOrder = null;

            JsonReader testReader = new JsonReader(makeObject,
                                                    closeObject,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);

            testReader.Dispose();

            Assert.AreEqual("Bibble", resultOrder.Id, false);
            Assert.AreEqual("Test Name", resultOrder.Name, false);
        }

        /// <summary>
        /// A moderately complex test, all data types. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void BasicMakeClassTest()
        {
            InitializeClass();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";
            Order resultOrder = null;

            JsonReader testReader = new JsonReader(makeObject,
                                                    closeObject,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);

            testReader.Dispose();

            decimal resultValue = 11.50M;
            Assert.AreEqual("Bibble", resultOrder.Id, false);
            Assert.AreEqual("Test Name", resultOrder.Name, false);
            Assert.AreEqual(1, resultOrder.Parcels);
            Assert.AreEqual(resultValue, resultOrder.Value);
            Assert.AreEqual(true, resultOrder.SaturdayDelivery);
        }

        /// <summary>
        /// A complex test with arrays and various data types. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void ComplexMakeClassTest()
        {
            InitializeClass();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"items\":"
                            + "["
                            + "{"
                            + "\"name\":\"Box 17\""
                            + " }"
                            + ","
                            + "{"
                            + "\"name\":\"22 Sphere\""
                            + " }"
                            + ","
                            + "{"
                            + "\"name\":\"Alphabet\""
                            + " }"
                            + "]"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";
            Order resultOrder = null;

            JsonReader testReader = new JsonReader(makeObject,
                                                    closeObject,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);

            testReader.Dispose();

            decimal resultValue = 11.50M;
            Assert.AreEqual("Bibble", resultOrder.Id, false);
            Assert.AreEqual("Test Name", resultOrder.Name, false);
            Assert.AreEqual(1, resultOrder.Parcels);
            Assert.AreEqual(resultValue, resultOrder.Value);
            Assert.AreEqual(true, resultOrder.SaturdayDelivery);
            Assert.AreEqual("Box 17", resultOrder.ItemsList[0].ItemName);
            Assert.AreEqual("22 Sphere", resultOrder.ItemsList[1].ItemName);
            Assert.AreEqual("Alphabet", resultOrder.ItemsList[2].ItemName);
        }
        #endregion

        #region DeligateVariables
        JsonReader.DeligateMakeObject makeObject;
        JsonReader.DeligateCloseObject closeObject;
        JsonReader.DeligateMakeArray makeArray;
        JsonReader.DeligateCloseArray closeArray;
        JsonReader.DeligateSetBoolean setBoolean;
        JsonReader.DeligateSetDecimal setDecimal;
        JsonReader.DeligateSetInteger setInteger;
        JsonReader.DeligateSetString setString;
        #endregion

        #region JsonMethods
        /// <summary>
        /// Decide if we need to add a comma to the JSON string.
        /// </summary>
        /// <param name="inpTempString">JSON string that we're building.</param>
        /// <returns>inpTempString, with the comma added.</returns>
        private static string AddComma(string inpTempString)
        {
            if (!inpTempString.EndsWith("{") &&
                !inpTempString.EndsWith("[") &&
                inpTempString != "")
            {
                inpTempString += ",";
            }
            return inpTempString;
        }
        /// <summary>
        /// Open a group/class in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonMakeClass(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            tempString = AddComma(tempString);
            tempString += inpName == "" ? "" : "\"" + inpName + "\":";
            tempString += "{";
            return tempString;
        }
        /// <summary>
        /// Closes an group/class in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonCloseClass(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString += "}";
            return tempString;
        }
        /// <summary>
        /// Open an array in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonMakeArray(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString = AddComma(tempString);
            tempString += inpName == "" ? "" : "\"" + inpName + "\":";
            tempString += "[";
            return tempString;
        }
        /// <summary>
        /// Closes an array in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonCloseArray(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString += "]";
            return tempString;
        }
        /// <summary>
        /// Add a boolean property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString = AddComma(tempString);
            tempString += "\"" + inpName + "\":" + Convert.ToString(inpValue).ToLower();
            return tempString;
        }
        /// <summary>
        /// Add a decimal property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString = AddComma(tempString);
            tempString += "\"" + inpName + "\":" + Convert.ToString(inpValue);
            return tempString;
        }
        /// <summary>
        /// Add a integer property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonSetInteger(string inpName, int inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString = AddComma(tempString);
            tempString += "\"" + inpName + "\":" + Convert.ToString(inpValue);
            return tempString;
        }
        /// <summary>
        /// Add a string property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object JsonSetString(string inpName, string inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString = AddComma(tempString);
            tempString += "\"" + inpName + "\":\"" + inpValue + "\"";
            return tempString;
        }

        /// <summary>
        /// Setup the deligates to output to a JSON.
        /// </summary>
        private void InitializeText()
        {
            makeObject = JsonMakeClass;
            closeObject = JsonCloseClass;
            makeArray = JsonMakeArray;
            closeArray = JsonCloseArray;
            setBoolean = JsonSetBoolean;
            setDecimal = JsonSetDecimal;
            setInteger = JsonSetInteger;
            setString = JsonSetString;
        }
        #endregion

        #region ClassMethods
        /// <summary>
        /// Open a group/class in the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassMakeObject(string inpName, object inpObject, string inpPath)
        {
            Order tempOrder = (Order)inpObject;
            if (inpPath == "items")
            {
                Order.Items tempItem = new Order.Items();
                tempOrder.ItemsList.Add(tempItem);
                tempOrder.CurrentItem += 1;
            }
            else
            {
                tempOrder = new Order();
            }

            return tempOrder;
        }
        /// <summary>
        /// Closes an group/class in the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassCloseObject(string inpName, object inpObject, string inpPath)
        {
            Order tempOrder = (Order)inpObject;

            return tempOrder;
        }
        /// <summary>
        /// Open an array in the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassMakeArray(string inpName, object inpObject, string inpPath)
        {
            return inpObject;
        }
        /// <summary>
        /// Close an array in the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassCloseArray(string inpName, object inpObject, string inpPath)
        {
            return inpObject;
        }
        /// <summary>
        /// Add a boolean property to the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath)
        {
            Order tempOrder = (Order)inpObject;
            tempOrder.SaturdayDelivery = inpValue;

            return tempOrder;
        }
        /// <summary>
        /// Add a decimal property to the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath)
        {
            Order tempOrder = (Order)inpObject;
            tempOrder.Value = inpValue;

            return tempOrder;
        }
        /// <summary>
        /// Add a integer property to the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassSetInteger(string inpName, int inpValue, object inpObject, string inpPath)
        {
            Order tempOrder = (Order)inpObject;
            tempOrder.Parcels = inpValue;

            return tempOrder;
        }
        /// <summary>
        /// Add a string property to the class object.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public static object ClassSetString(string inpName, string inpValue, object inpObject, string inpPath)
        {
            Order tempOrder = (Order)inpObject;
            switch (inpName)
            {
                case "id":
                    tempOrder.Id = inpValue;
                    break;
                case "name":
                    if (inpPath == "items.name")
                    {
                        tempOrder.ItemsList[tempOrder.CurrentItem - 1].ItemName = inpValue;
                    }
                    else
                    {
                        tempOrder.Name = inpValue;
                    }
                    break;
            }

            return tempOrder;
        }

        /// <summary>
        /// Setup the deligates to output to a custom class.
        /// </summary>
        private void InitializeClass()
        {
            makeObject = ClassMakeObject;
            closeObject = ClassCloseObject;
            makeArray = ClassMakeArray;
            closeArray = ClassCloseArray;
            setBoolean = ClassSetBoolean;
            setDecimal = ClassSetDecimal;
            setInteger = ClassSetInteger;
            setString = ClassSetString;
        }

        /// <summary>
        /// Dummy data class to populate.
        /// </summary>
        public class Order
        {
            public string Id = "";
            public string Name = "";
            public int Parcels = 0;
            public decimal Value = 0;
            public bool SaturdayDelivery = false;
            public int CurrentItem = 0;
            public List<Items> ItemsList = new List<Items>();

            public class Items
            {
                public string ItemName = "";
            }

        }
        #endregion
    }
}
