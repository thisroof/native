using System;
using ThisRoofN.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Acr.UserDialogs;

namespace ThisRoofN.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private MvxCommand _savedPropertyCommand;
		private MvxCommand _supportCommand;
		private MvxCommand _privacyCommand;
		private MvxCommand _termCommand;
		private MvxCommand _licenseCommand;
		private MvxCommand _clearLikeCommand;
		private MvxCommand _clearDislikeCommand;
		private MvxCommand _enableAllTutorialCommand;
		private MvxCommand _logoutCommand;

		public SettingsViewModel ()
		{
			
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

		private void DoGetSavedProperty()
		{
			UserDialogs.Instance.Alert ("You do not have any saved properties.", "Settings");
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

		private void DoClearLike()
		{
		}

		private void DoClearDislike()
		{
		}

		private void DoEnableAllTutorial()
		{
		}

		private void DoLogout()
		{
			ChangePresentation (new TRMvxPresentationHint (TRMvxPresentationHint.TRPresentationType.GotoRoot));
		}
	}
}

