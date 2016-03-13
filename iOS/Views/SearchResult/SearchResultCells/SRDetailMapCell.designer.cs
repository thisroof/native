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
