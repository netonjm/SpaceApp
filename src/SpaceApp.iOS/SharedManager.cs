using System.Net;
using Foundation;
using SpaceApp.Services;
using Xamarin.Forms;
using System.Linq;

//[assembly: Dependency(typeof(SharedManager))]
namespace SpaceApp.Services
{
	//class SharedManager : ISharedManager
	//{
	//	public string HostToIp(string host)
	//	{
	//		var hostEntry = Dns.GetHostEntry(host);
	//		var data = hostEntry.AddressList.Select(s => s.ToString()).Where(s => s.Contains("."));
	//		return data.ElementAt (0);
	//		//try
	//		//{
	//		//	// Host Name resolution to IP
	//		//	var hosts = Dns.GetHostEntry(host);
	//		//	IPAddress[]  = host.AddressList;
	//		//	// Loop through the IP Address array and add the IP address to Listbox
	//		//	foreach (IPAGddress addr in hosts.AddressList)
	//		//	{

	//		//	}
	//		//catch (System.Net.Sockets.SocketException ex)
	//		//{

	//		//}
	//		//catch (System.Exception ex)
	//		//{

	//		//}

	//	}
	//}
}
