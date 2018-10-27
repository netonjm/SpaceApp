using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using SofiaApp.Host.Core;
using System.Diagnostics;
using System.IO;
using IoTSharp.Components;

namespace SofiaApp.Host
{
	public static class SpaceService
	{
		public static IoTPin GreenLed, RedLed;

		static SpaceService () {
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

	public class AlertServiceController : ApiController
	{

		public AlertServiceController () {

		}

		[Route ("sofia/event")]
		public HttpResponseMessage GetActionEvent ()
		{
			Console.WriteLine ($"Client has sended an event");
			SpaceService.RedLed.Value = true;
			SpaceService.GreenLed.Value = false;
			SpaceService.Speak ("Atención! Se ha detectado un incendio cerca de tu posición, en la zona de Torrente");
			SpaceService.Speak ($"La humedad actual es de 86 por cien y una temperatura de 28 grados centigrados, con un viento de siete coma uno metros por hora");
			return new HttpResponseMessage (HttpStatusCode.OK);
		}

		[Route ("sofia/stopevent")]
		public HttpResponseMessage GetActionStop ()
		{
			Console.WriteLine ($"Client has stopped");
			SpaceService.RedLed.Value = false;
			SpaceService.GreenLed.Value = true;
			return new HttpResponseMessage (HttpStatusCode.OK);
		}
	}
}
