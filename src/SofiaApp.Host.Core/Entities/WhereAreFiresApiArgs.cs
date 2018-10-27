using System;

namespace SofiaApp.Host.Entities
{
	public abstract class ApiArgs
	{
		public int serviceId { get; private set; } = 25;
		public int userId { get; protected set; } = 42;
		public string purpose { get; protected set; } = "Sofia app";

		public ApiArgs (int serviceId)
		{
			this.serviceId = serviceId;
		}
	}

	public class WhereAreFiresApiArgs : ApiArgs
	{
		public WhereAreFiresApiArgs () : base (25)
		{

		}

		public WhereAreFiresApiArgs (GeoBox geoBox, DateTime date1, DateTime date2) : base (25)
		{
			//this.date1 = int.Parse (date1.ToString (""));
			//this.date2 = int.Parse (date2.ToString (""));

			ulx = geoBox.UpperLeft.Longitude;
			uly = geoBox.UpperLeft.Latitude;
			lrx = geoBox.LowerRight.Longitude;
			lry = geoBox.LowerRight.Latitude;
		}

		public int date1 { get; set; } = 2018012;
		public int date2 { get; set; } = 2018020;

		public double ulx { get; set; } = -120;
		public double uly { get; set; } = 25;
		public double lrx { get; set; } = -98;
		public double lry { get; set; } = 20;
	}
}
