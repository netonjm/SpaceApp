using System;
using System.Collections.Generic;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;

namespace SofiaApp.Host
{
	public class SofiaEnvirontment
	{
		readonly List<WhereAreFiresResponse> nasaResponses;
		readonly List<UserData> userResponses;

		public SofiaEnvirontment ()
		{
			nasaResponses = new List<WhereAreFiresResponse> ();
			userResponses = new List<UserData> ();
		}

		void AddItems (WhereAreFiresResponse[] items) {
			foreach (var fire in items) {
				if (!nasaResponses.Contains (fire)) {
					nasaResponses.Add (fire);
				}
			}
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
			var args = new WhereAreFiresApiArgs ();
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