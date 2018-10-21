// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SpaceAppNative.iOS
{
	[Register ("MapViewController")]
	partial class MapViewController
	{
		[Outlet]
		SpaceAppNative.iOS.MapView mapViewContent { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mapViewContent != null) {
				mapViewContent.Dispose ();
				mapViewContent = null;
			}
		}
	}
}
