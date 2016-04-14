
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Media;
using ThisRoofN.ViewModels;
using ThisRoofN.Droid.Helpers;
using Android.Webkit;
using Android.Graphics;
using Android.Views.InputMethods;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Acr.UserDialogs;
using ThisRoofN.Models.Service;

namespace ThisRoofN.Droid
{
	public class LoginView : BaseMvxFragment, MediaPlayer.IOnPreparedListener, ISurfaceHolderCallback, GraphRequest.ICallback
	{
		private MediaPlayer mediaPlayer;
		private ICallbackManager callbackManager;
		private ProfileTracker profileTracker;
		private List<string> readPermissions = new List<string> { "public_profile"};

		public LoginViewModel ViewModelInstance
		{
			get {
				return (LoginViewModel)base.ViewModel;
			}
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
			FacebookSdk.SdkInitialize (Android.App.Application.Context);

			callbackManager = CallbackManagerFactory.Create ();

			profileTracker = new CustomProfileTracker {
				HandleCurrentProfileChanged = (oldProfile, currentProfile) => {
					if(currentProfile == null)
					{
					}
				}
			};
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			base.OnCreateView (inflater, container, savedInstanceState);
			View view = this.BindingInflate (Resource.Layout.fragment_login, null);

			WebView gifWebView = view.FindViewById<WebView> (Resource.Id.gifWebView);
			if (gifWebView != null) {
				gifWebView.LoadUrl ("file:///android_res/raw/animator.html");
				gifWebView.SetBackgroundColor (Color.Transparent);
				gifWebView.SetLayerType (LayerType.Software, null);
			}

			RelativeLayout rootView = view.FindViewById<RelativeLayout> (Resource.Id.rootView);
			rootView.Click += (object sender, EventArgs e) => {
				InputMethodManager imm = (InputMethodManager)Activity.GetSystemService (Context.InputMethodService);
				imm.HideSoftInputFromWindow (rootView.WindowToken, 0);
			};

			var videoView = view.FindViewById<VideoView> (Resource.Id.view_video);
			ISurfaceHolder holder = videoView.Holder;
			holder.AddCallback (this);

			var back = view.FindViewById<ImageView> (Resource.Id.img_back);
			back.Click += (object sender, EventArgs e) => {
				ViewModelInstance.CloseCommand.Execute(null);
			};

			ImageView fbLogin = view.FindViewById<ImageView> (Resource.Id.img_fbLogin);
			fbLogin.Click += (object sender, EventArgs e) => {
				LoginManager.Instance.LogInWithReadPermissions(this, readPermissions);
			};

			profileTracker.StartTracking ();

			var loginCallback = new FacebookCallback<Xamarin.Facebook.Login.LoginResult> {
				HandleSuccess = loginResult => {
					// Get User Email Address
					if(loginResult.RecentlyGrantedPermissions.Contains("email")) {
						string NAME = "name";
						string ID = "id";
						string EMAIL = "email";
						string FIELDS = "fields";

						string REQUEST_FIELDS = String.Join(",", new string[] {
							NAME, ID, EMAIL
						});

						Bundle paramters = new Bundle();
						paramters.PutString(FIELDS, REQUEST_FIELDS);
						var request = new GraphRequest(AccessToken.CurrentAccessToken, "me", paramters, Xamarin.Facebook.HttpMethod.Get, this);
						request.ExecuteAsync();
					}
					else
					{
						UserDialogs.Instance.Alert("User didn't allow some permissions", "Facebook");
						LoginManager.Instance.LogOut();
					}
				},
				HandleCancel = () => {
					UserDialogs.Instance.Alert("User Cancelled Facebook Login", "Facebook");
					System.Console.WriteLine("Cancelled");
				},
				HandleError = loginError => {
					System.Console.WriteLine(loginError.Message);
					UserDialogs.Instance.Alert(string.Format("Facebook Login failed because {0}", loginError.Message), "Facebook");
				}
			};

			LoginManager.Instance.RegisterCallback (callbackManager, loginCallback);

			return view;
		}

		public override void OnActivityResult (int requestCode, int resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			callbackManager.OnActivityResult (requestCode, (int)resultCode, data);
		}

		public override void OnResume ()
		{
			base.OnResume ();

			HomeView homeView = (HomeView)Activity;
			homeView.ToolbarManager.SetToolbarType (ToolbarHelper.ToolbarType.None);
		}

		public override void OnStop ()
		{
			base.OnStop ();
			if (mediaPlayer.IsPlaying == true)
			{
				mediaPlayer.Stop();
			}

			mediaPlayer.SetDisplay(null);
			mediaPlayer.Release();
		}

		#region Video Callbacks
		public void OnPrepared(MediaPlayer player)
		{
			if (!mediaPlayer.IsPlaying) {
				mediaPlayer.Start ();
			}
		}

		public void SurfaceChanged (ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
		{
			Console.WriteLine ("changed");
		}

		public void SurfaceCreated (ISurfaceHolder holder)
		{
			var descriptor = Activity.Assets.OpenFd ("Videos/SplashVideo.mp4");
			mediaPlayer = new MediaPlayer ();
			mediaPlayer.Looping = true;
			mediaPlayer.SetDataSource (descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);
			mediaPlayer.SetVideoScalingMode (VideoScalingMode.ScaleToFitWithCropping);
			mediaPlayer.SetOnPreparedListener (this);
			mediaPlayer.Prepare ();
			mediaPlayer.SetDisplay (holder);	
		}

		public void SurfaceDestroyed (ISurfaceHolder holder)
		{
		}
		#endregion


		// Graph Completed Callback
		public void OnCompleted (GraphResponse response)
		{
			if (response.Error != null)
			{
				UserDialogs.Instance.Alert(string.Format("Facebook Login failed because {0}", response.Error.ErrorMessage), "Facebook");
			}
			else
			{
				string email = response.JSONObject.GetString("email");
				Console.Write (email);
				UserDialogs.Instance.Alert ("Success");
				FBUserInfo userInfo = new FBUserInfo () {
					UserEmail = email,
					UserID = AccessToken.CurrentAccessToken.UserId,
					UserToken = AccessToken.CurrentAccessToken.Token
				};

				ViewModelInstance.FacebookLoginCommand.Execute(userInfo);
			}

			LoginManager.Instance.LogOut();
		}
	}
}

