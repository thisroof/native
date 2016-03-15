// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ThisRoofN.iOS
{
	[Register ("LocationSuggestionCell")]
	partial class LocationSuggestionCell
	{
		[Outlet]
		UIKit.UILabel lbl_address { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbl_address != null) {
				lbl_address.Dispose ();
				lbl_address = null;
			}
		}
	}
}
