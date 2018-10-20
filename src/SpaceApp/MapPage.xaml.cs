using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SpaceApp
{
	public partial class MapPage : ContentPage
	{
		Map map;
		private Position _position;
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			GetPosition ();
			map.MoveToRegion (MapSpan.FromCenterAndRadius (
			new Position (_position.Latitude, _position.Longitude), Distance.FromMiles (3))); // Santa Cruz golf course

		}
		public async void GetPosition ()
		{
			Plugin.Geolocator.Abstractions.Position position = null;
			try {
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 100;

				position = await locator.GetLastKnownLocationAsync ();

				if (position != null) {
					_position = new Position (position.Latitude, position.Longitude);
					//got a cahched position, so let's use it.
					return;
				}

				if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled) {
					//not available or enabled
					return;
				}

				position = await locator.GetPositionAsync (TimeSpan.FromSeconds (20), null, true);

			} catch (Exception ex) {
				throw ex;
				//Display error as we have timed out or can't get location.
			}
			_position = new Position (position.Latitude, position.Longitude);
			if (position == null)
				return;

		}

	public bool IsLocationAvailable ()
		{
			if (!CrossGeolocator.IsSupported)
				return false;

			return CrossGeolocator.Current.IsGeolocationAvailable;
		}

		public MapPage ()
		{
			InitializeComponent ();

			map = new Map {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			map.MoveToRegion (MapSpan.FromCenterAndRadius (
				new Position (36.9628066, -122.0194722), Distance.FromMiles (3))); // Santa Cruz golf course

			var position = new Position (36.9628066, -122.0194722); // Latitude, Longitude
			var pin = new Pin {
				Type = PinType.Generic,
				Position = position,
				Label = "Santa Cruz",
				Address = "custom detail info"
			};
			map.Pins.Add (pin);

			// create buttons
			var morePins = new Button { Text = "Add more pins" };
			morePins.Clicked += (sender, e) => {
				map.Pins.Add (new Pin {
					Position = new Position (36.9641949, -122.0177232),
					Label = "Boardwalk"
				});
				map.Pins.Add (new Pin {
					Position = new Position (36.9571571, -122.0173544),
					Label = "Wharf"
				});
				map.MoveToRegion (MapSpan.FromCenterAndRadius (
					new Position (36.9628066, -122.0194722), Distance.FromMiles (1.5)));

			};
			var reLocate = new Button { Text = "Re-center" };
			reLocate.Clicked += (sender, e) => {
				map.MoveToRegion (MapSpan.FromCenterAndRadius (
					new Position (36.9628066, -122.0194722), Distance.FromMiles (3)));
			};
			var buttons = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = {
					morePins, reLocate
				}
			};

			// put the page together
			Content = new StackLayout {
				Spacing = 0,
				Children = {
					map,
					buttons
				}
			};
		}
	}
}
