/*
 * @file:RootObject
 * @brief: Strongly types Flickr Root object.
 * @author:AA 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flickrSense.Models
{
    public class RootObject
    {
        public Photos Photos { get; set; }
        public string Stat { get; set; }
    }
}
