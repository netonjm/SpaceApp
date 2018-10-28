using SofiaApp.Helpers;

namespace SofiaApp.Host.Entities
{
	public class FindNearestCity : ApiArgs
	{
		public float latitude { get; set; }
		public float longitude { get; set; }

		public FindNearestCity () : base (28)
		{

		}

		public FindNearestCity (GeoPoint point) : this ()
		{
			latitude = point.Latitude;
			longitude = point.Longitude;
		}

		public static FindNearestCityResponse From (GeoPoint geoPoint)
		{
			var args = new FindNearestCity (geoPoint);
			return WebApiHelper.GetWebApiResponse<FindNearestCityResponse> (args);
		}
	}

	//{"latitude":51.506321,"longitude":-0.12714}

	public class FindNearestCityResponse
	{
		public long distance { get; set; }
		public string title { get; set; }
		public string location_type { get; set; }
		public long woeid { get; set; }
		public string latt_long { get; set; }

		public override string ToString ()
		{
			return $"{title} ({latt_long})";
		}
	}
}
