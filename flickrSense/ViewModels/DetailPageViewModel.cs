/*
 * @file:DetailPageViewModel
 * @brief: ViewModel for Detail Page (Pivot)
 * @author:AA 
 */
using GalaSoft.MvvmLight.Command;
using flickrSense.Common;
using flickrSense.Common.Storage;
using flickrSense.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace flickrSense.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {

        #region <-PrivateMembers->
        private readonly object _locker = new object();
        #endregion

        #region <-Properties->
        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private ObservableCollection<Photo> _photoCollection=new ObservableCollection<Photo>();
        public ObservableCollection<Photo> PhotoCollection
        {
            get { return _photoCollection; }
            set
            {
                _photoCollection = value;
                RaisePropertyChanged();
            }
        }

        private Photo _selectedPhoto=null;
        public Photo SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                _selectedPhoto = value;
                RaisePropertyChanged();

                RaisePropertyChanged("IsLocationInfoAvailable");
                
            }
        }

        private int _selectedPhotoIndex;
        public int SelectedPhotoIndex
        {
            get { return _selectedPhotoIndex; }
            set
            {
                _selectedPhotoIndex = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsLocationInfoAvailable");
            }
        }

        public bool IsLocationInfoAvailable
        {
            get
            {
                if(!PhotoCollection.IsEmpty())
                {
                    _selectedPhoto = PhotoCollection[_selectedPhotoIndex];
                }

                if (_selectedPhoto != null)
                {
                    var condition = (_selectedPhoto.Latitude != 0) && (_selectedPhoto.Longitude != 0);

                    if (condition)
                        System.Diagnostics.Debug.WriteLine("Location found---");

                    return condition;
                }
                return false;
            }
            set
            {
                RaisePropertyChanged();
            }
        }
        #endregion

        #region <-Constructor->
        public DetailPageViewModel()
        {
            try
            {
                if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                {
                    Value = "Designtime value";
                }
            }
            catch (Exception)
            {

                throw;
            }            
        }
        #endregion

        #region<-Commands->
        public ICommand ShowLocationInfoCommand
        {
            get { return new RelayCommand<Object>(ShowLocationInfoCommandExecute); }
        }
        #endregion

        #region <-CommandMethods->
        private void ShowLocationInfoCommandExecute(object o)
        {
            try
            {
                NavigationService.Navigate(typeof(Views.MapControlPage), PhotoCollection[SelectedPhotoIndex]);
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("[ShowLocationInfoCommandExecute]", ex.ToString());
            }
        }
        #endregion       

        #region <-PublicMethods->
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                lock (_locker)
                {
                    var param = parameter as PhotoNavParameter;
                    if (param != null)
                    {
                        PhotoNavParameter photoNavParam = param;
                        PhotoCollection = new ObservableCollection<Photo>(photoNavParam.Photos);

                        if (!PhotoCollection.IsEmpty())
                        {
                            var strIndex = (suspensionState.ContainsKey(nameof(SelectedPhotoIndex))) ? 
                                suspensionState[nameof(SelectedPhotoIndex)]?.ToString() : null;

                            int index;
                            index = string.IsNullOrEmpty(strIndex) ? photoNavParam.Index : Convert.ToInt32(strIndex);

                            System.Diagnostics.Debug.WriteLine("PhotoCollection Count--> {0}", photoNavParam.Photos.Count);
                            System.Diagnostics.Debug.WriteLine("photoNavParam.Index--> {0}", index);

                            SelectedPhotoIndex = index;
                        }
                    }
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
                    suspensionState[nameof(Value)] = Value;
                }

                suspensionState[nameof(SelectedPhotoIndex)] = SelectedPhotoIndex;
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

