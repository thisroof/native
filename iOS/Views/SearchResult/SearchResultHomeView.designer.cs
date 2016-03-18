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

namespace ThisRoofN.iOS.Views
{
	[Register ("SearchResultHomeView")]
	partial class SearchResultHomeView
	{
		[Outlet]
		UIKit.UIScrollView page_scroll { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (page_scroll != null) {
				page_scroll.Dispose ();
				page_scroll = null;
			}
		}
	}
}
