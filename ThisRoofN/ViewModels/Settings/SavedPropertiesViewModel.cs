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
using System.Linq;

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
			DataHelper.SearchResults = SavedProperties.Select (i => new TRCottageSimple() {
				CottageID = i.PropertyID
			}).ToList();
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
			if (detail != null) {
				DataHelper.SelectedCottageDetail = new TRCottageDetail (detail);
				this.IsLoading = false;
				ShowViewModel<SearchResultDetailViewModel> (new {index = 0, savedProperty = true});
			} else {
				UserDialogs.Instance.Alert ("Please check your network connection and try again later", "Server Not Responding");
			}
		}

		private async void GotoDetailItem(TREntityLikes item)
		{
			string propertyID =  item.PropertyID;

			this.IsLoading = true;
			this.LoadingText = "Loading Detail";
			CottageDetail detail = await mTRService.GetCottageDetail (deviceInfo.GetUniqueIdentifier (), propertyID);
			if (detail != null) {
				DataHelper.SelectedCottageDetail = new TRCottageDetail (detail);
				this.IsLoading = false;
				ShowViewModel<SearchResultDetailViewModel> (new {index = 0, savedProperty = true});
			} else {
				UserDialogs.Instance.Alert ("Please check your network connection and try again later", "Server Not Responding");
			}
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

