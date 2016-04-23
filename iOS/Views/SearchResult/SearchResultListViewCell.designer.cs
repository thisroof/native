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
	[Register ("SearchResultListViewCell")]
	partial class SearchResultListViewCell
	{
		[Outlet]
		MvvmCross.Binding.iOS.Views.MvxImageView img_property { get; set; }

		[Outlet]
		UIKit.UILabel lbl_price { get; set; }

		[Outlet]
		UIKit.UILabel lbl_title { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (img_property != null) {
				img_property.Dispose ();
				img_property = null;
			}

			if (lbl_price != null) {
				lbl_price.Dispose ();
				lbl_price = null;
			}

			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}
		}
	}
}
