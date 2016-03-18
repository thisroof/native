using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;

namespace ThisRoofN.Helpers
{
	public static class DataHelper
	{
		// We store the search result as static because it's too large data and takes time to pass through several viewmodels.
		public static List<CottageSimple> SearchResults;

		public static CottageDetail SelectedDetail;
	}
}

