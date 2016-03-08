using System;
using ThisRoofN.ViewModels;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using ThisRoofN.Interfaces;
using System.Collections.Generic;

namespace ThisRoofN.ViewModels
{
	public class SearchTypeViewModel : BaseViewModel
	{
		private MvxCommand _normalSearchCommand;
		private MvxCommand _affordSearchCommand;

		public SearchTypeViewModel (IDevice deviceInfo)
		{
			// Identify the Xamarin Insights, here because this will be first screen after login.
			Xamarin.Insights.Identify (deviceInfo.GetUniqueIdentifier (), new Dictionary<string, string> {
				{Xamarin.Insights.Traits.Email, mUserPref.GetValue(TRConstant.UserPrefUserEmailKey)}
			});
		}

		public ICommand NormalSearchCommand
		{
			get {
				_normalSearchCommand = _normalSearchCommand ?? new MvxCommand (DoNormalSearch);
				return _normalSearchCommand;
			}
		}

		public ICommand AffordSearchCommand
		{
			get {
				_affordSearchCommand = _affordSearchCommand ?? new MvxCommand (DoAffordSearch);
				return _affordSearchCommand;
			}
		}

		private void DoNormalSearch()
		{
		}

		private void DoAffordSearch()
		{
		}
	}
}

