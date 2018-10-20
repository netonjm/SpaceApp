using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;

namespace SpaceApp
{
	public interface ILogger
	{
		void Write (string log);
	}

	public class ConsoleLog : ILogger
	{
		public void Write (string log)
		{
			Console.WriteLine (log);
		}
	}

	public class GpsService
	{
		public GpsService () {

		}
		ILogger logger;

		public void SetLogger (ILogger logger) {
			this.logger = logger;
		}

		public async Task<Position> GetPosition (int seconds, bool includeHeading, int accuraccy)
		{
			try {
				var hasPermission = await Utils.CheckPermissions (Permission.Location);
				if (!hasPermission)
					return null;

				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = accuraccy;
				var position = await locator.GetPositionAsync (TimeSpan.FromSeconds (seconds), null, includeHeading);

				if (position == null) {
					throw new Exception ("no gps!");
				}
				return position;
				//ButtonAddressForPosition.IsEnabled = true;
				//labelGPS.Text = string.Format ("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
				//position.Timestamp, position.Latitude, position.Longitude,
				//position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

			} catch (Exception ex) {
				throw ex;
			} 
		}
	}
}
