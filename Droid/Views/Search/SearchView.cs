
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
using RangeSlider;
using ThisRoofN.Droid.Helpers;
using ThisRoofN.ViewModels;

namespace ThisRoofN.Droid
{
	public class SearchView : BaseMvxFragment
	{
		SquareRelativeLayout layout_location;
		SquareRelativeLayout layout_lifestyle;
		SquareRelativeLayout layout_inArea;
		SquareRelativeLayout layout_homeDetail;
		SquareRelativeLayout layout_inHome;
		SquareRelativeLayout layout_savedHome;

		public SearchViewModel ViewModelInstance
		{
			get {
				return (SearchViewModel)base.ViewModel;
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
			View view = this.BindingInflate (Resource.Layout.fragment_search_main, null);

			layout_location = view.FindViewById<SquareRelativeLayout> (Resource.Id.layout_location);
			layout_lifestyle = view.FindViewById<SquareRelativeLayout> (Resource.Id.layout_lifeStyle);
			layout_inArea = view.FindViewById<SquareRelativeLayout> (Resource.Id.layout_inArea);
			layout_homeDetail = view.FindViewById<SquareRelativeLayout> (Resource.Id.layout_homeDetails);
			layout_inHome = view.FindViewById<SquareRelativeLayout> (Resource.Id.layout_inHome);
			layout_savedHome = view.FindViewById<SquareRelativeLayout> (Resource.Id.layout_savedHome);

			layout_location.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoModalCommand.Execute(ThisRoofN.ViewModels.SearchViewModel.ModalType.SearchArea);
			};

			layout_lifestyle.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoModalCommand.Execute(ThisRoofN.ViewModels.SearchViewModel.ModalType.Lifestyle);
			};

			layout_inArea.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoModalCommand.Execute(ThisRoofN.ViewModels.SearchViewModel.ModalType.InArea);
			};

			layout_homeDetail.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoModalCommand.Execute(ThisRoofN.ViewModels.SearchViewModel.ModalType.HomeDetails);
			};

			layout_inHome.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoModalCommand.Execute(ThisRoofN.ViewModels.SearchViewModel.ModalType.InHome);
			};

			layout_savedHome.Click += (object sender, EventArgs e) => {
				ViewModelInstance.GotoModalCommand.Execute(ThisRoofN.ViewModels.SearchViewModel.ModalType.SavedHome);
			};

			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.Visible);
		}
	}
}

