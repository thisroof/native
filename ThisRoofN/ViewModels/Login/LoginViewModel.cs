using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using MvvmCross.Platform.Platform;
using ThisRoofN.RestService;
using Acr.UserDialogs;
using ThisRoofN.Models;
using ThisRoofN.Models.Service;

namespace ThisRoofN.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private string _email;
		private string _password;

		private MvxCommand _loginCommand;
		private MvxCommand<FBUserInfo> _facebookLoginCommand;

		public LoginViewModel ()
		{
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set {
				this._email = value;
				RaisePropertyChanged (() => Email);
			}
		}

		public string Password
		{
			get
			{
				return this._password;
			}
			set {
				this._password = value;
				RaisePropertyChanged (() => Password);
			}
		}

		public ICommand LoginCommand
		{
			get {
				_loginCommand = _loginCommand ?? new MvxCommand (DoLogin);
				return _loginCommand;
			}
		}

		public ICommand FacebookLoginCommand
		{
			get {
				_facebookLoginCommand = _facebookLoginCommand ?? new MvxCommand<FBUserInfo> (DoFacebookLogin);
				return _facebookLoginCommand;
			}
		}

		private async void DoLogin()
		{
			if (Validate (Email, Password)) {
				UserDialogs.Instance.ShowLoading ();
				TRUser user = await mTRService.Login (Email, Password);
				UserDialogs.Instance.HideLoading ();

				if (user != null && user.Success) {
					TRService.Token = user.AccessToken;
					MvxTrace.Trace("Login success:{0}", user.AccessToken);

					// Save user data to user preference
					mUserPref.SetValue(TRConstant.UserPrefUserEmailKey, Email);
					mUserPref.SetValue(TRConstant.UserPrefUserPasswordKey, Password);
					mUserPref.SetValue(TRConstant.UserPrefAccessTokenKey, user.AccessToken);
					mUserPref.SetValue(TRConstant.UserPrefRefreshTokenKey, user.RefreshToken);

					ShowViewModel<SearchTypeViewModel> ();
				} else {
					bool isForgot = await UserDialogs.Instance.ConfirmAsync(
						"The username or password you entered did not match with our records. Please double-check and try again.",
						"Please try again...", 
						"Forgot Password?",
						"OK");

					if (isForgot)
					{
						UserDialogs.Instance.Alert("Please proceed to your email to receive your password.", "Password Sent");
					}
				}
			}
		}

		private async void DoFacebookLogin(FBUserInfo fbUserInfo)
		{
			UserDialogs.Instance.ShowLoading ();
			TRUser user = await mTRService.FacebookLogin (fbUserInfo);
			UserDialogs.Instance.HideLoading ();

			if (user != null && user.Success) {
				TRService.Token = user.AccessToken;
				MvxTrace.Trace("Facebook Login success:{0}", user.AccessToken);

				// Save user data to user preference
				mUserPref.SetValue(TRConstant.UserPrefUserEmailKey, fbUserInfo.UserEmail);
				mUserPref.SetValue(TRConstant.UserPrefAccessTokenKey, Password);
				mUserPref.SetValue(TRConstant.UserPrefAccessTokenKey, user.AccessToken);
				mUserPref.SetValue(TRConstant.UserPrefRefreshTokenKey, user.RefreshToken);

				ShowViewModel<SearchTypeViewModel> ();
			} else {
				UserDialogs.Instance.Alert ("Failed to login using Facebook. Please contact administrator", "ThisRoof", "OK");
			}
		}
	}
}

