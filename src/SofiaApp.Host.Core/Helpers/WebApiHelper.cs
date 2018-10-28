using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SofiaApp.Host.Entities;

namespace SofiaApp.Helpers
{
	public static class WebApiHelper
	{
		public static T GetNasaWebApiResponse<T> (ApiArgs args) => GetWebApiResponse <T> ("https://hawking.sv.cmu.edu:9016/opennex/getAPIResult", "jmedranojimenez@hotmail.com", "logitech", args);

		public static T GetWebApiResponse<T> (string url, string username, string password, ApiArgs args)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create (url);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			if (!string.IsNullOrEmpty (username)){
				// Create NetworkCredential Object 
				var admin_auth = new NetworkCredential (username, password);

				// Set your HTTP credentials in your request header
				httpWebRequest.Credentials = admin_auth;
			} 

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
	}
}
