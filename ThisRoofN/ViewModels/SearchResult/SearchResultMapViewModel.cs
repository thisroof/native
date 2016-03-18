using System;
using System.Collections.Generic;
using System.Linq;
using ThisRoofN.Models.App;
using ThisRoofN.Helpers;

namespace ThisRoofN.ViewModels
{
	public class SearchResultMapViewModel : BaseViewModel
	{
		private List<TRCottageSimple> _mapItems;
		public SearchResultMapViewModel ()
		{
			if (DataHelper.SearchResults != null) {
				_mapItems = DataHelper.SearchResults.Select (i =>
					new TRCottageSimple() {
						CottageID = i.ID,
						PrimaryPhotoLink = i.Photos.FirstOrDefault().MediaURL,
						Latitude = i.Latitude,
						Longitude = i.Longitude,
					}).ToList ();
			}
		}

		public List<TRCottageSimple> MapItems
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

