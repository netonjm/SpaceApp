using System;

namespace SofiaApp.Host.Entities
{
	public class WhereAreFires : ApiArgs
	{
		public WhereAreFires () : base (25)
		{

		}

		public WhereAreFires (GeoBox geoBox) : this ()
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

	public class WhereAreFiresResponse
	{
		public float lon { get; set; }
		public float lat { get; set; }
		public float temp4 { get; set; }
		public float temp11 { get; set; }
		public int temp { get; set; }
		public float size { get; set; }
		public int ecosys { get; set; }
		public int flag { get; set; }
		public int sat { get; set; }
		public int yd { get; set; }
		public int time { get; set; }
		public string datetime { get; set; }
	}
}
