using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceApp.Services;
using Xamarin.Forms;

namespace SpaceApp
{
	public class BlankPage : ContentPage
	{

	}

	public partial class MainPage : ContentPage
	{

		PoiService clientService;

		Color comedorBackgroundColor = Color.Red;
		public Color ComedorBackgroundColor
		{
			get => comedorBackgroundColor;
			set
			{
				if (comedorBackgroundColor != value)
				{
					return;
				}
				comedorBackgroundColor = value;
				OnPropertyChanged();
			}
		}


		Color wc1BackgroundColor = Color.Red;
		public Color WC1BackgroundColor
		{
			get => wc1BackgroundColor;
			set
			{
				if (wc1BackgroundColor == value)
				{
					return;
				}
				wc1BackgroundColor = value;

				OnPropertyChanged();
			}
		} 

		Color despachoBackgroundColor = Color.Red;
		public Color DespachoBackgroundColor
		{
			get => despachoBackgroundColor;
			set
			{
				if (despachoBackgroundColor == value)
				{
					return;
				}
				despachoBackgroundColor = value;
				OnPropertyChanged();
			}
		}

	
		public MainPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing   ()
		{
			base.OnAppearing ();
			clientService = App.ClientService;
			RefreshButtons ();

			//Device.StartTimer (TimeSpan.FromSeconds(10), () =>
			//{
			//	RefreshButtons();
			//	return true;
			//});
		} 

		void RefreshButtons ()
		{
			//ComedorBackgroundColor = RefreshConnection(clientService.Comedor);
			//WC1BackgroundColor = RefreshConnection(clientService.Wc1);
			//DespachoBackgroundColor = RefreshConnection(clientService.Despacho);
		}

		//Color RefreshConnection (Client client)
		//{
		//	if (client.IsConnected)
		//	{
		//		return Color.Green;
		//	}
		//	return Color.Red;
		//}

		void BtnWC1_Clicked(object sender, System.EventArgs e)
		{
			//var button = (Button)sender;
			//var device = clientService.Devices.FirstOrDefault(s => s.host == button.Text.ToLower());
			//device.connection.Commands.Reset();
		}

	}
}
