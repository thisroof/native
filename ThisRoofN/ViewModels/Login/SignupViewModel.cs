using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

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

		private void DoSignup()
		{
		}

		private void DoFacebookSignup(FBUserInfo userInfo)
		{
			
		}
	}
}

