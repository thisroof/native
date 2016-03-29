
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
using ThisRoofN.Models.App;

namespace ThisRoofN.iOS
{
	public partial class SearchAreaModalView : BaseModalView, IUICollectionViewDelegateFlowLayout, IUICollectionViewDelegate
	{
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

			InitUI ();

			// Init the State Collection View
			var nationsSource = new MvxCollectionViewSource (cv_nations, new NSString ("SearchAreaCheckboxCVCell"));
			cv_nations.AllowsSelection = true;
			cv_nations.Source = nationsSource;
			cv_nations.Delegate = this;

			// Bind Locatin Suggesion Table
			var locationSuggestionSource = new LocationSuggestionTableViewSource (this, tbl_suggestion);
			tbl_suggestion.Source = locationSuggestionSource;
			tbl_suggestion.RowHeight = UITableView.AutomaticDimension;
			tbl_suggestion.TableFooterView = new UITableView (CGRect.Empty);
			tbl_suggestion.ReloadData ();

			// Bind Item Tableview source
			var commuteSource = new SearchAreaCommuteItemsTableViewSource (this, tbl_commuteItems);
			tbl_commuteItems.Source = commuteSource;
			tbl_commuteItems.RowHeight = UITableView.AutomaticDimension;
			tbl_commuteItems.TableFooterView = new UITableView (CGRect.Empty);
			tbl_commuteItems.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			tbl_commuteItems.ReloadData ();

			var bindingSet = this.CreateBindingSet<SearchAreaModalView, SearchAreaModalViewModel> ();
			bindingSet.Bind (btn_modalBack).To (vm => vm.ModalCloseCommand);
			bindingSet.Bind (lbl_distanceRange).To (vm => vm.DistanceLabelText);
			bindingSet.Bind (slider_distance).To (vm => vm.Distance);
			bindingSet.Bind (seg_areaType).For("SelectedIndex").To (vm => vm.DistanceTypeSegValue);
			bindingSet.Bind (nationsSource).To (vm => vm.States);
			bindingSet.Bind (locationSuggestionSource).To (vm => vm.AddressSuggestionItems);
			bindingSet.Bind (txt_address).To (vm => vm.Address);
			bindingSet.Bind (commuteSource).To (vm => vm.CommuteItems);
			bindingSet.Bind (btn_selectAll).To (vm => vm.SelectAllStatesCommand).CommandParameter (false);
			bindingSet.Bind (btn_clearAll).To (vm => vm.SelectAllStatesCommand).CommandParameter (true);

			bindingSet.Apply ();

			this.view_distance.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.View.EndEditing(true);
			}));

			this.view_trans.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				ViewModelInstance.CloseCommand.Execute(null);
			}));
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			//Add Observer for keyboard event
			keyboardUpNotificationToken = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardUpNotification);
			keyboardDownNotificationToken = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			//Remove Observer
			if (keyboardUpNotificationToken != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardUpNotificationToken);
			if (keyboardDownNotificationToken != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardDownNotificationToken);
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

			slider_distance.MinValue = 0;

			switch (ViewModelInstance.DistanceType) {
			case 0: // Distance
				view_distance.Hidden = false;
				view_nationWide.Hidden = true;
				slider_distance.MaxValue = TRConstant.SearchDistances.Count() - 1;
				break;
			case 1: // Commute
				view_distance.Hidden = false;
				view_nationWide.Hidden = true;
				slider_distance.MaxValue = TRConstant.SearchMinutes.Count() - 1;
				break;
			case 2: // States
				view_distance.Hidden = true;
				view_nationWide.Hidden = false;
				break;
			default:
				break;
			}

			seg_areaType.ValueChanged += (object sender, EventArgs e) => {
				txt_address.ResignFirstResponder();
				switch (seg_areaType.SelectedSegment) {
				case 0: // Commute Selected
					slider_distance.MaxValue = TRConstant.SearchMinutes.Count() - 1;
					view_distance.Hidden = false;
					view_nationWide.Hidden = true;
					tbl_commuteItems.Hidden = false;
					break;
				case 1: // Distnace Selected
					slider_distance.MaxValue = TRConstant.SearchDistances.Count() - 1;
					view_distance.Hidden = false;
					view_nationWide.Hidden = true;
					tbl_commuteItems.Hidden = true;
					break;
				case 2: // Nation Wide Selected
					view_distance.Hidden = true;
					view_nationWide.Hidden = false;
					break;
				}
			};

			// init suggestion visibility
			view_suggestionBack.Hidden = true;
			txt_address.EditingDidBegin += (object sender, EventArgs e) => {
				view_suggestionBack.Hidden = false;
			};

			txt_address.EditingDidEnd += (object sender, EventArgs e) => {
				view_suggestionBack.Hidden = true;
			};

			txt_address.ShouldReturn += (textField) => {
				txt_address.ResignFirstResponder ();
				view_suggestionBack.Hidden = true;
				return true;
			};

			txt_address.EditingChanged += (object sender, EventArgs e) => {
				ViewModelInstance.UpdateLocationsCommand.Execute (txt_address.Text);
			};
		}

		private void KeyboardUpNotification(NSNotification notification) {
			if (moveViewUp)
				return;

			//Calculate how fore we need to scroll
			scroll_amount = img_title.Frame.Height;

			//Perform the scrolling
			if (scroll_amount > 0)
			{
				moveViewUp = true;
				ScrollTheView(moveViewUp);
			}
			else
			{
				moveViewUp = false;
			}
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
			SearchAreaModalView masterView;
			public LocationSuggestionTableViewSource (SearchAreaModalView _masterview, UITableView tv) : base (tv)
			{
				masterView = _masterview;
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

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow(indexPath, true);

				TRGoogleMapPlace place = masterView.ViewModelInstance.AddressSuggestionItems [indexPath.Row];
				masterView.ViewModelInstance.Address = place.FullAddress;
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
//				masterView.ViewModelInstance.CommuteItems [indexPath.Row].Selected = !masterView.ViewModelInstance.CommuteItems [indexPath.Row].Selected;
			}
		}
	}
}
