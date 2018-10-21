using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using SpaceApp.Host.Core;
using System.Diagnostics;

namespace SpaceApp.Host
{
	public class ProcessHelper
	{	
		public void StartProcess() {
			var proc = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = "program.exe",
					Arguments = "command line arguments to your executable",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
		}
	}

	public class BookServiceController : ApiController
	{
		[Route ("api/books")]
		[HttpPost]
		public HttpResponseMessage CreateCall (Poi book)
		{
			data.Add (book);
			return new HttpResponseMessage (HttpStatusCode.OK);
		}
	}
}
