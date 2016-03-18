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
	[Register ("NormalSearchViewController")]
	partial class NormalSearchViewController
	{
		[Outlet]
		UIKit.UIButton btn_viewResult { get; set; }

		[Outlet]
		UIKit.UITableView tbl_search { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_viewResult != null) {
				btn_viewResult.Dispose ();
				btn_viewResult = null;
			}
			if (tbl_search != null) {
				tbl_search.Dispose ();
				tbl_search = null;
			}
		}
	}
}
