using System.Collections.Generic;
using System.Linq;
using Windows.System.Profile;

namespace flickrSense.Common
{
    public class Utility
    {
        public static readonly bool IsMobile = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";
    }


    public static class CollectionExtension
    {
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
                return true;
            if (list is ICollection<T>)
                return ((ICollection<T>)list).Count == 0;

            return !list.Any();
        }

    }
}
