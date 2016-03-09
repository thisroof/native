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
	[Register ("SignupViewController")]
	partial class SignupViewController
	{
		[Outlet]
		UIKit.UIButton btn_signup { get; set; }

		[Outlet]
		UIKit.UIImageView img_btn_back { get; set; }

		[Outlet]
		UIKit.UIImageView img_btnFbLogin { get; set; }

		[Outlet]
		UIKit.UITextField txt_email { get; set; }

		[Outlet]
		UIKit.UITextField txt_password { get; set; }

		[Outlet]
		UIKit.UIView view_video { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_signup != null) {
				btn_signup.Dispose ();
				btn_signup = null;
			}
			if (img_btn_back != null) {
				img_btn_back.Dispose ();
				img_btn_back = null;
			}
			if (img_btnFbLogin != null) {
				img_btnFbLogin.Dispose ();
				img_btnFbLogin = null;
			}
			if (txt_email != null) {
				txt_email.Dispose ();
				txt_email = null;
			}
			if (txt_password != null) {
				txt_password.Dispose ();
				txt_password = null;
			}
			if (view_video != null) {
				view_video.Dispose ();
				view_video = null;
			}
		}
	}
}
