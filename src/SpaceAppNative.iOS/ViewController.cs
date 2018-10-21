using System;
using Foundation;
using UIKit;

namespace SpaceAppNative.iOS
{
	public static class StaticHelper
	{
		public static void ShowDialog (UIViewController controller)
		{
			var actionSheetAlert = UIAlertController.Create ("Important", "You are going  to displatch a Fire Alarm near you. Are you sure", UIAlertControllerStyle.ActionSheet);
			actionSheetAlert.AddAction (UIAlertAction.Create ("YES", UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item One pressed.")));
			actionSheetAlert.AddAction (UIAlertAction.Create ("NO", UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel button pressed.")));
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
