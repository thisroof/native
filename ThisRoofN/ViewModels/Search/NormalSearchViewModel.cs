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

namespace ThisRoofN.ViewModels
{
	public class NormalSearchViewModel : BaseViewModel
	{
		private IDevice deviceInfo;
		private TRUserSearchProperty searchProperty;

		public NormalSearchViewModel (IDevice device)
		{
			deviceInfo = device;
			searchProperty = TRUserSearchProperty.FetchLatestFromDatabase ();

			MaxLotSize = MaxLotSizeOptions [0];
			HasPool = HasPoolOptions [0];
			SelectedSortType = SortTypes [0];
			SelectedMaxSquareFeet = MaxSquareFeetOptions [0];
			InitStates ();
			InitPropertyTypes ();
			InitializeViewTypes ();

			AddressSuggestionItems = new List<TRGoogleMapPlace> {
				new TRGoogleMapPlace() {
					FullAddress = "Washington"
				},
				new TRGoogleMapPlace() {
					FullAddress = "California"
				},
				new TRGoogleMapPlace() {
					FullAddress = "St.Louis"
				},
				new TRGoogleMapPlace() {
					FullAddress = "San Jose"
				}

			};
		}

		private MvxCommand _saveCommand;
		private MvxCommand _searchCommand;

		public ICommand SaveCommand {
			get {
				_saveCommand = _saveCommand ?? new MvxCommand (DoSave);
				return _saveCommand;
			}
		}

		public ICommand SearchCommand {
			get {
				_searchCommand = _searchCommand ?? new MvxCommand (DoSearch);
				return _searchCommand;
			}
		}

		private void DoSave() {
			DoSaveOrSearch (false);
		}

		private void DoSearch() {
			DoSaveOrSearch (true);
		}

		private async void DoSaveOrSearch(bool isSearch) {
			if (!Invalidate ()) {
				return;
			}

			if (SelectedAddress == null) {
				searchProperty.Address = string.Empty;
				searchProperty.City = string.Empty;
				searchProperty.State = string.Empty;
				searchProperty.Zip = string.Empty;
				searchProperty.StartZip = string.Empty;
				searchProperty.Country = string.Empty;
			} else {
				if (!UpdateZipcode ()) {
					
					// Should be added
				}
			}

			switch (searchProperty.SearchType) {
			case 0:
				searchProperty.SearchDistance = TRConstant.SearchDistances[SearchMileDistance];
				searchProperty.StateFilters = "";
				break;
			case 1:
				searchProperty.SearchDistance = TRConstant.SearchMinutesInt[SearchTimeDisatnce];
				searchProperty.StateFilters = "";
				break;
			case 2:
				searchProperty.SearchDistance = 0;
				searchProperty.StateFilters = GetStateFiltersSelected ();
				break;
			}

			// When user didn't input the address.
			if (SelectedAddress == null && searchProperty.SearchType != 2) {
				searchProperty.SearchDistance = 0;
				searchProperty.StateFilters = string.Join(",", States.Select(x => x.Title).ToArray ());
			}

			searchProperty.PropertyTypes = GetPropertyTypesSelected ();
			searchProperty.ViewTypes = GetViewTypesSelected ();

			UserDialogs.Instance.ShowLoading (isSearch ? "Searching Results" : "Saving your Search");
			searchProperty.MobileNum = deviceInfo.GetUniqueIdentifier ();
			var res = await mTRService.UpdateUserSearchProperty(searchProperty);

			if (!isSearch) {
				
				// Save Search Case
				UserDialogs.Instance.HideLoading ();
				if (res != null) {
					UserDialogs.Instance.Alert ("Updated Successfully", "Success");
				} else {
					UserDialogs.Instance.Alert ("Failed to save, try again lager.", "Failure");
				}
			} else {
				
				// Search Case
				if (searchProperty.SearchType == 1) {
					var positions = await mTRService.GetPolygon (deviceInfo.GetUniqueIdentifier());
					UserDialogs.Instance.HideLoading ();
				} else {
					DataHelper.SearchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier(), 24);
					UserDialogs.Instance.HideLoading ();
					ShowViewModel<SearchResultHomeViewModel> ();
				}
			}
		}

		private bool Invalidate()
		{
			if (searchProperty.MaxBudget > TRConstant.MaxValidBudget || 
				searchProperty.MaxBudget < TRConstant.MinValidBudget) {
				UserDialogs.Instance.Alert (string.Format ("Budget value shoudl be {0} to {1}", TRConstant.MinValidBudget, TRConstant.MaxValidBudget), "Validation");
				return false;
			}

			if (searchProperty.MinSquareFootageStructure > 0 &&
				searchProperty.MaxLotSquareFootage > 0 &&
				searchProperty.MinSquareFootageStructure > searchProperty.MaxLotSquareFootage) {
				UserDialogs.Instance.Alert ("The minimum suqare footage should be less than maxmum square footage", "Validation");
				return false;
			}

			if (searchProperty.SearchType == 1 && SelectedAddress == null) {
				UserDialogs.Instance.Alert ("Location Field is Required for Commute Time Search.", "Validation");
				return false;
			}

			return true;
		}

		public string MaxBudget {
			get {
				if (searchProperty.MaxBudget == 0) {
					return string.Empty;
				}

				return searchProperty.MaxBudget.ToString ("#,##0");
			}
			set {
				double dvalue;
				double.TryParse (value, out dvalue);
				searchProperty.MaxBudget = dvalue;
				RaisePropertyChanged (() => MaxBudget);
			}
		}

		#region SORT_BY

		private List<string> _sortTypes;

		public List<string> SortTypes {
			get {
				return TRConstant.SortTypes.Values.ToList<string> ();
			}
		}

		public string SelectedSortType {
			get {
				if (string.IsNullOrEmpty (searchProperty.SortBy)) {
					searchProperty.SortBy = TRConstant.SortTypes.Keys.ElementAt (0);
				}

				return TRConstant.SortTypes [searchProperty.SortBy];
			}
			set {
				searchProperty.SortBy = TRConstant.SortTypes.Keys.ElementAt (TRConstant.SortTypes.Values.ToList<string> ().IndexOf (value));
				RaisePropertyChanged (() => SelectedSortType);
			}
		}

		#endregion

		#region SEARCH_AREA
		// 0 = distance, 1 = commute, 2 = state
		public int DistanceType {
			get {
				return searchProperty.SearchType;
			} 
			set {
				searchProperty.SearchType = (short)value;
				RaisePropertyChanged (() => DistanceType);
			}
		}

		private List<TRGoogleMapPlace> _addressSuggestionItems;
		public List<TRGoogleMapPlace> AddressSuggestionItems
		{
			get
			{
				return _addressSuggestionItems;
			}
			set
			{
				_addressSuggestionItems = value;
				RaisePropertyChanged (() => AddressSuggestionItems);
			}
		}

		private TRGoogleMapPlace _selectedAddress;
		public TRGoogleMapPlace SelectedAddress
		{
			get {
				return _selectedAddress;
			}
			set {
				_selectedAddress = value;
				RaisePropertyChanged (() => SelectedAddress);
			}
		}

		private int _searchTimeDistance;

		public int SearchTimeDisatnce {
			get {
				return _searchTimeDistance;
			} 
			set {
				_searchTimeDistance = value;
				RaisePropertyChanged (() => SearchTimeDisatnce);
			}
		}

		private int _searchMileDistance;

		public int SearchMileDistance {
			get {
				return _searchMileDistance;
			} 
			set {
				_searchMileDistance = value;
				RaisePropertyChanged (() => SearchMileDistance);
			}
		}

		// 0 = driving, 1 = carpool
		public int TravelMode {
			get {
				return searchProperty.TravelMode;
			} 
			set {
				searchProperty.TravelMode = value;
				RaisePropertyChanged (() => TravelMode);
			}
		}

		// 0 = no traffic, 1 = rush hour
		public int TrafficType {
			get {
				return searchProperty.TrafficType;
			} 
			set {
				searchProperty.TrafficType = value;
				RaisePropertyChanged (() => TrafficType);
			}
		}


		private List<CheckboxItemModel> _states;

		public List<CheckboxItemModel> States {
			get {
				return _states;
			}
			set {
				_states = value;
			}
		}

		private void InitStates()
		{
			_states = new List<CheckboxItemModel> ();

			// Init Property types
			List<string> statesList = new List<string> ();
			if (!string.IsNullOrEmpty(searchProperty.StateFilters)) {
				if (searchProperty.StateFilters.Contains (",")) {
					statesList.AddRange (searchProperty.StateFilters.Split (','));
				} else {
					statesList.Add (searchProperty.StateFilters);
				}
			}
				
			foreach (var item in TRConstant.StateFilters) {
				_states.Add (new CheckboxItemModel {
					Title = item,
					Selected = (statesList.Contains(item))
				});
			}
		}
		#endregion

		#region BEDS

		// 0 = Any, 1 = 1+, ...
		public int MinBeds {
			get {
				return (int)searchProperty.MinBeds;
			} 
			set {
				searchProperty.MinBeds = 0;
				RaisePropertyChanged (() => MinBeds);
			}
		}

		#endregion

		#region BATHS

		// 0 = Any, 1 = 1+, ...
		public int MinBaths {
			get {
				return (int)searchProperty.MinBaths;
			} 
			set {
				searchProperty.MinBaths = 0;
				RaisePropertyChanged (() => MinBaths);
			}
		}

		#endregion

		#region PROPERTY TYPES

		private List<CheckboxItemModel> _propertyTypes;

		public List<CheckboxItemModel> PropertyTypes {
			get {
				return _propertyTypes;
			}
			set {
				_propertyTypes = value;
			}
		}

		private void InitPropertyTypes()
		{
			_propertyTypes = new List<CheckboxItemModel> ();
			// Init Property types
			List<string> propertyTypesList = new List<string> ();
			if (!string.IsNullOrEmpty(searchProperty.PropertyTypes)) {
				if (searchProperty.PropertyTypes.Contains (",")) {
					propertyTypesList.AddRange (searchProperty.PropertyTypes.Split (','));
				} else {
					propertyTypesList.Add (searchProperty.PropertyTypes);
				}
			}

			int index = 0;
			foreach (var item in TRConstant.SearchPropertyTypes) {
				_propertyTypes.Add (new CheckboxItemModel {
					Title = item,
					Selected = (propertyTypesList.Contains(item) || index == 0 || index == 1 || index == 2)
				});

				index++;
			}
		}
		#endregion

		#region Additional Filters
		public List<string> MinSquareFeetOptions
		{
			get {
				return TRConstant.MinSquareFeetOptions;
			}
		}

		public string SelectedMinSquareFeet{ 
			set
			{
				int _parsed = 0;
				int.TryParse (value.ExtractNumber(), out _parsed);
				if (searchProperty.MinSquareFootageStructure != _parsed) {
					searchProperty.MinSquareFootageStructure = _parsed;
					RaisePropertyChanged (() => SelectedMinSquareFeet);
				}
			}
			get
			{
				return searchProperty.MinSquareFootageStructure.ToString();
			}
		}

		public List<string> MaxSquareFeetOptions{ 
			get
			{
				return TRConstant.MaxSquareFeetOptions;
			}
		}

		private string _selectedMaxSquareFeet;
		public string SelectedMaxSquareFeet{ 
			get
			{
				if (searchProperty.MaxSquareFootageStructure == 0) {
					return MaxSquareFeetOptions [MaxSquareFeetOptions.Count - 1];
				}
				return searchProperty.MaxSquareFootageStructure.ToString();
			}
			set
			{
				int _parsed = 0;
				int.TryParse (value.ExtractNumber(), out _parsed);
				if (searchProperty.MaxSquareFootageStructure != _parsed) {
					searchProperty.MaxSquareFootageStructure = _parsed;
					RaisePropertyChanged (() => SelectedMaxSquareFeet);
				}
			}
		}
			
		public List<string> YearBuiltOptions{ 
			get
			{
				return TRConstant.YearBuiltOptions;
			}
		}

		public string YearBuilt{ 
			get
			{
				if (searchProperty.YearBuilt == 0) {
					return YearBuiltOptions [0];
				}
				int newVal = DateTime.Now.Year - searchProperty.YearBuilt;
				return string.Format("{0} {1}", newVal, newVal > 1 ? "Years" : "Year");
			}
			set
			{
				int _parsed = 0;
				int.TryParse (value.ExtractNumber(), out _parsed);
				if (_parsed != 0) {
					_parsed = DateTime.Now.Year - _parsed;
				}
				if (searchProperty.YearBuilt != _parsed) {
					searchProperty.YearBuilt = _parsed;
					RaisePropertyChanged (() => YearBuilt);
				}
			}
		}

		public List<string> MinLotSizeOptions{ 
			get
			{
				return TRConstant.MinLotSizeOptions;
			}
		}

		public string MinLotSize{ 
			set
			{
				int _parsed = ConvertStringToLotSize(value);
				if (searchProperty.MinLotSquareFootage != _parsed) {
					searchProperty.MinLotSquareFootage = _parsed;
					RaisePropertyChanged (() => MinLotSize);
				}
			}
			get
			{
				if (searchProperty.MinLotSquareFootage == 0) {
					return MinLotSizeOptions [0];
				}
				return ConvertLotSizeToString(searchProperty.MinLotSquareFootage);
			}
		}

		public List<string> MaxLotSizeOptions{ 
			get
			{
				return TRConstant.MaxLotSizeOptions;
			}
		}

		public string MaxLotSize{ 
			set
			{
				int _parsed = ConvertStringToLotSize(value);
				if (searchProperty.MaxLotSquareFootage != _parsed) {
					searchProperty.MaxLotSquareFootage = _parsed;
					RaisePropertyChanged (() => MaxLotSize);
				}
			}
			get
			{
				return ConvertLotSizeToString(searchProperty.MaxLotSquareFootage);
			}
		}
			
		public List<string> DaysOnMarketOptions{ 
			get
			{
				return TRConstant.DaysOnMarketOptions;
			}
		}

		public string DaysOnMarket{ 
			set
			{
				int _parsed = 0;
				int.TryParse (value.ExtractNumber(), out _parsed);

				if (value.Contains ("Month")) {
					_parsed = _parsed * 30;
				} else if (value.Contains ("Year")) {
					_parsed = _parsed * 365;
				}

				if (searchProperty.DaysOnMarket != _parsed) {
					searchProperty.DaysOnMarket = _parsed;
					RaisePropertyChanged (() => DaysOnMarket);
				}
			}
			get
			{
				if (searchProperty.DaysOnMarket == 0) {
					return DaysOnMarketOptions [0];
				}
				return ParseDaysToReadable(searchProperty.DaysOnMarket);
			}
		}

		public List<string> HasPoolOptions{ 
			get
			{
				return TRConstant.PoolOptions;
			}
		}
			
		public string HasPool{ 
			set
			{
				string _parsed = "";
				if (value.Contains ("Pool")) {
					_parsed = value.ToLower ().Replace (" ", "_");
				}
				if (searchProperty.HasPool != _parsed) {
					searchProperty.HasPool = _parsed;
					RaisePropertyChanged (() => HasPool);
				}
			}
			get
			{
				if (string.IsNullOrEmpty (searchProperty.HasPool)) {
					return HasPoolOptions [0];
				}
				return searchProperty.HasPool.Contains("has") ? "Has Pool" : "No Pool";
			}
		}

		private List<CheckboxItemModel> _viewTypes;
		public  List<CheckboxItemModel> ViewTypes  {
			get {
				return _viewTypes;
			}
			set {
				_viewTypes = value;
			}
		}

		private void InitializeViewTypes ()
		{
			_viewTypes = new List<CheckboxItemModel> ();
			List<string> viewTypesList = new List<string>();

			if (searchProperty.ViewTypes != null) {
				if (searchProperty.ViewTypes.Contains (",")) {
					viewTypesList.AddRange (searchProperty.ViewTypes.Split (','));
				} else {
					viewTypesList.Add (searchProperty.ViewTypes);
				}
			}

			foreach (var item in TRConstant.SearchViewTypes) {
				ViewTypes.Add(new CheckboxItemModel{
					Title = item,
					Selected = viewTypesList.Contains(item)
				});
			}
		}
		#endregion

		#region FORECLOSURE
		public string ForeClosureStatus
		{
			get {
				return searchProperty.ForeclosureStatus;
			} 
			set {
				searchProperty.ForeclosureStatus = value;
			}
		}
		#endregion

		#region INTERNAL METHODS
		private string ParseDaysToReadable(int days) {
			int val = days;
			string res = "Any";
			if (days > 180) {
				val = val / 365;
				res = string.Format ("{0} {1}", val, val > 1 ? "Years" : "Year");
			} else if (days > 90) {
				val = val / 30;
				res = string.Format ("{0} {1}", val, val > 1 ? "Months" : "Month");
			} else if(days > 0) {
				res = string.Format ("{0} {1}", val, val > 1 ? "Days" : "Day");
			}
			return res;
		}

		private string ConvertLotSizeToString(int val) {
			if(val == 0) {
				return "No Limit";
			}
			if( val <= 10000) {
				return val.ToString ();
			}
			double acreVal = (double)val / (double)TRConstant.OneAcreToSquareFoot;
			return string.Format("{0} {1}", acreVal, acreVal <= 1 ? "Acre" : "Acres");
		}

		private int ConvertStringToLotSize(string val) {
			if (val == "No Limit") {
				return 0;
			}
			if(val.Contains("Acre")){
				double acreVal = double.Parse (val.Split (' ') [0]);
				return (int)(acreVal * TRConstant.OneAcreToSquareFoot);
			}
			return int.Parse (val.ExtractNumber());
		}

		private bool UpdateZipcode(){
			if (Regex.IsMatch (SelectedAddress.FullAddress, @"^\s*[\d]{5,}\s*$")) {
				searchProperty.Zip = Regex.Match (SelectedAddress.FullAddress, @"[\d]{5,}").Value;
				searchProperty.StartZip = searchProperty.Zip;
				return true;
			} else if (Regex.IsMatch (SelectedAddress.FullAddress, @"([A-Z]{2})\s([\d]{5,})")) {
				Match match = Regex.Match (SelectedAddress.FullAddress, @"([A-Z]{2})\s([\d]{5,})");
				string[] sub_comps = SelectedAddress.FullAddress.Substring(0, match.Index).Split (new String[]{", "}, StringSplitOptions.RemoveEmptyEntries);
				searchProperty.Address = sub_comps [0];
				if (sub_comps.Length > 1) {
					searchProperty.City = sub_comps [1];
				}
				searchProperty.State = match.Groups [1].Value;
				searchProperty.Country = "US";
				searchProperty.Zip = match.Groups[2].Value;
				searchProperty.StartZip = searchProperty.Zip;
				return true;
			}
			return false;
		}

		private string GetStateFiltersSelected(){
			return string.Join(",", States.Where (x => x.Selected).Select(x => x.Title).ToArray ());
		}

		private string GetPropertyTypesSelected(){
			return string.Join(",", PropertyTypes.Where (x => x.Selected).Select(x => x.Title).ToArray ());
		}
		private string GetViewTypesSelected(){
			return string.Join(",", ViewTypes.Where (x => x.Selected).Select(x => x.Title).ToArray ());
		}
		#endregion
	}
}

