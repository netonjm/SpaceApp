using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace SofiaApp.IoT
{

	public class AlertServiceController : ApiController
	{
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
