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
	[Register ("SRDetailTitleCell")]
	partial class SRDetailTitleCell
	{
		[Outlet]
		UIKit.UIImageView img_arrowMore { get; set; }

		[Outlet]
		MvvmCross.Binding.iOS.Views.MvxImageView img_major { get; set; }

		[Outlet]
		UIKit.UILabel lbl_acre { get; set; }

		[Outlet]
		UIKit.UILabel lbl_address { get; set; }

		[Outlet]
		UIKit.UILabel lbl_bedBath { get; set; }

		[Outlet]
		UIKit.UILabel lbl_budget { get; set; }

		[Outlet]
		UIKit.UILabel lbl_sqft { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (img_arrowMore != null) {
				img_arrowMore.Dispose ();
				img_arrowMore = null;
			}
			if (img_major != null) {
				img_major.Dispose ();
				img_major = null;
			}
			if (lbl_acre != null) {
				lbl_acre.Dispose ();
				lbl_acre = null;
			}
			if (lbl_address != null) {
				lbl_address.Dispose ();
				lbl_address = null;
			}
			if (lbl_bedBath != null) {
				lbl_bedBath.Dispose ();
				lbl_bedBath = null;
			}
			if (lbl_budget != null) {
				lbl_budget.Dispose ();
				lbl_budget = null;
			}
			if (lbl_sqft != null) {
				lbl_sqft.Dispose ();
				lbl_sqft = null;
			}
		}
	}
}
