# JsonTools
Tools to read and write to JSON objects.

## Use Of The Code
See the included Licence file for details, however my personal requests as the developer are:
1) You are welcome to use this program as part of a larger application, for commercial or no-commercial purposes.
2) If you modify this program, please credit my original work in there.
3) If you create your own, modified copy of this program an sell it for a profit, please discuss with me some form of royalties for my inial work.
4) If you find any bugs or problems in this program, please report them to me so that they can be fixed.
5) If you have any ideas for upgrades to this program, please let me know. I might want to include them.
6) If you make any use of this program, please let me know what you're doing with it. It's very good for my ego :~D

## Running JsonTools
To understand how to use this module, it is benefitial to take a look at the Test classes, as these provide excellent examples.
### Use of the JsonWriter

using CM.JsonTools;

JsonWriter testWriter = new JsonWriter();     // Prepare the class.

// Build the elements of the JSON
testWriter.OpenObject("");                    // Open the parent object
testWriter.AddNode("id", "Bibble");           // Add an 'id' string property.
testWriter.AddNode("name", "Test Name");      // Add a 'name' string property.
testWriter.AddNode("parcels", 1);             // Add a 'parcels' integert property.
testWriter.AddNode("value", (decimal)11.50);  // Add a 'value' decimal property.
testWriter.AddNode("orderDate", testDate);    // Add an 'orderDate' date-time property.
testWriter.OpenArray("items");                // Add an array called 'items'.
testWriter.OpenObject();                      // Open an 'item' object.
testWriter.AddNode("name", "Box 17");         // Add a 'name' string property to the 'item'.
testWriter.CloseObject();                     // Close the 'item' object.
testWriter.OpenObject();                      // Open an 'item' object.
testWriter.AddNode("name", "22 Sphere");      // Add a 'name' string property to the 'item'.
testWriter.CloseObject();                     // Close the 'item' object.
testWriter.OpenObject();                      // Open an 'item' object.
testWriter.AddNode("name", "Alphabet");       // Add a 'name' string property to the 'item'.
testWriter.CloseObject();                     // Close the 'item' object.
testWriter.CloseArray();                      // Close the 'items' array.
testWriter.AddNode("saturdayDelivery", true); // Add a 'saturdayDelivery' boolean property.
testWriter.CloseObject();                     // Close the parent object

string resultJson = testWriter.WriteJson();   // Output the data as a JSON.


This produces the following JSON:
{"id":"Bibble","name":"Test Name","parcels":1,"value":11.50","orderDate":"2023-09-30\","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery\":true}
