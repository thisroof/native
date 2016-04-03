
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Graphics;
using ThisRoofN.Droid.CustomMvxDroid;
using MvvmCross.Platform;
using MvvmCross.Droid.Views;
using ThisRoofN.Droid.Helpers;

namespace ThisRoofN.Droid
{
	[Activity (ScreenOrientation=global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HomeView : BaseMvxActivity
	{
		private TRMvxDroidViewPresenter mPresenter;
		private RelativeLayout contentLayout;
		private ToolbarHelper toolbarManager;

		public ToolbarHelper ToolbarManager {
			get {
				return this.toolbarManager;
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_navigation_base);
			contentLayout = FindViewById<RelativeLayout> (Resource.Id.contentLayout);
			toolbarManager = new ToolbarHelper (this, SupportFragmentManager);

			if (savedInstanceState == null) {
				mPresenter = (TRMvxDroidViewPresenter)Mvx.Resolve<IMvxAndroidViewPresenter> ();
				mPresenter.RegisterFragmentManager (SupportFragmentManager, this);
			}

			// Add HomeView Content as Default
			var transaction = SupportFragmentManager.BeginTransaction ();
			var fragment = new HomeViewContent ();
			fragment.ViewModel = this.ViewModel;

			transaction.Replace (Resource.Id.mainContentLayout, fragment, fragment.GetType ().Name)
				.AddToBackStack (null)
				.Commit ();
		}

		public override void OnBackPressed ()
		{
			base.OnBackPressed ();
		}
	}
}

