using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Acr.UserDialogs;
using ThisRoofN.RestService;

namespace ThisRoofN.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		private MvxCommand _loginCommand;
		private MvxCommand _signupCommand;

		public HomeViewModel ()
		{
			CheckAutoLogin ();
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

		private async void CheckAutoLogin()
		{
			// Trying to auto login
			if(!string.IsNullOrEmpty(mUserPref.GetValue(TRConstant.UserPrefUserEmailKey)) && 
				!string.IsNullOrEmpty(mUserPref.GetValue(TRConstant.UserPrefAccessTokenKey))) {
				this.LoadingText = "Loading...";
				this.IsLoading = true;
				// Set the service token
				TRService.Token = mUserPref.GetValue(TRConstant.UserPrefAccessTokenKey);
				bool tokenValid = await mTRService.IsTokenValid ();
				this.IsLoading = false;

				if (tokenValid) {
					ShowViewModel<SearchTypeViewModel> ();
				}
			}
		}
	}
}

