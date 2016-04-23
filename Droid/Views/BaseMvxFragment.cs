using System;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Xamarin.Facebook;
using Java.Interop;
using Android.OS;

namespace ThisRoofN.Droid
{
	public class BaseMvxFragment : MvxFragment
	{
		public BaseMvxFragment ()
		{
		}

		public override void OnStart ()
		{
			base.OnStart ();
		}

		public override void OnStop ()
		{
			base.OnStop ();
		}

		/// <summary>
		/// Facebook Login Callback
		/// </summary>
		protected class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
		{
			public Action HandleCancel { get; set; }
			public Action<FacebookException> HandleError { get; set; }
			public Action<TResult> HandleSuccess { get; set; }

			public void OnCancel ()
			{
				var c = HandleCancel;
				if (c != null)
					c ();
			}

			public void OnError (FacebookException error)
			{
				var c = HandleError;
				if (c != null)
					c (error);
			}

			public void OnSuccess (Java.Lang.Object result)
			{
				var c = HandleSuccess;
				if (c != null)
					c (result.JavaCast<TResult> ());
			}
		}

		/// <summary>
		/// Profile Tracker for take profile data
		/// </summary>
		protected class CustomProfileTracker : ProfileTracker
		{
			public delegate void CurrentProfileChangedDelegate (Profile oldProfile, Profile currentProfile);

			public CurrentProfileChangedDelegate HandleCurrentProfileChanged { get; set; }

			protected override void OnCurrentProfileChanged (Profile oldProfile, Profile currentProfile)
			{
				var p = HandleCurrentProfileChanged;
				if (p != null)
					p (oldProfile, currentProfile);
			}
		}
	}
}

