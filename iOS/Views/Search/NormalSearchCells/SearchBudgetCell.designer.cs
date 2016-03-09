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
		UIKit.UITextField txt_budget { get; set; }

		[Outlet]
		UIKit.UIView view_budgetBack { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txt_budget != null) {
				txt_budget.Dispose ();
				txt_budget = null;
			}

			if (view_budgetBack != null) {
				view_budgetBack.Dispose ();
				view_budgetBack = null;
			}
		}
	}
}
