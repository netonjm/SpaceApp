using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpaceApp
{
	public partial class App : Application
	{
		public static IWeatherService WeatherService;
		public static GpsService GpsService;

		public static Weather CurrentWeather;

		public App ()
		{
		}

		public App (IWeatherService service)
		{
			InitializeComponent ();



			MainPage = new NavigationPage (new Overview ());
			//GpsService = new GpsService ();
			WeatherService = service;
			CurrentWeather = service.GetWeather ("46920", "es", "metric");
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
