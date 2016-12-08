using Windows.UI.Xaml.Controls;

namespace flickrSense.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void AutoSuggestBox_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            // if the enter key is pressed
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                searchBox.IsEnabled = false;
                searchBox.IsEnabled = true;
            }
        }
    }
}
