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
	[Register ("SearchSoryByCell")]
	partial class SearchSoryByCell
	{
		[Outlet]
		UIKit.UITextField txt_sortBy { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txt_sortBy != null) {
				txt_sortBy.Dispose ();
				txt_sortBy = null;
			}
		}
	}
}
