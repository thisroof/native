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
	[Register ("SearchAreaCommuteCell")]
	partial class SearchAreaCommuteCell
	{
		[Outlet]
		UIKit.UILabel lbl_title { get; set; }

		[Outlet]
		UIKit.UISwitch swt_option { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (swt_option != null) {
				swt_option.Dispose ();
				swt_option = null;
			}

			if (lbl_title != null) {
				lbl_title.Dispose ();
				lbl_title = null;
			}
		}
	}
}
