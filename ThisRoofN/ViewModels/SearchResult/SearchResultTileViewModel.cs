using System;
using System.Collections.Generic;
using System.Linq;

namespace ThisRoofN.ViewModels
{
	public class SearchResultTileViewModel : BaseViewModel
	{
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
	}
}

