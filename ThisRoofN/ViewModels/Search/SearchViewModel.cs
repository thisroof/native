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

//			MaxLotSize = MaxLotSizeOptions [0];
//			HasPool = HasPoolOptions [0];
//			SelectedSortType = SortTypes [0];
//			SelectedMaxSquareFeet = MaxSquareFeetOptions [0];
//			InitPropertyTypes ();
//			InitializeViewTypes ();
		}

		public ICommand GotoModalCommand
		{
			get {
				_gotoModalCommand = _gotoModalCommand ?? new MvxCommand<ModalType> (GotoModal);
				return _gotoModalCommand;
			}
		}

//		public ICommand SaveCommand {
//			get {
//				_saveCommand = _saveCommand ?? new MvxCommand (DoSave);
//				return _saveCommand;
//			}
//		}
//
//		public ICommand SearchCommand {
//			get {
//				_searchCommand = _searchCommand ?? new MvxCommand (DoSearch);
//				return _searchCommand;
//			}
//		}


//
//		private void DoSave ()
//		{
//			DoSaveOrSearch (false);
//		}
//
//		private void DoSearch ()
//		{
//			DoSaveOrSearch (true);
//		}

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

//		private async void DoSaveOrSearch (bool isSearch)
//		{
//			if (!Invalidate ()) {
//				return;
//			}
//
//			if (SelectedAddress == null) {
//				searchProperty.Address = string.Empty;
//				searchProperty.City = string.Empty;
//				searchProperty.State = string.Empty;
//				searchProperty.Zip = string.Empty;
//				searchProperty.StartZip = string.Empty;
//				searchProperty.Country = string.Empty;
//			} else {
//				if (!UpdateZipcode ()) {
//
//					// Should be added
//				}
//			}
//
//			switch (searchProperty.SearchType) {
//			case 0:
//				searchProperty.SearchDistance = TRConstant.SearchDistances [SearchMileDistance];
//				searchProperty.StateFilters = "";
//				break;
//			case 1:
//				searchProperty.SearchDistance = TRConstant.SearchMinutesInt [SearchTimeDisatnce];
//				searchProperty.StateFilters = "";
//				break;
//			case 2:
//				searchProperty.SearchDistance = 0;
//				searchProperty.StateFilters = GetStateFiltersSelected ();
//				break;
//			}
//
//			// When user didn't input the address.
//			if (SelectedAddress == null && searchProperty.SearchType != 2) {
//				searchProperty.SearchDistance = 0;
//				searchProperty.StateFilters = string.Join (",", States.Select (x => x.Title).ToArray ());
//			}
//
//			searchProperty.PropertyTypes = GetPropertyTypesSelected ();
//			searchProperty.ViewTypes = GetViewTypesSelected ();
//
//			this.IsLoading = true;
//			this.LoadingText = isSearch ? "Searching..." : "Saving...";
//			searchProperty.MobileNum = deviceInfo.GetUniqueIdentifier ();
//			var res = await mTRService.UpdateUserSearchProperty (searchProperty);
//
//			if (res != null) {
//				mUserPref.SetValue (TRConstant.UserPrefUserIDKey, res.UserID);
//			}
//
//			if (!isSearch) {
//				// Save Search Case
//				this.IsLoading = false;
//
//				if (res != null) {
//					UserDialogs.Instance.Alert ("Updated Successfully", "Success");
//				} else {
//					UserDialogs.Instance.Alert ("Failed to save, try again lager.", "Failure");
//				}
//			} else {	
//				// Search Case
//				if (searchProperty.SearchType == 1) {
//					var positions = await mTRService.GetPolygon (deviceInfo.GetUniqueIdentifier ());
//					this.IsLoading = false;
//				} else {
//					List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), 24);
//					if (searchResults != null) {
//						DataHelper.SearchResults = searchResults.Select (i =>
//							new TRCottageSimple () {
//								CottageID = i.ID,
//								PrimaryPhotoLink = i.Photos.FirstOrDefault ().MediaURL,
//								Latitude = i.Latitude,
//								Longitude = i.Longitude,
//							}).ToList ();
//
//						this.IsLoading = false;
//						ShowViewModel<SearchResultHomeViewModel> ();
//					} else {
//						this.IsLoading = false;
//						UserDialogs.Instance.Alert ("Failed to get results from server. Please try again later.", "Error");
//					}
//				}
//			}
//		}
//
//		private bool Invalidate ()
//		{
//			if (searchProperty.MaxBudget > TRConstant.MaxValidBudget ||
//				searchProperty.MaxBudget < TRConstant.MinValidBudget) {
//				UserDialogs.Instance.Alert (string.Format ("Budget value shoudl be {0} to {1}", TRConstant.MinValidBudget, TRConstant.MaxValidBudget), "Validation");
//				return false;
//			}
//
//			if (searchProperty.MinSquareFootageStructure > 0 &&
//				searchProperty.MaxLotSquareFootage > 0 &&
//				searchProperty.MinSquareFootageStructure > searchProperty.MaxLotSquareFootage) {
//				UserDialogs.Instance.Alert ("The minimum suqare footage should be less than maxmum square footage", "Validation");
//				return false;
//			}
//
//			if (searchProperty.SearchType == 1 && SelectedAddress == null) {
//				UserDialogs.Instance.Alert ("Location Field is Required for Commute Time Search.", "Validation");
//				return false;
//			}
//
//			return true;
//		}
//
//
		#region BUDGET

		public double MaxBudget {
			get {
				return DataHelper.CurrentSearchFilter.MaxBudget;
			} 
			set {
				DataHelper.CurrentSearchFilter.MaxBudget = ((int)value / TRConstant.BudgetStep) * TRConstant.BudgetStep;
				RaisePropertyChanged (() => MaxBudget);
				RaisePropertyChanged (() => BudgetString);
			}
		}

		public double MinBudget {
			get {
				return DataHelper.CurrentSearchFilter.MinBudget;
			} 
			set {
				DataHelper.CurrentSearchFilter.MinBudget = ((int)value / TRConstant.BudgetStep) * TRConstant.BudgetStep;
				RaisePropertyChanged (() => MinBudget);
				RaisePropertyChanged (() => BudgetString);
			}
		}

		public string BudgetString {
			get {
				return string.Format ("${0} - ${1}", DataHelper.CurrentSearchFilter.MinBudget.ToString ("#,##0"), DataHelper.CurrentSearchFilter.MaxBudget.ToString ("#,##0"));
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

		public string SelectedSortType {
			get {
				if (string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.SortBy)) {
					DataHelper.CurrentSearchFilter.SortBy = TRConstant.SortTypes.Keys.ElementAt (0);
				}

				return TRConstant.SortTypes [DataHelper.CurrentSearchFilter.SortBy];
			}
			set {
				DataHelper.CurrentSearchFilter.SortBy = TRConstant.SortTypes.Keys.ElementAt (TRConstant.SortTypes.Values.ToList<string> ().IndexOf (value));
				RaisePropertyChanged (() => SelectedSortType);
			}
		}

		#endregion
//
//
//
//		#region BEDS
//
//		// 0 = Any, 1 = 1+, ...
//		public int MinBeds {
//			get {
//				return (int)searchProperty.MinBeds;
//			} 
//			set {
//				searchProperty.MinBeds = 0;
//				RaisePropertyChanged (() => MinBeds);
//			}
//		}
//
//		#endregion
//
//		#region BATHS
//
//		// 0 = Any, 1 = 1+, ...
//		public int MinBaths {
//			get {
//				return (int)searchProperty.MinBaths;
//			} 
//			set {
//				searchProperty.MinBaths = 0;
//				RaisePropertyChanged (() => MinBaths);
//			}
//		}
//
//		#endregion
//
//		#region PROPERTY TYPES
//
//		private List<CheckboxItemModel> _propertyTypes;
//
//		public List<CheckboxItemModel> PropertyTypes {
//			get {
//				return _propertyTypes;
//			}
//			set {
//				_propertyTypes = value;
//			}
//		}
//
//		private void InitPropertyTypes ()
//		{
//			_propertyTypes = new List<CheckboxItemModel> ();
//			// Init Property types
//			List<string> propertyTypesList = new List<string> ();
//			if (!string.IsNullOrEmpty (searchProperty.PropertyTypes)) {
//				if (searchProperty.PropertyTypes.Contains (",")) {
//					propertyTypesList.AddRange (searchProperty.PropertyTypes.Split (','));
//				} else {
//					propertyTypesList.Add (searchProperty.PropertyTypes);
//				}
//			}
//
//			int index = 0;
//			foreach (var item in TRConstant.SearchPropertyTypes) {
//				_propertyTypes.Add (new CheckboxItemModel {
//					Title = item,
//					Selected = (propertyTypesList.Contains (item) || index == 0 || index == 1 || index == 2)
//				});
//
//				index++;
//			}
//		}
//
//		#endregion
//
//		#region Additional Filters
//
//		public List<string> MinSquareFeetOptions {
//			get {
//				return TRConstant.MinSquareFeetOptions;
//			}
//		}
//
//		public string SelectedMinSquareFeet { 
//			set {
//				int _parsed = 0;
//				int.TryParse (value.ExtractNumber (), out _parsed);
//				if (searchProperty.MinSquareFootageStructure != _parsed) {
//					searchProperty.MinSquareFootageStructure = _parsed;
//					RaisePropertyChanged (() => SelectedMinSquareFeet);
//				}
//			}
//			get {
//				return searchProperty.MinSquareFootageStructure.ToString ();
//			}
//		}
//
//		public List<string> MaxSquareFeetOptions { 
//			get {
//				return TRConstant.MaxSquareFeetOptions;
//			}
//		}
//
//		private string _selectedMaxSquareFeet;
//
//		public string SelectedMaxSquareFeet { 
//			get {
//				if (searchProperty.MaxSquareFootageStructure == 0) {
//					return MaxSquareFeetOptions [MaxSquareFeetOptions.Count - 1];
//				}
//				return searchProperty.MaxSquareFootageStructure.ToString ();
//			}
//			set {
//				int _parsed = 0;
//				int.TryParse (value.ExtractNumber (), out _parsed);
//				if (searchProperty.MaxSquareFootageStructure != _parsed) {
//					searchProperty.MaxSquareFootageStructure = _parsed;
//					RaisePropertyChanged (() => SelectedMaxSquareFeet);
//				}
//			}
//		}
//
//		public List<string> YearBuiltOptions { 
//			get {
//				return TRConstant.YearBuiltOptions;
//			}
//		}
//
//		public string YearBuilt { 
//			get {
//				if (searchProperty.YearBuilt == 0) {
//					return YearBuiltOptions [0];
//				}
//				int newVal = DateTime.Now.Year - searchProperty.YearBuilt;
//				return string.Format ("{0} {1}", newVal, newVal > 1 ? "Years" : "Year");
//			}
//			set {
//				int _parsed = 0;
//				int.TryParse (value.ExtractNumber (), out _parsed);
//				if (_parsed != 0) {
//					_parsed = DateTime.Now.Year - _parsed;
//				}
//				if (searchProperty.YearBuilt != _parsed) {
//					searchProperty.YearBuilt = _parsed;
//					RaisePropertyChanged (() => YearBuilt);
//				}
//			}
//		}
//
//		public List<string> MinLotSizeOptions { 
//			get {
//				return TRConstant.MinLotSizeOptions;
//			}
//		}
//
//		public string MinLotSize { 
//			set {
//				int _parsed = ConvertStringToLotSize (value);
//				if (searchProperty.MinLotSquareFootage != _parsed) {
//					searchProperty.MinLotSquareFootage = _parsed;
//					RaisePropertyChanged (() => MinLotSize);
//				}
//			}
//			get {
//				if (searchProperty.MinLotSquareFootage == 0) {
//					return MinLotSizeOptions [0];
//				}
//				return ConvertLotSizeToString (searchProperty.MinLotSquareFootage);
//			}
//		}
//
//		public List<string> MaxLotSizeOptions { 
//			get {
//				return TRConstant.MaxLotSizeOptions;
//			}
//		}
//
//		public string MaxLotSize { 
//			set {
//				int _parsed = ConvertStringToLotSize (value);
//				if (searchProperty.MaxLotSquareFootage != _parsed) {
//					searchProperty.MaxLotSquareFootage = _parsed;
//					RaisePropertyChanged (() => MaxLotSize);
//				}
//			}
//			get {
//				return ConvertLotSizeToString (searchProperty.MaxLotSquareFootage);
//			}
//		}
//
//		public List<string> DaysOnMarketOptions { 
//			get {
//				return TRConstant.DaysOnMarketOptions;
//			}
//		}
//
//		public string DaysOnMarket { 
//			set {
//				int _parsed = 0;
//				int.TryParse (value.ExtractNumber (), out _parsed);
//
//				if (value.Contains ("Month")) {
//					_parsed = _parsed * 30;
//				} else if (value.Contains ("Year")) {
//					_parsed = _parsed * 365;
//				}
//
//				if (searchProperty.DaysOnMarket != _parsed) {
//					searchProperty.DaysOnMarket = _parsed;
//					RaisePropertyChanged (() => DaysOnMarket);
//				}
//			}
//			get {
//				if (searchProperty.DaysOnMarket == 0) {
//					return DaysOnMarketOptions [0];
//				}
//				return ParseDaysToReadable (searchProperty.DaysOnMarket);
//			}
//		}
//
//		public List<string> HasPoolOptions { 
//			get {
//				return TRConstant.PoolOptions;
//			}
//		}
//
//		public string HasPool { 
//			set {
//				string _parsed = "";
//				if (value.Contains ("Pool")) {
//					_parsed = value.ToLower ().Replace (" ", "_");
//				}
//				if (searchProperty.HasPool != _parsed) {
//					searchProperty.HasPool = _parsed;
//					RaisePropertyChanged (() => HasPool);
//				}
//			}
//			get {
//				if (string.IsNullOrEmpty (searchProperty.HasPool)) {
//					return HasPoolOptions [0];
//				}
//				return searchProperty.HasPool.Contains ("has") ? "Has Pool" : "No Pool";
//			}
//		}
//
//		private List<CheckboxItemModel> _viewTypes;
//
//		public  List<CheckboxItemModel> ViewTypes {
//			get {
//				return _viewTypes;
//			}
//			set {
//				_viewTypes = value;
//			}
//		}
//
//		private void InitializeViewTypes ()
//		{
//			_viewTypes = new List<CheckboxItemModel> ();
//			List<string> viewTypesList = new List<string> ();
//
//			if (searchProperty.ViewTypes != null) {
//				if (searchProperty.ViewTypes.Contains (",")) {
//					viewTypesList.AddRange (searchProperty.ViewTypes.Split (','));
//				} else {
//					viewTypesList.Add (searchProperty.ViewTypes);
//				}
//			}
//
//			foreach (var item in TRConstant.SearchViewTypes) {
//				ViewTypes.Add (new CheckboxItemModel {
//					Title = item,
//					Selected = viewTypesList.Contains (item)
//				});
//			}
//		}
//
//		#endregion
//
//		#region FORECLOSURE
//
//		public string ForeClosureStatus {
//			get {
//				return searchProperty.ForeclosureStatus;
//			} 
//			set {
//				searchProperty.ForeclosureStatus = value;
//			}
//		}
//
//		#endregion
//
//		#region INTERNAL METHODS
//
//		private string ParseDaysToReadable (int days)
//		{
//			int val = days;
//			string res = "Any";
//			if (days > 180) {
//				val = val / 365;
//				res = string.Format ("{0} {1}", val, val > 1 ? "Years" : "Year");
//			} else if (days > 90) {
//				val = val / 30;
//				res = string.Format ("{0} {1}", val, val > 1 ? "Months" : "Month");
//			} else if (days > 0) {
//				res = string.Format ("{0} {1}", val, val > 1 ? "Days" : "Day");
//			}
//			return res;
//		}
//
//		private string ConvertLotSizeToString (int val)
//		{
//			if (val == 0) {
//				return "No Limit";
//			}
//			if (val <= 10000) {
//				return val.ToString ();
//			}
//			double acreVal = (double)val / (double)TRConstant.OneAcreToSquareFoot;
//			return string.Format ("{0} {1}", acreVal, acreVal <= 1 ? "Acre" : "Acres");
//		}
//
//		private int ConvertStringToLotSize (string val)
//		{
//			if (val == "No Limit") {
//				return 0;
//			}
//			if (val.Contains ("Acre")) {
//				double acreVal = double.Parse (val.Split (' ') [0]);
//				return (int)(acreVal * TRConstant.OneAcreToSquareFoot);
//			}
//			return int.Parse (val.ExtractNumber ());
//		}
//
//		private bool UpdateZipcode ()
//		{
//			if (Regex.IsMatch (SelectedAddress.FullAddress, @"^\s*[\d]{5,}\s*$")) {
//				searchProperty.Zip = Regex.Match (SelectedAddress.FullAddress, @"[\d]{5,}").Value;
//				searchProperty.StartZip = searchProperty.Zip;
//				return true;
//			} else if (Regex.IsMatch (SelectedAddress.FullAddress, @"([A-Z]{2})\s([\d]{5,})")) {
//				Match match = Regex.Match (SelectedAddress.FullAddress, @"([A-Z]{2})\s([\d]{5,})");
//				string[] sub_comps = SelectedAddress.FullAddress.Substring (0, match.Index).Split (new String[]{ ", " }, StringSplitOptions.RemoveEmptyEntries);
//				searchProperty.Address = sub_comps [0];
//				if (sub_comps.Length > 1) {
//					searchProperty.City = sub_comps [1];
//				}
//				searchProperty.State = match.Groups [1].Value;
//				searchProperty.Country = "US";
//				searchProperty.Zip = match.Groups [2].Value;
//				searchProperty.StartZip = searchProperty.Zip;
//				return true;
//			}
//			return false;
//		}
//
//		private string GetStateFiltersSelected ()
//		{
//			return string.Join (",", States.Where (x => x.Selected).Select (x => x.Title).ToArray ());
//		}
//
//		private string GetPropertyTypesSelected ()
//		{
//			return string.Join (",", PropertyTypes.Where (x => x.Selected).Select (x => x.Title).ToArray ());
//		}
//
//		private string GetViewTypesSelected ()
//		{
//			return string.Join (",", ViewTypes.Where (x => x.Selected).Select (x => x.Title).ToArray ());
//		}
//
//		#endregion
	}
}

