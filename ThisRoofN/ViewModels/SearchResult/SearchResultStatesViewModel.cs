using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Acr.UserDialogs;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;
using ThisRoofN.Models.App;
using ThisRoofN.Helpers;
using ThisRoofN.Models.Service;

namespace ThisRoofN.ViewModels
{
	public class SearchResultStatesViewModel : BaseViewModel
	{
		private MvxCommand<NWSearchResult> _showAllCommand;
		private MvxCommand<TRCottageSimple> _detailItemCommand;
		private MvxCommand _loadMoreCommand;
		private IDevice deviceInfo;

		private bool _spinnerLoading;
		private int currentPage;

		public bool IsCitiesLevel { get; set; }

		public List<NWSearchResult> ListItems {
			get {
				return IsCitiesLevel ? DataHelper.CitySearchResults : DataHelper.StateSearchResults;
			}
		}

		public ICommand ShowAllCommand {
			get {
				_showAllCommand = _showAllCommand ?? new MvxCommand<NWSearchResult> (ShowAllListings);
				return _showAllCommand;
			}
		}

		public ICommand DetailItemCommand {
			get {
				_detailItemCommand = _detailItemCommand ?? new MvxCommand<TRCottageSimple> (GotoDetailItem);
				return _detailItemCommand;
			}
		}

		public ICommand LoadMoreCommand {
			get {
				_loadMoreCommand = _loadMoreCommand ?? new MvxCommand (LoadMore);
				return _loadMoreCommand;
			}
		}

		public bool SpinnerLoading {
			get {
				return _spinnerLoading;
			} set {
				_spinnerLoading = value;
				RaisePropertyChanged (() => SpinnerLoading);
			}
		}

		public SearchResultStatesViewModel ()
		{
			deviceInfo = Mvx.Resolve<IDevice> ();
			currentPage = 1;
		}

		public void Init(bool isCities)
		{
			IsCitiesLevel = isCities;
		}

		private void GotoDetailItem (TRCottageSimple item)
		{
			// UserDialogs.Instance.ShowError (item.Title);
			string propertyID =  item.CottageID;
			int index = DataHelper.SearchResults.FindIndex (a => a.CottageID == propertyID);

			ShowViewModel<SearchResultDetailViewModel> (new {index = index, propertyID = propertyID});
		}

		private async void ShowAllListings(NWSearchResult item) {
			if (IsCitiesLevel) {
				DataHelper.SelectedCity = item.City;
			} else {
				DataHelper.SelectedState = item.State;
			}
			this.IsLoading = true;
			List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), IsCitiesLevel ? 24 : 5, 1);
			this.IsLoading = false;

			if (searchResults != null) {
				List<TRCottageSimple> appResults = searchResults.Select (i =>
					new TRCottageSimple () {
						CottageID = i.ID,
						PrimaryPhotoLink = i.Photo != null ? i.Photo : string.Empty,
						Title = i.Title,
						Price = i.Price,
						Latitude = i.Latitude,
						Longitude = i.Longitude,
						Address = i.Address,
						City = i.City,
						State = i.State,
						IsForState = true
					}).ToList ();
				DataHelper.SearchResults = (appResults);
				DataHelper.TotalLoadedCount = appResults.Count;

				if (!IsCitiesLevel) {
					var appCityResults = appResults.GroupBy (i => i.City).Select (g => (NWSearchResult) new TRCityResult () {
						City = g.Key,
						State = DataHelper.SelectedState,
						Cottages = g.ToList ()
					}).ToList ();
					DataHelper.CitySearchResults = appCityResults;
					DataHelper.TotalLoadedCitiesCount = appCityResults.Count;
				}
				if (IsCitiesLevel) {
					DataHelper.SearchMapRange = null;
					ShowViewModel<SearchResultHomeViewModel> ( new { isNationWideSearch = true } );
				} else {
					ShowViewModel<SearchResultStatesViewModel> ( new { isCities = true } );
				}
			} else {
				UserDialogs.Instance.Alert ("Please check your network connection and try again later", "Server Not Responding");
			}
		}

		private async void LoadMore() {
			if ( (IsCitiesLevel ? DataHelper.TotalLoadedCitiesCount : DataHelper.TotalLoadedStatesCount) < 5 * currentPage) {
				return;
			}

			this.SpinnerLoading = true;

			int page = currentPage + 1;
			List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), 5, page);

			this.SpinnerLoading = false;

			if (searchResults != null) {
				if (searchResults.Count > 0) {
					List<TRCottageSimple> appResults = searchResults.Select (i =>
					new TRCottageSimple () {
						CottageID = i.ID,
						PrimaryPhotoLink = i.Photo != null ? i.Photo : string.Empty,
						Title = i.Title,
						Price = i.Price,
						Latitude = i.Latitude,
						Longitude = i.Longitude,
						Address = i.Address,
						City = i.City,
						State = i.State,
						IsForState = true
					}).ToList ();
					DataHelper.SearchResults.AddRange (appResults);
					DataHelper.TotalLoadedCount += appResults.Count;

					if (IsCitiesLevel) {
						var appCityResults = appResults.GroupBy (i => i.City).Select (g => new TRCityResult () {
							City = g.Key,
							State = DataHelper.SelectedState,
							Cottages = g.ToList ()
						}).ToList ();
						DataHelper.CitySearchResults.AddRange (appCityResults);
						DataHelper.TotalLoadedCitiesCount += appCityResults.Count;
					} else {
						var appStateResults = appResults.GroupBy (i => i.State).Select (g => new TRStateResult () { 
							State = g.Key, 
							StateName = TRConstant.StatesDict [g.Key], 
							Cottages = g.ToList ()
						}).ToList ();
						DataHelper.StateSearchResults.AddRange (appStateResults);
						DataHelper.TotalLoadedStatesCount += appStateResults.Count;
					}

					currentPage = page;

					RaisePropertyChanged (() => ListItems);
				}
			} else {
				UserDialogs.Instance.Alert ("Please check your network connection and try again later", "Server Not Responding");
			}
		}

		protected override void CloseView ()
		{
			base.CloseView ();
			if (IsCitiesLevel) {
				DataHelper.SelectedState = null;
				DataHelper.SearchResults = DataHelper.StateSearchResults.Select (i => i.Cottages).SelectMany(j => j).ToList();
				DataHelper.TotalLoadedCount = DataHelper.SearchResults.Count;
			}
		}

	}
}

