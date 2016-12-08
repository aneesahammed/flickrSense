using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace flickrSense.Models
{
    public class MapInfo
    {
        public Geopoint SnPoint { get; set; }

        public string Title { get; set; }
    }
}
