using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using SofiaApp.Maps;
using System.Collections.Generic;

namespace SofiaApp.Host
{
	public class PointServiceController : ApiController
	{
		[Route ("sofia/location/{data}")]
		public HttpResponseMessage GetActionEvent (string data)
		{
			GeoPoint result;
			if (!GeoPoint.TryParse (data, out result)) {


				return new HttpResponseMessage (HttpStatusCode.OK);
			}

			var  temp = new GeoJson ();
			var features = new List<Feature> ();
			var feature = new Feature () {
				geometry = new Geometry (-119.51185f, 37.57664f), properties = new FeatureProperty () { title = "test", description = "my point" }
			};
			features.Add (feature);
			temp.features = features.ToArray ();
			return Request.CreateResponse (HttpStatusCode.OK, temp);
		}
	}
}
