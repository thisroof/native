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
	[Register ("SearchButtonCell")]
	partial class SearchButtonCell
	{
		[Outlet]
		UIKit.UIButton btn_resetAllFilters { get; set; }

		[Outlet]
		UIKit.UIButton btn_saveSearch { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_resetAllFilters != null) {
				btn_resetAllFilters.Dispose ();
				btn_resetAllFilters = null;
			}
			if (btn_saveSearch != null) {
				btn_saveSearch.Dispose ();
				btn_saveSearch = null;
			}
		}
	}
}
