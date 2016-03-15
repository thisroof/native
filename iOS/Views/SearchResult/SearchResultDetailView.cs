// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using CoreGraphics;
using ThisRoofN.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace ThisRoofN.iOS
{
	public partial class SearchResultDetailView : BaseViewController
	{
		public MvxFluentBindingDescriptionSet<SearchResultDetailView, SearchResultDetailViewModel> BindingSet;

		public SearchResultDetailView (IntPtr handle) : base (handle)
		{
		}

		public SearchResultDetailViewModel ViewModelInstace {
			get {
				return this.ViewModel as SearchResultDetailViewModel;
			}
		}

		public UITableView MasterTableView {
			get {
				return tbl_detail;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SetupNavigationBar ();

			view_dislikeSetting.Layer.CornerRadius = 5.0f;

			BindingSet = this.CreateBindingSet<SearchResultDetailView, SearchResultDetailViewModel> ();
			BindingSet.Bind (backButton).To (vm => vm.CloseCommand);

			BindingSet.Bind (view_dislikeSetting).For (i => i.Hidden).To (vm => vm.IsDislikeHidden);
			BindingSet.Bind (btn_commit).To (vm => vm.DisLikeCommand);
			BindingSet.Bind (btn_cancel).To (vm => vm.ShowDislikeViewCommand).CommandParameter (false);

			BindingSet.Bind (icon_tooFar).To (vm => vm.TooFar);
			BindingSet.Bind (icon_tooClose).To (vm => vm.TooClose);
			BindingSet.Bind (icon_tooSmall).To (vm => vm.TooSmall);
			BindingSet.Bind (icon_lotTooSmall).To (vm => vm.LotTooSmall);
			BindingSet.Bind (icon_tooBig).To (vm => vm.HouseTooBig);
			BindingSet.Bind (icon_ugly).To (vm => vm.Ugly);

			icon_tooFar.UserInteractionEnabled = true;
			icon_tooClose.UserInteractionEnabled = true;
			icon_tooSmall.UserInteractionEnabled = true;
			icon_lotTooSmall.UserInteractionEnabled = true;
			icon_tooBig.UserInteractionEnabled = true;
			icon_ugly.UserInteractionEnabled = true;

			icon_tooFar.AddGestureRecognizer (new UIGestureRecognizer (() => {
				ViewModelInstace.TooFar = !ViewModelInstace.TooFar;
			}));
			icon_tooClose.AddGestureRecognizer (new UIGestureRecognizer (() => {
				ViewModelInstace.TooClose = !ViewModelInstace.TooClose;
			}));
			icon_tooSmall.AddGestureRecognizer (new UIGestureRecognizer (() => {
				ViewModelInstace.TooSmall = !ViewModelInstace.TooSmall;
			}));
			icon_lotTooSmall.AddGestureRecognizer (new UIGestureRecognizer (() => {
				ViewModelInstace.LotTooSmall = !ViewModelInstace.LotTooSmall;
			}));
			icon_tooBig.AddGestureRecognizer (new UIGestureRecognizer (() => {
				ViewModelInstace.LotTooBig = !ViewModelInstace.LotTooBig;
			}));
			icon_ugly.AddGestureRecognizer (new UIGestureRecognizer (() => {
				ViewModelInstace.Ugly = !ViewModelInstace.Ugly;
			}));
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (false, true);
			this.NavigationController.SetToolbarHidden (true, true);
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			// we set the source and bind here to get the tableview height, exactly
			var source = new SearchResultDetailTableViewSource (tbl_detail, this);
			tbl_detail.Source = source;
			tbl_detail.RowHeight = UITableView.AutomaticDimension;
			tbl_detail.AllowsSelection = false;
			tbl_detail.TableFooterView = new UITableView (CGRect.Empty);

			BindingSet.Bind (source).To (vm => vm.ItemDetail.Gallery);
			BindingSet.Apply ();
		}

		public class SearchResultDetailTableViewSource : MvxTableViewSource
		{
			SearchResultDetailView masterView;
			SRDetailMapCell mapCell;
			SRDetailTitleCell titleCell;
			SRDetailValueCell valueCell;
			SRDetailDescCell descCell;

			public SearchResultDetailTableViewSource (UITableView tableView, SearchResultDetailView vc) : base (tableView)
			{
				this.masterView = vc;

				mapCell = (SRDetailMapCell)tableView.DequeueReusableCell (SRDetailMapCell.Identifier);
				titleCell = (SRDetailTitleCell)tableView.DequeueReusableCell (SRDetailTitleCell.Identifier);
				valueCell = (SRDetailValueCell)tableView.DequeueReusableCell (SRDetailValueCell.Identifier);
				descCell = (SRDetailDescCell)tableView.DequeueReusableCell (SRDetailDescCell.Identifier);

				mapCell.BindData (masterView);
				titleCell.BindData (masterView);
				valueCell.BindData (masterView);
				descCell.BindData (masterView);

				masterView.BindingSet.Apply ();
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				if (ItemsSource == null) {
					return 4;
				} else {
					return ItemsSource.Cast<TileItemModel> ().Count () + 4;
				}
			}

			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				switch (indexPath.Row) {
				case 0:
					return mapCell;
				case 1:
					return titleCell;
				case 2:
					return valueCell;
				case 3:
					return descCell;
				default:
					SRDetailImageCell cell = (SRDetailImageCell)tableView.DequeueReusableCell (SRDetailImageCell.Identifier);
					cell.IVItem.ClipsToBounds = true;
					cell.IVItem.DefaultImagePath = NSBundle.MainBundle.PathForResource ("img_placeholder", "png");
					return cell;

				}
			}

			protected override object GetItemAt (NSIndexPath indexPath)
			{
				if (indexPath.Row > 3) {
					return ItemsSource.Cast<TileItemModel> ().ToList () [indexPath.Row - 4];
				} else {
					return null;
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				switch (indexPath.Row) {
				case 0:
					return mapCell.CellHeight;
				case 1:
					return titleCell.CellHeight;
				case 2:
					return valueCell.CellHeight;
				case 3:
					return descCell.CellHeight;
				default:
					return UIScreen.MainScreen.Bounds.Width * 9 / 16;
					break;
				}

				return 0;
			}
		}
	}
}
