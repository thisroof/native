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
			if (view_descBack != null) {
				view_descBack.Dispose ();
				view_descBack = null;
			}

			if (btn_more != null) {
				btn_more.Dispose ();
				btn_more = null;
			}

			if (lbl_desc != null) {
				lbl_desc.Dispose ();
				lbl_desc = null;
			}
		}
	}
}
