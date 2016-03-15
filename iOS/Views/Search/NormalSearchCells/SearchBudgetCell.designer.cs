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
	[Register ("SearchBudgetCell")]
	partial class SearchBudgetCell
	{
		[Outlet]
		UIKit.UIImageView img_valueMark { get; set; }

		[Outlet]
		UIKit.UILabel lbl_budget { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbl_budget != null) {
				lbl_budget.Dispose ();
				lbl_budget = null;
			}

			if (img_valueMark != null) {
				img_valueMark.Dispose ();
				img_valueMark = null;
			}
		}
	}
}
