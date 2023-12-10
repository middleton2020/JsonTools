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
            initializeText();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + "}";
            string resultJson = "";

            JsonReader testReader = new JsonReader(makeClass,
                                                    closeClass,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultJson = (string)testReader.ReadJson(testJson, "");

            Assert.AreEqual(testJson, resultJson, false);
        }

        /// <summary>
        /// A moderately complex test, all data types. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void BasicMakeJsonTest()
        {
            initializeText();
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

            JsonReader testReader = new JsonReader(makeClass,
                                                    closeClass,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultJson = (string)testReader.ReadJson(testJson, "");

            Assert.AreEqual(testJson, resultJson, false);
        }

        /// <summary>
        /// A very simple test, just strings. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void ComplexMakeJsonTest()
        {
            initializeText();
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

            JsonReader testReader = new JsonReader(makeClass,
                                                    closeClass,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultJson = (string)testReader.ReadJson(testJson, "");

            Assert.AreEqual(testJson, resultJson, false);
        }
        #endregion

        #region MakeClassTests
        /// <summary>
        /// A complex test with arrays and various data types. The output is written to a Class (see below).
        /// </summary>
        [TestMethod()]
        public void SimpleMakeClassTest()
        {
            initializeClass();
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + "}";
            Order resultOrder = null;

            JsonReader testReader = new JsonReader(makeClass,
                                                    closeClass,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);

            Assert.AreEqual("Bibble", resultOrder.Id, false);
            Assert.AreEqual("Test Name", resultOrder.Name, false);
        }

        /// <summary>
        /// A moderately complex test, all data types. The output is written to a JSON.
        /// </summary>
        [TestMethod()]
        public void BasicMakeClassTest()
        {
            initializeClass();
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

            JsonReader testReader = new JsonReader(makeClass,
                                                    closeClass,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);

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
            initializeClass();
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

            JsonReader testReader = new JsonReader(makeClass,
                                                    closeClass,
                                                    makeArray,
                                                    closeArray,
                                                    setBoolean,
                                                    setDecimal,
                                                    setInteger,
                                                    setString);
            resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);

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
        JsonReader.DeligateMakeClass makeClass;
        JsonReader.DeligateCloseClass closeClass;
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
        private string AddComma(string inpTempString)
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
        public object JsonMakeClass(string inpName, object inpObject, string inpPath)
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
        public object JsonCloseClass(string inpName, object inpObject, string inpPath)
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
        public object JsonMakeArray(string inpName, object inpObject, string inpPath)
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
        public object JsonCloseArray(string inpName, object inpObject, string inpPath)
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
        public object JsonSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath)
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
        public object JsonSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath)
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
        public object JsonSetInteger(string inpName, int inpValue, object inpObject, string inpPath)
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
        public object JsonSetString(string inpName, string inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;
            tempString = AddComma(tempString);
            tempString += "\"" + inpName + "\":\"" + inpValue + "\"";
            return tempString;
        }

        /// <summary>
        /// Setup the deligates to output to a JSON.
        /// </summary>
        private void initializeText()
        {
            makeClass = JsonMakeClass;
            closeClass = JsonCloseClass;
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
        public object ClassMakeClass(string inpName, object inpObject, string inpPath)
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
        public object ClassCloseClass(string inpName, object inpObject, string inpPath)
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
        public object ClassMakeArray(string inpName, object inpObject, string inpPath)
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
        public object ClassCloseArray(string inpName, object inpObject, string inpPath)
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
        public object ClassSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath)
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
        public object ClassSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath)
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
        public object ClassSetInteger(string inpName, int inpValue, object inpObject, string inpPath)
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
        public object ClassSetString(string inpName, string inpValue, object inpObject, string inpPath)
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
        private void initializeClass()
        {
            makeClass = ClassMakeClass;
            closeClass = ClassCloseClass;
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

//string testJson = "{\"order_id\":3986441,";
//testJson += "\"date_ordered\":\"2013-12-17T07:00:15.087Z\",";
//testJson += "\"seller_name\":\"sklee\",";
//testJson += "\"store_name\":\"skleestore\",";
//testJson += "\"buyer_name\":\"covariance1\",";
//testJson += "\"buyer_email\":\"skleew@gmail.com\",";
//testJson += "\"require_insurance\":true,";
//testJson += "\"status\":\"PENDING\",";
//testJson += "\"is_invoiced\":false,";
//testJson += "\"total_count\":10,";
//testJson += "\"unique_count\":1,";
//testJson += "\"payment\": {";
//testJson += "\"method\":\"PayPal.com\",";
//testJson += "\"currency_code\":\"USD\",";
//testJson += "\"date_paid\":\"2013-12-17T09:20:02.000Z\",";
//testJson += "\"status\":\"Sent\"";
//testJson += "},";
//testJson += "\"shipping\": {";
//testJson += "\"address\": {";
//testJson += "\"name\": {";
//testJson += "\"full\":\"Seulki Lee\"";
//testJson += "},";
//testJson += "\"full\":\"Geumho-dong 2-ga, Seongdong-gu\",";
//testJson += "\"country_code\":\"KR\"";
//testJson += "},";
//testJson += "\"date_shipped\":\"2013-12-17T03:00:15.087Z\"";
//testJson += "},";
//testJson += "\"cost\": {";
//testJson += "\"currency_code\":\"USD\",";
//testJson += "\"subtotal\":\"139.9900\",";
//testJson += "\"grand_total\":\"157.8000\",";
//testJson += "\"disp_currency_code\":\"USD\",";
//testJson += "\"disp_subtotal\":\"139.9900\",";
//testJson += "\"disp_grand_total\":\"157.8000\",";
//testJson += "\"etc1\":\"0.0000\",";
//testJson += "\"etc2\":\"0.0000\",";
//testJson += "\"insurance\":\"3.0500\",";
//testJson += "\"shipping\":\"14.7600\",";
//testJson += "\"credit\":\"0.0000\",";
//testJson += "\"coupon\":\"0.0000\"";
//testJson += "}";
//testJson += "},";

//testJson += "{";
//testJson += "\"order_id\":23215046,";
//testJson += "\"date_ordered\":\"2023-09-29T16:21:57.923Z\",";
//testJson += "\"date_status_changed\":\"2023-09-29T16:21:57.923Z\",";
//testJson += "\"seller_name\":\"bricksinbloom\",";
//testJson += "\"store_name\":\"Bricksinbloom\",";
//testJson += "\"buyer_name\":\"jaggerous\",";
//testJson += "\"status\":\"PAID\",";
//testJson += "\"total_count\":700,";
//testJson += "\"unique_count\":3,";
//testJson += "\"is_filed\":false,";
//testJson += "\"salesTax_collected_by_bl\":false,";
//testJson += "\"vat_collected_by_bl\":false,";
//testJson += "\"payment\":{";
//testJson += "\"method\":\"Credit/Debit (Powered by Stripe)\",";
//testJson += "\"currency_code\":\"GBP\",";
//testJson += "\"date_paid\":\"2023-09-29T16:21:57.923Z\",";
//testJson += "\"status\":\"Received\"";
//testJson += "},";
//testJson += "\"cost\":{";
//testJson += "\"currency_code\":\"GBP\",";
//testJson += "\"subtotal\":\"21.0000\",";
//testJson += "\"grand_total\":\"23.1500\",";
//testJson += "\"final_total\":\"23.1500\"";
//testJson += "},";
//testJson += "\"disp_cost\":{";
//testJson += "\"currency_code\":\"GBP\",";
//testJson += "\"subtotal\":\"21.0000\",";
//testJson += "\"grand_total\":\"23.1500\",";
//testJson += "\"final_total\":\"23.1500\"";
//testJson += "}";
//testJson += "},";

//testJson += "[";
//testJson += "{";
//testJson += "\"image_small\":\"https://img.brickowl.com/files/image_cache/small/lego-transparent-brick-2-x-2-6223-35275-27-939628-97.jpg\",";
//testJson += "\"name\":\"LEGO Transparent Brick 2 x 2 (6223 / 35275)\",";
//testJson += "\"type\":\"Part\",";
//testJson += "\"color_name\":\"Transparent\",";
//testJson += "\"color_id\":\"97\",";
//testJson += "\"boid\":\"939628-97\",";
//testJson += "\"lot_id\":\"82220185\",";
//testJson += "\"condition\":\"New\",";
//testJson += "\"full_con\":\"new\",";
//testJson += "\"ordered_quantity\":\"20\",";
//testJson += "\"personal_note\":\"drawer 155 and os 022\",";
//testJson += "\"bl_lot_id\":\"292687551\",";
//testJson += "\"external_lot_ids\":{";
//testJson += "\"other\":\"292687551\"";
//testJson += "},";
//testJson += "\"remaining_quantity\":\"412\",";
//testJson += "\"weight\":\"1.17\",";
//testJson += "\"public_note\":null,";
//testJson += "\"order_item_id\":\"49361745\",";
//testJson += "\"base_price\":\"0.189\",";
//testJson += "\"ids\":[";
//testJson += "{";
//testJson += "\"id\":\"3003\",";
//testJson += "\"type\":\"ldraw\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"6223\",";
//testJson += "\"type\":\"design_id\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"35275\",";
//testJson += "\"type\":\"design_id\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"939628-97\",";
//testJson += "\"type\":\"boid\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"4130389\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"4175396\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"4190520\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"4276601\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"4276601\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"6195264\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"6239418\",";
//testJson += "\"type\":\"item_no\"";
//testJson += "},";
//testJson += "{";
//testJson += "\"id\":\"6239418\",\"type\":\"item_no\"";
//testJson += "}";
//testJson += "]";
//testJson += "}";
//testJson += "]";

//JsonReader testReader = new JsonReader();
//testReader.ReadJson(testItem, testJson);

//            Assert.IsNotNull(testItem);
