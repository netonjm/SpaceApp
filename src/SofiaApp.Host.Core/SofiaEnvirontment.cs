using System;
using System.Collections.Generic;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;
using System.Linq;

namespace SofiaApp.Host
{

	public class SofiaEnvirontment
	{
		const int DefaultZoom = 500000;
		internal readonly List<NasaFirePoint> nasaFirePoints;
		internal readonly List<FirePoint> firePoints;

		public SofiaEnvirontment ()
		{
			nasaFirePoints = new List<NasaFirePoint> ();
			firePoints = new List<FirePoint> ();
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
				userData.Title = $"{userData.NearestCity} (U)";
				firePoints.Add (userData);
			return userData;
		}

		public FirePoint AddTwitterFirepoint (GeoPoint geoPoint, string ip, string account, string data)
		{
			var args = new FindNearestCity (geoPoint);
			var nearestCityResponse = WebApiHelper.GetWebApiResponse<FindNearestCityResponse> (args);
			var userData = new TwitterFirePoint () {
				Ip = ip,
				NearestCity = FindNearestCity.From (geoPoint), 
				Point = geoPoint, 
				Account = account, 
				Date = DateTime.Now, 
				ParsedData = data
			};
			userData.Title = $"{userData.NearestCity} (T)";
			firePoints.Add (userData);
			return userData;
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
			var args = new WhereAreFires (GeoBox.From (point, DefaultZoom));
			var firesDetected = WebApiHelper.GetWebApiResponse<WhereAreFiresResponse []> (args);
			AddItems (firesDetected);

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
	}
}