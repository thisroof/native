// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

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
