
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
using Android.Support.V4.View;
using ThisRoofN.Droid.CustomControls.TRSlidingTab;
using Android.Views.InputMethods;
using ThisRoofN.Droid.Adapters;
using ThisRoofN.ViewModels;
using ThisRoofN.Droid.Helpers;

namespace ThisRoofN.Droid
{
	public class SearchResultHomeView : BaseMvxFragment
	{
		ViewPager type_pager;
		TRIconSlidingTabLayout type_tab;

		public SearchResultHomeViewModel ViewModelInstance {
			get {
				return (SearchResultHomeViewModel)base.ViewModel;
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

			View view = this.BindingInflate (Resource.Layout.fragment_search_result_home, null);

			// Set Location Type tab and pager
			type_pager = view.FindViewById<ViewPager> (Resource.Id.pager_location);
			type_pager.PageSelected += (object sender, ViewPager.PageSelectedEventArgs e) => {
				//				switch (e.Position) {
				//				case 0:
				//					ViewModelInstance.DistanceType = (int)TRSearchType.Commute;
				//					break;
				//				case 1:
				//					ViewModelInstance.DistanceType = (int)TRSearchType.Distance;
				//					break;
				//				case 2:
				//					ViewModelInstance.DistanceType = (int)TRSearchType.State;
				//					break;
				//				default:
				//					break;
				//				}
			};

			type_tab = view.FindViewById<TRIconSlidingTabLayout> (Resource.Id.tab_type);
			type_tab.IconResourceArray = new int[] { Resource.Drawable.icon_tile, Resource.Drawable.icon_list, Resource.Drawable.icon_map };

			type_tab.TabViewTextSizeSp = 12;
			type_tab.TabViewPaddingDips = 8;
			type_tab.TabTitleTextSize = 12;

			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo> {
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(SearchResultTileView),
					Title = "TILE",
					ViewModel = ViewModelInstance.TileViewModel
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(SearchResultListView),
					Title = "LIST",
					ViewModel = ViewModelInstance.ListViewModel
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(SearchResultMapView),
					Title = "MAP",
					ViewModel = ViewModelInstance.MapViewModel
				},
			};

			MvxViewPagerFragmentAdapter tabAdapter = new MvxViewPagerFragmentAdapter (Activity, this.ChildFragmentManager, fragments);
			type_pager.Adapter = tabAdapter;
			type_pager.Adapter.NotifyDataSetChanged ();

			type_tab.SetDistributeEvenly (true);
			type_tab.SetViewPager (type_pager);
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.Visible);
		}
	}
}

