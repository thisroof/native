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
		private MPMoviePlayerController moviePlayer;

		public SearchTypeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitVideoView ();

//			btn_affordSearch.TitleLabel.Lines = 0;
//			btn_affordSearch.TitleLabel.TextAlignment = UITextAlignment.Center;
//			btn_affordSearch.SetTitle("FIND OUT HOW MUCH\nYOU CAN AFFORD...", UIControlState.Normal);
//			btn_affordSearch.LineBreakMode = UILineBreakMode.WordWrap;

			btn_affordSearch.Layer.BorderColor = UIColor.White.CGColor;
			btn_affordSearch.Layer.BorderWidth = 1;

			var set = this.CreateBindingSet<SearchTypeViewController, SearchTypeViewModel> ();
			set.Bind (btn_normalSearch).To (vm => vm.NormalSearchCommand);
			set.Bind (btn_affordSearch).To (vm => vm.AffordSearchCommand);
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
			moviePlayer.View.Frame = this.video_view.Frame;
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
			moviePlayer.View.Frame = this.video_view.Frame;
			moviePlayer.ScalingMode = MPMovieScalingMode.AspectFill;
			moviePlayer.ControlStyle = MPMovieControlStyle.None;
			moviePlayer.MovieControlMode = MPMovieControlMode.Hidden;
			moviePlayer.RepeatMode = MPMovieRepeatMode.One;

			this.video_view.Add (moviePlayer.View);
		}
	}
}
