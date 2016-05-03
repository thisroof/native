using System;
using ThisRoofN.ViewModels;
using Android.OS;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using ThisRoofN.Droid.Helpers;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Webkit;
using Android.Graphics;

namespace ThisRoofN.Droid
{
	public class SearchResultDetailMapViewView : BaseMvxFragment, IOnMapReadyCallback
	{
		SupportMapFragment mapFragment;
		GoogleMap map;

		public SearchResultDetailMapViewModel ViewModelInstance {
			get {
				return (SearchResultDetailMapViewModel)base.ViewModel;
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

			View view = this.BindingInflate (Resource.Layout.fragment_search_result_detail_map, null);

			WebView gifWebView = view.FindViewById<WebView> (Resource.Id.gifWebView);
			if (gifWebView != null) {
				gifWebView.LoadUrl ("file:///android_res/raw/loading_animator.gif");
				gifWebView.SetBackgroundColor (Color.Transparent);
				gifWebView.SetLayerType (LayerType.Software, null);
			}

			ImageView iv_back = view.FindViewById<ImageView> (Resource.Id.img_back);
			iv_back.Click += (object sender, EventArgs e) => {
				// Create your fragment here
				ViewModelInstance.CloseCommand.Execute (null);
			};

			mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById (Resource.Id.detail_map);
			mapFragment.GetMapAsync (this);

			TextView tv_satellite = view.FindViewById<TextView> (Resource.Id.txt_satellite);
			TextView tv_hybrid = view.FindViewById<TextView> (Resource.Id.txt_hybrid);
			TextView tv_street = view.FindViewById<TextView> (Resource.Id.txt_street);

			tv_satellite.Click += (object sender, EventArgs e) => {
				if (map != null) {
					tv_satellite.SetTextColor (Resources.GetColor (Resource.Color.white));
					tv_hybrid.SetTextColor (Resources.GetColor (Resource.Color.white_50));
					tv_street.SetTextColor (Resources.GetColor (Resource.Color.white_50));
					map.MapType = GoogleMap.MapTypeSatellite;
				}

			};

			tv_hybrid.Click += (object sender, EventArgs e) => {
				if (map != null) {
					tv_satellite.SetTextColor (Resources.GetColor (Resource.Color.white_50));
					tv_hybrid.SetTextColor (Resources.GetColor (Resource.Color.white));
					tv_street.SetTextColor (Resources.GetColor (Resource.Color.white_50));

					map.MapType = GoogleMap.MapTypeHybrid;
				}

			};

			tv_street.Click += (object sender, EventArgs e) => {
				if (map != null) {
					tv_satellite.SetTextColor (Resources.GetColor (Resource.Color.white_50));
					tv_hybrid.SetTextColor (Resources.GetColor (Resource.Color.white_50));
					tv_street.SetTextColor (Resources.GetColor (Resource.Color.white));
					map.MapType = GoogleMap.MapTypeNormal;
				}
			};

			tv_satellite.SetTextColor (Resources.GetColor (Resource.Color.white_50));
			tv_hybrid.SetTextColor (Resources.GetColor (Resource.Color.white_50));
			tv_street.SetTextColor (Resources.GetColor (Resource.Color.white));

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
			ChildFragmentManager.BeginTransaction ().Remove (ChildFragmentManager.FindFragmentById (Resource.Id.detail_map)).CommitAllowingStateLoss ();
		}
			

		public void OnMapReady (GoogleMap googleMap)
		{
			map = googleMap;

			googleMap.MapType = GoogleMap.MapTypeNormal;

			MarkerOptions markerOpt1 = new MarkerOptions ();
			markerOpt1.SetPosition (new LatLng (ViewModelInstance.Latitude, ViewModelInstance.Longitude));
			markerOpt1.SetTitle (ViewModelInstance.Address);
			map.AddMarker (markerOpt1);	


			CameraPosition.Builder builder = CameraPosition.InvokeBuilder ();
			builder.Target (new LatLng (ViewModelInstance.Latitude, ViewModelInstance.Longitude));
			builder.Zoom (15);
			CameraPosition cameraPosition = builder.Build ();

			CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition (cameraPosition);
			map.MoveCamera (cameraUpdate);
		}
	}
}

