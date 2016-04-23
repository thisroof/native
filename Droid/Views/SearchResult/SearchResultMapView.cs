
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using ThisRoofN.ViewModels;
using ThisRoofN.Models.App;

namespace ThisRoofN.Droid
{
	public class SearchResultMapView : BaseMvxFragment, IOnMapReadyCallback, GoogleMap.IOnInfoWindowClickListener, GoogleMap.IOnMapLoadedCallback
	{
		private GoogleMap map;
		LatLngBounds pinBounds;
		public LayoutInflater mInflater;
		public Dictionary<string, string> imageDic;
		public Dictionary<string, string> idDic;

		public SearchResultMapViewModel ViewModelInstance
		{
			get {
				return (SearchResultMapViewModel)base.ViewModel;
			}
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);


			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			base.OnCreateView (inflater, container, savedInstanceState);

			this.mInflater = inflater;

			View view = this.BindingInflate (Resource.Layout.fragment_search_result_map, null);

			SetupMapIfNeeded ();

			return view;
		}

		public override void OnDestroyView ()
		{
			base.OnDestroyView ();
			ChildFragmentManager.BeginTransaction ().Remove (ChildFragmentManager.FindFragmentById(Resource.Id.map)).CommitAllowingStateLoss ();
		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);

			if (map != null) {
				SetupMap ();
			} else {
				SetupMapIfNeeded ();
			}
				
		}

		private void SetupMapIfNeeded() {
			if (map == null) {
				SupportMapFragment mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById (Resource.Id.map);
				mapFragment.GetMapAsync (this);
			}
		}

		public void OnMapReady (GoogleMap googleMap)
		{
			map = googleMap;
			SetupMap ();
		}

		private void SetupMap() {
			map.MapType = GoogleMap.MapTypeNormal;
			map.SetOnInfoWindowClickListener (this);

			map.SetInfoWindowAdapter(new CustomInfoWindowAdapter(this));


			imageDic = new Dictionary<string, string> ();
			idDic = new Dictionary<string, string> ();

			LatLngBounds.Builder builder = new LatLngBounds.Builder ();
			if (ViewModelInstance.MapItems != null && ViewModelInstance.MapItems.Count > 0) {
				foreach (TRCottageSimple item in ViewModelInstance.MapItems) {
					builder.Include(new LatLng(item.Latitude, item.Longitude));
					MarkerOptions markerOpt1 = new MarkerOptions ();
					markerOpt1.SetPosition (new LatLng (item.Latitude, item.Longitude));
					markerOpt1.SetTitle (item.Title);
					markerOpt1.SetSnippet (item.FormattedPrice);
					Marker marker = map.AddMarker (markerOpt1);	

					if (imageDic.ContainsKey (item.Title)) {
						imageDic.Remove (item.Title);
					}

					if (idDic.ContainsKey (item.Title)) {
						idDic.Remove (item.Title);
					}

					imageDic.Add (item.Title, item.PrimaryPhotoLink);
					idDic.Add (item.Title, item.CottageID);
				}
			}

			CameraPosition.Builder cBuilder = CameraPosition.InvokeBuilder ();
			cBuilder.Target (new LatLng (ViewModelInstance.MapItems[0].Latitude, ViewModelInstance.MapItems[0].Longitude));
			cBuilder.Zoom (5);
			CameraPosition cameraPosition = cBuilder.Build ();

			CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition (cameraPosition);
			map.MoveCamera (cameraUpdate);

			pinBounds = builder.Build ();

			map.SetOnMapLoadedCallback (this);
		}

		public void OnMapLoaded () {
			int padding = 50;
			CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds (pinBounds, padding);
			map.MoveCamera (cu);
		}


		public void OnInfoWindowClick (Marker marker) 
		{
			if (idDic.ContainsKey (marker.Title)) {
				ViewModelInstance.DetailCommand.Execute (idDic[marker.Title]);
			}
		}

		class CustomInfoWindowAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter {
			SearchResultMapView masterview;

			public CustomInfoWindowAdapter(SearchResultMapView _masterView)
			{
				masterview = _masterView;
			}

			public View GetInfoContents(Marker marker1) {
				return null;
			}

			public View GetInfoWindow(Marker marker) {
				var customPopup = masterview.mInflater.Inflate (Resource.Layout.item_marker_window, null);

				ImageView iv_primary = customPopup.FindViewById<ImageView> (Resource.Id.iv_primary);
				TextView tv_title = customPopup.FindViewById<TextView> (Resource.Id.txt_title);
				TextView tv_price = customPopup.FindViewById<TextView> (Resource.Id.txt_price);

				if (masterview.imageDic.ContainsKey (marker.Title)) {
					string imageLink = masterview.imageDic [marker.Title];
					Koush.UrlImageViewHelper.SetUrlDrawable (iv_primary, imageLink, Resource.Drawable.img_placeholder_small);
				}

				tv_title.Text = marker.Title;
				tv_price.Text = marker.Snippet;

				return customPopup;
			}
		}
	}
}

