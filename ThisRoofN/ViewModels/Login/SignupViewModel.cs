﻿using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Acr.UserDialogs;
using ThisRoofN.RestService;
using MvvmCross.Platform.Platform;

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
			UserDialogs.Instance.ShowLoading ();
			TRUser user = await m_TRService.Signup (Email, Password);
			UserDialogs.Instance.HideLoading ();

			if (user != null && user.Success) {
				TRService.Token = user.AccessToken;

				// Goto Home screen
				MvxTrace.Trace("Signup success:{0}", user.AccessToken);
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

		private async void DoFacebookSignup(FBUserInfo fbUserInfo)
		{
			UserDialogs.Instance.ShowLoading ();
			TRUser user = await m_TRService.FacebookLogin (fbUserInfo);
			UserDialogs.Instance.HideLoading ();

			if (user != null && user.Success) {
				TRService.Token = user.AccessToken;

				// Goto Home screen
				MvxTrace.Trace("Facebook Signup success:{0}", user.AccessToken);
			} else {
				UserDialogs.Instance.Alert ("Failed to signup using Facebook. Please contact administrator", "ThisRoof", "OK");
			}
		}
	}
}

