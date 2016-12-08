using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using flickrSense.Models;
using UwpHelpers.Controls.Common;
using System.Collections.ObjectModel;
using flickrSense.Services.Flickr;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Controls;
using Template10.Services.NetworkAvailableService;
using flickrSense.Common.Storage;

namespace flickrSense.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        #region <-PrivateMembers->        
        private static int _pageIndex = 1;
        #endregion

        #region <-Properties->
        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private bool _isSearchBoxVisible;
        public bool IsSearchBoxVisible
        {
            get { return _isSearchBoxVisible; }
            set
            {
                _isSearchBoxVisible = value;
                RaisePropertyChanged();
            }
        }

        private bool _isNetworkAvailable=true;
        public bool IsNetworkAvailable
        {
            get { return _isNetworkAvailable; }
            set
            {
                _isNetworkAvailable = value;
            }
        }

        private IncrementalLoadingCollection<Photo> _photoCollection;
        public IncrementalLoadingCollection<Photo> PhotoCollection
        {
            get { return _photoCollection; }
            set
            {
                _photoCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region <-Constructor->
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }

            Init();            
        }
        #endregion

        #region <-Init->
        private void Init()
        {
            try
            {
                PhotoCollection = new IncrementalLoadingCollection<Photo>((cancellationToken, count)
                => Task.Run(() => GetFlickrPhotos(new FlickrDataConfig()), cancellationToken));
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[Init]", ex.ToString());
            }
        }
        #endregion

        #region <-Commands->
        public ICommand PhotoSelectedCommand
        {
            get { return new RelayCommand<object>(PhotoSelectedCommandExecute); }
        }

        public ICommand ToggleSearchBoxVisibilityCommand
        {
            get { return new RelayCommand(ToggleSearchBoxVisibilityCommandExecute); }
        }

        public ICommand SearchQueryCommand
        {
            get { return new RelayCommand<Object>(SearchQueryCommandExecute); }
        }
        #endregion

        #region <-CommandMethods->
        private void PhotoSelectedCommandExecute(object o)
        {
            try
            {
                Photo photo;
                if (o is ItemClickEventArgs)
                    photo = ((o as ItemClickEventArgs).ClickedItem) as Photo;
                else
                    photo = o as Photo;

                if (photo != null)
                {
                    var photoNavParam = new PhotoNavParameter() { Index = PhotoCollection.IndexOf(photo), Photos = PhotoCollection.ToList() };
                    NavigationService.Navigate(typeof(Views.DetailPage), photoNavParam);
                }
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[PhotoSelectedCommandExecute]", ex.ToString());
            }
        }

        private void ToggleSearchBoxVisibilityCommandExecute()
        {
            try
            {
                IsSearchBoxVisible = !IsSearchBoxVisible;
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[ToggleSearchBoxVisibilityCommandExecute]", ex.ToString());
            }            
        }

        private void SearchQueryCommandExecute(object args)
        {
            try
            {
                if (args is AutoSuggestBoxQuerySubmittedEventArgs)
                {
                    var queryText = (args as AutoSuggestBoxQuerySubmittedEventArgs).QueryText;

                    var flickrConfig = new FlickrDataConfig() { QueryType = FlickrQueryType.Search, Query = queryText };

                    Reset();

                    PhotoCollection = new IncrementalLoadingCollection<Photo>((cancellationToken, count)
                        => Task.Run(() => GetFlickrPhotos(flickrConfig), cancellationToken));
                }
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[SearchQueryCommandExecute]", ex.ToString());
            }
        }       
        #endregion

        #region <-PublicMethods->
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                if (suspensionState.Any())
                {
                    Value = suspensionState[nameof(Value)]?.ToString();
                }

                var networkService = new NetworkAvailableService();
                networkService.AvailabilityChanged += async e =>
                {
                    if(await networkService.IsInternetAvailable())
                    {
                        IsNetworkAvailable = true;
                    }
                    else
                    {
                        IsNetworkAvailable = false;
                    }
                };               

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
                    suspensionState[nameof(Value)] = Value;
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

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        #endregion

        #region <-PrivateMethods->
        private void Reset()
        {
            try
            {
                PhotoCollection.Clear();
                PhotoCollection = null;
                _pageIndex = 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<ObservableCollection<Photo>> GetFlickrPhotos(FlickrDataConfig flickrDataConfig)
        {
            try
            {
                Views.Busy.SetBusy(true, "Loading...");

                var photos = await FlickrService.Instance.RequestAsync(flickrDataConfig,_pageIndex,10);
                _pageIndex++;
                return new ObservableCollection<Photo>(photos);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Views.Busy.SetBusy(false);
            }
        }
        #endregion

    }
}

