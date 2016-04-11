
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
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Android.Graphics.Drawables;
using ThisRoofN.ViewModels;
using Android.Support.V4.View;
using ThisRoofN.Droid.CustomControls.TRSlidingTab;
using ThisRoofN.Droid.Adapters;

namespace ThisRoofN.Droid
{
	public class SearchAreaModalView : MvxDialogFragment
	{
		ViewPager locationPager;
		TRSlidingTabLayout tabs;
		SearchLocationTabAdapter tabAdapter;

		public SearchAreaModalView() 
		{
			this.SetStyle (MvxDialogFragment.StyleNoTitle, Resource.Style.SearchModalStyle);
		}

		public SearchAreaModalViewModel ViewModelInstance
		{
			get {
				return (SearchAreaModalViewModel)base.ViewModel;
			}
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			Dialog.Window.SetBackgroundDrawable (new ColorDrawable (Android.Graphics.Color.Transparent));

			View view = this.BindingInflate (Resource.Layout.fragment_search_modal_location, null);


			// Set modal back events
			LinearLayout modal_back = (LinearLayout)view.FindViewById (Resource.Id.layout_modal_back);
			modal_back.Click += (object sender, EventArgs e) => {
				ViewModelInstance.ModalCloseCommand.Execute(null);
			};

			ImageView img_back = (ImageView)view.FindViewById (Resource.Id.btn_back);
			img_back.Click += (object sender, EventArgs e) => {
				ViewModelInstance.ModalCloseCommand.Execute(null);
			};


			// Set Location Type tab and pager
			locationPager = view.FindViewById<ViewPager> (Resource.Id.pager_location);
			tabs = view.FindViewById<TRSlidingTabLayout> (Resource.Id.tab_location);

			locationPager.PageSelected += (object sender, ViewPager.PageSelectedEventArgs e) => {
				
			};

			tabs = view.FindViewById<TRSlidingTabLayout> (Resource.Id.tab_location);
			tabs.TabViewTextSizeSp = 12;
			tabs.TabViewPaddingDips = 8;
			tabs.TabTitleTextSize = 12;

			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.DialogAnimation;

			var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo> {
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(LocationCommuteTabItemView),
					Title = "COMMUTE",
					ViewModel = this.ViewModelInstance
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(LocationDistanceTabItemView),
					Title = "DISTANCE",
					ViewModel = this.ViewModelInstance
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(LocationNationTabItemView),
					Title = "NATION WIDE",
					ViewModel = this.ViewModelInstance
				},
			};

			tabAdapter = new SearchLocationTabAdapter (this.ChildFragmentManager, fragments);
			locationPager.Adapter = tabAdapter;
			locationPager.Adapter.NotifyDataSetChanged ();

			tabs.SetDistributeEvenly (true);
			tabs.SetViewPager (locationPager);
		}

		public override void OnResume ()
		{
			base.OnResume ();
			Dialog.Window.SetLayout (RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);

		}
	}
}

