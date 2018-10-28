namespace SofiaApp.Host.Entities
{
	public class FindGeographicCoordinates : ApiArgs
	{
		public string city { get; set; }

		public FindGeographicCoordinates (string city) : base (27)
		{
			this.city = city;
		}
	}

	public class FindGeographicCoordinatesResponse
	{
		public float latitude { get; set; }
		public float longitude { get; set; }
	}
}
