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
	[Register ("SearchTypeViewController")]
	partial class SearchTypeViewController
	{
		[Outlet]
		UIKit.UIButton btn_affordSearch { get; set; }

		[Outlet]
		UIKit.UIButton btn_normalSearch { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_affordSearch != null) {
				btn_affordSearch.Dispose ();
				btn_affordSearch = null;
			}
			if (btn_normalSearch != null) {
				btn_normalSearch.Dispose ();
				btn_normalSearch = null;
			}
		}
	}
}
