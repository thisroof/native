using System;
using System.Collections.Generic;

namespace ThisRoofN.Models.App
{
	public class TRStateResult : NWSearchResult
	{
		public override string Title { 
			get { 
				return StateName;
			} 
		}

		public override string PhotoLink {
			get {
				return string.Format ("{0}images/states/{1}.jpg", TRConstant.TRServiceBaseURL, State);
			}
		}
	}
}

