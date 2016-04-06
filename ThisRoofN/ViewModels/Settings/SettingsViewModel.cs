using System;
using ThisRoofN.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Acr.UserDialogs;
using ThisRoofN.Database;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using Newtonsoft.Json;
using ThisRoofN.Interfaces;

namespace ThisRoofN.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private IDevice deviceInfo;
		private MvxCommand _savedPropertyCommand;
		private MvxCommand _supportCommand;
		private MvxCommand _privacyCommand;
		private MvxCommand _termCommand;
		private MvxCommand _licenseCommand;
		private MvxCommand _clearLikeCommand;
		private MvxCommand _clearDislikeCommand;
		private MvxCommand _enableAllTutorialCommand;
		private MvxCommand _logoutCommand;

		public SettingsViewModel (IDevice device)
		{
			deviceInfo = device;
		}

		public ICommand SavedPropertyCommand
		{
			get {
				_savedPropertyCommand = _savedPropertyCommand ?? new MvxCommand (DoGetSavedProperty);
				return _savedPropertyCommand;
			}
		}

		public ICommand SupportCommand
		{
			get {
				_supportCommand = _supportCommand ?? new MvxCommand (DoSupport);
				return _supportCommand;
			}
		}


		public ICommand PrivacyCommand
		{
			get {
				_privacyCommand = _privacyCommand ?? new MvxCommand (GotoPrivacy);
				return _privacyCommand;
			}
		}

		public ICommand TermsCommand
		{
			get {
				_termCommand = _termCommand ?? new MvxCommand (GotoTerms);
				return _termCommand;
			}
		}

		public ICommand LicenseCommand
		{
			get {
				_licenseCommand = _licenseCommand ?? new MvxCommand (GotoLicense);
				return _licenseCommand;
			}
		}

		public ICommand ClearLikeCommand
		{
			get {
				_clearLikeCommand = _clearLikeCommand ?? new MvxCommand (DoClearLike);
				return _clearLikeCommand;
			}
		}

		public ICommand ClearDislikeCommand
		{
			get {
				_clearDislikeCommand = _clearDislikeCommand ?? new MvxCommand (DoClearLike);
				return _clearDislikeCommand;
			}
		}

		public ICommand EnableAllTutorialCommand
		{
			get {
				_enableAllTutorialCommand = _enableAllTutorialCommand ?? new MvxCommand (DoEnableAllTutorial);
				return _enableAllTutorialCommand;
			}
		}

		public ICommand LogoutCommand
		{
			get {
				_logoutCommand = _logoutCommand ?? new MvxCommand (DoLogout);
				return _logoutCommand;
			}
		}

		private async void DoGetSavedProperty()
		{
			IsLoading = true;
			List <CottageDetail> savedResults = await mTRService.GetLikes (deviceInfo.GetUniqueIdentifier ());
			IsLoading = false;

			if (savedResults.Count > 0) {
				var serialized = JsonConvert.SerializeObject (savedResults);
				ShowViewModel<SavedPropertiesViewModel> (new {data = serialized});
			} else {
				UserDialogs.Instance.Alert ("You do not have any saved properties.", "Saved Homes");
			}
		}

		private void DoSupport()
		{
			ShowViewModel<TRWebViewModel> (new {link=TRConstant.SupportPageLink});
		}

		private void GotoPrivacy()
		{
			ShowViewModel<TRWebViewModel> (new {link=TRConstant.PrivacyPolicyPageLink});
		}

		private void GotoTerms()
		{
			ShowViewModel<TRWebViewModel> (new {link=TRConstant.TermsOfUsePageLink});
		}

		private void GotoLicense()
		{
			ShowViewModel<TRWebViewModel> (new {link=TRConstant.LicensePageLink});
		}

		private async void DoClearLike()
		{
			bool confirm = await UserDialogs.Instance.ConfirmAsync(
				"Are you sure you would like to clear all of your saved properties? \n\r You will not be able to undo this action",
				"Clear Liked Properties",
				"Yes",
				"No");

			if (confirm)
			{
				TRDatabase.Instance.ClearLiked (mUserPref.GetValue(TRConstant.UserPrefUserIDKey, 0), true);
				UserDialogs.Instance.Alert("Liked Properties were removed", "Confirm");
			}
		}

		private async void DoClearDislike()
		{
			bool confirm = await UserDialogs.Instance.ConfirmAsync(
				"Are you sure you would like to clear all the properties you marked with a thumb down? \n\r You will not be able to undo this action",
				"Clear Liked Properties",
				"Yes",
				"No");

			if (confirm)
			{
				TRDatabase.Instance.ClearLiked (mUserPref.GetValue(TRConstant.UserPrefUserIDKey, 0), false);
				UserDialogs.Instance.Alert("Thumbs down properties were removed", "Confirm");
			}
		}

		private void DoEnableAllTutorial()
		{
		}

		private async void DoLogout()
		{
			bool confirm = await UserDialogs.Instance.ConfirmAsync(
				"Are you sure you would like to log out?",
				"Logout",
				"Yes",
				"No");
			
			if (confirm) {
				mUserPref.RemovePreference (TRConstant.UserPrefAccessTokenKey);
				mUserPref.RemovePreference (TRConstant.UserPrefRefreshTokenKey);
				mUserPref.RemovePreference (TRConstant.UserPrefUserEmailKey);
				mUserPref.RemovePreference (TRConstant.UserPrefUserIDKey);
				mUserPref.RemovePreference (TRConstant.UserPrefUserPasswordKey);
				ChangePresentation (new TRMvxPresentationHint (TRMvxPresentationHint.TRPresentationType.GotoRoot));
			}
		}
	}
}

