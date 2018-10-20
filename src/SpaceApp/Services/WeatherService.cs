using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceApp
{
	public static class WeatherService
	{
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

		public static async Task<Weather> GetWeather (string zipCode)
		{
			//Sign up for a free API key at http://openweathermap.org/appid  
			string key = "42c0e77ad3018dc3c35da3da97274faf";
			string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
				+ zipCode + ",us&appid=" + key + "&units=imperial";

			dynamic results = await GetDataFromService (queryString).ConfigureAwait (false);

			if (results ["weather"] != null) {
				Weather weather = new Weather ();
				weather.Title = (string)results ["name"];
				weather.Temperature = (string)results ["main"] ["temp"] + " F";
				weather.Wind = (string)results ["wind"] ["speed"] + " mph";
				weather.Humidity = (string)results ["main"] ["humidity"] + " %";
				weather.Visibility = (string)results ["weather"] [0] ["main"];

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

		static async Task<dynamic> GetDataFromService (string queryString)
		{
			HttpClient client = new HttpClient ();
			var response = await client.GetAsync (queryString);

			dynamic data = null;
			if (response != null) {
				string json = response.Content.ReadAsStringAsync ().Result;
				data = JsonConvert.DeserializeObject (json);
			}

			return data;
		}
	}
}
