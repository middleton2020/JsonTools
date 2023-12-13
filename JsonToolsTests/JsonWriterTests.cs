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
            testWriter.AddNode("orderDate",testDate);
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
            testWriter.AddNode("name","Box 17");
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
        public void DeleteNodeTest()
        {
            Assert.Fail();
        }
    }
}