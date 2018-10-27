namespace SofiaApp.Host.Entities
{
	public class HowManyFiresExist : ApiArgs
	{
		public HowManyFiresExist () : base (24)
		{

		}

		public HowManyFiresExist (GeoBox geoBox) : this ()
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

	public class HowManyFiresExistResponse
	{
		public int number { get; set; }
	}
}
