
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
using Android.Webkit;
using Android.Graphics;
using ThisRoofN.Droid.Helpers;
using ThisRoofN.ViewModels;

namespace ThisRoofN.Droid
{
	public class TRWebView : BaseMvxFragment
	{
		private WebView webView;

		public TRWebViewModel ViewModelInstance {
			get {
				return base.ViewModel as TRWebViewModel;
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
			View view = this.BindingInflate (Resource.Layout.fragment_webview, null);

			WebView gifWebView = view.FindViewById<WebView> (Resource.Id.gifWebView);
			if (gifWebView != null) {
				gifWebView.LoadUrl ("file:///android_res/raw/animator.html");
				gifWebView.SetBackgroundColor (Color.Transparent);
				gifWebView.SetLayerType (LayerType.Software, null);
			}

			webView = view.FindViewById<WebView> (Resource.Id.webview);
			webView.Settings.JavaScriptEnabled = true;
			webView.SetBackgroundColor (Color.Black);
			webView.LoadUrl (ViewModelInstance.ContentLink);

			ViewModelInstance.IsLoading = true;
			ViewModelInstance.LoadingText = "Loading";
			webView.SetWebViewClient (new MyWebClient(this));
			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.BackOnly);
		}

		class MyWebClient : WebViewClient {
			public TRWebView masterView;
			public MyWebClient(TRWebView _masterView) : base() {
				masterView = _masterView;
			}

			public override void OnPageFinished (WebView view, string url)
			{
				base.OnPageFinished (view, url);
				masterView.ViewModelInstance.IsLoading = false;
			}
		}
	}
}

