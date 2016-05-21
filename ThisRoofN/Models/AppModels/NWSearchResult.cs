using System;
using System.Collections.Generic;

namespace ThisRoofN.Models.App
{
	public abstract class NWSearchResult
	{
		public string City { get; set; }

		public string State { get; set; }

		public string StateName { get; set; }

		public abstract string Title { get; }

		public abstract string PhotoLink { get; }

		public List<TRCottageSimple> Cottages { get; set; }
	}
}

