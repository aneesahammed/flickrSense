/*
 * @file:FlickrService
 * @brief: Class for connecting to Flickr.
 * @author:AA 
 */
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flickrSense.Models;
using Microsoft.Toolkit.Uwp.Services.Core;

namespace flickrSense.Services.Flickr
{
    public class FlickrService : IDataService<FlickrDataProvider, Photo, FlickrDataConfig>
    {

        #region <-PrivateMembers->
        private FlickrDataProvider _flickrDataProvider;
        #endregion

        #region <-Properties->
        private static FlickrService _instance;
        public static FlickrService Instance => _instance ?? (_instance = new FlickrService());
        #endregion

        #region <-PublicMethods->
        /// <summary>
        /// Gets a reference to an instance of the underlying data provider.
        /// </summary>
        public FlickrDataProvider Provider => _flickrDataProvider ?? (_flickrDataProvider = new FlickrDataProvider());

        public async Task<List<Photo>> RequestAsync(FlickrDataConfig config,int pageIndex,int maxRecords)
        {
            var results = await Provider.LoadDataAsync(config, pageIndex, maxRecords);
            return results.ToList();
        }       
        #endregion
    }
}
