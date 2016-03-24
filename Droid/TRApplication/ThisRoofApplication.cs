using System;
using Android.App;
using Android.Runtime;
using Acr.UserDialogs;

namespace ThisRoofN.Droid
{
	public class ThisRoofApplication : Application
	{
		public ThisRoofApplication (IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			UserDialogs.Init (this);
		}
	}
}

