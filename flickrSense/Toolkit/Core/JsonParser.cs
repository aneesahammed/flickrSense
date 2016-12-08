/*
 * @file:JsonParser
 * @brief: JsonParser type.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */


using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Toolkit.Uwp.Services.Core
{
    /// <summary>
    /// JsonParser type.
    /// </summary>
    /// <typeparam name="T">Data type to parse</typeparam>
    internal class JsonParser<T> : IParser<T>
        where T : SchemaBase
    {
        /// <summary>
        /// Takes string data and parses to strong type.
        /// </summary>
        /// <param name="data">String data.</param>
        /// <returns>Strong type deserialized from string data.</returns>
        public IEnumerable<T> Parse(string data)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(data);
        }
    }
}
