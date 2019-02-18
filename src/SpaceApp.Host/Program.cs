using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using IoTSharp.Components;

namespace SpaceApp.Host
{
	class Program
	{
		static CancellationTokenSource cts;
		static TaskCompletionSource<object> processingCompletion = new TaskCompletionSource<object> ();

		public static void Main (string [] args)
		{
			//var dht = new DhtSensor (Connectors.GPIO4, DhtModel.Dht11);
			//dht.Start ();
			bool finished = false;
			var rask = Task.Run (() =>{

				for (int i = 0; i < 5000 && !finished; i++) {
					SpaceService.GreenLed.Value = i%2 == 0;
					SpaceService.RedLed.Value = i%3 ==0;
					Thread.Sleep (300);
				}
				processingCompletion.TrySetResult (null);
			});
		
			SpaceService.Speak ("Miiiiiira Sooooofíaaaaa. Siiin tu mirada, sigo. Siiin tu mirada, sigo. Miiiiiira Sooooofíaaa a a. Coooomo te mira, dime. Coooomo te mira, dime");
			finished = true;
			processingCompletion.Task.Wait ();
			//SpaceService.Speak ($"La humedad actual es de 86 por cien y una temperatura de 28 grados centigrados, con un viento de siete coma uno metros por hora");

			//var port = "8085";
			//var config = new HttpSelfHostConfiguration ($"http://localhost:{port}");
			//config.MapHttpAttributeRoutes ();

			//using (HttpSelfHostServer server = new HttpSelfHostServer (config)) {
			//	server.OpenAsync ().Wait ();
			//	Console.WriteLine ($"Started host server in {port}.");
			//	while (true) {
			//		Thread.Sleep (1000);
			//	}
			//}
		}
	}
}
