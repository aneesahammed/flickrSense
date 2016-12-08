using System.Collections.Generic;
using flickrSense.Common;
using Microsoft.Toolkit.Uwp.Services;
using Windows.Devices.Geolocation;

namespace flickrSense.Models
{
    public class Photo : SchemaBase
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Secret { get; set; }
        public string Server { get; set; }
        public int Farm { get; set; }
        public string Title { get; set; }
        public int IsPublic { get; set; }
        public int IsFriend { get; set; }
        public int IsFamily { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Accuracy { get; set; }
        public int Context { get; set; }
        public string Place_Id { get; set; }
        public string Woeid { get; set; }
        public int? Geo_Is_Family { get; set; }
        public int? Geo_Is_Friend { get; set; }
        public int? Geo_Is_Contact { get; set; }
        public int? Geo_Is_Public { get; set; }        

        public string ThumbnailPath
        {
            get
            {
                if (Utility.IsMobile)
                    return $"https://farm{Farm}.staticflickr.com/{Server}/{Id}_{Secret}_q.jpg";
                else
                    return $"https://farm{Farm}.staticflickr.com/{Server}/{Id}_{Secret}_n.jpg";
            }
        }

        public string MediumPath
        {
            get
            {
                if (Utility.IsMobile)
                    return $"https://farm{Farm}.staticflickr.com/{Server}/{Id}_{Secret}_z.jpg";
                else
                    return $"https://farm{Farm}.staticflickr.com/{Server}/{Id}_{Secret}_b.jpg";
            }
        }
    }
}


