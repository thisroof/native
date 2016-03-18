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
	[Register ("SRDetailDescCell")]
	partial class SRDetailDescCell
	{
		[Outlet]
		UIKit.UIButton btn_more { get; set; }

		[Outlet]
		UIKit.UILabel lbl_desc { get; set; }

		[Outlet]
		UIKit.UIView view_descBack { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_more != null) {
				btn_more.Dispose ();
				btn_more = null;
			}
			if (lbl_desc != null) {
				lbl_desc.Dispose ();
				lbl_desc = null;
			}
			if (view_descBack != null) {
				view_descBack.Dispose ();
				view_descBack = null;
			}
		}
	}
}
