using System;
using System.Collections.Generic;
using System.Net;
using Renci.SshNet;
using System.Linq;

namespace Raspberry.Core
{
	public static class NetworkHelper
	{
		public static IEnumerable<string> HostnameToIp (string host)
		{
			var hostEntry = Dns.GetHostEntry(host);
			return hostEntry.AddressList.Select(s => s.ToString()).Where(s => s.Contains("."));
		}
	}

	public class Client : IDisposable
	{
		readonly string ip;
		readonly string user;
		readonly string password;
		readonly int port;

		SshClient client;

		public bool IsConnected
		{
			get
			{
				try
				{
					return client?.IsConnected ?? false;
				}
				catch (Exception)
				{
					return false;
				}
			}
		}

		public ClientCommand Commands;

		public Client (string ip, string user, string password, int port = 22)
		{
			this.ip = ip; this.user = user; this.password = password; this.port = port;
			client = new SshClient(ip, port, user, password);
			client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(2);

			Commands = new ClientCommand(this);
		}

		public string Command (string command)
		{
			TryConnect();

			if (!client.IsConnected)
			{
				return "";
			}

			// Connection ok? Then continue, else display error
			try
			{
				SshCommand x = client.RunCommand(command);
				return x.Result.Trim();
			}
			catch (Exception ex)
			{
				return "";
			}
		}

		public Client TryConnect ()
		{
			if (client.IsConnected)
			{
				return this;
			}
			try
			{
				client.Connect();
			}
			catch (Exception)
			{
			}

			return this;
		}

		public void Dispose()
		{
			try
			{
				client.Disconnect();
				client.Dispose();
			}
			catch (Exception)
			{

			}

		}

		public class ClientCommand
		{
			readonly Client client;

			public ClientCommand(Client client)
			{
				this.client = client;
			}

			public void Reset()
			{
				client.Command("sudo reboot");
			}

			public void Shutdown()
			{
				client.Command("sudo shutdown -h now");
			}

		}
	}
}
