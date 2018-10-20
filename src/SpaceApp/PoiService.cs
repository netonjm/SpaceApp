using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpaceApp
{
	public class PoiService
	{

		//public List<(string host, Client connection, Color color)> Devices = new List<(string host, Client connection, Color color)> ();

		public PoiService ()
		{
			//comedor = new Client (RaspiComedor, credentials.user, credentials.password)
			//	.TryConnect ();

			//Devices.Add (("comedor", comedor, Color.Accent));

			//wc1 = new Client (RaspiWc1, credentials.user, credentials.password)
			//	.TryConnect ();
			//Devices.Add (("wc1", wc1, Color.Accent));

			//despacho = new Client (RaspiDespacho, credentials.user, credentials.password)
			//	.TryConnect ();
			//Devices.Add (("despacho", despacho, Color.Accent));
		}

		internal static void Send (string v)
		{
			throw new NotImplementedException ();
		}
	}
}
