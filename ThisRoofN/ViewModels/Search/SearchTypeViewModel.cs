using System;
using ThisRoofN.ViewModels;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using ThisRoofN.Interfaces;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using System.Linq;
using ThisRoofN.Database.Entities;
using ThisRoofN.Database;

namespace ThisRoofN.ViewModels
{
	public class SearchTypeViewModel : BaseViewModel
	{
		private IDevice device;
		private MvxCommand _normalSearchCommand;
		private MvxCommand _affordSearchCommand;

		public SearchTypeViewModel (IDevice deviceInfo)
		{
			// Identify the Xamarin Insights, here because this will be first screen after login.
			Xamarin.Insights.Identify (deviceInfo.GetUniqueIdentifier (), new Dictionary<string, string> {
				{Xamarin.Insights.Traits.Email, mUserPref.GetValue(TRConstant.UserPrefUserEmailKey)}
			});

			device = deviceInfo;

			SyncLikes ();
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

		private async void SyncLikes() {
			IsLoading = true;
			LoadingText = "Loading";
			List <CottageDetail> savedResults = await mTRService.GetLikes (device.GetUniqueIdentifier ());

			List<TREntityLikes> likedData = savedResults.Select (i => 
				new TREntityLikes () {
					UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
					PropertyID = i.Data.MatrixUniqueID,
					LikeDislike = true,
					Price = i.Data.ListPrice,
					PrimaryPhotoURL = i.PrimaryPhotoUrl,
					Address = i.Address,
					CityStateZip = i.CityStateZip
			}).ToList ();

			foreach (TREntityLikes item in likedData) {
				TRDatabase.Instance.SaveItem (item);	
			}

			IsLoading = false;
		}

		private void DoNormalSearch()
		{
			ShowViewModel<SearchViewModel> ();
		}

		private void DoAffordSearch()
		{
			ShowViewModel<AffordSearchViewModel> ();
		}
	}
}

