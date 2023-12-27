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
This process works by setting up a series of 'nodes' representing properties, objects and arrays within the JSON. Once the JsonWriter has been initialised, you need first to open an un-named object record (which will want closing at the end). This produces the currly-braces that enclose the JSON. Each property is added to the JSON using an `AddNode` method. Similarly Arrays, or contained Objects can be created using the `OpenArray` and `OpenObject` methods. Once these need closing, use the `CloseArray` and `CloseObject` methods.

### JsonWriter Method Descriptions
- `OpenObject(Name)`     - Adds the '{' to open the object. If a **name** is supplied, then the name and a colon preceeds it.
- `CloseObject()`        - Adds the '}' to close the object. If necessary, a comma will also be added before the next node. This accepts no inputs.
- `OpenArray(Name)`      - Adds the '[' to open the array. If a **name** is supplied, then the name and a colon preceeds it.
- `CloseArray()`         - Adds the ']' to close the array. If necessary, a comma will also be added before the next node. This accepts no inputs.
- `AddNode(Name, Value)` - Adds a property with the **name**, a colon and the **value**. Any common data type can be provided as the value. If necessary, a comma will also be added before the next node.

### Example of the JsonWriter
`using CM.JsonTools;`
  
`JsonWriter testWriter = new JsonWriter();     // Prepare the class.`

`// Build the elements of the JSON`

`testWriter.OpenObject("");                    // Open the parent object`

`testWriter.AddNode("id", "Bibble");           // Add an 'id' string property.`

`testWriter.AddNode("name", "Test Name");      // Add a 'name' string property.`

`testWriter.AddNode("parcels", 1);             // Add a 'parcels' integert property.`

`testWriter.AddNode("value", (decimal)11.50);  // Add a 'value' decimal property.`

`testWriter.AddNode("orderDate", testDate);    // Add an 'orderDate' date-time property.`

`testWriter.OpenArray("items");                // Add an array called 'items'.`

`testWriter.OpenObject();                      // Open an 'item' object.`

`testWriter.AddNode("name", "Box 17");         // Add a 'name' string property to the 'item'.`

`testWriter.CloseObject();                     // Close the 'item' object.`

`testWriter.OpenObject();                      // Open an 'item' object.`

`testWriter.AddNode("name", "22 Sphere");      // Add a 'name' string property to the 'item'.`

`testWriter.CloseObject();                     // Close the 'item' object.`

`testWriter.OpenObject();                      // Open an 'item' object.`

`testWriter.AddNode("name", "Alphabet");       // Add a 'name' string property to the 'item'.`

`testWriter.CloseObject();                     // Close the 'item' object.`

`testWriter.CloseArray();                      // Close the 'items' array.`

`testWriter.AddNode("saturdayDelivery", true); // Add a 'saturdayDelivery' boolean property.`

`testWriter.CloseObject();                     // Close the parent object`

`string resultJson = testWriter.WriteJson();   // Output the data as a JSON.`


This produces the following JSON:

`{"id":"Bibble","name":"Test Name","parcels":1,"value":11.50","orderDate":"2023-09-30\","items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery\":true}`

### Use of the JsonReader
This process is rather more complex. It opperates in a similar way to the JsonWriter, transforming the JSON into a series of 'Nodes'. Unfortunately, we need to provide instructions on how to wtrite these nodes to the object (or whatever we're storing them in) where they are being held. We do this by providing a series of deligates. Typically each deligate will have a switch statement to decide from the name or path what to do with each property of the JSON.


### JsonWriter Method Descriptions
- `MakeObject(Name, Object, Path)`          - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly.
- `CloseObject(Name, Object, Path)`         - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly.
- `MakeArray(Name, Object, Path)`           - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly.
- `CloseArray(Name, Object, Path)`          - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly.
- `SetBoolean(Name, Value, Object, Path)`   - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly. The **value** will be written to the field for storage.
- `SetDecimal(Name, Value, Object, Path)`   - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly. The **value** will be written to the field for storage.
- 'SetInteger(Name, pValue, Object, Path)`  - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly. The **value** will be written to the field for storage.
- 'SetString(Name, pValue, Object, Path)`  - The **name** is used to identify the property that we are extracting from. The **path** provides a more specific identification (e.g. Items.Name is the Name within the Items array). The **object** is returned from each deligate and passed into the next one so that it builds up correctly. The **value** will be written to the field for storage.

`// We have several strings, so sort them first by name. Unfortunately there are 2 properties called "name", one in the top level JSON, and one within the Items objects. We can use the 'path' of the node to differentiate between these.`

`public static object ClassSetString(string inpName, string inpValue, object inpObject, string inpPath)`


### Example of the JsonReader
The following JSON is used to populate the class below:

`{"id":"Bibble","name":"Test Name","parcels":1,"value":11.50,"items":[{"name":"Box 17"},{"name":"22 Sphere"},{"name":"Alphabet"}],"saturdayDelivery":true}`

`using CM.JsonTools;`

`// Create delegate variables from the JsonReader types.`

`JsonReader.DeligateMakeObject makeClass;`

`JsonReader.DeligateCloseObject closeClass;`

`JsonReader.DeligateMakeArray makeArray;`

`JsonReader.DeligateCloseArray closeArray;`

`JsonReader.DeligateSetBoolean setBoolean;`

`JsonReader.DeligateSetDecimal setDecimal;`

`JsonReader.DeligateSetInteger setInteger;`

`JsonReader.DeligateSetString setString;`

`// Assigns the methods to the deligates.`

`makeClass = ClassMakeClass;`

`closeClass = ClassCloseClass;`

`makeArray = ClassMakeArray;`

`closeArray = ClassCloseArray;`

`setBoolean = ClassSetBoolean;`

`setDecimal = ClassSetDecimal;`

`setInteger = ClassSetInteger;`

`setString = ClassSetString;`

`Order resultOrder = null;    // We need to initialise the object that will be passed into the deligates.`

`JsonReader testReader = new JsonReader(makeObject, closeObject, makeArray, closeArray, setBoolean, setDecimal, setInteger, setString);`

`resultOrder = (Order)testReader.ReadJson(testJson, resultOrder);    // This process outputs the populated object for us.`

`// In this example, we are populating a 'dummy' class which has the following definition.`

`public class Order`

`{`

`    public string Id = "";`

`    public string Name = "";`

`    public int Parcels = 0;`

`    public decimal Value = 0;`

`    public bool SaturdayDelivery = false;`

`    public int CurrentItem = 0;`

`    public List<Items> ItemsList = new List<Items>();`

`    public class Items`

`    {`

`        public string ItemName = "";`

`    }`

`}`

`// The blank 'Top' class is handled by the 'else' block. We use the path to identify the Item classes withing the Items array.

`public static object ClassMakeClass(string inpName, object inpObject, string inpPath)`

`{`

`    Order tempOrder = (Order)inpObject;`

`    if (inpPath == "items"){`

`        Order.Items tempItem = new Order.Items();`

`        tempOrder.ItemsList.Add(tempItem);`

`        tempOrder.CurrentItem += 1;`

`    }`

`    else{`

`        tempOrder = new Order();`

`    }`

`    return tempOrder;`

`}`

`public static object ClassCloseClass(string inpName, object inpObject, string inpPath)`

`{`

`    Order tempOrder = (Order)inpObject;`

`    return tempOrder;`

`}`

`// There is only one array, so no further processing is required.`

`public static object ClassMakeArray(string inpName, object inpObject, string inpPath)`

`{`

`    return inpObject;`

`}`

`public static object ClassCloseArray(string inpName, object inpObject, string inpPath)`

`{`

`    return inpObject;`

`}`

`// With only one boolean, we can simply assign that. We identify it as a boolean because it has no quotes but can be read into a boolean (i.e. true/false, yes/no, etc.).`

`public static object ClassSetBoolean(string inpName, bool inpValue, object inpObject, string inpPath)`

`{`

`    Order tempOrder = (Order)inpObject;`

`    tempOrder.SaturdayDelivery = inpValue;`

`    return tempOrder;`

`}`

`// With only one deciaml field, we can simply assign that. We identify it as a decimal because it has no quotes, but does have a decimal point.`

`public static object ClassSetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath)`

`{`

`    Order tempOrder = (Order)inpObject;`

`    tempOrder.Value = inpValue;`

`    return tempOrder;`

`}`

`// With only one integer field, we can simply assign that. We identify it as an integer because it has no quotes and no decimals.`

`public static object ClassSetInteger(string inpName, int inpValue, object inpObject, string inpPath)`

`{`

`    Order tempOrder = (Order)inpObject;`

`    tempOrder.Parcels = inpValue;`

`    return tempOrder;`

`}`

`// We have several strings, so sort them first by name. Unfortunately there are 2 properties called "name", one in the top level JSON, and one within the Items objects. We can use the 'path' of the node to differentiate between these.`

`public static object ClassSetString(string inpName, string inpValue, object inpObject, string inpPath)`

`{`

`    Order tempOrder = (Order)inpObject;`

`    switch (inpName)`

`    {`

`        case "id":`

`            tempOrder.Id = inpValue;`

`            break;`

`        case "name":`

`            if (inpPath == "items.name") {`

`                tempOrder.ItemsList[tempOrder.CurrentItem - 1].ItemName = inpValue;`

`            }`

`            else {`

`                tempOrder.Name = inpValue;`

`            }`

`            break;`

`    }`

`    return tempOrder;`

`}`
