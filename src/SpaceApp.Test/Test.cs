using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
namespace SpaceApp.Test
{
	[TestFixture ()]
	public class Test
	{
		public class ApiArgs
		{
			public int serviceId { get; private set; } = 25;
			public int userId { get; protected set; } = 42;
			public string purpose { get; protected set; } = "Test app";

			public ApiArgs (int serviceId)
			{
				this.serviceId = serviceId;
			}
		}

		public class WhereAreFiresApiArgs : ApiArgs
		{
			public WhereAreFiresApiArgs () : base (25)
			{

			}

			public int date1 { get; set; } = 2018012;
			public int date2 { get; set; } = 2018020;
			public int ulx { get; set; } = -120;
			public int uly { get; set; } = 25;
			public int lrx { get; set; } = -98;
			public int lry { get; set; } = 20;
		}

		public T GetWebApiResponse<T> (ApiArgs args)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create ("https://hawking.sv.cmu.edu:9016/opennex/getAPIResult");
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			// Create NetworkCredential Object 
			NetworkCredential admin_auth = new NetworkCredential ("jmedranojimenez@hotmail.com", "logitech");

			// Set your HTTP credentials in your request header
			httpWebRequest.Credentials = admin_auth;

			// callback for handling server certificates
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

			string json = JsonConvert.SerializeObject (args);

			using (var streamWriter = new StreamWriter (httpWebRequest.GetRequestStream ())) {
				streamWriter.Write (json);
				streamWriter.Flush ();
				streamWriter.Close ();
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse ();
				using (var streamReader = new StreamReader (httpResponse.GetResponseStream ())) {
					var result = streamReader.ReadToEnd ();

					return JsonConvert.DeserializeObject<T> (result);
				}
			}
		}

		[Test ()]
		public void TestCase ()
		{
			var post = new WhereAreFiresApiArgs ();
			var response = GetWebApiResponse<WhereAreFiresResponse[]> (post);
			Console.WriteLine ("");
		}

		public class WhereAreFiresResponse
		{
			public float lon { get; set; }
			public float lat { get; set; }
			public float temp4 { get; set; }
			public float temp11 { get; set; }
			public int temp { get; set; }
			public float size { get; set; }
			public int ecosys { get; set; }
			public int flag { get; set; }
			public int sat { get; set; }
			public int yd { get; set; }
			public int time { get; set; }
			public string datetime { get; set; }
		}

		string GetJson (string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create (url);
			request.Method = "GET";
			request.ContentType = @"application/json";
			long length = 0;
			try {
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse ()) {
					length = response.ContentLength;
					var enc = Encoding.ASCII;
					using (var reader = new System.IO.StreamReader (response.GetResponseStream (), enc)) {
						string responseText = reader.ReadToEnd ();
						return responseText;
					}
				}
			} catch (WebException ex) {
				Console.WriteLine ("");
			}
			return "";
		}
	}
}
