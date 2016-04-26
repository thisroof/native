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
	[Register ("SearchCVHeader")]
	partial class SearchCVHeader
	{
		[Outlet]
		UIKit.UIButton btn_sortBy { get; set; }

		[Outlet]
		UIKit.UITextField txt_sortBy { get; set; }

		[Outlet]
		UIKit.UIView view_priceRange { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (view_priceRange != null) {
				view_priceRange.Dispose ();
				view_priceRange = null;
			}

			if (txt_sortBy != null) {
				txt_sortBy.Dispose ();
				txt_sortBy = null;
			}

			if (btn_sortBy != null) {
				btn_sortBy.Dispose ();
				btn_sortBy = null;
			}
		}
	}
}
