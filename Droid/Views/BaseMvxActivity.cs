
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
using MvvmCross.Droid.Support.V7.Fragging;

namespace ThisRoofN.Droid
{
	public class BaseMvxActivity : MvxFragmentActivity
	{
		int onStartCount = 0;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			onStartCount = 1;
			if (savedInstanceState == null) {
				this.OverridePendingTransition (Resource.Animation.activity_slidein_left, Resource.Animation.activity_slideout_left);
			} else {
				onStartCount = 2;
			}
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			if (onStartCount > 1) {
				this.OverridePendingTransition (Resource.Animation.activity_slidein_left, Resource.Animation.activity_slideout_left);
			} else if (onStartCount == 1) {
				onStartCount++;
			}
		}

		protected override void OnStop()
		{
			base.OnStop ();
		}

		public void ProcessLogout()
		{
		}
	}
}

