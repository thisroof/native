﻿
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
using MvvmCross.Binding.Droid.BindingContext;
using Android.Media;
using ThisRoofN.Droid.Helpers;
using Android.Webkit;
using Android.Graphics;
using ThisRoofN.ViewModels;

namespace ThisRoofN.Droid
{
	public class AffordSearchView : BaseMvxFragment
	{
		public AffordSearchViewModel ViewModelInstance
		{
			get {
				return (AffordSearchViewModel)base.ViewModel;
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

			View view = this.BindingInflate (Resource.Layout.fragment_financial, null);

			WebView gifWebView = view.FindViewById<WebView> (Resource.Id.gifWebView);
			if (gifWebView != null) {
				gifWebView.LoadUrl ("file:///android_res/raw/loading_animator.gif");
				gifWebView.SetBackgroundColor (Color.Transparent);
				gifWebView.SetLayerType (LayerType.Software, null);
			}

			LinearLayout layoutTax = view.FindViewById<LinearLayout> (Resource.Id.layout_tax);
			layoutTax.Click += (object sender, EventArgs e) => {
				ViewModelInstance.IncludeTax = !ViewModelInstance.IncludeTax;
			};

			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.Visible);
			homeView.ToolbarManager.RightButtonAction = RightBarClicked;
		}

		private void RightBarClicked ()
		{
			ViewModelInstance.SettingCommand.Execute (null);
		}
	}
}

