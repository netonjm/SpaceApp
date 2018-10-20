using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using SpaceApp.Host.Core;

namespace SpaceApp.Host
{
	public class BookServiceController : ApiController
	{
		readonly List<Poi> data = new List<Poi> ();

		public BookServiceController ()
		{
			data.Add (new Poi { Id = 1, Name = "Test1" });
			data.Add (new Poi { Id = 2, Name = "Test2" });
		}

		[Route ("api/books")]
		public IEnumerable<Poi> GetBooks ()
		{
			return data;
		}

		[Route ("api/books/{id:int}")]
		public Poi GetBook (int id)
		{
			return data.FirstOrDefault (s => s.Id == id);
		}

		[Route ("api/books")]
		[HttpPost]
		public HttpResponseMessage CreateBook (Poi book)
		{
			data.Add (book);
			return new HttpResponseMessage (HttpStatusCode.OK);
		}
	}
}
