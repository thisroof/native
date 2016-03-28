using System;
using System.Collections.Generic;
using System.Linq;
using ThisRoofN.Models.App;
using ThisRoofN.Helpers;
using GeoJSON.Net.Geometry;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.Models.Service;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;

namespace ThisRoofN.ViewModels
{
	public class SearchResultMapViewModel : BaseViewModel
	{
		private MvxCommand<string> _detailCommand;
		private IDevice deviceInfo;

		public SearchResultMapViewModel ()
		{
			deviceInfo = Mvx.Resolve<IDevice> ();
		}

		public List<TRCottageSimple> MapItems
		{
			get {
				return DataHelper.SearchResults;
			} 
		}

		public List<IPosition> MapRange
		{
			get {
				return DataHelper.SearchMapRange;
			}
		}

		public ICommand DetailCommand {
			get {
				_detailCommand = _detailCommand ?? new MvxCommand<string> (GotoDetail);
				return _detailCommand;
			}
		}

		private async void GotoDetail (string propertyID)
		{
			this.IsLoading = true;
			this.LoadingText = "Loading Detail";
			CottageDetail detail = await mTRService.GetCottageDetail (deviceInfo.GetUniqueIdentifier (), propertyID);

			DataHelper.SelectedCottage = DataHelper.SearchResults.Find (i => i.CottageID == propertyID);
			DataHelper.SelectedCottageDetail = new TRCottageDetail (detail,  DataHelper.SelectedCottage);

			this.IsLoading = false;

			ShowViewModel<SearchResultDetailViewModel> ();
		}

	}
}

