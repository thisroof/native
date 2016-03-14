using System;
using System.Runtime.Serialization;

namespace ThisRoofN.Models
{
	[DataContract]
	public class TRUser
	{
		[DataMember(Name="success")]
		public bool Success { get; set; }

		[DataMember(Name="user_id")]
		public string UserID { get; set; }

		[DataMember(Name="access_token")]
		public string AccessToken { get; set; }

		[DataMember(Name="refresh_token")]
		public string RefreshToken { get; set; }
	}
}

