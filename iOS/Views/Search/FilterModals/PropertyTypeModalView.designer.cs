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
	[Register ("PropertyTypeModalView")]
	partial class PropertyTypeModalView
	{
		[Outlet]
		UIKit.UIButton btn_back { get; set; }

		[Outlet]
		UIKit.UITableView tbl_propertyTypes { get; set; }

		[Outlet]
		UIKit.UIView view_side { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (view_side != null) {
				view_side.Dispose ();
				view_side = null;
			}

			if (btn_back != null) {
				btn_back.Dispose ();
				btn_back = null;
			}

			if (tbl_propertyTypes != null) {
				tbl_propertyTypes.Dispose ();
				tbl_propertyTypes = null;
			}
		}
	}
}
