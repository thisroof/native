using System;
using ThisRoofN.Models;
using System.Linq;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using ThisRoofN.Interfaces;
using Newtonsoft.Json;
using ThisRoofN.Models.Service;
using ThisRoofN.RestService;
using ThisRoofN.Models.App;
using ThisRoofN.Helpers;
using ThisRoofN.Extensions;

namespace ThisRoofN.ViewModels
{
	public class SearchViewModel : BaseViewModel
	{
		public enum ModalType
		{
			SearchArea,
			InHome,
			InArea,
			Location,
			Architecture,
			Lifestyle,
			HomeStructure,
			HomeDetails
		}

		private IDevice deviceInfo;

		private MvxCommand _saveCommand;
		private MvxCommand _searchCommand;

		private MvxCommand<ModalType> _gotoModalCommand;

		public SearchViewModel (IDevice device)
		{
			deviceInfo = device;
			DataHelper.CurrentSearchFilter = SearchFilters.FetchLatestFromDatabase();

			MinBudget = 0;
			MaxBudget = TRConstant.PriceStringValues.Count - 1;
			SelectedSortType = SortTypes[0];
		}

		public ICommand GotoModalCommand
		{
			get {
				_gotoModalCommand = _gotoModalCommand ?? new MvxCommand<ModalType> (GotoModal);
				return _gotoModalCommand;
			}
		}

		public ICommand SearchCommand {
			get {
				_searchCommand = _searchCommand ?? new MvxCommand (DoSearch);
				return _searchCommand;
			}
		}


		#region BUDGET
		private float _maxBudget;
		public float MaxBudget {
			get {
				return _maxBudget;
			} 
			set {
				_maxBudget = value;
				RaisePropertyChanged (() => MaxBudgetString);
				RaisePropertyChanged (() => MaxBudget);
			}
		}

		public string MaxBudgetString {
			get {
				return  TRConstant.PriceStringValues [(int)Math.Round(MaxBudget, MidpointRounding.AwayFromZero)];
			}
		}

		private float _minBudget;
		public float MinBudget {
			get {
				return _minBudget;
			} 
			set {
				_minBudget = value;
				RaisePropertyChanged (() => MinBudgetString);
				RaisePropertyChanged (() => MinBudget);
			}
		}

		public string MinBudgetString {
			get {
				return  TRConstant.PriceStringValues [(int)Math.Round(MinBudget, MidpointRounding.AwayFromZero)];
			}
		}
		#endregion

		#region SORT_BY

		private List<string> _sortTypes;

		public List<string> SortTypes {
			get {
				return TRConstant.SortTypes.Values.ToList<string> ();
			}
		}

		private string _selectedSortType;
		public string SelectedSortType {
			get {
				return _selectedSortType;
			}
			set {
				_selectedSortType = value;
				RaisePropertyChanged (() => SelectedSortType);
			}
		}

		#endregion

		#region Methods
		private void GotoModal(ModalType type)
		{
			switch (type) {
			case ModalType.SearchArea:
				ShowViewModel<SearchAreaModalViewModel> ();
				break;
			case ModalType.InHome:
				ShowViewModel<InHomeModalViewModel> ();
				break;
			case ModalType.InArea:
				ShowViewModel<InAreaModalViewModel> ();
				break;
			case ModalType.Location:
				ShowViewModel<LocationModalViewModel> ();
				break;
			case ModalType.Architecture:
				ShowViewModel<ArchitectureModalViewModel> ();
				break;
			case ModalType.Lifestyle:
				ShowViewModel<LifestyleModalViewModel> ();
				break;
			case ModalType.HomeStructure:
				ShowViewModel<HomeStructureModalViewModel> ();
				break;
			case ModalType.HomeDetails:
				ShowViewModel<HomeDetailModalViewModel> ();
				break;
			}
		}

		private void DoSearch ()
		{
			DoSaveOrSearch (true);
		}

		private async void DoSaveOrSearch (bool isSearch)
		{
			DataHelper.CurrentSearchFilter.MinBudget = TRConstant.PriceValues [(int)Math.Round(MinBudget, MidpointRounding.AwayFromZero)];
			DataHelper.CurrentSearchFilter.MaxBudget =  TRConstant.PriceValues [(int)Math.Round(MaxBudget, MidpointRounding.AwayFromZero)];
			DataHelper.CurrentSearchFilter.SortBy = TRConstant.SortTypes.Keys.ElementAt (TRConstant.SortTypes.Values.ToList<string> ().IndexOf (SelectedSortType));
			DataHelper.CurrentSearchFilter.MobileNum = deviceInfo.GetUniqueIdentifier ();

			if (!Invalidate ()) {
				return;
			}

			this.IsLoading = true;
			this.LoadingText = isSearch ? "Searching..." : "Saving...";



			// Call Update User Search Property API
			var res = await mTRService.UpdateUserSearchProperty (DataHelper.CurrentSearchFilter);
			if (res != null) {
				mUserPref.SetValue (TRConstant.UserPrefUserIDKey, res.UserID);
			}

			if (!isSearch) {
				// Save Search Case
				this.IsLoading = false;

				if (res != null) {
					UserDialogs.Instance.Alert ("Updated Successfully", "Success");
				} else {
					UserDialogs.Instance.Alert ("Failed to save, try again lager.", "Failure");
				}
			} else {	
				// Search Case
				if (DataHelper.CurrentSearchFilter.SearchType == 1) {
					var positions = await mTRService.GetPolygon (deviceInfo.GetUniqueIdentifier ());
					this.IsLoading = false;
				} else {
					List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), 24);
					if (searchResults != null) {
						DataHelper.SearchResults = searchResults.Select (i =>
							new TRCottageSimple () {
								CottageID = i.ID,
								PrimaryPhotoLink = i.Photos.FirstOrDefault ().MediaURL,
								Latitude = i.Latitude,
								Longitude = i.Longitude,
							}).ToList ();

						this.IsLoading = false;
						ShowViewModel<SearchResultHomeViewModel> ();
					} else {
						this.IsLoading = false;
						UserDialogs.Instance.Alert ("Failed to get results from server. Please try again later.", "Error");
					}
				}
			}
		}

		private bool Invalidate ()
		{
			if (DataHelper.CurrentSearchFilter.MaxBudget > TRConstant.MaxValidBudget ||
				DataHelper.CurrentSearchFilter.MaxBudget < TRConstant.MinValidBudget) {
				UserDialogs.Instance.Alert (string.Format ("Budget value should be {0} to {1}", TRConstant.MinValidBudget, TRConstant.MaxValidBudget), "Validation");
				return false;
			}

			if (string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.StartZip) && DataHelper.CurrentSearchFilter.SearchType != (int)TRSearchType.State) {
				UserDialogs.Instance.Alert ("Please input address in SEARCH AREA category", "Validation");
				return false;
			}

			if (DataHelper.CurrentSearchFilter.MinSquareFootageStructure > 0 &&
				DataHelper.CurrentSearchFilter.MaxLotSquareFootage > 0 &&
				DataHelper.CurrentSearchFilter.MinSquareFootageStructure > DataHelper.CurrentSearchFilter.MaxLotSquareFootage) {
				UserDialogs.Instance.Alert ("The minimum suqare footage should be less than maxmum square footage", "Validation");
				return false;
			}

			if (DataHelper.CurrentSearchFilter.SearchType == 1 && DataHelper.CurrentSearchFilter == null) {
				UserDialogs.Instance.Alert ("Location Field is Required for Commute Time Search.", "Validation");
				return false;
			}

			return true;
		}
		#endregion
	}
}

