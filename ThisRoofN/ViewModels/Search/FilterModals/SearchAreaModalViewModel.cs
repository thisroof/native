﻿using System;
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
			Distance = DataHelper.CurrentSearchFilter.SearchDistance;



			// init states
			InitAddress();
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


		private MvxCommand<string> _updateLocationsCommand;

		public ICommand UpdateLocationsCommand {
			get {
				_updateLocationsCommand = _updateLocationsCommand ?? new MvxCommand<string> (DoUpdateLocations);
				return _updateLocationsCommand;
			}
		}

		public async void DoUpdateLocations (string hint)
		{
			AddressSuggestionItems = await mGeocodeService.GetAutoCompleteSuggestionsAsync (hint);
		}

		private async void DoSaveAndClose ()
		{
			DataHelper.CurrentSearchFilter.SearchType = (short)DistanceType;
			if (string.IsNullOrEmpty (Address)) {
				if (DistanceType == (int)TRSearchType.State) {
					DataHelper.CurrentSearchFilter.Address = string.Empty;
					DataHelper.CurrentSearchFilter.City = string.Empty;
					DataHelper.CurrentSearchFilter.State = string.Empty;
					DataHelper.CurrentSearchFilter.Zip = string.Empty;
					DataHelper.CurrentSearchFilter.StartZip = string.Empty;
					DataHelper.CurrentSearchFilter.Country = string.Empty;
				} else {
					UserDialogs.Instance.Alert ("Please input address for Commute or Distance search", "Invalid Input");
					return;
				}
			} else {
				if (!UpdateZipcode (Address)) {
					Address[] addresses = new MvxPlugins.Geocoder.Address[] {
					};
					try {
						UserDialogs.Instance.ShowLoading ();
						addresses = await geocoder.GetAddressesAsync (Address);
						UserDialogs.Instance.HideLoading ();
					} catch(Exception e) {
						Mvx.Trace ("GetAddressesAsync Failed");
					}

					if (addresses.Count () > 0) {
						DataHelper.CurrentSearchFilter.GeoLat = addresses [0].Latitude;
						DataHelper.CurrentSearchFilter.GeoLng = addresses [0].Longitude;


						DataHelper.CurrentSearchFilter.Zip = addresses [0].PostalCode;
						DataHelper.CurrentSearchFilter.StartZip = addresses [0].PostalCode;
					} else {
						UserDialogs.Instance.Alert ("Please input valid address.", "Invalid Input");
						return;
					}
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

		private int _distanceType;
		// 0 = distance, 1 = commute, 2 = state
		public int DistanceType {
			get {
				return _distanceType;
			} 
			set {
				_distanceType = (short)value;
				RaisePropertyChanged (() => DistanceType);
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

		private string _address;

		public string Address {
			get {
				return _address;
			}
			set {
				_address = value;
				RaisePropertyChanged (() => Address);
			}
		}

		private void InitAddress()
		{
			string addressStr = string.Empty;
			if (!string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.Address)) {
				addressStr = DataHelper.CurrentSearchFilter.Address;
			}

			if (!string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.City)) {
				addressStr = addressStr + ", " + DataHelper.CurrentSearchFilter.City;
			}

			if (!string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.State)) {
				addressStr = addressStr + " " + DataHelper.CurrentSearchFilter.State;
			}

			if (!string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.StartZip)) {
				addressStr = addressStr + " " + DataHelper.CurrentSearchFilter.StartZip;
			}

			Address = addressStr;
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
				return true;
			} else if (Regex.IsMatch (address, @"([A-Z]{2})\s([\d]{5,})")) {
				Match match = Regex.Match (address, @"([A-Z]{2})\s([\d]{5,})");
				string[] sub_comps = address.Substring (0, match.Index).Split (new String[]{ ", " }, StringSplitOptions.RemoveEmptyEntries);
				DataHelper.CurrentSearchFilter.Address = sub_comps [0];
				if (sub_comps.Length > 1) {
					DataHelper.CurrentSearchFilter.City = sub_comps [1];
				}
				DataHelper.CurrentSearchFilter.State = match.Groups [1].Value;
				DataHelper.CurrentSearchFilter.Country = "US";
				DataHelper.CurrentSearchFilter.Zip = match.Groups [2].Value;
				DataHelper.CurrentSearchFilter.StartZip = DataHelper.CurrentSearchFilter.Zip;
				return true;
			}
			return false;
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

