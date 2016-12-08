using GalaSoft.MvvmLight.Messaging;
using flickrSense.Common.Storage;
using flickrSense.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Navigation;


namespace flickrSense.ViewModels
{
    public class MapControlPageViewModel: ViewModelBase
    {
        #region <-PrivateMembers->
        private object _locker = new object();

        #endregion

        #region <-Properties->
        //private Location _location = null;
        //public Location Location { get; set; }
        #endregion

        #region <-Constructor->
        public MapControlPageViewModel()
        {

        }
        #endregion

        #region <-PublicMethods->
        private static int _testCounter = 1;
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                if(parameter is Photo)
                {
                    var photo= parameter as Photo;

                    // Specify a known location.
                    BasicGeoposition snPosition = new BasicGeoposition() { Latitude = photo.Latitude, Longitude = photo.Longitude+ _testCounter++ };
                    Geopoint snPoint = new Geopoint(snPosition);

                    Messenger.Default.Send<MapInfo>(new MapInfo() { SnPoint=snPoint,Title=photo.Title});
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[OnNavigatedToAsync]", ex.ToString());
            }
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            try
            {
                if (suspending)
                {

                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[OnNavigatedFromAsync]", ex.ToString());
            }
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            try
            {
                args.Cancel = false;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[OnNavigatingFromAsync]", ex.ToString());
            }
        }
        #endregion
    }
}
