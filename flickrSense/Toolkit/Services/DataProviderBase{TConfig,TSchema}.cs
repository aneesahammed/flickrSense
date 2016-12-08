/*
 * @file:DataProviderBase{TConfig,TSchema}
 * @brief: Base class for data providers in this library.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Toolkit.Uwp.Services
{
    /// <summary>
    /// Base class for data providers in this library.
    /// </summary>
    /// <typeparam name="TConfig">Strong typed query configuration object.</typeparam>
    /// <typeparam name="TSchema">Strong typed object to parse the response items into.</typeparam>
    public abstract class DataProviderBase<TConfig, TSchema> : DataProviderBase<TConfig>
        where TSchema : SchemaBase
    {
        /// <summary>
        /// Load data from provider endpoint.
        /// </summary>
        /// <param name="config">Query configuration.</param>
        /// <param name="pageIndex">Current page number</param>
        /// <param name="maxRecords">Upper record limit.</param>
        /// <returns>List of strong typed objects.</returns>
        public Task<IEnumerable<TSchema>> LoadDataAsync(TConfig config,int pageIndex=1,int maxRecords = 20)
        {
            return LoadDataAsync(config,pageIndex, maxRecords, GetDefaultParser(config));
        }

        /// <summary>
        /// Default parser abstract method.
        /// </summary>
        /// <param name="config">Query configuration object.</param>
        /// <returns>Strong typed default parser.</returns>
        protected abstract IParser<TSchema> GetDefaultParser(TConfig config);
    }
}
