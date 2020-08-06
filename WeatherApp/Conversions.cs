using System;

namespace WeatherApp
{
    public class Conversions
    {
        public static double KelvinToFahrenheit(double kelvin)
        {
            return Math.Round((kelvin - 273.15) * 1.8000 + 32.00, 2);
        }

        public static double KelvinToFahrenheit(string kelvin)
        {
            Console.WriteLine("Converting [" + kelvin + "] to F");
            return KelvinToFahrenheit(double.Parse(kelvin));
        }

        public static double KelvinDToFahrenheit(dynamic kelvin)
        {
            return KelvinToFahrenheit(Convert.ToString(kelvin));
        }
    }
}