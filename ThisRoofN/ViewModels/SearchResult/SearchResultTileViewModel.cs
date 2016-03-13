using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace ThisRoofN.ViewModels
{
	public class SearchResultTileViewModel : BaseViewModel
	{
		private MvxCommand<int> _detailCommand;
		private List<TileItemModel> _tileItems;

		public SearchResultTileViewModel ()
		{
			if (DataHelper.SearchResults != null) {
				_tileItems = DataHelper.SearchResults.Select (i =>
					new TileItemModel() {
						propertyID = i.Listing.MatrixUniqueID,
						ImageUrl = i.PrimaryPhotoUrl
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

		private void GotoDetail(int index)
		{
			string propertyID = _tileItems [index].propertyID;
			ShowViewModel<SearchResultDetailViewModel> (new {selectedPropertyID = propertyID});
		}
	}
}

