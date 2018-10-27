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
}
