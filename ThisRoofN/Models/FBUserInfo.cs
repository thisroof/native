using System;
using System.Runtime.Serialization;

namespace ThisRoofN.Models
{
	[DataContract]
	public class FBUserInfo
	{
		[DataMember(Name="email")]
		public string UserEmail {get;set;}

		[DataMember(Name="third_party_id")]
		public string UserID {get;set;}

		[DataMember(Name="third_party_token")]
		public string UserToken {get;set;}

		[DataMember(Name="provider")]
		public string Provider { get; set;}
	}
}

