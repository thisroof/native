using System;
using ThisRoofN.Helpers;
using ThisRoofN.Models.App;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using ThisRoofN.RestService;
using ThisRoofN.Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using Acr.UserDialogs;
using MvxPlugins.Geocoder;
using MvvmCross.Platform;
using ThisRoofN.Models.Service;

namespace ThisRoofN.ViewModels
{
	public class SearchAreaModalViewModel : BaseViewModel
	{
		private GeocodeService mGeocodeService;
		private IGeocoder geocoder;

		public SearchAreaModalViewModel ()
		{
			geocoder = Mvx.Resolve<IGeocoder> ();
			mGeocodeService = new GeocodeService ();

			DistanceType = DataHelper.CurrentSearchFilter.SearchType;

			if (DistanceType == 0) {
				Distance = TRConstant.SearchDistances.IndexOf (DataHelper.CurrentSearchFilter.SearchDistance);
			} else if (DistanceType == 1) {
				Distance = TRConstant.SearchMinutesInt.IndexOf (DataHelper.CurrentSearchFilter.SearchDistance);
			}
				
			// init states
			InitAddress ();
			InitStates ();
			InitCommuteItems ();
		}

		private MvxCommand _modalCloseCommand;

		public ICommand ModalCloseCommand {
			get {
				_modalCloseCommand = _modalCloseCommand ?? new MvxCommand (() => {
					DoSaveAndClose ();
				});
				return _modalCloseCommand;
			}
		}


		private MvxCommand<CheckboxItemModel> _nationItemSelectCommand;

		public ICommand NationItemClickCommand {
			get {
				_nationItemSelectCommand = _nationItemSelectCommand ?? new MvxCommand<CheckboxItemModel> (DoNationItemSelect);
				return _nationItemSelectCommand;
			}
		}

		private void DoNationItemSelect(CheckboxItemModel item) {
			item.Selected = !item.Selected;
		}

		private MvxCommand<bool> _selectAllStatesCommand;

		public ICommand SelectAllStatesCommand {
			get {
				_selectAllStatesCommand = _selectAllStatesCommand ?? new MvxCommand<bool> (SelectAllStates);
				return _selectAllStatesCommand;
			}
		}

		private void SelectAllStates (bool isClean)
		{
			if (isClean) {
				foreach (CheckboxItemModel item in States) {
					item.Selected = false;
				}
			} else {
				foreach (CheckboxItemModel item in States) {
					item.Selected = true;
				}
			}
		}

		private MvxCommand<Location> _getCurrentAddressCommand;

		public ICommand GetCurrentAddressCommand {
			get {
				_getCurrentAddressCommand = _getCurrentAddressCommand ?? new MvxCommand<Location> (DoGetCurrentAddress);
				return _getCurrentAddressCommand;
			}
		}

		public async void DoGetCurrentAddress (Location curLocation)
		{
			UserDialogs.Instance.ShowLoading ();
			TRGoogleMapGeocoding result = await mGeocodeService.GetAddressGeocode (curLocation.lat, curLocation.lng);
			UserDialogs.Instance.HideLoading ();

			if (result.results != null && result.results.Count > 0) {
				Address = result.results [0].formatted_address;
			} else {
				UserDialogs.Instance.Alert ("Failed to get current location", "Error");
			}
		}

		public async void DoUpdateLocations (string hint)
		{
			if (hint != null) {
				AddressSuggestionItems = await mGeocodeService.GetAutoCompleteSuggestionsAsync (hint);	
			}
		}

		private async void DoSaveAndClose ()
		{
			DataHelper.CurrentSearchFilter.SearchType = (short)DistanceType;
			if (string.IsNullOrEmpty (Address) || DistanceType == (int)TRSearchType.State) {
				ClearAddressFields ();
				if (DistanceType != (int)TRSearchType.State) {
					UserDialogs.Instance.Alert ("Please input address for Commute or Distance search", "Invalid Input");
					return;
				}
			} else {
				DataHelper.CurrentSearchFilter.Address = Address;

				// First, we try to get from address string
				UserDialogs.Instance.ShowLoading ();
				TRGoogleMapGeocoding result = await mGeocodeService.GetAddressGeocode (Address);
				UserDialogs.Instance.HideLoading ();

				try {
					AddressComponent component = result.results [0].address_components.Where (i => i.types.Contains ("postal_code")).FirstOrDefault ();

					// If we fail to get postal_code from address then try with latitude and longitude
					if(component == null) {
						UserDialogs.Instance.ShowLoading ();
						result = await mGeocodeService.GetAddressGeocode (result.results[0].geometry.location.lat, result.results[0].geometry.location.lng);
						UserDialogs.Instance.HideLoading ();

						foreach(Result item in result.results) {
							component = item.address_components.Where (i => i.types.Contains ("postal_code")).FirstOrDefault ();
							if(component != null) {
								break;
							}
						}
					}

					DataHelper.CurrentSearchFilter.StartZip = component.long_name;
					DataHelper.CurrentSearchFilter.Zip = component.long_name;
					DataHelper.CurrentSearchFilter.GeoLat = result.results [0].geometry.location.lat;
					DataHelper.CurrentSearchFilter.GeoLng = result.results [0].geometry.location.lng;
				} catch (Exception e) {
					ClearAddressFields ();
					UserDialogs.Instance.Alert ("Please input valid address.", "Invalid Input");
					return;
				}
			}

			switch (DistanceType) {
			case 0:
				DataHelper.CurrentSearchFilter.SearchDistance = TRConstant.SearchDistances [Distance];
				DataHelper.CurrentSearchFilter.StateFilters = "";
				break;
			case 1:
				DataHelper.CurrentSearchFilter.SearchDistance = TRConstant.SearchMinutesInt [Distance];
				DataHelper.CurrentSearchFilter.StateFilters = "";
				break;
			case 2:
				DataHelper.CurrentSearchFilter.SearchDistance = 0;
				DataHelper.CurrentSearchFilter.StateFilters = GetStateFiltersSelected ();
				break;
			}

			Close (this);
		}

		private void ClearAddressFields ()
		{
			DataHelper.CurrentSearchFilter.Address = string.Empty;
			DataHelper.CurrentSearchFilter.City = string.Empty;
			DataHelper.CurrentSearchFilter.State = string.Empty;
			DataHelper.CurrentSearchFilter.Zip = string.Empty;
			DataHelper.CurrentSearchFilter.StartZip = string.Empty;
			DataHelper.CurrentSearchFilter.Country = string.Empty;
			DataHelper.CurrentSearchFilter.GeoLat = 0;
			DataHelper.CurrentSearchFilter.GeoLng = 0;
		}




		private short _distanceType;
		// 0 = distance, 1 = commute, 2 = state
		public short DistanceType {
			get {
				return _distanceType;
			} 
			set {
				_distanceType = (short)value;

				Distance = 0;
				RaisePropertyChanged (() => DistanceType);
				RaisePropertyChanged (() => DistanceTypeSegValue);
			}
		}

		public int DistanceTypeSegValue {
			get {
				// Swap Commute and Distance between UI and Model
				switch (DistanceType) {
				case 0:
					return 1;
				case 1:
					return 0;
				case 2:
					return 2;
				}

				return 0;
			}
			set {
				short distType = 0;

				switch (value) {
				case 0:
					distType = 1;
					break;
				case 1:
					distType = 0;
					break;
				case 2:
					distType = 2;
					break;
				}

				short oldDistanceType = DistanceType;
				DistanceType = distType;

				if (oldDistanceType != DistanceType) {
					Distance = 0;
				}

				RaisePropertyChanged (() => DistanceType);
				RaisePropertyChanged (() => DistanceTypeSegValue);
			}
		}

		private int _distance;

		public int Distance {
			get {
				return _distance;
			} 
			set {
				_distance = value;
				RaisePropertyChanged (() => Distance);
				RaisePropertyChanged (() => DistanceLabelText);
			}
		}

		public string DistanceLabelText {
			get {
				string retString = string.Empty;
				switch (DistanceType) {
				case 0: //Distance
					retString = string.Format ("{0} mile ", TRConstant.SearchDistances [Distance]);
					break;
				case 1: // Commute
					retString = string.Format ("{0}", TRConstant.SearchMinutes [Distance]);
					break;
				}
				return retString;
			}
		}


		// 0 = driving, 1 = carpool
		public int TravelMode {
			get {
				return DataHelper.CurrentSearchFilter.TravelMode;
			} 
			set {
				DataHelper.CurrentSearchFilter.TravelMode = value;
				RaisePropertyChanged (() => TravelMode);
			}
		}

		// 0 = no traffic, 1 = rush hour
		public int TrafficType {
			get {
				return DataHelper.CurrentSearchFilter.TrafficType;
			} 
			set {
				DataHelper.CurrentSearchFilter.TrafficType = value;
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

		private List<CheckboxItemModel> _commuteItems;

		public List<CheckboxItemModel> CommuteItems {
			get {
				return _commuteItems;
			}
			set {
				_commuteItems = value;
			}
		}

		private List<TRGoogleMapPlace> _addressSuggestionItems;

		public List<TRGoogleMapPlace> AddressSuggestionItems {
			get {
				return _addressSuggestionItems;
			}
			set {
				_addressSuggestionItems = value;
				RaisePropertyChanged (() => AddressSuggestionItems);
			}
		}

		private TRGoogleMapPlace _selectedSuggestion;
		public TRGoogleMapPlace SelectedSuggestion {
			get {
				return _selectedSuggestion;
			} set {
				_selectedSuggestion = value;
				Address = value.FullAddress;
			}
		}

		private string _address;

		public string Address {
			get {
				return _address;
			}
			set {
				_address = value;
				RaisePropertyChanged (() => Address);
				DoUpdateLocations (value);
			}
		}

		private void InitAddress ()
		{
			Address = DataHelper.CurrentSearchFilter.Address;
		}

		private void InitStates ()
		{
			_states = new List<CheckboxItemModel> ();

			// Init Property types
			List<string> statesList = new List<string> ();
			if (!string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.StateFilters)) {
				if (DataHelper.CurrentSearchFilter.StateFilters.Contains (",")) {
					statesList.AddRange (DataHelper.CurrentSearchFilter.StateFilters.Split (','));
				} else {
					statesList.Add (DataHelper.CurrentSearchFilter.StateFilters);
				}
			}

			foreach (var item in TRConstant.StateFilters) {
				_states.Add (new CheckboxItemModel {
					Title = item,
					Selected = (statesList.Contains (item))
				});
			}
		}

		private bool UpdateZipcode (string address)
		{
			if (Regex.IsMatch (address, @"^\s*[\d]{5,}\s*$")) {
				DataHelper.CurrentSearchFilter.Zip = Regex.Match (address, @"[\d]{5,}").Value;
				DataHelper.CurrentSearchFilter.StartZip = DataHelper.CurrentSearchFilter.Zip;

				DataHelper.CurrentSearchFilter.Address = string.Empty;
				DataHelper.CurrentSearchFilter.City = string.Empty;
				DataHelper.CurrentSearchFilter.State = string.Empty;
				DataHelper.CurrentSearchFilter.Country = string.Empty;
				return true;
			} else {
				string[] sub_comps = address.Split (new String[]{ ", " }, StringSplitOptions.RemoveEmptyEntries);

				if (sub_comps.Length > 3) {
					DataHelper.CurrentSearchFilter.StartZip = "";
					DataHelper.CurrentSearchFilter.Zip = "";
					DataHelper.CurrentSearchFilter.Address = sub_comps [0];
					DataHelper.CurrentSearchFilter.City = sub_comps [1];
					DataHelper.CurrentSearchFilter.State = sub_comps [2];
					DataHelper.CurrentSearchFilter.Country = sub_comps [3];
					return true;
				} else {
					return false;
				}
			}
		}

		private string GetStateFiltersSelected ()
		{
			return string.Join (",", States.Where (x => x.Selected).Select (x => x.Title).ToArray ());
		}

		private void InitCommuteItems ()
		{
			_commuteItems = new List<CheckboxItemModel> ();

			foreach (var item in TRConstant.CommuteItems) {
				_commuteItems.Add (new CheckboxItemModel {
					Title = item,
					Selected = false
				});
			}
		}
	}
}

