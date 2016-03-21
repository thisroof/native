
using System;

using Foundation;
using UIKit;
using MvvmCross.iOS.Views;
using ThisRoofN.ViewModels;
using MvvmCross.Binding.BindingContext;
using CoreAnimation;
using ThisRoofN.iOS.Helpers;
using MvvmCross.Binding.iOS.Views;
using CoreGraphics;
using RangeSlider;
using System.Linq;

namespace ThisRoofN.iOS
{
	public partial class SearchAreaModalView : BaseModalView, IUICollectionViewDelegateFlowLayout, IUICollectionViewDelegate
	{
		private RangeSliderView distanceRangeSlider;
		public SearchAreaModalView (IntPtr handle) : base (handle)
		{
		}

		public SearchAreaModalViewModel ViewModelInstance
		{
			get {
				return (SearchAreaModalViewModel)base.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Init the State Collection View
			var propertyTypeSource = new MvxCollectionViewSource (cv_nations, new NSString ("SearchAreaCheckboxCVCell"));
			cv_nations.AllowsSelection = true;
			cv_nations.Source = propertyTypeSource;
			cv_nations.Delegate = this;

			// Bind Locatin Suggesion Table
			var source = new LocationSuggestionTableViewSource (tbl_suggestion);
			tbl_suggestion.Source = source;
			tbl_suggestion.RowHeight = UITableView.AutomaticDimension;
			tbl_suggestion.AllowsSelection = false;
			tbl_suggestion.TableFooterView = new UITableView (CGRect.Empty);
			tbl_suggestion.ReloadData ();

			// Bind Item Tableview source
			var itemSource = new SearchAreaCommuteItemsTableViewSource (this, tbl_commuteItems);
			tbl_commuteItems.Source = itemSource;
			tbl_commuteItems.RowHeight = UITableView.AutomaticDimension;
			tbl_commuteItems.TableFooterView = new UITableView (CGRect.Empty);
			tbl_commuteItems.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			tbl_commuteItems.ReloadData ();

			var bindingSet = this.CreateBindingSet<SearchAreaModalView, SearchAreaModalViewModel> ();
			bindingSet.Bind (btn_modalBack).To (vm => vm.ModalCloseCommand);
			bindingSet.Bind (propertyTypeSource).To (vm => vm.States);
			bindingSet.Bind (lbl_distanceRange).To (vm => vm.DistanceLabelText);
			bindingSet.Bind (source).To (vm => vm.AddressSuggestionItems);
			bindingSet.Bind (itemSource).To (vm => vm.CommuteItems);
			bindingSet.Apply ();

			InitUI ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		private void InitUI() {

			view_addressBack.Layer.BorderWidth = 1.0f;
			view_addressBack.Layer.BorderColor = UIColor.LightGray.CGColor;
			view_addressBack.Layer.CornerRadius = 3.0f;

			btn_selectAll.Layer.BorderWidth = 1.0f;
			btn_selectAll.Layer.BorderColor = UIColor.LightGray.CGColor;
			btn_selectAll.Layer.CornerRadius = 3.0f;

			btn_clearAll.Layer.BorderWidth = 1.0f;
			btn_clearAll.Layer.BorderColor = UIColor.LightGray.CGColor;
			btn_clearAll.Layer.CornerRadius = 3.0f;

			switch (ViewModelInstance.DistanceType) {
			case 0: // Distance
				seg_areaType.SelectedSegment = 1;
				view_distance.Hidden = false;
				view_nationWide.Hidden = true;
				break;
			case 1: // Commute
				seg_areaType.SelectedSegment = 0;
				view_distance.Hidden = false;
				view_nationWide.Hidden = true;
				break;
			case 2: // States
				seg_areaType.SelectedSegment = 2;
				view_distance.Hidden = true;
				view_nationWide.Hidden = false;
				break;
			default:
				break;
			}

			seg_areaType.ValueChanged += (object sender, EventArgs e) => {
				switch (seg_areaType.SelectedSegment) {
				case 0: // Commute Selected
					distanceRangeSlider.MaxValue = TRConstant.SearchMinutes.Count() - 1;
					view_distance.Hidden = false;
					view_nationWide.Hidden = true;
					tbl_commuteItems.Hidden = false;
					ViewModelInstance.DistanceType = 1;
					break;
				case 1: // Distnace Selected
					distanceRangeSlider.MaxValue = TRConstant.SearchDistances.Count() - 1;
					view_distance.Hidden = false;
					view_nationWide.Hidden = true;
					tbl_commuteItems.Hidden = true;
					ViewModelInstance.DistanceType = 0;
					break;
				case 2: // Nation Wide Selected
					view_distance.Hidden = true;
					view_nationWide.Hidden = false;
					ViewModelInstance.DistanceType = 2;
					break;
				}
			};

			// init distance range slider
			distanceRangeSlider = new RangeSliderView (new CGRect (0, 0, this.view_rangeSlider.Frame.Width, this.view_rangeSlider.Frame.Height));
			distanceRangeSlider.MinValue = 0;
			distanceRangeSlider.MaxValue = TRConstant.SearchMinutes.Count();	// Default selection is commute
			distanceRangeSlider.LeftValueChanged += (nfloat value) => {
				ViewModelInstance.MinDistance = (int)distanceRangeSlider.LeftValue;
			};
			distanceRangeSlider.RightValueChanged += (nfloat value) => {
				ViewModelInstance.MaxDistance = (int)distanceRangeSlider.RightValue;
			};
			this.view_rangeSlider.AddSubview (distanceRangeSlider);

			// init suggestion visibility
			view_suggestionBack.Hidden = true;
			txt_address.EditingDidBegin += (object sender, EventArgs e) => {
				view_suggestionBack.Hidden = false;
			};

			txt_address.EditingDidEnd += (object sender, EventArgs e) => {
				view_suggestionBack.Hidden = true;
			};

			txt_address.EditingChanged += (object sender, EventArgs e) => {
				ViewModelInstance.UpdateLocationsCommand.Execute (txt_address.Text);
			};
		}

		// UICollectionView Delegate
		[Export ("collectionView:layout:sizeForItemAtIndexPath:")]
		public CGSize GetSizeForItem (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
		{
			return new CGSize ((collectionView.Frame.Width - 48) / 3, 30);
		}

		[Export ("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
		public nfloat GetMinimumInteritemSpacingForSection (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, System.nint section)
		{
			return 16.0f;
		}

		[Export ("collectionView:didSelectItemAtIndexPath:")]
		public void ItemSelected (UIKit.UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			ViewModelInstance.States [indexPath.Row].Selected = !ViewModelInstance.States [indexPath.Row].Selected;
		}

		public class LocationSuggestionTableViewSource : MvxTableViewSource
		{
			public LocationSuggestionTableViewSource (UITableView tv) : base (tv)
			{
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 30.0f;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				LocationSuggestionCell cell = (LocationSuggestionCell)tableView.DequeueReusableCell (LocationSuggestionCell.Identifier);
				return cell;
			}
		}

		public class SearchAreaCommuteItemsTableViewSource : MvxTableViewSource
		{
			SearchAreaModalView masterView;
			public SearchAreaCommuteItemsTableViewSource (SearchAreaModalView _masterView, UITableView tv) : base (tv)
			{
				masterView = _masterView;
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 50.0f;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				SearchAreaCommuteCell cell = (SearchAreaCommuteCell)tableView.DequeueReusableCell (SearchAreaCommuteCell.Identifier);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				masterView.ViewModelInstance.CommuteItems [indexPath.Row].Selected = !masterView.ViewModelInstance.CommuteItems [indexPath.Row].Selected;
			}
		}
	}
}
