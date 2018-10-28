
using System;

namespace SofiaApp.Host.Entities
{
	public class NasaFirePoint
	{
		public WhereAreFiresResponse Fire { get; private set; }
		public string Id { get; private set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public FindNearestCityResponse NearestCity { get; private set; }

		public NasaFirePoint (WhereAreFiresResponse fire)
		{
			Id = Guid.NewGuid ().ToString ();
			Fire = fire;
			NearestCity = FindNearestCity.From (new GeoPoint (fire.lat, fire.lon));
		}
	}
}