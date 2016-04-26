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
	[Register ("SearchCVCell")]
	partial class SearchCVCell
	{
		[Outlet]
		UIKit.UIImageView iv_item { get; set; }

		[Outlet]
		UIKit.UILabel lbl_title { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (iv_item != null) {
				iv_item.Dispose ();
				iv_item = null;
			}

			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}
		}
	}
}
