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
	[Register ("SearchForeclosureCell")]
	partial class SearchForeclosureCell
	{
		[Outlet]
		UIKit.UIImageView img_expandMarker { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_type { get; set; }

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

			if (seg_type != null) {
				seg_type.Dispose ();
				seg_type = null;
			}
		}
	}
}