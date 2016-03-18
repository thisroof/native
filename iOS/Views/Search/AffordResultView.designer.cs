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
	[Register ("AffordResultView")]
	partial class AffordResultView
	{
		[Outlet]
		UIKit.UIButton btn_getPreApprove { get; set; }

		[Outlet]
		UIKit.UIButton btn_startSearch { get; set; }

		[Outlet]
		UIKit.UILabel lbl_budget { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_getPreApprove != null) {
				btn_getPreApprove.Dispose ();
				btn_getPreApprove = null;
			}
			if (btn_startSearch != null) {
				btn_startSearch.Dispose ();
				btn_startSearch = null;
			}
			if (lbl_budget != null) {
				lbl_budget.Dispose ();
				lbl_budget = null;
			}
		}
	}
}
