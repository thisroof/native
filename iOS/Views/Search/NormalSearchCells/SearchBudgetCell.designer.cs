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
	[Register ("SearchBudgetCell")]
	partial class SearchBudgetCell
	{
		[Outlet]
		UIKit.UIImageView img_valueMark { get; set; }

		[Outlet]
		UIKit.UILabel lbl_budget { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (img_valueMark != null) {
				img_valueMark.Dispose ();
				img_valueMark = null;
			}
			if (lbl_budget != null) {
				lbl_budget.Dispose ();
				lbl_budget = null;
			}
		}
	}
}
