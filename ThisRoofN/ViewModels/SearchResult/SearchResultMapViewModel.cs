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
				_mapItems = DataHelper.SearchResults;
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

