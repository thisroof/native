using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;
using MediaPlayer;

namespace ThisRoofN.iOS
{
	partial class HomeViewController : BaseViewController
	{
		private MPMoviePlayerController moviePlayer;

		public HomeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.SetNavigationBarHidden (true, true);

			InitVideoView ();

			var set = this.CreateBindingSet<HomeViewController, HomeViewModel> ();
			set.Bind (btn_login).To (vm => vm.LoginCommand);
			set.Bind (btn_signup).To (vm => vm.SignupCommand);
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
				moviePlayer.Play();
			}
		}

		public override void ViewWillLayoutSubviews ()
		{
			base.ViewWillLayoutSubviews ();
			moviePlayer.View.Frame = this.view_video.Frame;
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
			moviePlayer	= new MPMoviePlayerController (NSUrl.FromFilename ("Videos/SplashVideo.mp4"));
			moviePlayer.View.Frame = this.view_video.Frame;
			moviePlayer.ScalingMode = MPMovieScalingMode.AspectFill;
			moviePlayer.ControlStyle = MPMovieControlStyle.None;
			moviePlayer.MovieControlMode = MPMovieControlMode.Hidden;
			moviePlayer.RepeatMode = MPMovieRepeatMode.One;

			this.view_video.Add (moviePlayer.View);
		}
	}
}
