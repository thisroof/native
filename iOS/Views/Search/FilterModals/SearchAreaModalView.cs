
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
using CoreLocation;
using Acr.UserDialogs;
using ThisRoofN.Models.Service;

namespace ThisRoofN.iOS
{
	public partial class SearchAreaModalView : BaseModalView, IUICollectionViewDelegateFlowLayout, IUICollectionViewDelegate, IUIGestureRecognizerDelegate
	{
		private CLLocationManager locationManager;
		private CLGeocoder geocoder;

		public SearchAreaModalView (IntPtr handle) : base (handle)
		{
		}

		public SearchAreaModalViewModel ViewModelInstance {
			get {
				return (SearchAreaModalViewModel)base.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitUI ();
			InitUserLocations ();

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
			bindingSet.Bind (seg_areaType).For ("SelectedIndex").To (vm => vm.DistanceTypeSegValue);
			bindingSet.Bind (nationsSource).To (vm => vm.States);
			bindingSet.Bind (locationSuggestionSource).To (vm => vm.AddressSuggestionItems);
			bindingSet.Bind (txt_address).To (vm => vm.Address);
			bindingSet.Bind (commuteSource).To (vm => vm.CommuteItems);
			bindingSet.Bind (btn_selectAll).To (vm => vm.SelectAllStatesCommand).CommandParameter (false);
			bindingSet.Bind (btn_clearAll).To (vm => vm.SelectAllStatesCommand).CommandParameter (true);

			bindingSet.Apply ();

			UITapGestureRecognizer endEditingTap = new UITapGestureRecognizer (() => {
				this.View.EndEditing (true);
			});
			endEditingTap.Delegate = this;

			this.view_distance.AddGestureRecognizer (endEditingTap);

			this.view_trans.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				ViewModelInstance.ModalCloseCommand.Execute (null);
			}));

			btn_getOwnLocation.TouchUpInside += (object sender, EventArgs e) => {
				if(CLLocationManager.Status == CLAuthorizationStatus.Authorized || 
					CLLocationManager.Status == CLAuthorizationStatus.AuthorizedAlways || 
					CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse) {
					UserDialogs.Instance.ShowLoading();
					locationManager.StartUpdatingLocation();
				} else {
					UserDialogs.Instance.Alert ("Please turn on location service in settings.", "Location Service OFF");
				}
			};
		}

		private void InitUserLocations() {

			locationManager = new CLLocationManager ();
			locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
//			locationManager.DistanceFilter = 1;
			geocoder = new CLGeocoder ();

			switch (CLLocationManager.Status) {
			case CLAuthorizationStatus.Authorized:
			case CLAuthorizationStatus.AuthorizedWhenInUse:

				break;
			case CLAuthorizationStatus.NotDetermined:
				this.locationManager.RequestWhenInUseAuthorization ();
				break;
			case CLAuthorizationStatus.Denied:
			case CLAuthorizationStatus.Restricted:
				UserDialogs.Instance.Alert ("Please turn on location service in settings.", "Location Service OFF");
				break;
			default:
				break;
			}

			locationManager.AuthorizationChanged += (object sender, CLAuthorizationChangedEventArgs e) => {
				if(e.Status == CLAuthorizationStatus.Authorized || 
					e.Status == CLAuthorizationStatus.AuthorizedAlways || 
					e.Status == CLAuthorizationStatus.AuthorizedWhenInUse) {
					this.locationManager.RequestWhenInUseAuthorization ();
					if(string.IsNullOrEmpty(ViewModelInstance.Address)) {
						UserDialogs.Instance.ShowLoading();
						locationManager.StartUpdatingLocation();
					}

				} else if(e.Status == CLAuthorizationStatus.NotDetermined) {
					this.locationManager.RequestWhenInUseAuthorization ();
				} else {
					UserDialogs.Instance.Alert ("Please turn on location service in settings.", "Location Service OFF");
				}
			};
				
			locationManager.LocationsUpdated += LocationUpdated;
			locationManager.LocationUpdatesPaused += (object sender, EventArgs e) => {
				UserDialogs.Instance.HideLoading();
			};
		}

		private async void LocationUpdated(object sender, CLLocationsUpdatedEventArgs e) {
			locationManager.StopUpdatingLocation();	
			UserDialogs.Instance.HideLoading();

			if (e.Locations.Length > 0) {
				ViewModelInstance.GetCurrentAddressCommand.Execute (new Location () {
					lat = e.Locations [0].Coordinate.Latitude,
					lng = e.Locations [0].Coordinate.Longitude
				});
			} else {
				UserDialogs.Instance.Alert ("Failed to get current location", "Error");
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			//Add Observer for keyboard event
			keyboardUpNotificationToken = NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.DidShowNotification, KeyboardUpNotification);
			keyboardDownNotificationToken = NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, KeyBoardDownNotification);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			//Remove Observer
			if (keyboardUpNotificationToken != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver (keyboardUpNotificationToken);
			if (keyboardDownNotificationToken != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver (keyboardDownNotificationToken);
		}

		private void InitUI ()
		{
			switch_city.Transform = CGAffineTransform.MakeScale(0.75f, 0.75f);
			switch_metro.Transform = CGAffineTransform.MakeScale(0.75f, 0.75f);
			switch_rural.Transform = CGAffineTransform.MakeScale(0.75f, 0.75f);
			switch_suburb.Transform = CGAffineTransform.MakeScale(0.75f, 0.75f);

			switch_rural.On = false;

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
			case (int)TRSearchType.Distance:
				lbl_distanceMark.Text = "Distance";
				view_distance.Hidden = false;
				view_nationWide.Hidden = true;
				slider_distance.MaxValue = TRConstant.SearchDistances.Count () - 1;
				break;
			case (int)TRSearchType.Commute:
				lbl_distanceMark.Text = "Commute";
				view_distance.Hidden = false;
				view_nationWide.Hidden = true;
				slider_distance.MaxValue = TRConstant.SearchMinutes.Count () - 1;
				break;
			case (int)TRSearchType.State: // States
				view_distance.Hidden = true;
				view_nationWide.Hidden = false;
				break;
			default:
				break;
			}

			seg_areaType.ValueChanged += (object sender, EventArgs e) => {
				txt_address.ResignFirstResponder ();
				switch (seg_areaType.SelectedSegment) {
				case 0: // Commute Selected
					lbl_distanceMark.Text = "Commute";
					slider_distance.MaxValue = TRConstant.SearchMinutes.Count () - 1;
					view_distance.Hidden = false;
					view_nationWide.Hidden = true;
					tbl_commuteItems.Hidden = false;
					break;
				case 1: // Distnace Selected
					lbl_distanceMark.Text = "Distance";
					slider_distance.MaxValue = TRConstant.SearchDistances.Count () - 1;
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

		private void KeyboardUpNotification (NSNotification notification)
		{
			if (moveViewUp)
				return;

			//Calculate how fore we need to scroll
			scroll_amount = img_title.Frame.Height;

			//Perform the scrolling
			if (scroll_amount > 0) {
				moveViewUp = true;
				ScrollTheView (moveViewUp);
			} else {
				moveViewUp = false;
			}
		}

		[Export ("gestureRecognizer:shouldReceiveTouch:")]
		public bool ShouldReceiveTouch (UIKit.UIGestureRecognizer recognizer, UIKit.UITouch touch)
		{
			if (touch.View.IsDescendantOfView (tbl_suggestion)) {
				return false;
			}
			return true;
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
				tableView.DeselectRow (indexPath, true);

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
