

//using System;
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
		}
	}
}
