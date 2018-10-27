
using SofiaApp.Host;

namespace SofiaApp.Host.Entities
{
	public class GeoJson
	{
		public string type { get; set; } = "FeatureCollection";
		public Feature [] features { get; set; }
	}

	public class Feature
	{
		public string type { get; set; } = "Feature";
		public Geometry geometry { get; set; }
		public FeatureProperty properties { get; set; }

		public static Feature From (UserData userData)
		{
			var item = new Feature () {
				properties = new FeatureProperty ("", ""),
				geometry = new Geometry (userData.Point.Latitude,
										 userData.Point.Longitude)
			};
			return item;
		}

		public static Feature From (WhereAreFiresResponse userData)
		{
			var item = new Feature () {
				properties = new FeatureProperty ("", ""),
				geometry = new Geometry (userData.lat,
				                         userData.lon)
			};
			return item;
		}
	}

	public class Geometry
	{
		public string type { get; set; } = "Point";
		public string [] coordinates { get; set; }

		public Geometry (float latitude, float longitude)
		{
			coordinates = new string [] { latitude.ToString (), longitude.ToString () };
		}
	}

	public class FeatureProperty
	{
		public string title { get; set; } = "Point";
		public string description { get; set; } = "Point";

		public FeatureProperty (string title, string description) {
			this.title = title; this.description = description;
		}
	}
}
