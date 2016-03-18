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
	[Register ("SearchBathsCell")]
	partial class SearchBathsCell
	{
		[Outlet]
		UIKit.UIImageView img_expandMarker { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_bathCount { get; set; }

		[Outlet]
		UIKit.UIView view_cellTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (img_expandMarker != null) {
				img_expandMarker.Dispose ();
				img_expandMarker = null;
			}
			if (seg_bathCount != null) {
				seg_bathCount.Dispose ();
				seg_bathCount = null;
			}
			if (view_cellTitle != null) {
				view_cellTitle.Dispose ();
				view_cellTitle = null;
			}
		}
	}
}
