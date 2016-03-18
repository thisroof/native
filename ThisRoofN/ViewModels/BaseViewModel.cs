using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.RestService;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;
using System.Collections.Generic;
using Acr.UserDialogs;
using ThisRoofN.Extensions;

namespace ThisRoofN.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		protected TRService mTRService;
		protected IUserPreference mUserPref;
		protected IDevice mDeviceInfo;

		private MvxCommand _closeCommand;
		private MvxCommand _settingCommand;

		public BaseViewModel ()
		{
			mTRService = new TRService ();
			mUserPref = Mvx.Resolve<IUserPreference> ();
			mDeviceInfo = Mvx.Resolve<IDevice> ();
		}

		/// <summary>
		/// API loading flag
		/// </summary>
		private bool _isLoading;
		public bool IsLoading { 
			get { return _isLoading; }
			set {
				_isLoading = value;
				RaisePropertyChanged (() => IsLoading);
				RaisePropertyChanged (() => IsHideLoading);
			}
		}

		public bool IsHideLoading 
		{
			get { return !_isLoading; }
		}

		public ICommand SettingCommand
		{
			get {
				_settingCommand = _settingCommand ?? new MvxCommand (() => {
					ShowViewModel<SettingsViewModel>();
				});
				return _settingCommand;
			}
		}

		public ICommand CloseCommand
		{
			get {
				_closeCommand = _closeCommand ?? new MvxCommand (() => {
					Close (this);
				});
				return _closeCommand;
			}
		}

		public bool Validate(string email, string password)
		{
			bool result = true;
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				result = false;
				UserDialogs.Instance.Alert("Please input email address and password.");
			}
			else if (!email.IsValidEmail())
			{
				result = false;
				UserDialogs.Instance.Alert("You must enter a valid email address.");
			}
			else if (password.Length < 8)
			{
				result = false;
				UserDialogs.Instance.Alert("Password should be more than 8 characters.");
			}
			return result;
		}
	}
}

