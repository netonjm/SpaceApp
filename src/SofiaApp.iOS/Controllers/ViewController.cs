using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace SofiaApp.iOS
{
	public partial class ViewController : UIViewController
	{
		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			SofiaClientEnvirontment.Current.Start ();
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
