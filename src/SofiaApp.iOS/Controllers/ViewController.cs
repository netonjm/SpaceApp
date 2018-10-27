using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace SofiaApp.iOS
{
	public static class StaticHelper
	{

		static async Task FetchUrl (string url)
		{
			try {
				// Create an HTTP web request using the URL:
				var request = (HttpWebRequest)WebRequest.Create (new Uri (url));
				request.ContentType = "application/json";
				request.Method = "GET";

				// Send the request to the server and wait for the response:
				using (WebResponse response = await request.GetResponseAsync ()) {
					// Get a stream representation of the HTTP web response:
					using (Stream stream = response.GetResponseStream ()) {
						// Use this stream to build a JSON document object:
					}
				}
			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}
		}


		static string SofiaIp = "192.168.2.5"; //192.168.2.5 //192.168.130.72
		public static void CallSofia ()
		{
			FetchUrl ($"http://{SofiaIp}:8085/sofia/event");
		}

		public static void StopSofia ()
		{
			FetchUrl ($"http://{SofiaIp}:8085/sofia/stopevent");
		}

		public static void ShowDialog (UIViewController controller)
		{
			var actionSheetAlert = UIAlertController.Create ("Important", "You are going  to displatch a Fire Alarm near you. Are you sure", UIAlertControllerStyle.ActionSheet);
			actionSheetAlert.AddAction (UIAlertAction.Create ("YES", UIAlertActionStyle.Default, (action) => CallSofia ()));
			actionSheetAlert.AddAction (UIAlertAction.Create ("NO", UIAlertActionStyle.Cancel, (action) => StopSofia ()));
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null) {
				presentationPopover.SourceView = controller.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}
			controller.PresentViewController (actionSheetAlert, true, null);
		}
	}

	public partial class ViewController : UIViewController
	{
		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		partial void btnPress (NSObject sender)
		{
			StaticHelper.ShowDialog (this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
