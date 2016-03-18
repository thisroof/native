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
	[Register ("SRDetailMapCell")]
	partial class SRDetailMapCell
	{
		[Outlet]
		UIKit.UIImageView img_dislike { get; set; }

		[Outlet]
		UIKit.UIImageView img_like { get; set; }

		[Outlet]
		UIKit.UIImageView img_mapLock { get; set; }

		[Outlet]
		MapKit.MKMapView map_result { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (img_dislike != null) {
				img_dislike.Dispose ();
				img_dislike = null;
			}
			if (img_like != null) {
				img_like.Dispose ();
				img_like = null;
			}
			if (img_mapLock != null) {
				img_mapLock.Dispose ();
				img_mapLock = null;
			}
			if (map_result != null) {
				map_result.Dispose ();
				map_result = null;
			}
		}
	}
}
