using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IoTSharp.Components;

namespace SofiaApp.IoT
{
	class SofiaIoTEnvirontment
	{
		public static IoTPin GreenLed, RedLed;

		CancellationTokenSource cancellationTokenSource;

		SofiaIoTEnvirontment ()
		{
			GreenLed = new IoTPin (Connectors.GPIO27);
			GreenLed.SetDirection (IoTPinDirection.DirectionOutInitiallyLow);
			RedLed = new IoTPin (Connectors.GPIO22);
			RedLed.SetDirection (IoTPinDirection.DirectionOutInitiallyLow);
			cancellationTokenSource = new CancellationTokenSource ();
			Task.Run (() => {
				while (!cancellationTokenSource.IsCancellationRequested) {


					Thread.Sleep (5000);
				}
			}, cancellationTokenSource.Token);
		}

		public string Speak (string text)
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

		readonly public static SofiaIoTEnvirontment Current = new SofiaIoTEnvirontment ();
	}
}
