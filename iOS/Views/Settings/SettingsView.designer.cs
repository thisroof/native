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
	[Register ("SettingsView")]
	partial class SettingsView
	{
		[Outlet]
		UIKit.UIButton btn_clearDisliked { get; set; }

		[Outlet]
		UIKit.UIButton btn_cliearLiked { get; set; }

		[Outlet]
		UIKit.UIButton btn_enableAllTutorial { get; set; }

		[Outlet]
		UIKit.UIButton btn_licenses { get; set; }

		[Outlet]
		UIKit.UIButton btn_logout { get; set; }

		[Outlet]
		UIKit.UIButton btn_privacyPolicy { get; set; }

		[Outlet]
		UIKit.UIButton btn_savedProperty { get; set; }

		[Outlet]
		UIKit.UIButton btn_support { get; set; }

		[Outlet]
		UIKit.UIButton btn_termsOfUse { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_clearDisliked != null) {
				btn_clearDisliked.Dispose ();
				btn_clearDisliked = null;
			}
			if (btn_cliearLiked != null) {
				btn_cliearLiked.Dispose ();
				btn_cliearLiked = null;
			}
			if (btn_enableAllTutorial != null) {
				btn_enableAllTutorial.Dispose ();
				btn_enableAllTutorial = null;
			}
			if (btn_licenses != null) {
				btn_licenses.Dispose ();
				btn_licenses = null;
			}
			if (btn_logout != null) {
				btn_logout.Dispose ();
				btn_logout = null;
			}
			if (btn_privacyPolicy != null) {
				btn_privacyPolicy.Dispose ();
				btn_privacyPolicy = null;
			}
			if (btn_savedProperty != null) {
				btn_savedProperty.Dispose ();
				btn_savedProperty = null;
			}
			if (btn_support != null) {
				btn_support.Dispose ();
				btn_support = null;
			}
			if (btn_termsOfUse != null) {
				btn_termsOfUse.Dispose ();
				btn_termsOfUse = null;
			}
		}
	}
}
