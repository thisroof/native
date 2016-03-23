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
	[Register ("HomeDetailModalView")]
	partial class HomeDetailModalView
	{
		[Outlet]
		UIKit.UIButton btn_modalBack { get; set; }

		[Outlet]
		UIKit.UITableView tbl_homeDetail { get; set; }

		[Outlet]
		UIKit.UIView view_rangeSliderHomeAge { get; set; }

		[Outlet]
		UIKit.UIView view_rangeSliderLotSize { get; set; }

		[Outlet]
		UIKit.UIView view_rangeSliderSquare { get; set; }

		[Outlet]
		UIKit.UIView view_tableHeader { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_modalBack != null) {
				btn_modalBack.Dispose ();
				btn_modalBack = null;
			}

			if (tbl_homeDetail != null) {
				tbl_homeDetail.Dispose ();
				tbl_homeDetail = null;
			}

			if (view_rangeSliderHomeAge != null) {
				view_rangeSliderHomeAge.Dispose ();
				view_rangeSliderHomeAge = null;
			}

			if (view_rangeSliderLotSize != null) {
				view_rangeSliderLotSize.Dispose ();
				view_rangeSliderLotSize = null;
			}

			if (view_rangeSliderSquare != null) {
				view_rangeSliderSquare.Dispose ();
				view_rangeSliderSquare = null;
			}

			if (view_tableHeader != null) {
				view_tableHeader.Dispose ();
				view_tableHeader = null;
			}
		}
	}
}
