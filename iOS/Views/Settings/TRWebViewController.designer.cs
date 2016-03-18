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
	[Register ("TRWebViewController")]
	partial class TRWebViewController
	{
		[Outlet]
		UIKit.UIWebView view_web { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (view_web != null) {
				view_web.Dispose ();
				view_web = null;
			}
		}
	}
}
