using System;
using System.Runtime.Serialization;

namespace ThisRoofN
{
	[DataContract]
	public class TRSetLikeRequest
	{
		[DataMember(Name="user_id")]
		public int UserID { get; set; }

		[DataMember(Name="mobile_num")]
		public string DeviceID {get;set;}

		[DataMember(Name="property_id")]
		public string PropertyID { get; set; }

		[DataMember(Name="like_dislike")]
		public bool Like { get; set; }
	}

	[DataContract]
	public class TRSetDisLikeRequest
	{
		[DataMember(Name="user_id")]
		public int UserID { get; set; }

		[DataMember(Name="mobile_num")]
		public string DeviceID {get;set;}

		[DataMember(Name="property_id")]
		public string PropertyID { get; set; }

		[DataMember(Name="like_dislike")]
		public bool Like { get; set; }

		[DataMember(Name="reject_reasons")]
		public string RejectReason {get;set;}

		[DataMember(Name="too_far")]
		public bool TooFar { get; set; }

		[DataMember(Name="too_close")]
		public bool TooClose { get; set; }

		[DataMember(Name="bad_area")]
		public bool BadArea { get; set; }

		[DataMember(Name="too_small")]
		public bool TooSmall { get; set; }

		[DataMember(Name="house_too_big")]
		public bool HouseTooBig { get; set; }

		[DataMember(Name="lot_too_big")]
		public bool LotTooBig { get; set; }

		[DataMember(Name="ugly")]
		public bool Ugly { get; set; }

		[DataMember(Name="lot_too_small")]
		public bool LotTooSmall { get; set; }
	}
}

