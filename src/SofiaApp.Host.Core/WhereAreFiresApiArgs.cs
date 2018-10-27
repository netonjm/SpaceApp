namespace SofiaApp.Test
{
	public abstract class ApiArgs
	{
		public int serviceId { get; private set; } = 25;
		public int userId { get; protected set; } = 42;
		public string purpose { get; protected set; } = "Test app";

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

		public int date1 { get; set; } = 2018012;
		public int date2 { get; set; } = 2018020;
		public int ulx { get; set; } = -120;
		public int uly { get; set; } = 25;
		public int lrx { get; set; } = -98;
		public int lry { get; set; } = 20;
	}
}
