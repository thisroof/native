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
	[Register ("CheckboxCVCell")]
	partial class CheckboxCVCell
	{
		[Outlet]
		UIKit.UIImageView iv_checkMark { get; set; }

		[Outlet]
		UIKit.UILabel lbl_title { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (iv_checkMark != null) {
				iv_checkMark.Dispose ();
				iv_checkMark = null;
			}

			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}
		}
	}
}
