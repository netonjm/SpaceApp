namespace SofiaApp.Maps
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
	}

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
}
