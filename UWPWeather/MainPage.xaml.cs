using System;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var position = await LocationManager.GetPosition();

                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;


                RootObject myWeather = await OpenWeatherMapProxy.GetWeather(lat, lon);

                // Schedule update
                var uri = String.Format("http://webservic.azurewebsites.net/?lat={0}&lon={1}", lat, lon);

                var tileContent = new Uri(uri);
                var requestedInterval = PeriodicUpdateRecurrence.HalfHour;

                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.StartPeriodicUpdate(tileContent, requestedInterval);

                // get icon from outside websites(use API actually) and also we need follow some formats
                //string icon = String.Format("http://openweathermap.org/img/w/{0}.png", myWeather.weather[0].icon);

                // use prefix"ms-appx:///" to get the URI for local resource.
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

                TempTextBlock.Text = myWeather.weather[0].description;
                DescriptionTextBlock.Text = ((int)myWeather.main.temp).ToString();
                LocationTextBlock.Text = myWeather.name;
            }
            catch
            {
                LocationTextBlock.Text = "Cannot get your location's weather.";
            }
           
        }
    }
}
