using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WeatherApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string city = LocationHelper.GetCity();

        public MainWindow()
        {
            InitializeComponent();
            Refresh(false);
        }

        private void Image_Failed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Image failed to load: {e.ErrorException.Message}");
        }

        private void SetTempText(string text)
        {
            Dispatcher.Invoke(() => { lblTemp.Text = text; });
        }

        private void SetCloudText(string text)
        {
            Dispatcher.Invoke(() => { lblCloud.Text = text; });
        }

        private void SetPrecipText(string text)
        {
            Dispatcher.Invoke(() => { lblPrecip.Text = text; });
        }

        private void SetFeelsText(string text)
        {
            Dispatcher.Invoke(() => { lblFeels.Text = text; });
        }

        private void SetHumidityText(string text)
        {
            Dispatcher.Invoke(() => { lblHumidity.Text = text; });
        }

        private void SetCityText(string text)
        {
            Dispatcher.Invoke(() =>
            {
                lblCity.Text = text;
                if (text.Equals("Refreshed!"))
                    lblCity.Foreground = Brushes.LimeGreen;
                else
                    lblCity.Foreground = Brushes.White;
            });
        }

        private void SetIcon(string icon)
        {
            Dispatcher.Invoke(() =>
            {
                weatherIcon.Source = new BitmapImage(new Uri($"https://openweathermap.org/img/wn/{icon}@4x.png"));
            });
        }

        private void Refresh(object sender, RoutedEventArgs mouseButtonEventArgs)
        {
            Refresh(false);
        }

        private void Refresh(bool loadingText)
        {
            Task.Run(() =>
            {
                try
                {
                    SetCityText("Loading...");
                    if (loadingText)
                    {
                        SetTempText("Loading...");
                        SetCloudText("Loading...");
                        SetFeelsText("Loading...");
                        SetPrecipText("Loading...");
                    }

                    WeatherHelper.ResetWeatherInfo();
                    //dynamic d = WeatherHelper.GetWeatherDynamic("Cary");

                    SetTempText(
                        Conversions.KelvinDToFahrenheit(WeatherHelper.GetWeatherValue(city, "main.temp")) + "°F");

                    SetPrecipText("Rain: " + WeatherHelper.GetWeatherValue(city, "rain.1h") + "mm (last hour)*");
                    SetCloudText(WeatherHelper.GetWeatherValue(city, "weather[0].main"));
                    SetFeelsText("Feels Like " +
                        Conversions.KelvinDToFahrenheit(
                            WeatherHelper.GetWeatherValue(city, "main.feels_like")) + "°F");
                    SetHumidityText(WeatherHelper.GetWeatherValue(city, "main.humidity") + "% humidity");
                    SetIcon(WeatherHelper.GetWeatherValue(city, "weather[0].icon"));

                    SetCityText("Refreshed!");
                    Thread.Sleep(1500);
                    SetCityText(city);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });
        }

        private void ChangeCity(object sender, RoutedEventArgs routedEventArgs)
        {
            ChangeCity();
        }

        private void ChangeCity()
        {
            InputDialog dialog = new InputDialog("Enter City", city);
            if (dialog.ShowDialog() == true)
            {
                string ans = dialog.Answer;
                if (ans != null && !ans.Equals(string.Empty)) city = ans;
                else city = LocationHelper.GetCity();
                Refresh(true);
            }
        }
    }
}