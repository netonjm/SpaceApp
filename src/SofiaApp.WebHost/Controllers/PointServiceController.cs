using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace SofiaApp.Host
{
	public class GeoPoint
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public static bool TryParse (string s, out GeoPoint result)
		{
			result = null;

			var parts = s.Split (',');
			if (parts.Length != 2) {
				return false;
			}

			double latitude, longitude;
			if (double.TryParse (parts [0], out latitude) &&
				double.TryParse (parts [1], out longitude)) {
				result = new GeoPoint () { Longitude = longitude, Latitude = latitude };
				return true;
			}
			return false;
		}
	}

	public class PointServiceController : ApiController
	{
		[Route ("sofia/location/{data}")]
		public HttpResponseMessage GetActionEvent (string data)
		{
			GeoPoint result;
			if (!GeoPoint.TryParse (data, out result)) {
				return new HttpResponseMessage (HttpStatusCode.OK);
			}
			return Request.CreateResponse (HttpStatusCode.OK, result);
		}
	}
}
