using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MediaPlayer;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;
using System.Collections.Generic;
using Facebook.LoginKit;
using Facebook.CoreKit;
using Acr.UserDialogs;
using ThisRoofN.Models;
using ThisRoofN.Models.Service;

namespace ThisRoofN.iOS
{
	partial class LoginViewController : BaseViewController, IUIGestureRecognizerDelegate
	{
		private MPMoviePlayerController moviePlayer;

		private LoginManager loginManager;
		private List<string> readPermissions = new List<string>{ "public_profile" };

		public LoginViewController (IntPtr handle) : base (handle)
		{
		}

		public LoginViewModel ViewModelInstance
		{
			get {
				return (LoginViewModel)this.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.SetNavigationBarHidden (true, true);

			InitVideoView ();

			var set = this.CreateBindingSet<LoginViewController, LoginViewModel> ();
			set.Bind (txt_email).To (vm => vm.Email);
			set.Bind (txt_password).To (vm => vm.Password);
			set.Bind (btn_login).To (vm => vm.LoginCommand);
			set.Bind (loadingView).For(i => i.Hidden).To (vm => vm.IsHideLoading);
			set.Bind (loadingLabel).To (vm => vm.LoadingText);
			set.Apply ();

			var backGesture = new UITapGestureRecognizer (() => {
				ViewModelInstance.CloseCommand.Execute(null);
			});

			var facebookGesture = new UITapGestureRecognizer (() => {
				ProcessFacebookLogin();
			});

			var resignGesture = new UITapGestureRecognizer (HideKeyboard);
			resignGesture.Delegate = this;

			img_btnFbLogin.UserInteractionEnabled = true;
			img_btnBack.UserInteractionEnabled = true;
			img_btnFbLogin.AddGestureRecognizer (facebookGesture);
			img_btnBack.AddGestureRecognizer (backGesture);

			view_video.AddGestureRecognizer (resignGesture);

			loginManager = new LoginManager ();
			loginManager.LoginBehavior = LoginBehavior.Native;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

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

		private void HideKeyboard()
		{
			this.View.EndEditing (true);
		}

		[Export ("gestureRecognizer:shouldReceiveTouch:")]
		public bool ShouldReceiveTouch (UIKit.UIGestureRecognizer recognizer, UIKit.UITouch touch)
		{
			return true;
		}

		[Export ("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
		public bool ShouldRecognizeSimultaneously (UIKit.UIGestureRecognizer gestureRecognizer, UIKit.UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}

		private async void ProcessFacebookLogin()
		{
			LoginManagerLoginResult result = await loginManager.LogInWithReadPermissionsAsync (readPermissions.ToArray());

			if (result.IsCancelled) {
				UserDialogs.Instance.Alert ("User cancelled Facebook Login", "ThisRoof");
				return;
			}

			if (result.GrantedPermissions != null && result.GrantedPermissions.Contains ("email")) {
				string NAME = "name";
				string ID = "id";
				string EMAIL = "email";
				string FIELDS = "fields";

				string REQUEST_FIELDS = String.Join (",", new string[] {
					NAME, ID, EMAIL
				});

				UserDialogs.Instance.ShowLoading ();
				var request = new GraphRequest ("me", new NSDictionary (FIELDS, REQUEST_FIELDS), AccessToken.CurrentAccessToken.TokenString, null, "GET");
				var requestConnection = new GraphRequestConnection ();
				requestConnection.AddRequest (request, (connection, graphResult, error) => {
					UserDialogs.Instance.HideLoading ();
					if (error != null || graphResult == null) {
						// Error Handler
						UserDialogs.Instance.Alert (String.Format("Facebook Login Error:{0}", error.Description), "ThisRoof");
					} else {
						string email = (graphResult as NSDictionary) ["email"].ToString ();
						FBUserInfo userInfo = new FBUserInfo () {
							UserEmail = email,
							UserID = AccessToken.CurrentAccessToken.UserID,
							UserToken = AccessToken.CurrentAccessToken.TokenString
						};

						ViewModelInstance.FacebookLoginCommand.Execute(userInfo);

						// after get user info we logout again.
						loginManager.LogOut ();
					}
				});

				requestConnection.Start ();
			} else {
				UserDialogs.Instance.Alert ("Email Permission is not allowed", "ThisRoof");
			}
		}
	}
}
