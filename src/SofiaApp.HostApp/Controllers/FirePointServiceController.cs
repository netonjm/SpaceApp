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
		//[Route ("sofia/point/")]
		//public ActionResult<int> GetActionEvent ()
		//{
		//	ServiceTest.Count++;
		//	return ServiceTest.Count;
		//}

		[Route ("sofia/location/{data}")]
		public ActionResult<GeoJson> GetActionEvent (string data)
		{
			if (!GeoPoint.TryParse (data, out GeoPoint point)) {
				return null;
			}

			GeoJson result = SofiaEnvirontment.Current.GetGeoData (point);
			return result;
		}
	}
}