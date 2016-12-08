using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using flickrSense.Services.Flickr;
using flickrSense.ViewModels;

namespace flickrSense
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // We are at design time.
                //SimpleIoc.Default.Register<IDataService, DesignTimeDataService>();
            }          

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
            SimpleIoc.Default.Register<MapControlPageViewModel>();
        }


        //MainPageVm
        public MainPageViewModel MainPageVm
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }

        //DetailPageVm
        public DetailPageViewModel DetailPageVm
        {
            get { return ServiceLocator.Current.GetInstance<DetailPageViewModel>(); }
        }

        //MapPageVm
        public MapControlPageViewModel MapControlPageVm
        {
            get { return ServiceLocator.Current.GetInstance<MapControlPageViewModel>(); }
        }
    }
}
