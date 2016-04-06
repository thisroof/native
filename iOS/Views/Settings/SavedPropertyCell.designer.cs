// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;
using MvvmCross.Binding.iOS.Views;

namespace ThisRoofN.iOS
{
	[Register ("SavedPropertyCell")]
	partial class SavedPropertyCell
	{
		[Outlet]
		MvxImageView img_primary { get; set; }

		[Outlet]
		UIKit.UILabel lbl_address { get; set; }

		[Outlet]
		UIKit.UILabel lbl_cityStateZip { get; set; }

		[Outlet]
		UIKit.UILabel lbl_price { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (img_primary != null) {
				img_primary.Dispose ();
				img_primary = null;
			}

			if (lbl_price != null) {
				lbl_price.Dispose ();
				lbl_price = null;
			}

			if (lbl_address != null) {
				lbl_address.Dispose ();
				lbl_address = null;
			}

			if (lbl_cityStateZip != null) {
				lbl_cityStateZip.Dispose ();
				lbl_cityStateZip = null;
			}
		}
	}
}
