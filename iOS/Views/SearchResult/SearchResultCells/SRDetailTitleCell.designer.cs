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
	[Register ("SRDetailTitleCell")]
	partial class SRDetailTitleCell
	{
		[Outlet]
		UIKit.UIImageView img_arrowMore { get; set; }

		[Outlet]
		UIKit.UIImageView img_result { get; set; }

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
			if (img_result != null) {
				img_result.Dispose ();
				img_result = null;
			}

			if (lbl_budget != null) {
				lbl_budget.Dispose ();
				lbl_budget = null;
			}

			if (lbl_address != null) {
				lbl_address.Dispose ();
				lbl_address = null;
			}

			if (lbl_bedBath != null) {
				lbl_bedBath.Dispose ();
				lbl_bedBath = null;
			}

			if (lbl_sqft != null) {
				lbl_sqft.Dispose ();
				lbl_sqft = null;
			}

			if (lbl_acre != null) {
				lbl_acre.Dispose ();
				lbl_acre = null;
			}

			if (img_arrowMore != null) {
				img_arrowMore.Dispose ();
				img_arrowMore = null;
			}
		}
	}
}
