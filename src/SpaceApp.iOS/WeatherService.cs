using System.Net;
using Foundation;
using SpaceApp.Services;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net.Http;

//[assembly: Dependency(typeof(SharedManager))]
namespace SpaceApp.Services
{
	public class WeatherService : IWeatherService
	{
		public Weather GetWeather (string zipCode, string country, string measure)
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
	}
}
