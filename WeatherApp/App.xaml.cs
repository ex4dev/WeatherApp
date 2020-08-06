using System;
using System.Windows;
using System.Windows.Threading;

namespace WeatherApp
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"An unexpected error has occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Console.WriteLine("Starting up!");
        }
    }
}