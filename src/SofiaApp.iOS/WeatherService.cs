using System.Net;
using System;
using Newtonsoft.Json;

namespace SofiaApp.Services
{
	public interface IWeatherService
	{
		Weather GetWeather (string zipCode, string country, string measure);
	}

	public class Weather
	{
		// Because labels bind to these values, set them to an empty string to
		// ensure that the label appears on all platforms by default.
		public string Title { get; set; } = " ";
		public string Temperature { get; set; } = " ";
		public string Wind { get; set; } = " ";
		public string Humidity { get; set; } = " ";
		public string Visibility { get; set; } = " ";
		public string Sunrise { get; set; } = " ";
		public string Sunset { get; set; } = " ";
	}

	public class WeatherService : IWeatherService
	{
		public Weather GetWeather (string zipCode, string country, string measure)
		{
			//Sign up for a free API key at http://openweathermap.org/appid  
			string key = "42c0e77ad3018dc3c35da3da97274faf";
			string queryString = $"http://api.openweathermap.org/data/2.5/weather?zip={zipCode},{country}&appid={key}&units={measure}";

			var results = GetDataFromService (queryString);

			if (results ["weather"] != null) {
				var weather = new Weather {
					Title = (string)results ["name"],
					Temperature = (string)results ["main"] ["temp"] + " F",
					Wind = (string)results ["wind"] ["speed"] + " mph",
					Humidity = (string)results ["main"] ["humidity"] + " %",
					Visibility = (string)results ["weather"] [0] ["main"]
				};

				DateTime time = new System.DateTime (1970, 1, 1, 0, 0, 0, 0);
				DateTime sunrise = time.AddSeconds ((double)results ["sys"] ["sunrise"]);
				DateTime sunset = time.AddSeconds ((double)results ["sys"] ["sunset"]);
				weather.Sunrise = sunrise.ToString () + " UTC";
				weather.Sunset = sunset.ToString () + " UTC";
				return weather;
			} else {
				return null;
			}
		}

		static dynamic GetDataFromService (string queryString)
		{
			var client = new WebClient ();
			var result = client.DownloadString (queryString);

			dynamic data = null;
			if (result != null) {
				data = JsonConvert.DeserializeObject (result);
			}
			return data;
		}
	}
}
