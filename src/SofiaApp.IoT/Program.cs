using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using IoTSharp.Components;

namespace SofiaApp.IoT
{
	class Program
	{
		public static void Main (string [] args)
		{
			var dht = new DhtSensor (Connectors.GPIO4, DhtModel.Dht11);
			dht.Start ();

			SpaceService.GreenLed.Value = true;
			SpaceService.RedLed.Value = false;

			var port = "8085";
			var config = new HttpSelfHostConfiguration ($"http://localhost:{port}");
			config.MapHttpAttributeRoutes ();

			using (HttpSelfHostServer server = new HttpSelfHostServer (config)) {
				server.OpenAsync ().Wait ();
				Console.WriteLine ($"Started host server in {port}.");
				while (true) {
					Thread.Sleep (1000);
				}
			}
		}
	}
}
