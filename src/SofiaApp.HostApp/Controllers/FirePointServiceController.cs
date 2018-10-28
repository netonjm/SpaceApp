using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;

namespace SofiaApp.Host.Web.Controllers
{
	public class FirePointServiceController : ControllerBase
	{
		[Route ("sofia/firepoints/add/{firepoint}")]
		public ActionResult<string> GetFirepointEvent (string firepoint)
		{
			if (!GeoPoint.TryParse (firepoint, out GeoPoint point)) {
				return "";
			}
			var firePoint = SofiaEnvirontment.Current.AddAppFirepoint (point, "127.0.0.1", "testAccount");
			return firePoint.ID;
		}

		[Route ("sofia/firepoint/{id}/title/{title}")]
		public ActionResult<bool> GetFirepointTitleEvent (string id, string title)
		{
			var firepoint = SofiaEnvirontment.Current.GetFirepoint (id);
			if (firepoint == null) {
				return false;
			}
			firepoint.Title = title;
			return true;
		}

		[Route ("sofia/firepoints/nasa/get/{location}")]
		public ActionResult<GeoJson> GetNasaFirePoints (string geopoint)
		{
			if (!GeoPoint.TryParse (geopoint, out GeoPoint point)) {
				return null;
			}

			GeoJson result = SofiaEnvirontment.Current.GetGeoNasaFirePoints (point);
			return result;
		}

		[Route ("sofia/firepoints/get")]
		public ActionResult<GeoJson> GetSofiaFirePoints ()
		{
			GeoJson result = SofiaEnvirontment.Current.GetGeoSofiaFirePoints ();
			return result;
		}
	}
}