using System;
using System.Collections.Generic;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;

namespace SofiaApp.Host
{
	public class SofiaEnvirontment
	{
		internal readonly List<WhereAreFiresResponse> nasaResponses;
		internal readonly List<FirePoint> userResponses;

		public SofiaEnvirontment ()
		{
			nasaResponses = new List<WhereAreFiresResponse> ();
			userResponses = new List<FirePoint> ();
		}

		void AddItems (WhereAreFiresResponse[] items) {
			foreach (var fire in items) {
				if (!nasaResponses.Contains (fire)) {
					nasaResponses.Add (fire);
				}
			}
		}

		FindNearestCityResponse GetFindNearestCity (GeoPoint geoPoint) {
			var args = new FindNearestCity (geoPoint);
			return WebApiHelper.GetWebApiResponse<FindNearestCityResponse> (args);
		}

		public FirePoint AddAppFirepoint (GeoPoint geoPoint, string ip, string account)
		{
			var userData = new AppFirePoint () {
				Ip = ip,
				Date = DateTime.Now, Title = $"User",
				NearestCity = GetFindNearestCity (geoPoint) , 
				Point = geoPoint };
				userData.Title = $"{userData.NearestCity} (U)";
				userResponses.Add (userData);
			return userData;
		}

		public FirePoint AddTwitterFirepoint (GeoPoint geoPoint, string ip, string account, string data)
		{
			var args = new FindNearestCity (geoPoint);
			var nearestCityResponse = WebApiHelper.GetWebApiResponse<FindNearestCityResponse> (args);
			var userData = new TwitterFirePoint () {
				Ip = ip,
				NearestCity = GetFindNearestCity (geoPoint), 
				Point = geoPoint, 
				Account = account, 
				Date = DateTime.Now, 
				ParsedData = data
			};
			userData.Title = $"{userData.NearestCity} (T)";
			userResponses.Add (userData);
			return userData;
		}

		List<Feature> GetFeatures () 
		{
			List<Feature> items = new List<Feature> ();
			foreach (var response in nasaResponses) {
				items.Add (Feature.From (response));
			}
			return items;
		}

		public GeoJson GetGeoData (GeoPoint point)
		{
			var args = new WhereAreFires ();
			var firesDetected = WebApiHelper.GetWebApiResponse<WhereAreFiresResponse []> (args);
			AddItems (firesDetected);

			var result = new GeoJson ();
			var features = GetFeatures ();
			result.features = features.ToArray ();
			return result;
		}

		readonly public static SofiaEnvirontment Current = new SofiaEnvirontment ();
	}
}