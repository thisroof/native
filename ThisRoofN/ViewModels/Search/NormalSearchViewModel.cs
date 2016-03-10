using System;
using ThisRoofN.Models;
using System.Linq;
using System.Collections.Generic;

namespace ThisRoofN.ViewModels
{
	public class NormalSearchViewModel : BaseViewModel
	{
		private TRUserSearchProperty searchProperty;

		public NormalSearchViewModel ()
		{
			searchProperty = TRUserSearchProperty.FetchLatestFromDatabase ();


			InitPropertyTypes ();
			InitializeViewTypes ();
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

		//
		// HINT: should be addedAutcomplete Address VIEW HERE
		//

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
				if (_viewTypes == null) {
					_viewTypes = new List<CheckboxItemModel> ();

				}
				return _viewTypes;
			}
			set {
				_viewTypes = value;
				RaisePropertyChanged (() => ViewTypes);
			}
		}

		private void InitializeViewTypes ()
		{
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
		#endregion
	}
}

