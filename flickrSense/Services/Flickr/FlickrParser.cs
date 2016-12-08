/*
 * @file:FlickrParser
 * @brief: Parse Flickr results into strong type.
 * @author:AA 
 */
using Microsoft.Toolkit.Uwp.Services;
using flickrSense.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace flickrSense.Services.Flickr
{
    /// <summary>
    /// Flickr Response Parser.
    /// </summary>
    public class FlickrParser : IParser<Photo>
    {
        /// <summary>
        /// Parse string data into strongly typed list.
        /// </summary>
        /// <param name="data">Input string.</param>
        /// <returns>List of strongly typed objects.</returns>
        public IEnumerable<Photo> Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var rootObject = JsonConvert.DeserializeObject<RootObject>(data);
            return rootObject.Photos.Photo;
        }      
    }
}
