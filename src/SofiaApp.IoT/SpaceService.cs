using System.Diagnostics;
using IoTSharp.Components;

namespace SofiaApp.IoT
{
	public static class SpaceService
	{
		public static IoTPin GreenLed, RedLed;

		static SpaceService ()
		{
			GreenLed = new IoTPin (Connectors.GPIO27);
			GreenLed.SetDirection (IoTPinDirection.DirectionOutInitiallyLow);
			RedLed = new IoTPin (Connectors.GPIO22);
			RedLed.SetDirection (IoTPinDirection.DirectionOutInitiallyLow);
		}

		public static string Speak (string text)
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
	}
}
