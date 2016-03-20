using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using ThisRoofN.Models.App;

namespace ThisRoofN.Helpers
{
	public static class DataHelper
	{
		public static SearchFilters CurrentSearchFilter;

		public static List<TRCottageSimple> SearchResults;
		public static TRCottageSimple SelectedCottage;
		public static TRCottageDetail SelectedCottageDetail;
	}
}

