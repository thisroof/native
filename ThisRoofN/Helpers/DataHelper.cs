using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using ThisRoofN.Models.App;

namespace ThisRoofN.Helpers
{
	public static class DataHelper
	{
		// We store the search result as static because it's too large data and takes time to pass through several viewmodels.
//		public static List<CottageSimple> SearchResults;
//
//		public static CottageDetail SelectedCottageDetail;

		public static List<TRCottageSimple> SearchResults;
		public static TRCottageSimple SelectedCottage;
		public static TRCottageDetail SelectedCottageDetail;

	}
}

