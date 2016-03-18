using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using ThisRoofN.ViewModels;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;

namespace ThisRoofN.iOS
{
	public partial class NormalSearchViewController : BaseViewController
	{
		public MvxFluentBindingDescriptionSet<NormalSearchViewController, NormalSearchViewModel> BindingSet;

		public NormalSearchViewController (IntPtr handle) : base (handle)
		{
		}

		public NormalSearchViewModel ViewModelInstance
		{
			get {
				return (NormalSearchViewModel)this.ViewModel;
			}
		}

		public UITableView MasterTableView
		{
			get {
				return this.tbl_search;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SetupNavigationBar ();

			UITapGestureRecognizer tapper = new UITapGestureRecognizer (HandleTableViewTap);
			tapper.CancelsTouchesInView = false;
			tbl_search.AddGestureRecognizer (tapper);

			BindingSet = this.CreateBindingSet<NormalSearchViewController, NormalSearchViewModel> ();
			BindingSet.Bind (backButton).To (vm => vm.CloseCommand);
			BindingSet.Bind (settingButton).To (vm => vm.SettingCommand);
			BindingSet.Bind (btn_viewResult).To (vm => vm.SearchCommand);

			var source = new SearchTableViewSource (tbl_search, this);
			tbl_search.Source = source;
			tbl_search.ReloadData ();
			tbl_search.RowHeight = UITableView.AutomaticDimension;
			tbl_search.AllowsSelection = false;
			tbl_search.TableFooterView = new UITableView (CGRect.Empty);
			tbl_search.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (false, true);
			this.NavigationController.SetToolbarHidden (true, true);
		}

		private void HandleTableViewTap()
		{
			this.tbl_search.EndEditing (true);
		}

		public class SearchTableViewSource : MvxTableViewSource
		{
			public ISearchCell ExpandedCell {get;set;}

			NormalSearchViewController masterView;
			SearchBudgetCell budgetCell;
			SearchSoryByCell sortbyCell;
			SearchAreaCell areaCell;
			SearchBedsCell bedCell;
			SearchBathsCell bathsCell;
			SearchDetailFilterCell detailFilterCell;
			SearchButtonCell buttonCell;

			public SearchTableViewSource(UITableView tableView, NormalSearchViewController vc):base(tableView) {
				this.masterView = vc;

				budgetCell = (SearchBudgetCell)tableView.DequeueReusableCell(SearchBudgetCell.Identifier);
				sortbyCell = (SearchSoryByCell)tableView.DequeueReusableCell(SearchSoryByCell.Identifier);
				areaCell = (SearchAreaCell)tableView.DequeueReusableCell(SearchAreaCell.Identifier);
				bedCell = (SearchBedsCell)tableView.DequeueReusableCell(SearchBedsCell.Identifier);
				bathsCell = (SearchBathsCell)tableView.DequeueReusableCell(SearchBathsCell.Identifier);
				detailFilterCell = (SearchDetailFilterCell)tableView.DequeueReusableCell(SearchDetailFilterCell.Identifier);
				buttonCell = (SearchButtonCell)tableView.DequeueReusableCell(SearchButtonCell.Identifier);

				budgetCell.BindData(masterView);
				sortbyCell.BindData(masterView);
				areaCell.BindData(masterView);
				bedCell.BindData(masterView);
				bathsCell.BindData(masterView);
				detailFilterCell.BindData(masterView);
				buttonCell.BindData(masterView);

				masterView.BindingSet.Apply();
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return 7;
			}

			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				switch (indexPath.Row) {
				case 0:
					return budgetCell;
				case 1:
					return sortbyCell;
				case 2:
					return areaCell;
				case 3:
					return bedCell;
				case 4:
					return bathsCell;
				case 5:
					return detailFilterCell;
				case 6:
					return buttonCell;
				}

				return null;
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				switch (indexPath.Row) {
				case 0:
					return budgetCell.CellHeight;
				case 1:
					return sortbyCell.CellHeight;
				case 2:
					return areaCell.CellHeight;
				case 3:
					return bedCell.CellHeight;
				case 4:
					return bathsCell.CellHeight;
				case 5:
					return detailFilterCell.CellHeight;
				case 6:
					return buttonCell.CellHeight;
				}

				return 0;
			}
		}
	}
}
