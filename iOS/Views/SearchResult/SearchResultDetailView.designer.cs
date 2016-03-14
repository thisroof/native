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
	[Register ("SearchResultDetailView")]
	partial class SearchResultDetailView
	{
		[Outlet]
		UIKit.UIButton btn_cancel { get; set; }

		[Outlet]
		UIKit.UIButton btn_commit { get; set; }

		[Outlet]
		UIKit.UIImageView icon_lotTooSmall { get; set; }

		[Outlet]
		UIKit.UIImageView icon_tooBig { get; set; }

		[Outlet]
		UIKit.UIImageView icon_tooClose { get; set; }

		[Outlet]
		UIKit.UIImageView icon_tooFar { get; set; }

		[Outlet]
		UIKit.UIImageView icon_tooSmall { get; set; }

		[Outlet]
		UIKit.UIImageView icon_ugly { get; set; }

		[Outlet]
		UIKit.UITableView tbl_detail { get; set; }

		[Outlet]
		UIKit.UIView view_dislikeSetting { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbl_detail != null) {
				tbl_detail.Dispose ();
				tbl_detail = null;
			}

			if (view_dislikeSetting != null) {
				view_dislikeSetting.Dispose ();
				view_dislikeSetting = null;
			}

			if (icon_tooFar != null) {
				icon_tooFar.Dispose ();
				icon_tooFar = null;
			}

			if (icon_tooClose != null) {
				icon_tooClose.Dispose ();
				icon_tooClose = null;
			}

			if (icon_tooSmall != null) {
				icon_tooSmall.Dispose ();
				icon_tooSmall = null;
			}

			if (icon_lotTooSmall != null) {
				icon_lotTooSmall.Dispose ();
				icon_lotTooSmall = null;
			}

			if (icon_tooBig != null) {
				icon_tooBig.Dispose ();
				icon_tooBig = null;
			}

			if (icon_ugly != null) {
				icon_ugly.Dispose ();
				icon_ugly = null;
			}

			if (btn_cancel != null) {
				btn_cancel.Dispose ();
				btn_cancel = null;
			}

			if (btn_commit != null) {
				btn_commit.Dispose ();
				btn_commit = null;
			}
		}
	}
}
