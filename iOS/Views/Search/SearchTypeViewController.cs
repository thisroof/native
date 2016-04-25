using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;
using MediaPlayer;

namespace ThisRoofN.iOS
{
	partial class SearchTypeViewController : BaseViewController
	{
		private bool playbackDurationSet;
		private MPMoviePlayerController moviePlayer;

		public SearchTypeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitVideoView ();

			btn_affordSearch.Layer.BorderColor = UIColor.White.CGColor;
			btn_affordSearch.Layer.BorderWidth = 1;

			var set = this.CreateBindingSet<SearchTypeViewController, SearchTypeViewModel> ();
			set.Bind (btn_normalSearch).To (vm => vm.NormalSearchCommand);
			set.Bind (btn_affordSearch).To (vm => vm.AffordSearchCommand);
			set.Bind (loadingView).For(i => i.Hidden).To (vm => vm.IsHideLoading);
			set.Bind (loadingLabel).To (vm => vm.LoadingText);
			set.Apply ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, true);
			if (moviePlayer != null) {
				moviePlayer.PrepareToPlay ();
				moviePlayer.Play ();

			}
		}

		public override void ViewWillLayoutSubviews ()
		{
			base.ViewWillLayoutSubviews ();
			moviePlayer.View.Frame = UIScreen.MainScreen.Bounds;
		}

		public override void ViewWillDisappear (bool animated)
		{ 
			base.ViewWillDisappear (animated);

			if (moviePlayer != null) {
				moviePlayer.Pause ();
			}
		}

		private void InitVideoView()
		{
			moviePlayer	= new MPMoviePlayerController (NSUrl.FromFilename ("Videos/WelcomeVideo.mp4"));
			moviePlayer.View.Frame = UIScreen.MainScreen.Bounds;
			moviePlayer.ScalingMode = MPMovieScalingMode.AspectFill;
			moviePlayer.ControlStyle = MPMovieControlStyle.None;
			moviePlayer.MovieControlMode = MPMovieControlMode.Hidden;
			moviePlayer.RepeatMode = MPMovieRepeatMode.One;

			MPMoviePlayerController.Notifications.ObservePlaybackStateDidChange (OnStateChanged);

			this.video_view.Add (moviePlayer.View);
		}

		private void OnStateChanged(object sender, NSNotificationEventArgs e)
		{
			switch (moviePlayer.PlaybackState) {
			case MPMoviePlaybackState.Playing:
				if (!playbackDurationSet) {
					moviePlayer.CurrentPlaybackTime = 20.0;
					playbackDurationSet = true;
				}
				break;
			default:
				break;
			}
		}
	}
}
