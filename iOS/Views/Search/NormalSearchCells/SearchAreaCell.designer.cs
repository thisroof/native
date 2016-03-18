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
	[Register ("SearchAreaCell")]
	partial class SearchAreaCell
	{
		[Outlet]
		UIKit.UIButton btn_nationClearAll { get; set; }

		[Outlet]
		UIKit.UIButton btn_nationSelectAll { get; set; }

		[Outlet]
		UIKit.UICollectionView cv_nations { get; set; }

		[Outlet]
		UIKit.UIImageView img_expandMark { get; set; }

		[Outlet]
		UIKit.UIImageView img_locationLogo { get; set; }

		[Outlet]
		UIKit.UIImageView img_navigator { get; set; }

		[Outlet]
		UIKit.UILabel lbl_distance { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_areaType { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_driving { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_traffic { get; set; }

		[Outlet]
		UIKit.UISlider slider_distance { get; set; }

		[Outlet]
		UIKit.UITableView tbl_locations { get; set; }

		[Outlet]
		UIKit.UITextField txt_address { get; set; }

		[Outlet]
		UIKit.UIView view_autocomplete { get; set; }

		[Outlet]
		UIKit.UIView view_cellTitle { get; set; }

		[Outlet]
		UIKit.UIView view_distance { get; set; }

		[Outlet]
		UIKit.UIView view_locations { get; set; }

		[Outlet]
		UIKit.UIView view_nation { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_nationClearAll != null) {
				btn_nationClearAll.Dispose ();
				btn_nationClearAll = null;
			}
			if (btn_nationSelectAll != null) {
				btn_nationSelectAll.Dispose ();
				btn_nationSelectAll = null;
			}
			if (cv_nations != null) {
				cv_nations.Dispose ();
				cv_nations = null;
			}
			if (img_expandMark != null) {
				img_expandMark.Dispose ();
				img_expandMark = null;
			}
			if (img_locationLogo != null) {
				img_locationLogo.Dispose ();
				img_locationLogo = null;
			}
			if (img_navigator != null) {
				img_navigator.Dispose ();
				img_navigator = null;
			}
			if (lbl_distance != null) {
				lbl_distance.Dispose ();
				lbl_distance = null;
			}
			if (seg_areaType != null) {
				seg_areaType.Dispose ();
				seg_areaType = null;
			}
			if (seg_driving != null) {
				seg_driving.Dispose ();
				seg_driving = null;
			}
			if (seg_traffic != null) {
				seg_traffic.Dispose ();
				seg_traffic = null;
			}
			if (slider_distance != null) {
				slider_distance.Dispose ();
				slider_distance = null;
			}
			if (tbl_locations != null) {
				tbl_locations.Dispose ();
				tbl_locations = null;
			}
			if (txt_address != null) {
				txt_address.Dispose ();
				txt_address = null;
			}
			if (view_autocomplete != null) {
				view_autocomplete.Dispose ();
				view_autocomplete = null;
			}
			if (view_cellTitle != null) {
				view_cellTitle.Dispose ();
				view_cellTitle = null;
			}
			if (view_distance != null) {
				view_distance.Dispose ();
				view_distance = null;
			}
			if (view_locations != null) {
				view_locations.Dispose ();
				view_locations = null;
			}
			if (view_nation != null) {
				view_nation.Dispose ();
				view_nation = null;
			}
		}
	}
}
