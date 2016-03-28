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
	public class SearchResultTileViewModel : BaseViewModel
	{
		private MvxCommand<int> _detailCommand;
		private MvxCommand _loadMoreCommand;
		private IDevice deviceInfo;

		private bool _spinnerLoading;

		public SearchResultTileViewModel ()
		{
			deviceInfo = Mvx.Resolve<IDevice> ();
		}

		public List<TRCottageSimple> TileItems {
			get {
				return  DataHelper.SearchResults;
			}
		}

		public ICommand DetailCommand {
			get {
				_detailCommand = _detailCommand ?? new MvxCommand<int> (GotoDetail);
				return _detailCommand;
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

		private async void GotoDetail (int index)
		{
			string propertyID =  DataHelper.SearchResults [index].CottageID;

			this.IsLoading = true;
			this.LoadingText = "Loading Detail";
			CottageDetail detail = await mTRService.GetCottageDetail (deviceInfo.GetUniqueIdentifier (), propertyID);

			DataHelper.SelectedCottage =  DataHelper.SearchResults [index];
			DataHelper.SelectedCottageDetail = new TRCottageDetail (detail,  DataHelper.SearchResults[index]);

			this.IsLoading = false;

			ShowViewModel<SearchResultDetailViewModel> ();
		}

		private async void LoadMore() {
			int page = TileItems.Count / 24;

			this.SpinnerLoading = true;

			List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), 24, page);
			List<TRCottageSimple> appResults = searchResults.Select (i =>
				new TRCottageSimple () {
					CottageID = i.ID,
					PrimaryPhotoLink = (i.Photos != null) ? i.Photos.FirstOrDefault ().MediaURL : string.Empty,
					Latitude = i.Latitude,
					Longitude = i.Longitude,
				}).ToList ();

			DataHelper.SearchResults.AddRange (appResults);
			this.SpinnerLoading = false;

			RaisePropertyChanged (() => TileItems);
		}
	}
}

