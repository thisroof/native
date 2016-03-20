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
	[Register ("HomeStructureModalView")]
	partial class HomeStructureModalView
	{
		[Outlet]
		UIKit.UIButton btn_modalBack { get; set; }

		[Outlet]
		UIKit.UITableView tbl_items { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_modalBack != null) {
				btn_modalBack.Dispose ();
				btn_modalBack = null;
			}

			if (tbl_items != null) {
				tbl_items.Dispose ();
				tbl_items = null;
			}
		}
	}
}
