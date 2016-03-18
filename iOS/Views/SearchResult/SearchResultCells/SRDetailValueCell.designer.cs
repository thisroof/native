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
	[Register ("SRDetailValueCell")]
	partial class SRDetailValueCell
	{
		[Outlet]
		UIKit.UILabel lbl_address { get; set; }

		[Outlet]
		UIKit.UILabel lbl_ba { get; set; }

		[Outlet]
		UIKit.UILabel lbl_bd { get; set; }

		[Outlet]
		UIKit.UILabel lbl_lotSqft { get; set; }

		[Outlet]
		UIKit.UILabel lbl_price { get; set; }

		[Outlet]
		UIKit.UILabel lbl_sqft { get; set; }

		[Outlet]
		UIKit.UILabel lbl_street { get; set; }

		[Outlet]
		UIKit.UIView view_contact { get; set; }

		[Outlet]
		UIKit.UIView view_findAgent { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lbl_address != null) {
				lbl_address.Dispose ();
				lbl_address = null;
			}
			if (lbl_ba != null) {
				lbl_ba.Dispose ();
				lbl_ba = null;
			}
			if (lbl_bd != null) {
				lbl_bd.Dispose ();
				lbl_bd = null;
			}
			if (lbl_lotSqft != null) {
				lbl_lotSqft.Dispose ();
				lbl_lotSqft = null;
			}
			if (lbl_price != null) {
				lbl_price.Dispose ();
				lbl_price = null;
			}
			if (lbl_sqft != null) {
				lbl_sqft.Dispose ();
				lbl_sqft = null;
			}
			if (lbl_street != null) {
				lbl_street.Dispose ();
				lbl_street = null;
			}
			if (view_contact != null) {
				view_contact.Dispose ();
				view_contact = null;
			}
			if (view_findAgent != null) {
				view_findAgent.Dispose ();
				view_findAgent = null;
			}
		}
	}
}
