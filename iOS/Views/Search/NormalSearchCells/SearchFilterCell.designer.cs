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
	[Register ("SearchFilterCell")]
	partial class SearchFilterCell
	{
		[Outlet]
		UIKit.UICollectionView cv_viewTypes { get; set; }

		[Outlet]
		UIKit.UIImageView img_expandMarker { get; set; }

		[Outlet]
		UIKit.UITextField txt_daysOnMarket { get; set; }

		[Outlet]
		UIKit.UITextField txt_logSizeTo { get; set; }

		[Outlet]
		UIKit.UITextField txt_lotSizeFrom { get; set; }

		[Outlet]
		UIKit.UITextField txt_newerThan { get; set; }

		[Outlet]
		UIKit.UITextField txt_pool { get; set; }

		[Outlet]
		UIKit.UITextField txt_squareFeetFrom { get; set; }

		[Outlet]
		UIKit.UITextField txt_squareFeetTo { get; set; }

		[Outlet]
		UIKit.UIView view_cellTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (view_cellTitle != null) {
				view_cellTitle.Dispose ();
				view_cellTitle = null;
			}

			if (img_expandMarker != null) {
				img_expandMarker.Dispose ();
				img_expandMarker = null;
			}

			if (txt_squareFeetFrom != null) {
				txt_squareFeetFrom.Dispose ();
				txt_squareFeetFrom = null;
			}

			if (txt_squareFeetTo != null) {
				txt_squareFeetTo.Dispose ();
				txt_squareFeetTo = null;
			}

			if (txt_newerThan != null) {
				txt_newerThan.Dispose ();
				txt_newerThan = null;
			}

			if (txt_lotSizeFrom != null) {
				txt_lotSizeFrom.Dispose ();
				txt_lotSizeFrom = null;
			}

			if (txt_logSizeTo != null) {
				txt_logSizeTo.Dispose ();
				txt_logSizeTo = null;
			}

			if (txt_daysOnMarket != null) {
				txt_daysOnMarket.Dispose ();
				txt_daysOnMarket = null;
			}

			if (txt_pool != null) {
				txt_pool.Dispose ();
				txt_pool = null;
			}

			if (cv_viewTypes != null) {
				cv_viewTypes.Dispose ();
				cv_viewTypes = null;
			}
		}
	}
}
