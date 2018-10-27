using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SofiaApp.IoT
{
	public class BrowserJsonFormatter : JsonMediaTypeFormatter
	{
		public BrowserJsonFormatter ()
		{
			this.SupportedMediaTypes.Add (new MediaTypeHeaderValue ("text/html"));
			this.SerializerSettings.Formatting = Formatting.Indented;
		}

		public override void SetDefaultContentHeaders (Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			base.SetDefaultContentHeaders (type, headers, mediaType);
			headers.ContentType = new MediaTypeHeaderValue ("application/json");
		}
	}
	class Program
	{
		public static void Main (string [] args)
		{
			var port = "8085";
			var config = new HttpSelfHostConfiguration ($"http://localhost:{port}");

			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault (t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove (appXmlType);
			config.Formatters.Add (new BrowserJsonFormatter ());

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
