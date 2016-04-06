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
	[Register ("SavedPropertiesView")]
	partial class SavedPropertiesView
	{
		[Outlet]
		UIKit.UITableView tbl_saved { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbl_saved != null) {
				tbl_saved.Dispose ();
				tbl_saved = null;
			}
		}
	}
}
