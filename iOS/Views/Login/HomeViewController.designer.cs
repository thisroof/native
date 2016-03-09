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
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UIButton btn_login { get; set; }

		[Outlet]
		UIKit.UIButton btn_signup { get; set; }

		[Outlet]
		UIKit.UIView view_video { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_login != null) {
				btn_login.Dispose ();
				btn_login = null;
			}
			if (btn_signup != null) {
				btn_signup.Dispose ();
				btn_signup = null;
			}
			if (view_video != null) {
				view_video.Dispose ();
				view_video = null;
			}
		}
	}
}
