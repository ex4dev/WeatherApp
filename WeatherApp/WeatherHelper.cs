using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    public class WeatherHelper
    {
        private static readonly HttpClient client = new HttpClient();
        private static string weatherInfo;

        internal static void ResetWeatherInfo()
        {
            weatherInfo = null;
        }

        internal static async Task<string> RequestWeatherInfo(string city)
        {
            if (weatherInfo != null) return weatherInfo;
            string API_KEY = getApiKey();
            string URL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}";
            string product = null;
            Console.WriteLine("Sending request to: " + URL);
            HttpResponseMessage response = await client.GetAsync(URL);
            Console.WriteLine("Content received!");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Success! Result: " + product);
            }

            weatherInfo = product;
            return product;
        }

        internal static dynamic GetWeatherDynamic(string city)
        {
            string product = RequestWeatherInfo("Cary").Result;
            dynamic stuff = JObject.Parse(product);
            Console.WriteLine("JSON parsed!");
            return stuff;
        }

        internal static dynamic GetWeatherValue(string city, string path)
        {
            string product = RequestWeatherInfo(city).Result;
            if (product == null)
            {
                weatherInfo = "Error!";
                MessageBox.Show("Weather information is not available for that city.");
                return "-1";
            }

            if (product == "Error!") return "-1";

            JObject stuff = JObject.Parse(product);
            //Console.WriteLine("JSON parsed!");
            //Console.WriteLine("Path: " + path);

            JToken tokenn = stuff.SelectToken(path);
            if (tokenn == null)
            {
                Console.WriteLine("Error! Invalid path " + path);
                return "0";
            }

            string token = tokenn.ToString();
            //Console.WriteLine("Returning " + token);
            return token;
        }

        private static string getApiKey()
        {
            //return "c487a8c49b12d0d17b8c253136c35820";

            try
            {
                return File.ReadAllText(
                    Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName).FullName +
                    Path.DirectorySeparatorChar + "api-key.txt");
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "An error occurred while reading openweathermap.org api key. Please ensure it is set in api-key.txt.",
                    "API Key", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
                Environment.Exit(-1);
                return "Invalid";
            }
        }
    }
}