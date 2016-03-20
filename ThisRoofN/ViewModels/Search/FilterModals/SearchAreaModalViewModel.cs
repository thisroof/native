using System;
using ThisRoofN.Helpers;
using ThisRoofN.Models.App;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using ThisRoofN.RestService;
using ThisRoofN.Extensions;

namespace ThisRoofN.ViewModels
{
	public class SearchAreaModalViewModel : BaseViewModel
	{
		private GeocodeService mGeocodeService;

		public SearchAreaModalViewModel ()
		{
			mGeocodeService = new GeocodeService ();

			// init the distance type as commute
			DistanceType = 1;

			// init min and max distance
			MinDistance = 0;
			MaxDistance = TRConstant.SearchMinutes.Count () - 1; // Default Commute Selected

			// init states
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

		private void DoSaveAndClose ()
		{
			// Save edited values

			Close (this);
		}

		// 0 = distance, 1 = commute, 2 = state
		public int DistanceType {
			get {
				return DataHelper.CurrentSearchFilter.SearchType;
			} 
			set {
				DataHelper.CurrentSearchFilter.SearchType = (short)value;
				RaisePropertyChanged (() => DistanceType);
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

		private TRGoogleMapPlace _selectedAddress;

		public TRGoogleMapPlace SelectedAddress {
			get {
				return _selectedAddress;
			}
			set {
				_selectedAddress = value;
				RaisePropertyChanged (() => SelectedAddress);
			}
		}

		private int _minDistance;

		public int MinDistance {
			get {
				return _minDistance;
			} 
			set {
				_minDistance = value;
				RaisePropertyChanged (() => MinDistance);
				RaisePropertyChanged (() => DistanceLabelText);
			}
		}

		private int _maxDistance;

		public int MaxDistance {
			get {
				return _maxDistance;
			} 
			set {
				_maxDistance = value;
				RaisePropertyChanged (() => MaxDistance);
				RaisePropertyChanged (() => DistanceLabelText);
			}
		}

		private string _distanceLabelText;

		public string DistanceLabelText {
			get {
				string retString = string.Empty;
				switch (DistanceType) {
				case 0: //Distance
					retString = string.Format ("{0} mile - {1} mile", TRConstant.SearchDistances [MinDistance], TRConstant.SearchDistances [MaxDistance]);
					break;
				case 1: // Commute
					retString = string.Format("{0} - {1}", TRConstant.SearchMinutes [MinDistance], TRConstant.SearchMinutes [MaxDistance]);
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

		private void InitCommuteItems()
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

