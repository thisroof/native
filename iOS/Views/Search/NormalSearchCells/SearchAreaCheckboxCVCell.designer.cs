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
	[Register ("SearchAreaCheckboxCVCell")]
	partial class SearchAreaCheckboxCVCell
	{
		[Outlet]
		UIKit.UIImageView iv_check { get; set; }

		[Outlet]
		UIKit.UILabel lbl_title { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (iv_check != null) {
				iv_check.Dispose ();
				iv_check = null;
			}
			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}
		}
	}
}
