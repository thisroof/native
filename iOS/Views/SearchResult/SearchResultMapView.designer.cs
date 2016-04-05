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
	[Register ("SearchResultMapView")]
	partial class SearchResultMapView
	{
		[Outlet]
		MapKit.MKMapView map_results { get; set; }

		[Outlet]
		UIKit.UISegmentedControl seg_mapMode { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (map_results != null) {
				map_results.Dispose ();
				map_results = null;
			}

			if (seg_mapMode != null) {
				seg_mapMode.Dispose ();
				seg_mapMode = null;
			}
		}
	}
}
