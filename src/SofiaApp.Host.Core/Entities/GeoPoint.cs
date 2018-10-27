namespace SofiaApp.Host.Entities
{
	public class GeoPoint
	{
		public float Latitude { get; set; }
		public float Longitude { get; set; }

		public static bool TryParse (string s, out GeoPoint result)
		{
			result = null;

			var parts = s.Split (',');
			if (parts.Length != 2) {
				return false;
			}

			float latitude, longitude;
			if (float.TryParse (parts [0], out latitude) &&
				float.TryParse (parts [1], out longitude)) {
				result = new GeoPoint () { Longitude = longitude, Latitude = latitude };
				return true;
			}
			return false;
		}
	}
}
