using System;
using System.Collections.Generic;
using System.Text;

namespace CM.JsonTools
{
    public abstract class JsonWriterTools
    {
        private const string quote = "\"";
        private const string colon = ":";
        private const string comma = ",";

        /// <summary>
        /// A simple way to get the approrpiate 'comma' character for calling programs
        /// </summary>
        /// <returns> A constant, string, holding the "comma" (,) encoded character.</returns>
        protected static string AddComma()
        {
            return comma;
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">String. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending with a comma.</returns>
        protected static string AddProperty(string propertyName, string propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }
                if (propertyValue == null)
                {
                    throw new ArgumentException($"'{nameof(propertyValue)}' cannot be null.", nameof(propertyValue));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + quote + propertyValue + quote + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">DateTime. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending with a comma.</returns>
        protected static string AddProperty(string propertyName, DateTime propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + quote + propertyValue.ToString("yyyy-MM-dd") + quote + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">Double. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending with a comma.</returns>
        protected static string AddProperty(string propertyName, double propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + propertyValue.ToString() + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">Double. The proerty value to output.</param>
        /// <param name="decimalPlaces">Int. The number of decimal places to output the double to.</param>
        /// <returns>A string containing a JSON experession of the property, ending with a comma.</returns>
        protected static string AddProperty(string propertyName, double propertyValue, int decimalPlaces)
        {
            try
            {
                string format;
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                if (decimalPlaces >= 0)
                {
                    format = "N" + decimalPlaces.ToString();
                }
                else
                {
                    throw new ArgumentException($"'{nameof(decimalPlaces)}' cannot be negative.", nameof(decimalPlaces));
                }
                string jsonEntry = quote + propertyName + quote + colon
                        + propertyValue.ToString(format) + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">Integer. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending with a comma.</returns>
        protected static string AddProperty(string propertyName, int propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + propertyValue + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">Boolian. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending with a comma.</returns>
        protected static string AddProperty(string propertyName, bool propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + propertyValue.ToString().ToLower() + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">String. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending without a comma.</returns>
        protected static string AddLastProperty(string propertyName, string propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }
                if (propertyValue == null)
                {
                    throw new ArgumentException($"'{nameof(propertyValue)}' cannot be null.", nameof(propertyValue));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + quote + propertyValue + quote;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">DateTime. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending without a comma.</returns>
        protected static string AddLastProperty(string propertyName, DateTime propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + quote + propertyValue.ToString("yyyy-MM-dd") + quote;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">String. The proerty value to output.</param>
        /// <returns></returns>
        protected static string AddLastProperty(string propertyName, double propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + propertyValue.ToString("N2");

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">Int. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending without a comma.</returns>
        protected static string AddLastProperty(string propertyName, int propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }
                string jsonEntry = quote + propertyName + quote + colon + propertyValue;
                return jsonEntry;
            }
            catch
            {
                throw;
            };
        }

        /// <summary>
        /// Ensures that the correct format is applied to a property when output as a JSON.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">Boolian. The proerty value to output.</param>
        /// <returns>A string containing a JSON experession of the property, ending without a comma.</returns>
        protected static string AddLastProperty(string propertyName, bool propertyValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }
                string jsonEntry = quote + propertyName + quote + colon
                    + propertyValue.ToString().ToLower();
                return jsonEntry;
            }
            catch
            {
                throw;
            };
        }

        /// <summary>
        /// Takes a small JSON element as a property and encapsulates it as part of a larger JSON
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">String containing a JSON. The proerty value to output.</param>
        /// <returns>A string containing the output JSON.</returns>
        protected static string AddJson(string propertyName, string jsonValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }
                if (jsonValue == null)
                {
                    throw new ArgumentException($"'{nameof(jsonValue)}' cannot be null.", nameof(jsonValue));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + jsonValue + comma;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Takes a small JSON element as a property and encapsulates it as part of a larger JSON. No comma on the end.
        /// </summary>
        /// <param name="propertyName">Label to attach to the property.</param>
        /// <param name="propertyValue">String containing a JSON. The proerty value to output.</param>
        /// <returns>A string containing the output JSON.</returns>
        protected static string AddLastJson(string propertyName, string jsonValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentException($"'{nameof(propertyName)}' cannot be null.", nameof(propertyName));
                }
                if (jsonValue == null)
                {
                    throw new ArgumentException($"'{nameof(jsonValue)}' cannot be null.", nameof(jsonValue));
                }

                string jsonEntry = quote + propertyName + quote + colon
                        + jsonValue;

                return jsonEntry;
            }
            catch
            {
                throw;
            }
        }

        ///// <summary>
        ///// Converts a collection of strings into a string containing a JSON array.
        ///// </summary>
        ///// <param name="propertyName">Label to attach to the property.</param>
        ///// <param name="propertyValue">Collection of strings.</param>
        ///// <param name="listSize">The number of entries in the collection.</param>
        ///// <returns>A string containing the JSON array.</returns>
        //protected static string AddPropertyArray(string propertyName,
        //    CollectionBase propertyValue,
        //    int listSize)
        //{
        //    string jsonEntry = "";
        //    bool firstEntry = true;

        //    try
        //    {
        //        if (listSize > 0)
        //        {
        //            jsonEntry += quote + propertyName + quote + colon + "[";
        //            foreach (OrderData entryJson in propertyValue)
        //            {
        //                //Add a comma before each entry, except for the first.
        //                if (firstEntry)
        //                { firstEntry = false; }
        //                else
        //                { jsonEntry += comma; }

        //                jsonEntry += entryJson.MakeJson();
        //            }
        //            jsonEntry += "]";
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return jsonEntry;
        //}

        ///// <summary>
        ///// Converts an array of type OrderData into a string containing a JSON array.
        ///// </summary>
        ///// <param name="propertyName">Label to attach to the property.</param>
        ///// <param name="propertyValue">Array of type OrderData.</param>
        ///// <returns>A string containing the JSON array.</returns>
        //protected static string AddPropertyArray(string propertyName, OrderData[] propertyValue)
        //{
        //    string jsonEntry = "";

        //    try
        //    {
        //        // Loop through array of packages and create their JSONs.
        //        for (int thisEntry = 0; thisEntry < propertyValue.Length; thisEntry++)
        //        {
        //            // The first entry needs to start with the label and open the array bracket.
        //            if (thisEntry == 0)
        //            { jsonEntry += quote + propertyName + quote + colon + "["; }

        //            // Output the JSON for this package.
        //            jsonEntry += propertyValue[thisEntry].MakeJson();

        //            // The last entry needs to close the array bracket.
        //            if (thisEntry == (propertyValue.Length - 1))
        //            { jsonEntry += "]"; }
        //            // If not the last entry, then add a comma to the end.
        //            else
        //            { jsonEntry += comma; }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return jsonEntry;
        //}

        /// <summary>
        /// This is a 'fake' method in this class and should always be overridden 
        ///   in the child class. It is in here to allow us to use it in the above 
        ///   AddPropertyArray method.
        /// </summary>
        /// <param name="inpSourceData">A data object holdinng all the data used to build the JSON.</param>
        /// <returns>The JSON data as a string.</returns>
     //   public static abstract string MakeMessageBody(IOrderData inpSourceData);

        //public static string MakeJson<T>(string propertyName, List<T> recordList) where T : OrderData
        //{
        //    string payload = "";

        //    if (recordList.Count > 0)
        //    { payload += ","; }

        //    payload += AddPropertyArray<T>(propertyName, recordList, recordList.Count);

        //    return payload;
        //}

        //protected static string AddPropertyArray<T>(string propertyName,
        //    List<T> propertyValue,
        //    int listSize) where T : OrderData
        //{
        //    string jsonEntry = "";
        //    bool firstEntry = true;

        //    try
        //    {
        //        if (listSize > 0)
        //        {
        //            jsonEntry += quote + propertyName + quote + colon + "[";
        //            foreach (T entryJson in propertyValue)
        //            {
        //                //Add a comma before each entry, except for the first.
        //                if (firstEntry)
        //                { firstEntry = false; }
        //                else
        //                { jsonEntry += comma; }

        //                jsonEntry += entryJson.MakeJson();
        //            }
        //            jsonEntry += "]";
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return jsonEntry;
        //}
    }
}
