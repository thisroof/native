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

namespace ThisRoofN.ViewModels
{
	public class SearchResultTileViewModel : BaseViewModel
	{
		private MvxCommand<int> _detailCommand;
		private List<TRCottageSimple> _tileItems;
		private IDevice deviceInfo;

		public SearchResultTileViewModel ()
		{
			deviceInfo = Mvx.Resolve<IDevice> ();
		
			if (DataHelper.SearchResults != null) {
				_tileItems = DataHelper.SearchResults.Select (i =>
					new TRCottageSimple() {
						CottageID = i.ID,
						PrimaryPhotoLink = i.Photos.FirstOrDefault().MediaURL,
						Latitude = i.Latitude,
						Longitude = i.Longitude,
					}).ToList ();
			}
		}

		public List<TRCottageSimple> TileItems
		{
			get {
				return _tileItems;
			} 
			set {
				_tileItems = value;
				RaisePropertyChanged (() => TileItems);
			}
		}

		public ICommand DetailCommand
		{
			get {
				_detailCommand = _detailCommand ?? new MvxCommand<int> (GotoDetail);
				return _detailCommand;
			}
		}

		private async void GotoDetail(int index)
		{
			string propertyID = _tileItems [index].CottageID;

			UserDialogs.Instance.ShowLoading ();
			DataHelper.SelectedDetail = await mTRService.GetCottageDetail(deviceInfo.GetUniqueIdentifier(), propertyID);
			UserDialogs.Instance.HideLoading();

			ShowViewModel<SearchResultDetailViewModel> ();
		}
	}
}

