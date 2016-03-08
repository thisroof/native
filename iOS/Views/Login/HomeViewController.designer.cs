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
			if (view_video != null) {
				view_video.Dispose ();
				view_video = null;
			}

			if (btn_login != null) {
				btn_login.Dispose ();
				btn_login = null;
			}

			if (btn_signup != null) {
				btn_signup.Dispose ();
				btn_signup = null;
			}
		}
	}
}
