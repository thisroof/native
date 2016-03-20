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
		UIKit.UILabel lbl_homeAge { get; set; }

		[Outlet]
		UIKit.UILabel lbl_lotSize { get; set; }

		[Outlet]
		UIKit.UILabel lbl_squareFootage { get; set; }

		[Outlet]
		UIKit.UISlider slider_lotSize { get; set; }

		[Outlet]
		UIKit.UISlider slider_squareFootage { get; set; }

		[Outlet]
		UIKit.UITableView tbl_homeDetail { get; set; }

		[Outlet]
		UIKit.UIView view_rangeSliderHomeAge { get; set; }

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

			if (view_tableHeader != null) {
				view_tableHeader.Dispose ();
				view_tableHeader = null;
			}

			if (slider_squareFootage != null) {
				slider_squareFootage.Dispose ();
				slider_squareFootage = null;
			}

			if (lbl_squareFootage != null) {
				lbl_squareFootage.Dispose ();
				lbl_squareFootage = null;
			}

			if (view_rangeSliderHomeAge != null) {
				view_rangeSliderHomeAge.Dispose ();
				view_rangeSliderHomeAge = null;
			}

			if (lbl_homeAge != null) {
				lbl_homeAge.Dispose ();
				lbl_homeAge = null;
			}

			if (slider_lotSize != null) {
				slider_lotSize.Dispose ();
				slider_lotSize = null;
			}

			if (lbl_lotSize != null) {
				lbl_lotSize.Dispose ();
				lbl_lotSize = null;
			}
		}
	}
}
