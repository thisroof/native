using System;
using ThisRoofN.ViewModels;
using Android.OS;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using ThisRoofN.Droid.Helpers;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace ThisRoofN.Droid
{
	public class SearchResultDetailView : BaseMvxFragment, IOnMapReadyCallback
	{
		SupportMapFragment mapFragment;
		public SearchResultDetailViewModel ViewModelInstance {
			get {
				return (SearchResultDetailViewModel)base.ViewModel;
			}
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			base.OnCreateView (inflater, container, savedInstanceState);

			View view = this.BindingInflate (Resource.Layout.fragment_search_result_detail, null);

			ImageView iv_back = view.FindViewById<ImageView> (Resource.Id.img_back);
			iv_back.Click += (object sender, EventArgs e) => {
				// Create your fragment here
//				ChildFragmentManager.BeginTransaction().Remove(mapFragment).Commit();

				ViewModelInstance.CloseCommand.Execute(null);
			};

			ImageView iv_like = view.FindViewById<ImageView> (Resource.Id.img_like);
			ImageView iv_dislike = view.FindViewById<ImageView> (Resource.Id.img_dislike);
			iv_like.Click += (object sender, EventArgs e) => {
				ViewModelInstance.LikeCommand.Execute(null);
			};

			iv_dislike.Click += (object sender, EventArgs e) => {
				ViewModelInstance.DisLikeCommand.Execute(null);
			};

			Button btn_map = view.FindViewById<Button> (Resource.Id.btn_map);
			btn_map.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoMap.Execute(null);
			};

			ImageView iv_nextImage = (ImageView)view.FindViewById<ImageView> (Resource.Id.iv_nextImage);
			iv_nextImage.Click += (object sender, EventArgs e) => {
				ViewModelInstance.NextImageCommand.Execute(true);
			};

			TextView tv_prevProp = (TextView)view.FindViewById<TextView> (Resource.Id.txt_prevProperty);
			tv_prevProp.Click += (object sender, EventArgs e) => {
				ViewModelInstance.NextPropertyCommand.Execute(false);
			};

			TextView tv_nextProp = (TextView)view.FindViewById<TextView> (Resource.Id.txt_nextProperty);
			tv_nextProp.Click += (object sender, EventArgs e) => {
				ViewModelInstance.NextPropertyCommand.Execute(true);
			};

			mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById (Resource.Id.small_map);
			mapFragment.GetMapAsync (this);
			mapFragment.View.Enabled = false;
			mapFragment.View.Clickable = false;

			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.None);
		}

		public override void OnDestroyView ()
		{
			base.OnDestroyView ();
			ChildFragmentManager.BeginTransaction ().Remove (ChildFragmentManager.FindFragmentById (Resource.Id.small_map)).CommitAllowingStateLoss ();
		}

		public void OnMapReady (GoogleMap googleMap)
		{
			googleMap.UiSettings.SetAllGesturesEnabled (false);
			googleMap.MapType = GoogleMap.MapTypeNormal;

			LatLngBounds.Builder builder = new LatLngBounds.Builder ();

			builder.Include (new LatLng (36.7783, -119.4179));
			MarkerOptions markerOpt1 = new MarkerOptions ();
			markerOpt1.SetPosition (new LatLng (36.7783, -119.4179));
			googleMap.AddMarker (markerOpt1);	

			LatLngBounds bounds = builder.Build ();

			int padding = 5;
			CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds (bounds, 100, 100, padding);
			googleMap.MoveCamera (cu);
		}
	}
}

