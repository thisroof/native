using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ThisRoofN.Interfaces;

namespace ThisRoofN.ViewModels
{
	public class SearchResultHomeViewModel : BaseViewModel
	{
		private int defaultPosition;
		private SearchResultTileViewModel tileViewModel;
		private SearchResultMapViewModel mapViewModel;
		private SearchResultListViewModel listViewModel;

		public SearchResultHomeViewModel ()
		{
			this.tileViewModel = new SearchResultTileViewModel ();
			this.listViewModel = new SearchResultListViewModel ();
			this.mapViewModel = new SearchResultMapViewModel ();
		}

		public void Init() {
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
	}
}

