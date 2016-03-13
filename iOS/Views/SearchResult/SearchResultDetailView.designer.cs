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
	[Register ("SearchResultDetailView")]
	partial class SearchResultDetailView
	{
		[Outlet]
		UIKit.UITableView tbl_detail { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbl_detail != null) {
				tbl_detail.Dispose ();
				tbl_detail = null;
			}
		}
	}
}
