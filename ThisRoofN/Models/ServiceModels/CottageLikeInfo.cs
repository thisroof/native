using System;
using System.Runtime.Serialization;
using ThisRoofN.Models.App;

namespace ThisRoofN.Models.Service
{
	[DataContract]
	public class CottageLikeInfo
	{
		[DataMember(Name="property_id")]
		public string PropertyID { get; set; }

		[DataMember(Name="like_dislike")]
		public bool LikeDislike { get; set; } //( 1 - Like; 0 - Dislike)
	}
}

