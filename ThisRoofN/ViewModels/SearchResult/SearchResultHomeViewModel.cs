using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ThisRoofN.Interfaces;
using ThisRoofN.Helpers;
using System.Linq;

namespace ThisRoofN.ViewModels
{
	public class SearchResultHomeViewModel : BaseViewModel
	{
		private int defaultPosition;
		private SearchResultTileViewModel tileViewModel;
		private SearchResultMapViewModel mapViewModel;
		private SearchResultListViewModel listViewModel;

		public bool IsNationWideSearch { get; set; }

		public SearchResultHomeViewModel ()
		{
			this.tileViewModel = new SearchResultTileViewModel ();
			this.listViewModel = new SearchResultListViewModel ();
			this.mapViewModel = new SearchResultMapViewModel ();
		}

		public void Init(bool isNationWideSearch) {
			IsNationWideSearch = isNationWideSearch;
		}

		public int DefaultPosition
		{
			get
			{
				return defaultPosition;
			}
			set
			{
				defaultPosition = value;
			}
		}

		public SearchResultTileViewModel TileViewModel
		{
			get
			{
				return tileViewModel;
			}
		}

		public SearchResultListViewModel ListViewModel
		{
			get
			{
				return listViewModel;
			}
		}

		public SearchResultMapViewModel MapViewModel
		{
			get
			{
				return mapViewModel;
			}
		}

		protected override void CloseView ()
		{
			base.CloseView ();
			if (IsNationWideSearch) {
				DataHelper.SelectedCity = null;
				DataHelper.SearchResults = DataHelper.CitySearchResults.Select (i => i.Cottages).SelectMany(j => j).ToList();
				DataHelper.TotalLoadedCount = DataHelper.CitySearchResults.Count;
			}
		}
	}
}

