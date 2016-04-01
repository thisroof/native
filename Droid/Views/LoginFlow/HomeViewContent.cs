using System;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Views;
using Android.Media;
using Android.Widget;
using ThisRoofN.Droid.Helpers;

namespace ThisRoofN.Droid
{
	public class HomeViewContent : BaseMvxFragment, MediaPlayer.IOnPreparedListener, ISurfaceHolderCallback
	{
		private MediaPlayer mediaPlayer;

		public HomeViewContent ()
		{
			
		}

		public override View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);



			View view = this.BindingInflate (Resource.Layout.fragment_home, null);

			var videoView = view.FindViewById<VideoView> (Resource.Id.view_video);
			ISurfaceHolder holder = videoView.Holder;
			holder.AddCallback (this);

			var descriptor = Activity.Assets.OpenFd ("Videos/SplashVideo.mp4");
			mediaPlayer = new MediaPlayer ();
			mediaPlayer.SetDataSource (descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);
			mediaPlayer.Prepare ();
			mediaPlayer.Looping = true;
			mediaPlayer.Start ();

			return view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.None);
		}

		public void OnPrepared(MediaPlayer player)
		{
			Console.WriteLine ("Mediaplayer prepared");
		}

		public void SurfaceChanged (ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
		{
			mediaPlayer.SetDisplay (holder);	
		}

		public void SurfaceCreated (ISurfaceHolder holder)
		{
			Console.WriteLine ("Surface Created");
		}

		public void SurfaceDestroyed (ISurfaceHolder holder) 
		{
			Console.WriteLine ("Surface Destroyed");
		}
	}
}

