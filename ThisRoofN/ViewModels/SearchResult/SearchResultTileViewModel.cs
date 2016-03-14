using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Acr.UserDialogs;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;

namespace ThisRoofN.ViewModels
{
	public class SearchResultTileViewModel : BaseViewModel
	{
		private MvxCommand<int> _detailCommand;
		private List<TileItemModel> _tileItems;
		private IDevice deviceInfo;

		public SearchResultTileViewModel ()
		{
			deviceInfo = Mvx.Resolve<IDevice> ();
		
			if (DataHelper.SearchResults != null) {
				_tileItems = DataHelper.SearchResults.Select (i =>
					new TileItemModel() {
						propertyID = i.ID,
						ImageUrl = i.Photos.FirstOrDefault().MediaURL
					}).ToList ();
			}
		}

		public List<TileItemModel> TileItems
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
			string propertyID = _tileItems [index].propertyID;

			UserDialogs.Instance.ShowLoading ();
			DataHelper.SelectedDetail = await mTRService.GetCottageDetail(deviceInfo.GetUniqueIdentifier(), propertyID);
			UserDialogs.Instance.HideLoading();

			ShowViewModel<SearchResultDetailViewModel> ();
		}
	}
}

