// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SpaceAppNative.iOS
{
    [Register ("SpotViewController")]
    partial class SpotViewController
    {
        [Outlet]
        UIKit.UIImageView backBackgroundImage { get; set; }


        [Outlet]
        UIKit.UIImageView imageSelected { get; set; }


        [Outlet]
        UIKit.UIButton noButton { get; set; }


        [Outlet]
        UIKit.UIImageView noImage { get; set; }


        [Outlet]
        UIKit.UILabel noSpotsNearDetected { get; set; }


        [Outlet]
        UIKit.UILabel noTextField { get; set; }


        [Outlet]
        UIKit.UIImageView spotTheFireImage { get; set; }


        [Outlet]
        UIKit.UILabel yesButton { get; set; }


        [Outlet]
        UIKit.UIImageView yesImage { get; set; }


        [Outlet]
        UIKit.UILabel yesTextField { get; set; }


        [Action ("noButtonAction:")]
        partial void noButtonAction (Foundation.NSObject sender);


        [Action ("yesButtonAction:")]
        partial void yesButtonAction (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (backBackgroundImage != null) {
                backBackgroundImage.Dispose ();
                backBackgroundImage = null;
            }

            if (imageSelected != null) {
                imageSelected.Dispose ();
                imageSelected = null;
            }

            if (noButton != null) {
                noButton.Dispose ();
                noButton = null;
            }

            if (noImage != null) {
                noImage.Dispose ();
                noImage = null;
            }

            if (noSpotsNearDetected != null) {
                noSpotsNearDetected.Dispose ();
                noSpotsNearDetected = null;
            }

            if (noTextField != null) {
                noTextField.Dispose ();
                noTextField = null;
            }

            if (spotTheFireImage != null) {
                spotTheFireImage.Dispose ();
                spotTheFireImage = null;
            }

            if (yesButton != null) {
                yesButton.Dispose ();
                yesButton = null;
            }

            if (yesImage != null) {
                yesImage.Dispose ();
                yesImage = null;
            }

            if (yesTextField != null) {
                yesTextField.Dispose ();
                yesTextField = null;
            }
        }
    }
}