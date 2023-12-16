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
    public class JsonWriterTests
    {
        [TestMethod()]
        public void SimpleMakeJsonTest()
        {
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + "}";

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.CloseObject();
            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void IntegerMakeJsonTest()
        {
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + "}";

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.CloseObject();
            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void DecimalMakeJsonTest()
        {
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + "}";

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.CloseObject();
            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void BooleanMakeJsonTest()
        {
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

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.AddNode("saturdayDelivery", true);
            testWriter.CloseObject();
            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void DateTimeMakeJsonTest()
        {
            DateTime testDate = Convert.ToDateTime("2023-09-30");
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"orderDate\":\"2023-09-30\""
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.AddNode("orderDate", testDate);
            testWriter.AddNode("saturdayDelivery", true);
            testWriter.CloseObject();
            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void ArrayMakeJsonTest()
        {
            DateTime testDate = Convert.ToDateTime("2023-09-30");
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"orderDate\":\"2023-09-30\""
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

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.AddNode("orderDate", testDate);
            testWriter.OpenArray("items");
            testWriter.OpenObject();
            testWriter.AddNode("name", "Box 17");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "22 Sphere");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "Alphabet");
            testWriter.CloseObject();
            testWriter.CloseArray();
            testWriter.AddNode("saturdayDelivery", true);
            testWriter.CloseObject();
            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void DeleteSingleNodeTest()
        {
            DateTime testDate = Convert.ToDateTime("2023-09-30");
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
//                            + "\"value\":11.50"
//                            + ","
                            + "\"orderDate\":\"2023-09-30\""
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

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.AddNode("orderDate", testDate);
            testWriter.OpenArray("items");
            testWriter.OpenObject();
            testWriter.AddNode("name", "Box 17");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "22 Sphere");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "Alphabet");
            testWriter.CloseObject();
            testWriter.CloseArray();
            testWriter.AddNode("saturdayDelivery", true);
            testWriter.CloseObject();

            testWriter.DeleteNode("value");

            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void DeleteRecursiveNodeTest()
        {
            DateTime testDate = Convert.ToDateTime("2023-09-30");
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"orderDate\":\"2023-09-30\""
                            + ","
                            //+ "\"items\":"
                            //+ "["
                            //+ "{"
                            //+ "\"name\":\"Box 17\""
                            //+ "}"
                            //+ ","
                            //+ "{"
                            //+ "\"name\":\"22 Sphere\""
                            //+ "}"
                            //+ ","
                            //+ "{"
                            //+ "\"name\":\"Alphabet\""
                            //+ "}"
                            //+ "]"
                            //+ ","
                            + "\"saturdayDelivery\":true"
                            + "}";

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.AddNode("orderDate", testDate);
            testWriter.OpenArray("items");
            testWriter.OpenObject();
            testWriter.AddNode("name", "Box 17");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "22 Sphere");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "Alphabet");
            testWriter.CloseObject();
            testWriter.CloseArray();
            testWriter.AddNode("saturdayDelivery", true);
            testWriter.CloseObject();

            testWriter.DeleteNode("items",false,true);

            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void DeleteMultipleNodeTest()
        {
            DateTime testDate = Convert.ToDateTime("2023-09-30");
            string testJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
//                            + "\"name\":\"Test Name\""
//                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"orderDate\":\"2023-09-30\""
                            + ","
                            + "\"items\":"
                            + "["
                            + "{"
//                            + "\"name\":\"Box 17\""
                            + "}"
                            + ","
                            + "{"
//                            + "\"name\":\"22 Sphere\""
                            + "}"
                            + ","
                            + "{"
//                            + "\"name\":\"Alphabet\""
                            + "}"
                            + "]"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";

            JsonWriter testWriter = new JsonWriter();
            testWriter.OpenObject("");
            testWriter.AddNode("id", "Bibble");
            testWriter.AddNode("name", "Test Name");
            testWriter.AddNode("parcels", 1);
            testWriter.AddNode("value", (decimal)11.50);
            testWriter.AddNode("orderDate", testDate);
            testWriter.OpenArray("items");
            testWriter.OpenObject();
            testWriter.AddNode("name", "Box 17");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "22 Sphere");
            testWriter.CloseObject();
            testWriter.OpenObject();
            testWriter.AddNode("name", "Alphabet");
            testWriter.CloseObject();
            testWriter.CloseArray();
            testWriter.AddNode("saturdayDelivery", true);
            testWriter.CloseObject();

            // Delete all nodes called "name".
            testWriter.DeleteNode("name",false,false,true);

            string resultJson = testWriter.WriteJson();

            Assert.AreEqual(testJson, resultJson, false);
        }

        [TestMethod()]
        public void FailDeleteRecursiveNodeTest()
        {
            try
            {
                DateTime testDate = Convert.ToDateTime("2023-09-30");
                // We're testing for errors, so don't need to create a JSON.
                // It would have read:
                // {"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"orderDate":"2023-09-30","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}

                JsonWriter testWriter = new JsonWriter();
                testWriter.OpenObject("");
                testWriter.AddNode("id", "Bibble");
                testWriter.AddNode("name", "Test Name");
                testWriter.AddNode("parcels", 1);
                testWriter.AddNode("value", (decimal)11.50);
                testWriter.AddNode("orderDate", testDate);
                testWriter.OpenArray("items");
                testWriter.OpenObject();
                testWriter.AddNode("name", "Box 17");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "22 Sphere");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "Alphabet");
                testWriter.CloseObject();
                testWriter.CloseArray();
                testWriter.AddNode("saturdayDelivery", true);
                testWriter.CloseObject();

                testWriter.DeleteNode("items");

                string resultJson = testWriter.WriteJson();

                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("'items' has child values. Use recursive handler.", e.Message);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void FailDeleteMultipleNodeTest()
        {
            try
            {
                DateTime testDate = Convert.ToDateTime("2023-09-30");
                // We're testing for errors, so don't need to create a JSON.
                // It would have read:
                // {"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"orderDate":"2023-09-30","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}

                JsonWriter testWriter = new JsonWriter();
                testWriter.OpenObject("");
                testWriter.AddNode("id", "Bibble");
                testWriter.AddNode("name", "Test Name");
                testWriter.AddNode("parcels", 1);
                testWriter.AddNode("value", (decimal)11.50);
                testWriter.AddNode("orderDate", testDate);
                testWriter.OpenArray("items");
                testWriter.OpenObject();
                testWriter.AddNode("name", "Box 17");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "22 Sphere");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "Alphabet");
                testWriter.CloseObject();
                testWriter.CloseArray();
                testWriter.AddNode("saturdayDelivery", true);
                testWriter.CloseObject();

                // There are several nodes with this name, so will fail.
                testWriter.DeleteNode("name");

                string resultJson = testWriter.WriteJson();

                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual("Multiple records exist for 'name'. Use multiple handler.", e.ParamName);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void FailDeleteNamedNodeTest()
        {
            try
            {
                DateTime testDate = Convert.ToDateTime("2023-09-30");
                // We're testing for errors, so don't need to create a JSON.
                // It would have read:
                // {"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"orderDate":"2023-09-30","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}

                JsonWriter testWriter = new JsonWriter();
                testWriter.OpenObject("");
                testWriter.AddNode("id", "Bibble");
                testWriter.AddNode("name", "Test Name");
                testWriter.AddNode("parcels", 1);
                testWriter.AddNode("value", (decimal)11.50);
                testWriter.AddNode("orderDate", testDate);
                testWriter.OpenArray("items");
                testWriter.OpenObject();
                testWriter.AddNode("name", "Box 17");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "22 Sphere");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "Alphabet");
                testWriter.CloseObject();
                testWriter.CloseArray();
                testWriter.AddNode("saturdayDelivery", true);
                testWriter.CloseObject();

                // No node of this name exists.
                testWriter.DeleteNode("nome");

                string resultJson = testWriter.WriteJson();

                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual("No rercords found for 'nome'", e.ParamName);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void FailDeletePathNodeTest()
        {
            try
            {
                DateTime testDate = Convert.ToDateTime("2023-09-30");
                // We're testing for errors, so don't need to create a JSON.
                // It would have read:
                // {"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"orderDate":"2023-09-30","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}

                JsonWriter testWriter = new JsonWriter();
                testWriter.OpenObject("");
                testWriter.AddNode("id", "Bibble");
                testWriter.AddNode("name", "Test Name");
                testWriter.AddNode("parcels", 1);
                testWriter.AddNode("value", (decimal)11.50);
                testWriter.AddNode("orderDate", testDate);
                testWriter.OpenArray("items");
                testWriter.OpenObject();
                testWriter.AddNode("name", "Box 17");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "22 Sphere");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "Alphabet");
                testWriter.CloseObject();
                testWriter.CloseArray();
                testWriter.AddNode("saturdayDelivery", true);
                testWriter.CloseObject();

                testWriter.DeleteNode("items.id",true);

                string resultJson = testWriter.WriteJson();

                Assert.Fail();
            }
            catch(ArgumentOutOfRangeException e)
            {
                Assert.AreEqual("No rercords found for 'items.id'", e.ParamName);
            }
            catch
            {
            Assert.Fail();
            }
        }

        [TestMethod()]
        public void FailDeleteTooHighNodeTest()
        {
            try
            {
                DateTime testDate = Convert.ToDateTime("2023-09-30");
                // We're testing for errors, so don't need to create a JSON.
                // It would have read:
                // {"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"orderDate":"2023-09-30","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}

                JsonWriter testWriter = new JsonWriter();
                testWriter.OpenObject("");
                testWriter.AddNode("id", "Bibble");
                testWriter.AddNode("name", "Test Name");
                testWriter.AddNode("parcels", 1);
                testWriter.AddNode("value", (decimal)11.50);
                testWriter.AddNode("orderDate", testDate);
                testWriter.OpenArray("items");
                testWriter.OpenObject();
                testWriter.AddNode("name", "Box 17");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "22 Sphere");
                testWriter.CloseObject();
                testWriter.OpenObject();
                testWriter.AddNode("name", "Alphabet");
                testWriter.CloseObject();
                testWriter.CloseArray();
                testWriter.AddNode("saturdayDelivery", true);
                testWriter.CloseObject();

                // There should be only 19 nodes, so 40 is off the end.
                testWriter.DeleteNode(40,false);
                string resultJson = testWriter.WriteJson();

                Assert.Fail();
            }
            catch (IndexOutOfRangeException e)
            {
                Assert.AreEqual("There is no node 40", e.Message);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}