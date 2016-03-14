using System;
using System.Collections.Generic;

namespace ThisRoofN
{
	public static class DataHelper
	{
		// We store the search result as static because it's too large data and takes time to pass through several viewmodels.
		public static List<TRCottage> SearchResults;

		public static TRCottageDetail SelectedDetail;
	}
}

