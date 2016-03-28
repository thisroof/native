﻿using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using ThisRoofN.Models.App;
using GeoJSON.Net.Geometry;

namespace ThisRoofN.Helpers
{
	public static class DataHelper
	{
		public static SearchFilters CurrentSearchFilter;

		public static List<TRCottageSimple> SearchResults;
		public static List<IPosition> SearchMapRange;
		public static TRCottageSimple SelectedCottage;
		public static TRCottageDetail SelectedCottageDetail;
	}
}

