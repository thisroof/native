using System;
using ThisRoofN.Models.Service;
using SQLite.Net.Attributes;

namespace ThisRoofN.Database.Entities
{
	public class TREntityLikes : TREntityBase
	{
		public TREntityLikes() {
		}

		public TREntityLikes(CottageSimple simple) {
		}

		public int UserID {get;set;}

		public string PropertyID {get;set;}

		public bool LikeDislike {get;set;}	// true: Liked, false: Disliked

		public string PrimaryPhotoURL {get;set;}

		public double Price {get;set;}

		public string Address { get; set; }

		public string CityStateZip {get;set;}

		[Ignore]
		public string FormattedSalePrice {
			get {
				return string.Format ("Sales For {0:C0}", Price);
			}
		}
	}
}

