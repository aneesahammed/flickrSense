using GalaSoft.MvvmLight.Messaging;
using flickrSense.Common.Storage;
using flickrSense.Models;
using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace flickrSense.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapControlPage : Page
    {
        public static readonly Geopoint SeattleGeopoint = new Geopoint(new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 });
        RandomAccessStreamReference mapIconStreamReference;

        public MapControlPage()
        {
            this.InitializeComponent();
            mapIconStreamReference = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/MapPin.png"));

            Messenger.Default.Register<MapInfo>(this,SetMapView);
        }

        private void SetMapView(MapInfo mapInfo)
        {
            try
            {
                // Create a MapIcon.
                MapIcon mapIcon1 = new MapIcon();
                mapIcon1.Location = mapInfo.SnPoint;
                mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                mapIcon1.Title = mapInfo.Title;
                mapIcon1.ZIndex = 0;

                // Add the MapIcon to the map.
                mapControl.MapElements.Add(mapIcon1);

                // Center the map over the POI.
                mapControl.Center = mapInfo.SnPoint;
                mapControl.ZoomLevel = 14;
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[SetMapView]", ex.ToString());
            }
           
        }
    }
}
