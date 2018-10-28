
using System;

namespace SofiaApp.Host.Entities
{
	public class NasaFirePoint
	{
		public WhereAreFiresResponse Fire { get; internal set; }
		public string ID { get; internal set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public FindNearestCityResponse NearestCity { get; internal set; }

		public NasaFirePoint (WhereAreFiresResponse fire)
		{
			ID = Guid.NewGuid ().ToString ();
			Fire = fire;
			NearestCity = FindNearestCity.From (new GeoPoint (fire.lat, fire.lon));
		}
	}
}