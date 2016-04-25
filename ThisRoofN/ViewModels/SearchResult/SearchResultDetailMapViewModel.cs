using System;
using ThisRoofN.ViewModels;
using ThisRoofN.Models.App;
using ThisRoofN.Helpers;

namespace ThisRoofN
{
	public class SearchResultDetailMapViewModel : BaseViewModel
	{
		public SearchResultDetailMapViewModel ()
		{
		}

		public string Address {
			get {
				return DataHelper.SelectedCottageDetail.FormattedAddress;
			}
		}

		public double Latitude {
			get {
				return DataHelper.SelectedCottageDetail.Latitude;
			}
		}

		public double Longitude {
			get {
				return DataHelper.SelectedCottageDetail.Longitude;
			}
		}
	}
}

