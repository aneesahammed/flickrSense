/*
 * @file:FlickrDataConfig
 * @brief:  Query string configuration.
 * @author:AA 
 */
namespace flickrSense.Services.Flickr
{
    /// <summary>
    /// Query string configuration.
    /// </summary>
    public class FlickrDataConfig
    {
        /// <summary>
        /// Gets or sets flickr query type.
        /// </summary>
        public FlickrQueryType QueryType { get; set; } = FlickrQueryType.Recent;

        /// <summary>
        /// Gets or sets query parameters.
        /// </summary>
        public string Query { get; set; }
    }
}
