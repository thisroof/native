
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
using Android.Views.InputMethods;

namespace ThisRoofN.Droid
{
	public class LocationDistanceTabItemView : BaseMvxFragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			View view = this.BindingInflate (Resource.Layout.fragment_tab_location_distance, container, false);

			LinearLayout rootView = view.FindViewById<LinearLayout> (Resource.Id.rootView);
			rootView.Click += (object sender, EventArgs e) => {
				InputMethodManager imm = (InputMethodManager)Activity.GetSystemService (Context.InputMethodService);
				imm.HideSoftInputFromWindow (rootView.WindowToken, 0);
			};

			SeekBar distanceBar = view.FindViewById<SeekBar> (Resource.Id.seekbar_distance);
			distanceBar.Max = TRConstant.SearchDistances.Count () - 1;

			return view;
		}
	}
}

