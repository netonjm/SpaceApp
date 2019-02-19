using System;
using System.Diagnostics;
using System.Threading;
using IoTSharp.Components;
using SofiaApp.Helpers;
using SofiaApp.Host.Entities;
using SofiaApp.Services;

namespace SofiaApp.IoT
{
	class Program
	{
		public static IoTPin GreenLed, RedLed;
		static readonly WeatherService weatherService = new WeatherService();

		public static GeoPoint ReadLocalGeoPoint ()
		{
			var envPosition = Environment.GetEnvironmentVariable ("LOCAL_GEOPOINT");
			var position = envPosition.Split (',');
			return new GeoPoint (float.Parse (position [0]), float.Parse (position [1]));
		}

		static int GetFiresAroundYou (GeoPoint point) => 
			WebApiHelper.GetNasaWebApiResponse<HowManyFiresExistResponse> (new HowManyFiresExist (GeoBox.From (point))).number;

		static bool alertDectected;

		static string Speak (string text)
		{
			var proc = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = "espeak",
					Arguments = $"-v es \"{text}\"",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};

			proc.Start ();
			return proc.StandardOutput.ReadToEnd ();
		}

		public static void Main (string [] args)
		{
			GreenLed.Value = true;
			RedLed.Value = false;

			GreenLed = new IoTPin (Connectors.GPIO27);
			GreenLed.SetDirection (IoTPinDirection.DirectionOutInitiallyLow);
			RedLed = new IoTPin (Connectors.GPIO22);
			RedLed.SetDirection (IoTPinDirection.DirectionOutInitiallyLow);

			while (true) {
				var count = GetFiresAroundYou (geopoint);
				if (count > 0) {
					if (!alertDectected) {
						RedLed.Value = true;
						GreenLed.Value = false;
						var currentWeather = weatherService.GetWeather (weatherZipCode, weatherContry, WeatherMeasure.Metric);
						Speak ($"Warning! {count} has been detected near your position");
						Speak ($"The current humidity is {currentWeather.Humidity} percent and a temperature of {currentWeather.Temperature} degrees, with a wind of {currentWeather.Wind} meters per hour");
						alertDectected = true;
					}
				} else {
					if (alertDectected) {
						RedLed.Value = false;
						GreenLed.Value = true;
						alertDectected = false;
					}
				}
				Thread.Sleep (5000);
			}
		}
	}
}
