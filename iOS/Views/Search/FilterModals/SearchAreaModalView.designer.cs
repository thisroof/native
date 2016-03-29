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
	[Register ("SearchAreaModalView")]
	partial class SearchAreaModalView
	{
		[Outlet]
		UIKit.UIButton btn_clearAll { get; set; }

		[Outlet]
		UIKit.UIButton btn_getOwnLocation { get; set; }

		[Outlet]
		UIKit.UIButton btn_modalBack { get; set; }

		[Outlet]
		UIKit.UIButton btn_selectAll { get; set; }

		[Outlet]
		UIKit.UICollectionView cv_nations { get; set; }

		[Outlet]
		UIKit.UIImageView img_title { get; set; }

		[Outlet]
		UIKit.UILabel lbl_distanceRange { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_areaType { get; set; }

		[Outlet]
		UIKit.UISlider slider_distance { get; set; }

		[Outlet]
		UIKit.UITableView tbl_commuteItems { get; set; }

		[Outlet]
		UIKit.UITableView tbl_suggestion { get; set; }

		[Outlet]
		UIKit.UITextField txt_address { get; set; }

		[Outlet]
		UIKit.UIView view_addressBack { get; set; }

		[Outlet]
		UIKit.UIView view_back { get; set; }

		[Outlet]
		UIKit.UIView view_distance { get; set; }

		[Outlet]
		UIKit.UIView view_nationWide { get; set; }

		[Outlet]
		UIKit.UIView view_suggestionBack { get; set; }

		[Outlet]
		UIKit.UIView view_trans { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_clearAll != null) {
				btn_clearAll.Dispose ();
				btn_clearAll = null;
			}

			if (btn_getOwnLocation != null) {
				btn_getOwnLocation.Dispose ();
				btn_getOwnLocation = null;
			}

			if (btn_modalBack != null) {
				btn_modalBack.Dispose ();
				btn_modalBack = null;
			}

			if (btn_selectAll != null) {
				btn_selectAll.Dispose ();
				btn_selectAll = null;
			}

			if (cv_nations != null) {
				cv_nations.Dispose ();
				cv_nations = null;
			}

			if (img_title != null) {
				img_title.Dispose ();
				img_title = null;
			}

			if (lbl_distanceRange != null) {
				lbl_distanceRange.Dispose ();
				lbl_distanceRange = null;
			}

			if (seg_areaType != null) {
				seg_areaType.Dispose ();
				seg_areaType = null;
			}

			if (slider_distance != null) {
				slider_distance.Dispose ();
				slider_distance = null;
			}

			if (tbl_commuteItems != null) {
				tbl_commuteItems.Dispose ();
				tbl_commuteItems = null;
			}

			if (tbl_suggestion != null) {
				tbl_suggestion.Dispose ();
				tbl_suggestion = null;
			}

			if (txt_address != null) {
				txt_address.Dispose ();
				txt_address = null;
			}

			if (view_addressBack != null) {
				view_addressBack.Dispose ();
				view_addressBack = null;
			}

			if (view_distance != null) {
				view_distance.Dispose ();
				view_distance = null;
			}

			if (view_nationWide != null) {
				view_nationWide.Dispose ();
				view_nationWide = null;
			}

			if (view_suggestionBack != null) {
				view_suggestionBack.Dispose ();
				view_suggestionBack = null;
			}

			if (view_back != null) {
				view_back.Dispose ();
				view_back = null;
			}

			if (view_trans != null) {
				view_trans.Dispose ();
				view_trans = null;
			}
		}
	}
}
