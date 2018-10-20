using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace SpaceApp
{
	public partial class Overview : ContentPage
	{
		MainTabbedPage navPage;

		async void Handle_Clicked (object sender, System.EventArgs e)
		{
			if (await DisplayAlert ("Important", "You are going  to displatch a Fire Alarm near you. Are you sure", "YES", "NO")) {
				PoiService.Send ("");
			}
		}

		public Overview ()
		{
			InitializeComponent ();
		}

		ImageSource GetFromCloudy (Weather weather) {
			return ImageSource.FromFile ("cloud.png");
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			navPage = new MainTabbedPage ();

			var currentWeather = App.CurrentWeather;
			locationLabel.Text = DateTime.Now.ToString ("dddd, dd MMMM yyyy"); //fecha
			cloudLabel.Text = currentWeather.Temperature; //temperature
			cloudImage.Source = GetFromCloudy (currentWeather) ; //clouds
			locationLabel.Text = currentWeather.Title; //location

			//Console.WriteLine ("");
			signUpButton.Pressed += SignUpButton_Pressed;
			signTwitterButton.Pressed += SignUpButton_Pressed;
			signFacebookButton.Pressed += SignUpButton_Pressed;
		}

		void SignUpButton_Pressed (object sender, EventArgs e)
		{
			navPage.Parent = null;
			Navigation.PushAsync (navPage);
		}
	}
}
