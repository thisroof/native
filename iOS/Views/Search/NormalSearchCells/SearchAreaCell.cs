// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using CoreGraphics;
using System.Linq;

namespace ThisRoofN.iOS
{
	public partial class SearchAreaCell : UITableViewCell, ISearchCell, IUICollectionViewDelegateFlowLayout, IUICollectionViewDelegate
	{
		private bool expanded;
		public static string Identifier = "SearchAreaCell";
		private nfloat cellHeight;
		private NormalSearchViewController masterView;


		public SearchAreaCell (IntPtr handle) : base (handle)
		{
		}

		public nfloat CellHeight {
			get {
				return cellHeight;
			}
		}

		public void BindData (NormalSearchViewController _masterView)
		{
			this.masterView = _masterView;

			switch (_masterView.ViewModelInstance.DistanceType) {
			case 0: // Distance
				seg_areaType.SelectedSegment = 1;
				view_distance.Hidden = false;
				view_nation.Hidden = true;
				break;
			case 1: // Commute
				seg_areaType.SelectedSegment = 0;
				view_distance.Hidden = false;
				view_nation.Hidden = true;
				break;
			case 2: // States
				seg_areaType.SelectedSegment = 2;
				view_distance.Hidden = true;
				view_nation.Hidden = false;
				break;
			default:
				break;
			}

			InitUI ();

			// Init the State Collection View
			var propertyTypeSource = new MvxCollectionViewSource (cv_nations, new NSString ("SearchAreaCheckboxCVCell"));
			cv_nations.AllowsSelection = true;
			cv_nations.Source = propertyTypeSource;
			cv_nations.Delegate = this;
			_masterView.BindingSet.Bind (propertyTypeSource).To (vm => vm.States);
			_masterView.BindingSet.Bind (lbl_distance).To (vm => vm.DistanceLabelText);
		}

		public void HandleExpandTap ()
		{
			expanded = !expanded;

			masterView.MasterTableView.BeginUpdates ();
			if (expanded) {
				img_expandMark.Image = UIImage.FromBundle ("icon_arrow_blue_down");

				switch (seg_areaType.SelectedSegment) {
				case 0: // Commute
					cellHeight = view_distance.Frame.Bottom;
					break;
				case 1: // Distance
					cellHeight = view_distance.Frame.Bottom - 16 - seg_driving.Frame.Height * 2;
					break;
				case 2:
					cellHeight = view_nation.Frame.Bottom;
					break;
				}
			} else {
				img_expandMark.Image = UIImage.FromBundle ("icon_arrow_blue_right");
				cellHeight = view_cellTitle.Frame.Bottom;
			}
			masterView.MasterTableView.EndUpdates ();
		}

		private void InitUI ()
		{
			UITapGestureRecognizer expandTap = new UITapGestureRecognizer (HandleExpandTap);
			view_cellTitle.UserInteractionEnabled = true;
			view_cellTitle.RemoveGestureRecognizer (expandTap);
			view_cellTitle.AddGestureRecognizer (expandTap);

			view_autocomplete.Layer.BorderColor = UIColor.LightGray.CGColor;
			view_autocomplete.Layer.BorderWidth = 1;
			view_locations.Layer.BorderColor = UIColor.LightGray.CGColor;
			view_locations.Layer.BorderWidth = 1;

			cellHeight = view_cellTitle.Frame.Bottom;

			seg_areaType.ValueChanged += (object sender, EventArgs e) => {
				masterView.MasterTableView.BeginUpdates ();

				switch (seg_areaType.SelectedSegment) {
				case 0: // Distance
					slider_distance.MinValue = 0; 
					slider_distance.MaxValue = TRConstant.SearchMinutes.Count();
					slider_distance.Value = 5;
					view_distance.Hidden = false;
					view_nation.Hidden = true;
					cellHeight = view_distance.Frame.Bottom;
					masterView.ViewModelInstance.DistanceType = 1;
					break;
				case 1: // Commute
					slider_distance.MinValue = 0; 
					slider_distance.MaxValue = TRConstant.SearchDistances.Count();
					slider_distance.Value = 0;
					view_distance.Hidden = false;
					view_nation.Hidden = true;
					cellHeight = view_distance.Frame.Bottom - 16 - seg_driving.Frame.Height * 2;
					masterView.ViewModelInstance.DistanceType = 0;
					break;
				case 2: // Nation Wide
					view_distance.Hidden = true;
					view_nation.Hidden = false;
					cellHeight = view_nation.Frame.Bottom + 8;
					masterView.ViewModelInstance.DistanceType = 2;
					break;
				}
				masterView.MasterTableView.EndUpdates ();
			};


			slider_distance.Value = 0;
			slider_distance.ValueChanged += (object sender, EventArgs e) => {
				if(seg_areaType.SelectedSegment == 0) {
					masterView.ViewModelInstance.SearchTimeDisatnce = (int)slider_distance.Value;
				} else {
					masterView.ViewModelInstance.SearchMileDistance = (int)slider_distance.Value;
				}
			};

			seg_driving.ValueChanged += (object sender, EventArgs e) => {
				masterView.ViewModelInstance.TravelMode = (int)seg_driving.SelectedSegment;
			};

			seg_traffic.ValueChanged += (object sender, EventArgs e) => {
				masterView.ViewModelInstance.TrafficType = (int)seg_driving.SelectedSegment;
			};

			view_locations.Hidden = true;
			txt_address.EditingDidBegin += (object sender, EventArgs e) => {
				view_locations.Hidden = false;
			};

			txt_address.EditingDidEnd += (object sender, EventArgs e) => {
				view_locations.Hidden = true;
			};

			txt_address.EditingChanged += (object sender, EventArgs e) => {
				masterView.ViewModelInstance.UpdateLocationsCommand.Execute (txt_address.Text);
			};

			// Bind Locatin Suggesion Table
			var source = new LocationSuggestionTableViewSource (tbl_locations);
			tbl_locations.Source = source;
			tbl_locations.RowHeight = UITableView.AutomaticDimension;
			tbl_locations.AllowsSelection = false;
			tbl_locations.TableFooterView = new UITableView (CGRect.Empty);
			tbl_locations.ReloadData ();
			masterView.BindingSet.Bind (source).To (vm => vm.AddressSuggestionItems);
		}

		// UICollectionView Delegate
		[Export ("collectionView:layout:sizeForItemAtIndexPath:")]
		public CoreGraphics.CGSize GetSizeForItem (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
		{
			return new CGSize ((collectionView.Frame.Width - 40) / 4, 20);
		}

		[Export ("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
		public System.nfloat GetMinimumInteritemSpacingForSection (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, System.nint section)
		{
			return 8.0f;
		}

		[Export ("collectionView:didSelectItemAtIndexPath:")]
		public void ItemSelected (UIKit.UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			masterView.ViewModelInstance.States [indexPath.Row].Selected = !masterView.ViewModelInstance.States [indexPath.Row].Selected;
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
	}
}
