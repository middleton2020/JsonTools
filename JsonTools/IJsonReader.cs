namespace CM.JsonTools
{
    public interface IJsonReader
    {
        // You will need to use the following code elements in order to apply this interface.
        
        //#region DeligateVariables
        //JsonReader.DeligateMakeClass makeClass;
        //JsonReader.DeligateCloseClass closeClass;
        //JsonReader.DeligateMakeArray makeArray;
        //JsonReader.DeligateCloseArray closeArray;
        //JsonReader.DeligateSetBoolean setBoolean;
        //JsonReader.DeligateSetDecimal setDecimal;
        //JsonReader.DeligateSetInteger setInteger;
        //JsonReader.DeligateSetString setString;
        //#endregion

        //makeClass = MakeClass;
        //closeClass = CloseClass;
        //makeArray = MakeArray;
        //closeArray = CloseArray;
        //setBoolean = SetBoolean;
        //setDecimal = SetDecimal;
        //setInteger = SetInteger;
        //setString = SetString;

        //JsonReader testReader = new JsonReader(makeClass,
        //                                       closeClass,
        //                                       makeArray,
        //                                       closeArray,
        //                                       setBoolean,
        //                                       setDecimal,
        //                                       setInteger,
        //                                       setString);
        //resultOrder = (Order) testReader.ReadJson(testJson, resultOrder);


        object CloseArray(string inpName, object inpObject, string inpPath);
        object CloseClass(string inpName, object inpObject, string inpPath);
        object MakeArray(string inpName, object inpObject, string inpPath);
        object MakeClass(string inpName, object inpObject, string inpPath);
        object SetBoolean(string inpName, bool inpValue, object inpObject, string inpPath);
        object SetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath);
        object SetInteger(string inpName, int inpValue, object inpObject, string inpPath);
        object SetString(string inpName, string inpValue, object inpObject, string inpPath);
    }
}