// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

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
