using System;

namespace ThisRoofN.Models.App
{
	public class TRCottageSimple
	{
		public string CottageID { get; set; }

		public string PrimaryPhotoLink {get;set;}

		public string Title {get;set;}

		public double Price {get;set;}

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public string FormattedPrice {
			get {
				return string.Format ("{0:C0}", this.Price);
			}
		}
	}
}

