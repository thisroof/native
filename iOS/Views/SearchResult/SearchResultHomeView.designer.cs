// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ThisRoofN.iOS.Views
{
	[Register ("SearchResultHomeView")]
	partial class SearchResultHomeView
	{
		[Outlet]
		UIKit.UIButton btn_list { get; set; }

		[Outlet]
		UIKit.UIButton btn_map { get; set; }

		[Outlet]
		UIKit.UIButton btn_tile { get; set; }

		[Outlet]
		UIKit.UIScrollView page_scroll { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (page_scroll != null) {
				page_scroll.Dispose ();
				page_scroll = null;
			}

			if (btn_list != null) {
				btn_list.Dispose ();
				btn_list = null;
			}

			if (btn_tile != null) {
				btn_tile.Dispose ();
				btn_tile = null;
			}

			if (btn_map != null) {
				btn_map.Dispose ();
				btn_map = null;
			}
		}
	}
}
