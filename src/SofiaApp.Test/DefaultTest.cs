using System;
using NUnit.Framework;
using SofiaApp.Helpers;
using SofiaApp.Host;
using SofiaApp.Host.Entities;

namespace SofiaApp.Test
{
	public class DefaultTest
	{
		SofiaEnvirontment sofiaEnvirontment;

		public DefaultTest () {
			sofiaEnvirontment = SofiaEnvirontment.Current;
		}

		[Test ()]
		public void GetGeoData ()
		{
			var json = sofiaEnvirontment.GetGeoData (new GeoPoint (39.561673f, -0.505148f));
			Assert.IsTrue (json.features.Length > 0);
		}

		[Test ()]
		public void GetGeo () 
		{
			var point = new GeoPoint (39.561673f, -0.505148f);
			var geobox = GeoBox.From (point, 1000);
			var query = new WhereAreFires (geobox);
			var firesDetected = WebApiHelper.GetWebApiResponse<WhereAreFiresResponse []> (query);
			Assert.IsNotNull (firesDetected);
			Assert.IsTrue (firesDetected.Length > 0);
		}

		[Test ()]
		public void AddUserData ()
		{
			var response = sofiaEnvirontment.AddAppFirepoint (new GeoPoint (39.561673f, -0.505148f), "192.168.1.1", "test");
			Assert.IsNotNull (response);
			Assert.AreEqual (response.Ip, "192.168.1.1");
		}

		[Test ()]
		public void WhereTheFireTest ()
		{
			var post = new WhereAreFires ();
			var response = WebApiHelper.GetWebApiResponse<WhereAreFiresResponse []> (post);
			Console.WriteLine ("");
		}

		[Test ()]
		public void FindNearestCityTest ()
		{
			var post = new FindNearestCity (new GeoPoint (39.561673f, -0.505148f));
			var response = WebApiHelper.GetWebApiResponse<FindNearestCityResponse> (post);
			Assert.AreEqual (response.title, "Madrid");
		}
	
		[Test ()]
		public void CreateGoBox ()
		{
			var point = new GeoPoint (39.461538f, -0.4098373f);
			var geobox = GeoBox.From (point);
			Assert.NotNull (geobox);
		}
	}
}
