namespace CM.JsonTools
{
    public interface IJsonReader
    {
        // You will need to use the following code elements in order to apply this interface.
        
        //#region DeligateVariables
        //JsonReader.DeligateMakeObject makeObject;
        //JsonReader.DeligateCloseObject closeObject;
        //JsonReader.DeligateMakeArray makeArray;
        //JsonReader.DeligateCloseArray closeArray;
        //JsonReader.DeligateSetBoolean setBoolean;
        //JsonReader.DeligateSetDecimal setDecimal;
        //JsonReader.DeligateSetDouble setDouble;
        //JsonReader.DeligateSetInteger setInteger;
        //JsonReader.DeligateSetLongInt setLongInt;
        //JsonReader.DeligateSetString setString;
        //#endregion

        //makeObject = MakeObject
        //closeObject = CloseObject;
        //makeArray = MakeArray;
        //closeArray = CloseArray;
        //setBoolean = SetBoolean;
        //setDecimal = SetDecimal;
        //setDouble = SetDouble;
        //setInteger = SetInteger;
        //setLongInt = SetLongInt;
        //setString = SetString;

        //JsonReader testReader = new JsonReader(makeObject,
        //                                       closeObject,
        //                                       makeArray,
        //                                       closeArray,
        //                                       setBoolean,
        //                                       setDecimal,
        //                                       setDouble,
        //                                       setInteger,
        //                                       setLongInt,
        //                                       setString,
        //                                       acceptNulls);
        //resultOrder = (Order) testReader.ReadJson(testJson, resultOrder);


        object CloseArray(string inpName, object inpObject, string inpPath);
        object CloseObject(string inpName, object inpObject, string inpPath);
        object MakeArray(string inpName, object inpObject, string inpPath);
        object MakeObject(string inpName, object inpObject, string inpPath);
        object SetBoolean(string inpName, bool inpValue, object inpObject, string inpPath);
        object SetDouble(string inpName, double inpValue, object inpObject, string inpPath);
        object SetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath);
        object SetInteger(string inpName, uint inpValue, object inpObject, string inpPath);
        object SetLongInt(string inpName, long inpValue, object inpObject, string inpPath);
        object SetString(string inpName, string inpValue, object inpObject, string inpPath);
    }
}