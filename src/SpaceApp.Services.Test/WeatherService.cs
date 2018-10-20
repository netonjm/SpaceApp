using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceApp.Services.Test
{
	public class WeatherService : IWeatherService
	{
		public Weather GetWeather (string zipCode, string country = "es", string measure = "metric")
		{
			//Sign up for a free API key at http://openweathermap.org/appid  
			string key = "42c0e77ad3018dc3c35da3da97274faf";
			string queryString = $"http://api.openweathermap.org/data/2.5/weather?zip={zipCode},{country}&appid={key}&units={measure}";

			dynamic results = GetDataFromService (queryString);

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
			WebClient client = new WebClient ();
			var result = client.DownloadString (queryString);

			dynamic data = null;
			if (result != null) {
				data = JsonConvert.DeserializeObject (result);
			}
			return data;
		}
		[TestFixture ()]
		public class Test
		{
			[Test ()]
			public void TestCase ()
			{
				var service = new WeatherService ();
				var data = service.GetWeather ("46920");
				Assert.IsNotNull (data);
			}
		}
	}
}