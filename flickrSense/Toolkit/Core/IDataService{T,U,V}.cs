/*
 * @file:IDataService{T,U,V}
 * @brief: Generic interface that all deployed service providers implement.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */


using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Toolkit.Uwp.Services.Core
{
    /// <summary>
    /// Generic interface that all deployed service providers implement.
    /// </summary>
    /// <typeparam name="T">Reference to underlying data service provider.</typeparam>
    /// <typeparam name="U">Strongly-typed schema for data returned in list query.</typeparam>
    /// <typeparam name="V">Configuration type specifying query parameters.</typeparam>
    public interface IDataService<T, U, V>
    {
        /// <summary>
        /// Gets the underlying data service provider.
        /// </summary>
        T Provider { get; }

        /// <summary>
        /// Makes a request for a list of data from the given service provider.
        /// </summary>
        /// <param name="config">Describes the query on the list data request.</param>
        /// <param name="maxRecords">Specifies an upper limit to the number of records returned.</param>
        /// <returns>Returns a strongly typed list of results from the service.</returns>
        Task<List<U>> RequestAsync(V config, int pageIndex, int maxRecords);

    }
}
