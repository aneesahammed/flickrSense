/*
 * @file:Photos
 * @brief: Strongly types Flickr Photos object.
 * @author:AA 
 */
using System.Collections.Generic;

namespace flickrSense.Models
{
    public class Photos
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Perpage { get; set; }
        public string Total { get; set; }
        public List<Photo> Photo { get; set; }
    }
}
