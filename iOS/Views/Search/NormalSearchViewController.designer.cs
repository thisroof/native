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
	[Register ("NormalSearchViewController")]
	partial class NormalSearchViewController
	{
		[Outlet]
		UIKit.UIButton btn_viewResult { get; set; }

		[Outlet]
		UIKit.UITableView tbl_search { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbl_search != null) {
				tbl_search.Dispose ();
				tbl_search = null;
			}

			if (btn_viewResult != null) {
				btn_viewResult.Dispose ();
				btn_viewResult = null;
			}
		}
	}
}
