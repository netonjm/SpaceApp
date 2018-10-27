using System;
using NUnit.Framework;

namespace SofiaApp.Test
{
	public class DefaultTest
	{
		[Test ()]
		public void TestCase ()
		{
			var post = new WhereAreFiresApiArgs ();
			var response = WebApiHelper.GetWebApiResponse<WhereAreFiresResponse []> (post);
			Console.WriteLine ("");
		}
	}
}
