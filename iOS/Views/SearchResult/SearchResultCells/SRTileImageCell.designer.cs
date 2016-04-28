// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ThisRoofN.iOS
{
	[Register ("SRTileImageCell")]
	partial class SRTileImageCell
	{
		[Outlet]
		MvvmCross.Binding.iOS.Views.MvxImageView img_item { get; set; }

		[Outlet]
		UIKit.UILabel lbl_itemPrice { get; set; }

		[Outlet]
		UIKit.UILabel lbl_title { get; set; }

		[Outlet]
		UIKit.UIView view_titleBack { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (img_item != null) {
				img_item.Dispose ();
				img_item = null;
			}

			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}

			if (lbl_itemPrice != null) {
				lbl_itemPrice.Dispose ();
				lbl_itemPrice = null;
			}

			if (view_titleBack != null) {
				view_titleBack.Dispose ();
				view_titleBack = null;
			}
		}
	}
}
