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
