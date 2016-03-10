// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace ThisRoofN.iOS
{
	public partial class SearchBudgetCell : UITableViewCell, ISearchCell
	{
		public static string Identifier = "SearchBudgetCell";
		private nfloat cellHeight;
		private NormalSearchViewController masterView;

		public SearchBudgetCell (IntPtr handle) : base (handle)
		{
		}

		public nfloat CellHeight 
		{
			get
			{
				return cellHeight;
			}
		}

		public void BindData(NormalSearchViewController _masterView)
		{
			this.masterView = _masterView;

			masterView.BindingSet.Bind (txt_budget).To (vm => vm.MaxBudget);

			InitUI ();
		}

		public void HandleExpandTap()
		{
		}

		public void InitUI()
		{
			cellHeight = view_budgetBack.Frame.Bottom;
		}
	}
}
