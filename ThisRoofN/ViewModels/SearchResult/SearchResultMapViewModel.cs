using System;
using System.Collections.Generic;
using System.Linq;

namespace ThisRoofN.ViewModels
{
	public class SearchResultMapViewModel : BaseViewModel
	{
		private List<MapItemModel> _mapItems;
		public SearchResultMapViewModel ()
		{
			if (DataHelper.SearchResults != null) {
				_mapItems = DataHelper.SearchResults.Select (i =>
					new MapItemModel() {
						PropertyID = i.Listing.MatrixUniqueID,
						Latitude = i.Listing.Location.Latitude,
						Longitude = i.Listing.Location.Longitude,
					}).ToList ();
			}
		}

		public List<MapItemModel> MapItems
		{
			get {
				return _mapItems;
			} 
			set {
				_mapItems = value;
				RaisePropertyChanged (() => MapItems);
			}
		}
	}
}

