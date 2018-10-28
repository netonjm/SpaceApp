using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SofiaApp.Services;

namespace SofiaApp.Host.Web.Controllers
{
	public class FirePointServiceController : ControllerBase
	{
		[Route ("sofia/firepoints/add")]
		public ActionResult<string> GetFirepointEvent ()
		{
			var query = Request.QueryString;
			var parameters = GetParameters (query);
			if (parameters.Count == 0) {
				return "";
			}
			var latStr = parameters.FirstOrDefault (s => s.parameter == nameof ("lat")).value;
			var lonStr = parameters.FirstOrDefault (s => s.parameter == nameof ("lon")).value;

			if (!float.TryParse (latStr, out float lat) || !float.TryParse (lonStr, out float lon))
				return "";

			var firePoint = SofiaEnvirontment.Current.AddAppFirepoint (new GeoPoint (lat, lon), "127.0.0.1", "testAccount");
			return firePoint.ID;
		}

		private string nameof (object lat)
		{
			throw new NotImplementedException ();
		}

		List<(string parameter, string value)> GetParameters (QueryString query) {
			var result = new List<(string parameter, string value)> ();
			if (query.HasValue && query.Value.Length > 0) {
				if (query.Value [0] != '?') {
					return result;
				}
				var parameters = query.Value.Substring (1).Split ('&');
				foreach (var parameter in parameters) {
					var values = parameter.Split ('=');
					if (values.Length == 2) {
						result.Add ((values [0], values [1]));
					}
				}
			}
			return result;
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
		public ActionResult<GeoJson> GetNasaFirePoints (string location)
		{
			if (!GeoPoint.TryParse (location, out GeoPoint point)) {
				return null;
			}

			GeoJson result = SofiaEnvirontment.Current.GetGeoNasaFirePoints (point);
			return result;
		}

		[Route ("sofia/public/weather")]
		public ActionResult<Weather> GetWeatherFromCity ()
		{
			var query = Request.QueryString;
			var parameters = GetParameters (query);
			if (parameters.Count == 0) {
				return null;
			}
			var country = parameters.FirstOrDefault (s => s.parameter == "country").value;
			var zipCode = parameters.FirstOrDefault (s => s.parameter == "zipCode").value;
			var metric = parameters.FirstOrDefault (s => s.parameter == "metric").value;
			if (string.IsNullOrEmpty (zipCode) || string.IsNullOrEmpty (country))
				return null;

			var measure = metric == "imperial" ? WeatherMeasure.Imperial : WeatherMeasure.Metric;
			var weather = SofiaEnvirontment.Current.GetWeather (country, zipCode, measure);
			return weather;
		}

		[Route ("sofia/public/firepoints/get")]
		public ActionResult<FirePoint []> GetSofiaPublicFirePoints ()
		{
			var query = Request.QueryString;
			var parameters = GetParameters (query);
			if (parameters.Count == 0) {
				return new FirePoint [0];
			}
			var latStr = parameters.FirstOrDefault (s => s.parameter == "lat").value;
			var lonStr = parameters.FirstOrDefault (s => s.parameter == "lon").value;

			if (!float.TryParse (latStr, out float lat) || !float.TryParse (lonStr, out float lon))
				return new FirePoint [0];

			return SofiaEnvirontment.Current.GetPublicSofiaFirePoints ();
		}

		[Route ("sofia/firepoints/get")]
		public ActionResult<GeoJson> GetSofiaFirePoints ()
		{
			GeoJson result = SofiaEnvirontment.Current.GetGeoSofiaFirePoints ();
			return result;
		}
	}
}