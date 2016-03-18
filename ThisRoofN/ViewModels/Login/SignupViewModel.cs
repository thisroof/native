﻿using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Acr.UserDialogs;
using ThisRoofN.RestService;
using MvvmCross.Platform.Platform;
using ThisRoofN.Models.Service;

namespace ThisRoofN.ViewModels
{
	public class SignupViewModel : BaseViewModel
	{
		private string _email;
		private string _password;

		private MvxCommand _singupCommand;
		private MvxCommand<FBUserInfo> _facebookSignupCommand;

		public SignupViewModel ()
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

		public ICommand SignupCommand
		{
			get {
				_singupCommand = _singupCommand ?? new MvxCommand (DoSignup);
				return _singupCommand;
			}
		}

		public ICommand FacebookSignupCommand
		{
			get {
				_facebookSignupCommand = _facebookSignupCommand ?? new MvxCommand<FBUserInfo> (DoFacebookSignup);
				return _facebookSignupCommand;
			}
		}

		private async void DoSignup()
		{
			if (Validate (Email, Password)) {
				UserDialogs.Instance.ShowLoading ();
				TRUser user = await mTRService.Signup (Email, Password);
				UserDialogs.Instance.HideLoading ();

				if (user != null && user.Success) {
					TRService.Token = user.AccessToken;
					MvxTrace.Trace("Signup success:{0}", user.AccessToken);

					// Save user data to user preference
					mUserPref.SetValue(TRConstant.UserPrefUserEmailKey, Email);
					mUserPref.SetValue(TRConstant.UserPrefUserPasswordKey, Password);
					mUserPref.SetValue(TRConstant.UserPrefAccessTokenKey, user.AccessToken);
					mUserPref.SetValue(TRConstant.UserPrefRefreshTokenKey, user.RefreshToken);

					ShowViewModel<SearchTypeViewModel> ();
				} else {
					UserDialogs.Instance.Alert("There is already an account with this email address. Please visit the login screen.", "Signup Error", "OK");
				}
			}
		}

		private async void DoFacebookSignup(FBUserInfo fbUserInfo)
		{
			UserDialogs.Instance.ShowLoading ();
			TRUser user = await mTRService.FacebookLogin (fbUserInfo);
			UserDialogs.Instance.HideLoading ();

			if (user != null && user.Success) {
				TRService.Token = user.AccessToken;
				MvxTrace.Trace("Facebook Signup success:{0}", user.AccessToken);

				// Save user data to user preference
				mUserPref.SetValue(TRConstant.UserPrefUserEmailKey, fbUserInfo.UserEmail);
				mUserPref.SetValue(TRConstant.UserPrefUserPasswordKey, Password);
				mUserPref.SetValue(TRConstant.UserPrefAccessTokenKey, user.AccessToken);
				mUserPref.SetValue(TRConstant.UserPrefRefreshTokenKey, user.RefreshToken);

				ShowViewModel<SearchTypeViewModel> ();
			} else {
				UserDialogs.Instance.Alert ("Failed to signup using Facebook. Please contact administrator", "ThisRoof", "OK");
			}
		}
	}
}

