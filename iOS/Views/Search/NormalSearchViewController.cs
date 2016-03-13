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
			BindingSet.Bind (btn_viewResult).To (vm => vm.SearchCommand);

			var source = new SearchTableViewSource (tbl_search, this);
			tbl_search.Source = source;
			tbl_search.ReloadData ();
			tbl_search.RowHeight = UITableView.AutomaticDimension;
			tbl_search.AllowsSelection = false;
			tbl_search.TableFooterView = new UITableView (CGRect.Empty);
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
			SearchPropertyTypeCell propertyTypeCell;
			SearchFilterCell filterCell;
			SearchForeclosureCell forclosureCell;
			SearchExcludedAreaCell excludedAreaCell;
			SearchButtonCell buttonCell;

			public SearchTableViewSource(UITableView tableView, NormalSearchViewController vc):base(tableView) {
				this.masterView = vc;

				budgetCell = (SearchBudgetCell)tableView.DequeueReusableCell(SearchBudgetCell.Identifier);
				sortbyCell = (SearchSoryByCell)tableView.DequeueReusableCell(SearchSoryByCell.Identifier);
				areaCell = (SearchAreaCell)tableView.DequeueReusableCell(SearchAreaCell.Identifier);
				bedCell = (SearchBedsCell)tableView.DequeueReusableCell(SearchBedsCell.Identifier);
				bathsCell = (SearchBathsCell)tableView.DequeueReusableCell(SearchBathsCell.Identifier);
				propertyTypeCell = (SearchPropertyTypeCell)tableView.DequeueReusableCell(SearchPropertyTypeCell.Identifier);
				filterCell = (SearchFilterCell)tableView.DequeueReusableCell(SearchFilterCell.Identifier);
				forclosureCell = (SearchForeclosureCell)tableView.DequeueReusableCell(SearchForeclosureCell.Identifier);
				excludedAreaCell = (SearchExcludedAreaCell)tableView.DequeueReusableCell(SearchExcludedAreaCell.Identifier);
				buttonCell = (SearchButtonCell)tableView.DequeueReusableCell(SearchButtonCell.Identifier);

				budgetCell.BindData(masterView);
				sortbyCell.BindData(masterView);
				areaCell.BindData(masterView);
				bedCell.BindData(masterView);
				bathsCell.BindData(masterView);
				propertyTypeCell.BindData(masterView);
				filterCell.BindData(masterView);
				forclosureCell.BindData(masterView);
				excludedAreaCell.BindData(masterView);
				buttonCell.BindData(masterView);

				masterView.BindingSet.Apply();
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return 10;
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
					return propertyTypeCell;
				case 6:
					return filterCell;
				case 7:
					return forclosureCell;
				case 8:
					return excludedAreaCell;
				case 9:
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
					return propertyTypeCell.CellHeight;
				case 6:
					return filterCell.CellHeight;
				case 7:
					return forclosureCell.CellHeight;
				case 8:
					return excludedAreaCell.CellHeight;
				case 9:
					return buttonCell.CellHeight;
				}

				return 0;
			}
		}
	}
}
