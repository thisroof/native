using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThisRoofN.ViewModels
{
	public class SearchResultHomeViewModel : BaseViewModel
	{
		private int defaultPosition;
		private SearchResultTileViewModel tileViewModel;
		private SearchResultMapViewModel mapViewModel;



		public SearchResultHomeViewModel ()
		{
			this.tileViewModel = new SearchResultTileViewModel ();
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

		public SearchResultMapViewModel MapViewModel
		{
			get
			{
				return mapViewModel;
			}
		}
	}
}

