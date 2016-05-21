using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using ThisRoofN.Models.App;
using GeoJSON.Net.Geometry;

namespace ThisRoofN.Helpers
{
	public static class DataHelper
	{
		public static SearchFilters CurrentSearchFilter;

		public static int TotalLoadedCount;	// we use this count because search results would be removed by dislike
		public static List<TRCottageSimple> SearchResults;
		public static List<IPosition> SearchMapRange;
		public static TRCottageSimple SelectedCottage;
		public static TRCottageDetail SelectedCottageDetail;

		public static int TotalLoadedStatesCount;
		public static List<NWSearchResult> StateSearchResults;
		public static string SelectedState;

		public static int TotalLoadedCitiesCount;
		public static List<NWSearchResult> CitySearchResults;
		public static string SelectedCity;
	}
}

