using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using MvvmCross.Platform.Platform;

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

		private void DoLogin()
		{
		}

		private void DoFacebookLogin(FBUserInfo fbUserInfo)
		{
			MvxTrace.Trace (fbUserInfo.UserEmail);
		}
	}
}

