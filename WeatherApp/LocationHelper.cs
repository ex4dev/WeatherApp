using System.Xml;

namespace WeatherApp
{
    public class LocationHelper
    {
        public static string GetCity()
        {
            XmlDocument doc = new XmlDocument();

            string details = "http://ip-api.com/xml/";

            doc.Load(details);

            XmlNodeList nodeLstCity = doc.GetElementsByTagName("city");

            return nodeLstCity[0].InnerText;
        }
    }
}