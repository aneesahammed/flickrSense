/*
 * @file:PhotoNavParameter
 * @brief:Class to pass selected photo info to Detail page.
 * @author:AA 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flickrSense.Models
{
    public class PhotoNavParameter
    {
        public int Index { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
