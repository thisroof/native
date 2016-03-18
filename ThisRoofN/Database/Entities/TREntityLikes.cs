using System;
using ThisRoofN.Models.Service;

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

		public double Latitude {get;set; }

		public double Longitude { get; set; }

		public string PrimaryImageLink {get;set;}
	}
}

