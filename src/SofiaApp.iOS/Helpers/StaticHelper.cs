using System;
using SofiaApp.iOS.Services;
using UIKit;

namespace SofiaApp.iOS
{
	public static class StaticHelper
	{
		public static void SharePossibleFirePoint ()
		{
			var user = SofiaClientEnvirontment.Current.UserGuid;
			var currentPosition = SofiaClientEnvirontment.Current.DeviceLocation;
			var inserted = WebServices.Sofia.SetFirePoint (currentPosition, user);
			Console.WriteLine (inserted);
		}

		public static void ShowDialog (UIViewController controller)
		{
			var actionSheetAlert = UIAlertController.Create ("Important", "You are going  to displatch a Fire Alarm near you. Are you sure", UIAlertControllerStyle.ActionSheet);
			actionSheetAlert.AddAction (UIAlertAction.Create ("YES", UIAlertActionStyle.Default, (action) => SharePossibleFirePoint ()));
			actionSheetAlert.AddAction (UIAlertAction.Create ("NO", UIAlertActionStyle.Cancel, null));
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null) {
				presentationPopover.SourceView = controller.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}
			controller.PresentViewController (actionSheetAlert, true, null);
		}
	}
}
