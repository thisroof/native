
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
using Android.Graphics;
using Android.Views.InputMethods;

namespace ThisRoofN.Droid
{
	public class LocationCommuteTabItemView : BaseMvxFragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			View view = this.BindingInflate (Resource.Layout.fragment_tab_location_commute, container, false);

			LinearLayout rootView = view.FindViewById<LinearLayout> (Resource.Id.rootView);
			rootView.Click += (object sender, EventArgs e) => {
				InputMethodManager imm = (InputMethodManager)Activity.GetSystemService (Context.InputMethodService);
				imm.HideSoftInputFromWindow (rootView.WindowToken, 0);
			};

			ImageView img_nav = (ImageView)view.FindViewById (Resource.Id.img_nav);
			img_nav.Click += (object sender, EventArgs e) => {
				if(ParentFragment != null) {
					SearchAreaModalView parent = (SearchAreaModalView) ParentFragment;
					parent.ConnectGoogleServiceForLocation();
				}
			};

			SeekBar distanceBar = view.FindViewById<SeekBar> (Resource.Id.seekbar_distance);
			distanceBar.Max = TRConstant.SearchMinutes.Count () - 1;

			return view;
		}
	}
}

