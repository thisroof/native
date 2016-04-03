
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

namespace ThisRoofN.Droid
{
	public class SearchTypeView : BaseMvxFragment, MediaPlayer.IOnPreparedListener, ISurfaceHolderCallback
	{
		private MediaPlayer mediaPlayer;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			View view = this.BindingInflate (Resource.Layout.fragment_search_type, null);

			var videoView = view.FindViewById<VideoView> (Resource.Id.view_video);
			ISurfaceHolder holder = videoView.Holder;
			holder.AddCallback (this);

			var descriptor = Activity.Assets.OpenFd ("Videos/WelcomeVideo.mp4");
			mediaPlayer = new MediaPlayer ();
			mediaPlayer.Looping = true;
			mediaPlayer.SetDataSource (descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);
			mediaPlayer.SetVideoScalingMode (VideoScalingMode.ScaleToFitWithCropping);
			mediaPlayer.SetOnPreparedListener (this);
			mediaPlayer.Prepare ();

			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.None);
		}

		public override void OnPause ()
		{
			base.OnPause ();

			if (mediaPlayer.IsPlaying == true)
			{
				mediaPlayer.Stop();
			}

			mediaPlayer.SetDisplay(null);
			mediaPlayer.Release();
		}

		public void OnPrepared(MediaPlayer player)
		{
			if (!mediaPlayer.IsPlaying) {
				mediaPlayer.Start ();  
			}
		}

		public void SurfaceChanged (ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
		{

		}

		public void SurfaceCreated (ISurfaceHolder holder)
		{
			mediaPlayer.SetDisplay (holder);	
		}

		public void SurfaceDestroyed (ISurfaceHolder holder) 
		{
		}
	}
}

