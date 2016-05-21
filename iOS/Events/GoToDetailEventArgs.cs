using System;
using ThisRoofN.Models.App;

namespace ThisRoofN.iOS
{
	public class GoToDetailEventArgs: EventArgs
	{
		public TRCottageSimple Cottage { get; set;}

		public GoToDetailEventArgs (TRCottageSimple _cottage)
		{
			Cottage = _cottage;
		}
	}
}

