using System;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;

namespace SofiaApp.iOS.Services
{
	static class WebServices
	{
		public static class Sofia
		{
			public const string host = "http://sofianasachallenge.azurewebsites.net/";
			public static string SetFirePoint (GeoPoint currentPosition, string user)
			{
				var args = new AddPointRequest () { lat = currentPosition.Latitude.ToString (), lon = currentPosition.Longitude.ToString () };
				var pointId = WebApiHelper.GetWebApiResponse<string> ($"{host}sofia/firepoints/add", null, null, args);
				return pointId;
			}

			public static WhereAreFiresResponse [] GetFirePoints (GeoPoint currentPosition, string user)
			{
				var args = new WhereAreFires (GeoBox.From (currentPosition, 100));
				var firesDetected = WebApiHelper.GetNasaWebApiResponse<WhereAreFiresResponse []> (args);
				return firesDetected;
			}
		}
	}
}