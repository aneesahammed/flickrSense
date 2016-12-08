/*
 * @file:FlickrDataProvider
 * @brief:  Data Provider for connecting to Flickr service.
 * @author:AA 
 */
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.Services;
using Microsoft.Toolkit.Uwp.Services.Exceptions;
using flickrSense.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace flickrSense.Services.Flickr
{
    public class FlickrDataProvider : DataProviderBase<FlickrDataConfig, Photo>
    {   
        /// <summary>
        /// Base Url for service.
        /// </summary>
        private const string BaseUrl = "https://api.flickr.com/services/rest/?";
        private const string ApiKey = @"9b5820dc2b4e70307d65979f23d5789f";


        /// <summary>
        /// Wrapper around REST API for making data request.
        /// </summary>
        /// <typeparam name="TSchema">Schema to use</typeparam>
        /// <param name="config">Query configuration.</param>
        /// <param name="maxRecords">Upper limit for records returned.</param>
        /// <param name="parser">IParser implementation for interpreting results.</param>
        /// <returns>Strongly typed list of results.</returns>
        protected override async Task<IEnumerable<TSchema>> GetDataAsync<TSchema>(FlickrDataConfig config,int pageIndex,int maxRecords, IParser<TSchema> parser)
        {
            var queryMethod = string.Empty;
            var searchText = string.Empty;

            switch (config.QueryType)
            {
                case FlickrQueryType.Recent:
                    queryMethod = "flickr.photos.getRecent";
                    break;
                case FlickrQueryType.Search:
                    queryMethod = "flickr.photos.search";
                    searchText = config.Query;
                    break;
                default:
                    break;
            }

            var uri = new Uri($"{BaseUrl}method={queryMethod}&extras=geo&description&per_page={maxRecords}&page={pageIndex}&text={searchText}&api_key={ApiKey}&format=json&nojsoncallback=1");

            using (HttpHelperRequest request = new HttpHelperRequest(uri, HttpMethod.Get))
            {
                using (var response = await HttpHelper.Instance.SendRequestAsync(request).ConfigureAwait(false))
                {
                    var data = await response.GetTextResultAsync().ConfigureAwait(false);

                    if (response.Success && !string.IsNullOrEmpty(data))
                    {   
                        return parser.Parse(data);
                    }

                    throw new RequestFailedException(response.StatusCode, data);
                }
            }
            
        }

        /// <summary>
        /// Returns parser implementation for specified configuration.
        /// </summary>
        /// <param name="config">Query configuration.</param>
        /// <returns>Strongly typed parser.</returns>
        protected override IParser<Photo> GetDefaultParser(FlickrDataConfig config)
        {
            return new FlickrParser();
        }

        /// <summary>
        /// Check validity of configuration.
        /// </summary>
        /// <param name="config">Query configuration.</param>
        protected override void ValidateConfig(FlickrDataConfig config)
        {
            
        }
    }
}
