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
		private MvxCommand<TRCottageSimple> _detailItemCommand;
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

		private void GotoDetail (int index)
		{
			GotoDetailItem(TileItems[index]);
		}

		private void GotoDetailItem (TRCottageSimple item)
		{
			string propertyID =  item.CottageID;
			int index = DataHelper.SearchResults.FindIndex (a => a.CottageID == propertyID);

			ShowViewModel<SearchResultDetailViewModel> (new {index = index, propertyID = propertyID});
		}

		private async void LoadMore() {
			if (DataHelper.TotalLoadedCount < 24) {
				return;
			}

			int page = DataHelper.TotalLoadedCount / 24 + 1;

			this.SpinnerLoading = true;

			List<CottageSimple> searchResults = await mTRService.GetSearchResults (deviceInfo.GetUniqueIdentifier (), 24, page);
			if (searchResults != null) {
				DataHelper.TotalLoadedCount += searchResults.Count;

				List<TRCottageSimple> appResults = searchResults.Select (i =>
					new TRCottageSimple () {
					CottageID = i.ID,
					PrimaryPhotoLink = (i.Photos != null) ? i.Photos.FirstOrDefault ().MediaURL : string.Empty,
					Title = i.Title,
					Price = i.Price,
					Latitude = i.Latitude,
					Longitude = i.Longitude,
				}).ToList ();

				DataHelper.SearchResults.AddRange (appResults);
				this.SpinnerLoading = false;

				RaisePropertyChanged (() => TileItems);
			} else {
				UserDialogs.Instance.Alert ("Please check your network connection and try again later", "Server Not Responding");
			}
		}
	}
}

