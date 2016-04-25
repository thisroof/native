// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ThisRoofN.iOS
{
	[Register ("SearchResultDetailMapView")]
	partial class SearchResultDetailMapView
	{
		[Outlet]
		UIKit.UIButton btn_back { get; set; }

		[Outlet]
		UIKit.UIButton btn_hybrid { get; set; }

		[Outlet]
		UIKit.UIButton btn_satellite { get; set; }

		[Outlet]
		UIKit.UIButton btn_street { get; set; }

		[Outlet]
		UIKit.UILabel lbl_title { get; set; }

		[Outlet]
		MapKit.MKMapView map_property { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}

			if (btn_back != null) {
				btn_back.Dispose ();
				btn_back = null;
			}

			if (btn_satellite != null) {
				btn_satellite.Dispose ();
				btn_satellite = null;
			}

			if (btn_hybrid != null) {
				btn_hybrid.Dispose ();
				btn_hybrid = null;
			}

			if (btn_street != null) {
				btn_street.Dispose ();
				btn_street = null;
			}

			if (map_property != null) {
				map_property.Dispose ();
				map_property = null;
			}
		}
	}
}
