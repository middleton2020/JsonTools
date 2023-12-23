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
        //JsonReader.DeligateSetInteger setInteger;
        //JsonReader.DeligateSetString setString;
        //#endregion

        //makeObject = MakeObject
        //closeObject = CloseObject;
        //makeArray = MakeArray;
        //closeArray = CloseArray;
        //setBoolean = SetBoolean;
        //setDecimal = SetDecimal;
        //setInteger = SetInteger;
        //setString = SetString;

        //JsonReader testReader = new JsonReader(makeObject,
        //                                       closeObject,
        //                                       makeArray,
        //                                       closeArray,
        //                                       setBoolean,
        //                                       setDecimal,
        //                                       setInteger,
        //                                       setString);
        //resultOrder = (Order) testReader.ReadJson(testJson, resultOrder);


        object CloseArray(string inpName, object inpObject, string inpPath);
        object CloseObject(string inpName, object inpObject, string inpPath);
        object MakeArray(string inpName, object inpObject, string inpPath);
        object MakeObject(string inpName, object inpObject, string inpPath);
        object SetBoolean(string inpName, bool inpValue, object inpObject, string inpPath);
        object SetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath);
        object SetInteger(string inpName, int inpValue, object inpObject, string inpPath);
        object SetString(string inpName, string inpValue, object inpObject, string inpPath);
    }
}