using System;
using System.Collections.Generic;

namespace ThisRoofN.Models.App
{
	public class TRCityResult : NWSearchResult
	{
		public override string Title { 
			get { 
				return City;
			} 
		}

		public override string PhotoLink {
			get {
				return string.Format ("{0}images/cities/{1}-{2}.jpg", TRConstant.TRServiceBaseURL, City.Replace(" ", ""), State);
			}
		}
	}
}

