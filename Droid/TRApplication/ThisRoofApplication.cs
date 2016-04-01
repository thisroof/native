using System;
using Android.App;
using Android.Runtime;
using Acr.UserDialogs;

namespace ThisRoofN.Droid
{
	#if DEBUG
	[Application(Debuggable=true)]
	#else
	[Application(Debuggable=false)]
	#endif
	public class ThisRoofApplication : Application
	{
		public ThisRoofApplication (IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			// Initialize Xamarin Insights
			Xamarin.Insights.Initialize(TRConstant.InsightsApiKey, this);

			// Initialize User Dialogs
			UserDialogs.Init (this);
		}
	}
}

