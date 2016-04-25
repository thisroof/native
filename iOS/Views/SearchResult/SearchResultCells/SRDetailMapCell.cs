// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MapKit;
using CoreLocation;
using ThisRoofN.Helpers;
using ThisRoofN.iOS.ValueConverters;

namespace ThisRoofN.iOS
{
	public partial class SRDetailMapCell : UITableViewCell
	{
		public static string Identifier = "SRDetailMapCell";

		private SearchResultDetailView masterView;
		private nfloat cellHeight;

		private bool mapEnabled;

		public SRDetailMapCell (IntPtr handle) : base (handle)
		{
		}

		public nfloat CellHeight 
		{
			get
			{
				return cellHeight;
			}
		}

		public void BindData(SearchResultDetailView _masterView)
		{
			this.masterView = _masterView;

//			masterView.BindingSet.Bind (img_like).To (vm => vm.Liked).WithConversion(new LikemarkConverter());
//			masterView.BindingSet.Bind (img_dislike).To (vm => vm.Disliked).WithConversion(new DislikemarkConverter());

			InitUI ();
		}

		private void InitUI()
		{
			cellHeight = (UIScreen.MainScreen.Bounds.Height - 60) / 2;

//			TRDetailItemAnnotation annotation = new TRDetailItemAnnotation (
//				masterView.ViewModelInstace.ItemDetail.Latitude, 
//				masterView.ViewModelInstace.ItemDetail.Longitude, 
//				masterView.ViewModelInstace.ItemDetail.Address.FullStreetAddress);
//			map_result.AddAnnotation (annotation);

			map_result.ZoomEnabled = true;
			map_result.UserInteractionEnabled = false;
			mapEnabled = false;

			map_result.SetRegion(new MKCoordinateRegion(
				new CLLocationCoordinate2D(
					masterView.ViewModelInstace.ItemDetail.Latitude, 
					masterView.ViewModelInstace.ItemDetail.Longitude),
				new MKCoordinateSpan(
					LocationHelper.KilometersToLatitudeDegrees(2),
					LocationHelper.KilometersToLongitudeDegrees(2, masterView.ViewModelInstace.ItemDetail.Latitude)
				)), true);

			// set map lock gesture
			UITapGestureRecognizer mapLockTap = new UITapGestureRecognizer (() => {
				if(mapEnabled) {
					img_mapLock.Image = UIImage.FromBundle("icon_map_locked");
					map_result.UserInteractionEnabled = false;
				} else {
					img_mapLock.Image = UIImage.FromBundle("icon_map_unlocked");
					map_result.UserInteractionEnabled = true;
				}

				mapEnabled = !mapEnabled;
			});

			img_mapLock.UserInteractionEnabled = true;
			img_mapLock.AddGestureRecognizer (mapLockTap);

			// set like and dislike tap gesture
			UITapGestureRecognizer likeGesture = new UITapGestureRecognizer (() => {
				masterView.ViewModelInstace.LikeCommand.Execute(null);
			});
			UITapGestureRecognizer dislikeGesture = new UITapGestureRecognizer (() => {
				masterView.ViewModelInstace.ShowDislikeViewCommand.Execute(true);
			});

			img_like.UserInteractionEnabled = true;
			img_like.AddGestureRecognizer (likeGesture);

			img_dislike.UserInteractionEnabled = true;
			img_dislike.AddGestureRecognizer (dislikeGesture);
		}
	}
}
