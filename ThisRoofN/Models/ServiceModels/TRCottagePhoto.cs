using System;
using System.Runtime.Serialization;

namespace ThisRoofN
{
	[DataContract]
	public class TRCottagePhoto
	{
		[DataMember(Name="media_url")]
		public string MediaURL;

		[DataMember(Name="media_modification_timestamp")]
		public string ModificationTime;
	}
}

