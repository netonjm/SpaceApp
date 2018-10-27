using System;

namespace SofiaApp.Host.Entities
{
	public class FirePoint
	{
		public DateTime Date { get; set; }
		public GeoPoint Point { get; set; }
		public string Ip { get; set; }
		public string Title { get; set; }
		public FindNearestCityResponse NearestCity { get; set; }
	}

	public class AppFirePoint : FirePoint
	{
		public string Account { get; set; }
	}

	public class WebFirePoint : FirePoint
	{
		public string Account { get; set; }
	}

	public class TwitterFirePoint : FirePoint
	{
		public string Account { get; set; }
		public string StatusMessage { get; set; }
		public string ParsedData { get; set; }
	}
}