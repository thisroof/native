using System;
using System.Runtime.Serialization;

namespace ThisRoofN
{
	[DataContract]
	public class TRLikeStatus
	{
		[DataMember(Name="property_id")]
		public string PropertyID { get; set; }

		[DataMember(Name="like_dislike")]
		public bool IsLiked;
	}
}

