using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using Newtonsoft.Json;
using ThisRoofN.Helpers;
using ThisRoofN.Models.App;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using ThisRoofN.Database.Entities;
using ThisRoofN.Interfaces;
using ThisRoofN.Database;
using Acr.UserDialogs;

namespace ThisRoofN.ViewModels
{
	public class SavedPropertiesViewModel : BaseViewModel
	{
		private IDevice deviceInfo;
		private MvxCommand<int> _detailCommand;
		private MvxCommand<TREntityLikes> _detailItemCommand;
		private MvxCommand _reloadDataCommand;
		private List<TREntityLikes> savedProperties;

		public SavedPropertiesViewModel (IDevice device)
		{
			deviceInfo = device;
		}

		public void Init () {
			
		}

		public ICommand ReloadDataCommand {
			get {
				_reloadDataCommand = _reloadDataCommand ?? new MvxCommand (DoReloadData);
				return _reloadDataCommand;
			}
		}

		public ICommand DetailCommand {
			get {
				_detailCommand = _detailCommand ?? new MvxCommand<int> (GotoDetail);
				return _detailCommand;
			}
		}

		public ICommand DetailItemCommand {
			get {
				_detailItemCommand = _detailItemCommand ?? new MvxCommand<TREntityLikes> (GotoDetailItem);
				return _detailItemCommand;
			}
		}

		private void DoReloadData() {
			SavedProperties = TRDatabase.Instance.GetCottageLikedList (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0));
			if (SavedProperties.Count == 0) {
				UserDialogs.Instance.Alert ("You have no saved property", "ThisRoof");
			}
		}

		private async void GotoDetail (int index)
		{
			string propertyID =  savedProperties[index].PropertyID;

			this.IsLoading = true;
			this.LoadingText = "Loading Detail";
			CottageDetail detail = await mTRService.GetCottageDetail (deviceInfo.GetUniqueIdentifier (), propertyID);

			DataHelper.SelectedCottageDetail = new TRCottageDetail (detail);

			this.IsLoading = false;

			ShowViewModel<SearchResultDetailViewModel> ();
		}

		private async void GotoDetailItem(TREntityLikes item)
		{
			string propertyID =  item.PropertyID;

			this.IsLoading = true;
			this.LoadingText = "Loading Detail";
			CottageDetail detail = await mTRService.GetCottageDetail (deviceInfo.GetUniqueIdentifier (), propertyID);

			DataHelper.SelectedCottageDetail = new TRCottageDetail (detail);

			this.IsLoading = false;

			ShowViewModel<SearchResultDetailViewModel> (new {index = 0, savedProperty = true});
		}

		public List<TREntityLikes> SavedProperties 
		{
			get {
				return savedProperties;
			}
			set {
				savedProperties = value;
				RaisePropertyChanged (() => SavedProperties);
			}
		}
	}
}

