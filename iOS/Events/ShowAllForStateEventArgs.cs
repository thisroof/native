using System;
using ThisRoofN.Models.App;

namespace ThisRoofN.iOS
{
	public class ShowAllForStateEventArgs: EventArgs
	{
		public NWSearchResult StateResult { get; set;}

		public ShowAllForStateEventArgs (NWSearchResult _result)
		{
			StateResult = _result;
		}
	}
}

