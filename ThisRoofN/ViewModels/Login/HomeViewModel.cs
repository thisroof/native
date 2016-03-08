using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace ThisRoofN.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		private MvxCommand _loginCommand;
		private MvxCommand _signupCommand;

		public HomeViewModel ()
		{
			// Trying to auto login
			if(!string.IsNullOrEmpty(userPref.GetValue(TRConstant.UserPrefUserEmailKey)) && 
				!string.IsNullOrEmpty(userPref.GetValue(TRConstant.UserPrefAccessTokenKey))) {

			}
		}

		public ICommand LoginCommand
		{
			get {
				_loginCommand = _loginCommand ?? new MvxCommand (DoLogin);
				return _loginCommand;
			}
		}

		public ICommand SignupCommand
		{
			get {
				_signupCommand = _signupCommand ?? new MvxCommand (DoSignup);
				return _signupCommand;
			}
		}

		private void DoLogin()
		{
			ShowViewModel<LoginViewModel> ();
		}

		private void DoSignup()
		{
			ShowViewModel<SignupViewModel> ();
		}
	}
}

