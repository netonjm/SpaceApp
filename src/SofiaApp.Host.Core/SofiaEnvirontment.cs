using System;
using System.Collections.Generic;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;
using System.Linq;
using SofiaApp.Services;

namespace SofiaApp.Host
{
	class SofiaEnvirontment
	{
		const int DefaultZoom = 500000;
		internal readonly List<NasaFirePoint> nasaFirePoints;
		internal readonly List<FirePoint> firePoints;

		readonly GeoPoint defaultPoint = new GeoPoint (39.495387f, -0.475974f);
		readonly WeatherService weatherService;

		SofiaEnvirontment ()
		{
			nasaFirePoints = new List<NasaFirePoint> ();
			firePoints = new List<FirePoint> ();

			weatherService = new WeatherService ();

			var args = new WhereAreFires (GeoBox.From (defaultPoint, DefaultZoom));
			var firesDetected = WebApiHelper.GetNasaWebApiResponse <WhereAreFiresResponse []> (args);
			AddItems (firesDetected);
		}

		void AddItems (WhereAreFiresResponse[] items) {
			foreach (var fire in items) {
				if (!nasaFirePoints.Select (s => s.Fire).Contains (fire)) {
					var firePoint = new NasaFirePoint (fire) {
						Title = $"Fire Detected {nasaFirePoints.Count}#", 
					};
					firePoint.Description = $"A fire was detected near {firePoint.NearestCity.title}";
					nasaFirePoints.Add (firePoint);
				}
			}
		}

		public FirePoint GetFirepoint (string id) => firePoints.FirstOrDefault (s => s.ID == id);

		public FirePoint AddAppFirepoint (GeoPoint geoPoint, string ip, string account)
		{
			var userData = new AppFirePoint () {
				Ip = ip,
				Date = DateTime.Now, Title = $"User",
				NearestCity = FindNearestCity.From (geoPoint) , 
				Point = geoPoint };
				userData.Title = $"{userData.GetCityName ()} (U)";
				userData.Description = "";
				firePoints.Add (userData);
			return userData;
		}

		public FirePoint AddTwitterFirepoint (GeoPoint geoPoint, string ip, string account, string data)
		{
			var args = new FindNearestCity (geoPoint);
			var nearestCityResponse = WebApiHelper.GetNasaWebApiResponse<FindNearestCityResponse> (args);
			var userData = new TwitterFirePoint () {
				Ip = ip,
				NearestCity = FindNearestCity.From (geoPoint), 
				Point = geoPoint, 
				Account = account, 
				Date = DateTime.Now, 
				ParsedData = data
			};
			userData.Title = $"{userData.GetCityName ()} (T)";
			userData.Description = "";
			firePoints.Add (userData);
			return userData;
		}

		public FirePoint[] GetPublicSofiaFirePoints ()
		{
			return firePoints.ToArray ();
		}

		public GeoJson GetGeoSofiaFirePoints ()
		{
			var result = new GeoJson ();
			List<Feature> features = new List<Feature> ();
			foreach (var response in firePoints) {
				features.Add (Feature.From (response));
			}
			result.features = features.ToArray ();
			return result;
		}

		public GeoJson GetGeoNasaFirePoints (GeoPoint point)
		{
			var result = new GeoJson ();
			List<Feature> features = new List<Feature> ();
			foreach (var response in nasaFirePoints) {
				var feature = Feature.From (response);
				features.Add (feature);
			}
			result.features = features.ToArray ();
			return result;
		}

		readonly public static SofiaEnvirontment Current = new SofiaEnvirontment ();

		public Weather GetWeather (string country, string zipCode, WeatherMeasure measure)
		{
			var weather = weatherService.GetWeather (zipCode, country, measure);
			return weather;
		}
	}
}