using System;
using System.Runtime.Serialization;

namespace ThisRoofN.Models
{
	[DataContract]
	public class TRLikedProperty : TREntityBase
	{
		[DataMember(Name="property_id")]
		public string PropertyID { get; set; }

		[DataMember(Name="like_dislike")]
		public bool LikeDislike { get; set; } //( 1 - Like; 0 - Dislike)
	}
}

