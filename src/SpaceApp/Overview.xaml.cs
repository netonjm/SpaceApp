using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SpaceApp
{
	public partial class Overview : ContentPage
	{
		async void Handle_Clicked (object sender, System.EventArgs e)
		{
			if (await DisplayAlert ("Important", "You are going  to displatch a Fire Alarm near you. Are you sure", "YES", "NO")) {

			}
		}

		public Overview ()
		{
			InitializeComponent ();
		}
	}
}
