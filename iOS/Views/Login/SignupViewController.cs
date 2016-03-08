using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MediaPlayer;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;
using Facebook.LoginKit;
using Acr.UserDialogs;
using Facebook.CoreKit;
using System.Collections.Generic;

namespace ThisRoofN.iOS
{
	partial class SignupViewController : BaseViewController
	{
		private MPMoviePlayerController moviePlayer;
		private LoginManager loginManager;
		private List<string> readPermissions = new List<string>{ "public_profile" };

		public SignupViewController (IntPtr handle) : base (handle)
		{
		}

		public SignupViewModel ViewModelInstance
		{
			get {
				return (SignupViewModel)this.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.SetNavigationBarHidden (true, true);

			InitVideoView ();

			var set = this.CreateBindingSet<SignupViewController, SignupViewModel> ();
			set.Bind (txt_email).To (vm => vm.Email);
			set.Bind (txt_password).To (vm => vm.Password);
			set.Bind (btn_signup).To (vm => vm.SignupCommand);
			set.Apply ();

			var backGesture = new UITapGestureRecognizer (() => {
				ViewModelInstance.CloseCommand.Execute(null);
			});

			var facebookGesture = new UITapGestureRecognizer (() => {
				ViewModelInstance.FacebookSignupCommand.Execute(null);
			});

			img_btnFbLogin.UserInteractionEnabled = true;
			img_btn_back.UserInteractionEnabled = true;
			img_btnFbLogin.AddGestureRecognizer (facebookGesture);
			img_btn_back.AddGestureRecognizer (backGesture);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (moviePlayer != null) {
				moviePlayer.PrepareToPlay ();
				moviePlayer.Play();
			}
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

		private async void ProcessFacebookLogin()
		{
			LoginManagerLoginResult result = await loginManager.LogInWithReadPermissionsAsync (readPermissions.ToArray(), this);

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

						ViewModelInstance.FacebookSignupCommand.Execute(userInfo);

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
