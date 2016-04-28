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
using ThisRoofN.Database.Entities;
using ThisRoofN.Database;

namespace ThisRoofN.ViewModels
{
	public class SearchViewModel : BaseViewModel
	{
		public enum ModalType
		{
			PropertyTypes,
			SearchArea,
			InHome,
			InArea,
			Lifestyle,
			HomeType,
			HomeDetails,
			SavedHome,
		}

		private IDevice deviceInfo;

		private MvxCommand _saveCommand;
		private MvxCommand _searchCommand;

		private MvxCommand<ModalType> _gotoModalCommand;

		public SearchViewModel (IDevice device)
		{
			deviceInfo = device;
			DataHelper.CurrentSearchFilter = SearchFilters.FetchLatestFromDatabase ();

			if (TRConstant.PriceValues.Contains ((int)DataHelper.CurrentSearchFilter.MinBudget)) {
				MinBudget = TRConstant.PriceValues.IndexOf ((int)DataHelper.CurrentSearchFilter.MinBudget);
			} else {
				MinBudget = 0;
			}

			if (TRConstant.PriceValues.Contains ((int)DataHelper.CurrentSearchFilter.MaxBudget)) {
				MaxBudget = TRConstant.PriceValues.IndexOf ((int)DataHelper.CurrentSearchFilter.MaxBudget);
			} else {
				MaxBudget = TRConstant.PriceValues.Count - 1;
			}

			SelectedSortType = SortTypes [0];

			_searchItems = new List<ModalType> {
				ModalType.SearchArea,
				ModalType.Lifestyle,
				ModalType.InArea,
				ModalType.HomeDetails,
				ModalType.InHome,
				ModalType.SavedHome
			};
		}

		private List<ModalType> _searchItems;
		public List<ModalType> SearchItems {
			get {
				return _searchItems;
			}
		}

		public ICommand GotoModalCommand {
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
				return  TRConstant.PriceStringValues [(int)Math.Round (MaxBudget, MidpointRounding.AwayFromZero)];
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
				return  TRConstant.PriceStringValues [(int)Math.Round (MinBudget, MidpointRounding.AwayFromZero)];
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

		private async void GotoModal (ModalType type)
		{
			switch (type) {
			case ModalType.PropertyTypes:
				ShowViewModel<PropertyTypeModalViewModel> ();
				break;
			case ModalType.SearchArea:
				ShowViewModel<SearchAreaModalViewModel> ();
				break;
			case ModalType.InHome:
				ShowViewModel<InHomeModalViewModel> ();
				break;
			case ModalType.InArea:
				ShowViewModel<InAreaModalViewModel> ();
				break;
			case ModalType.Lifestyle:
				ShowViewModel<LifestyleModalViewModel> ();
				break;
			case ModalType.HomeType:
				ShowViewModel<HomeStructureModalViewModel> ();
				break;
			case ModalType.HomeDetails:
				ShowViewModel<HomeDetailModalViewModel> ();
				break;
			case ModalType.SavedHome:
				ShowViewModel<SavedPropertiesViewModel> ();
				break;
			}
		}

		private void DoSearch ()
		{
			DoSaveOrSearch (true);
		}

		private async void DoSaveOrSearch (bool isSearch)
		{
			DataHelper.CurrentSearchFilter.MinBudget = TRConstant.PriceValues [(int)Math.Round (MinBudget, MidpointRounding.AwayFromZero)];
			DataHelper.CurrentSearchFilter.MaxBudget = TRConstant.PriceValues [(int)Math.Round (MaxBudget, MidpointRounding.AwayFromZero)];
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
				List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), 24);
				if (searchResults != null) {
					DataHelper.TotalLoadedCount = searchResults.Count;
					if (searchResults != null) {
						DataHelper.SearchResults = searchResults.Select (i =>
							new TRCottageSimple () {
							CottageID = i.ID,
							PrimaryPhotoLink = (i.Photos != null) ? i.Photos.FirstOrDefault ().MediaURL : string.Empty,
							Title = i.Title,
							Price = i.Price,
							Latitude = i.Latitude,
							Longitude = i.Longitude,
						}).ToList ();

						if (DataHelper.CurrentSearchFilter.SearchType == (int)TRSearchType.Commute) {
							DataHelper.SearchMapRange = await mTRService.GetPolygon (deviceInfo.GetUniqueIdentifier ());
						} else {
							DataHelper.SearchMapRange = null;
						}

						this.IsLoading = false;

						ShowViewModel<SearchResultHomeViewModel> ();
					} else {
						this.IsLoading = false;
						bool confirm = await UserDialogs.Instance.ConfirmAsync (
							               "Failed to get results from server. Are you going to continue?",
							               "Error",
							               "Yes",
							               "No");

						if (confirm) {
							ShowViewModel<SearchResultHomeViewModel> ();
						}
					}
				} else {
					UserDialogs.Instance.Alert ("Please check your network connection and try again later", "Server Not Responding");
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

