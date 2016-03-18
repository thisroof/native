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
