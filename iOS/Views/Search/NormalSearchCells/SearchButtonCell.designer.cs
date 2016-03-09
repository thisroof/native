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
	[Register ("SearchButtonCell")]
	partial class SearchButtonCell
	{
		[Outlet]
		UIKit.UIButton btn_resetAllFilters { get; set; }

		[Outlet]
		UIKit.UIButton btn_saveSearch { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_saveSearch != null) {
				btn_saveSearch.Dispose ();
				btn_saveSearch = null;
			}

			if (btn_resetAllFilters != null) {
				btn_resetAllFilters.Dispose ();
				btn_resetAllFilters = null;
			}
		}
	}
}
