/*
 * @file:IFlickrService
 * @brief: Obsolete file
 * @author:AA 
 */

using flickrSense.Models;
using System.Threading.Tasks;

namespace flickrSense.Services.Flickr
{
    public interface IFlickrService
    {
        Task<RootObject> GetRecentPhotos(string query);
    }
}
