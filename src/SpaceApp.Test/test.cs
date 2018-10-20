using System;
using NUnit.Framework;
using Raspberry.Core;
using System.Linq;

namespace Raspberry.Test
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void EchoResponse()
		{
			var client = new Client("10.67.1.190", "pi", "raspberry");
			client.TryConnect();
			string msg = "hola";
			var response = client.Command($"echo {msg}");
			Assert.AreEqual(msg, response);
		}

		[Test()]
		public void HostToIp()
		{
			var ip = "10.67.1.190";
			var response = NetworkHelper.HostnameToIp("raspi-wc1.local").FirstOrDefault();
			Assert.AreEqual(ip, response);
		}
	}
}
