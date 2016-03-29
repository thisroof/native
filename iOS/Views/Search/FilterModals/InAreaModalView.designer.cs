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
	[Register ("InAreaModalView")]
	partial class InAreaModalView
	{
		[Outlet]
		UIKit.UIButton btn_modalBack { get; set; }

		[Outlet]
		UIKit.UITableView tbl_inAreaItems { get; set; }

		[Outlet]
		UIKit.UIView view_back { get; set; }

		[Outlet]
		UIKit.UIView view_side { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_modalBack != null) {
				btn_modalBack.Dispose ();
				btn_modalBack = null;
			}

			if (tbl_inAreaItems != null) {
				tbl_inAreaItems.Dispose ();
				tbl_inAreaItems = null;
			}

			if (view_back != null) {
				view_back.Dispose ();
				view_back = null;
			}

			if (view_side != null) {
				view_side.Dispose ();
				view_side = null;
			}
		}
	}
}
