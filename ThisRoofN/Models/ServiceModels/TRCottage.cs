using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ThisRoofN
{
	[DataContract]
	public class TRCottage
	{
		[DataMember(Name="listing_key")]
		public string ID {get;set;}

		[DataMember(Name="list_price")]
		public string Price {get;set;}

		[DataMember(Name="latitude")]
		public double Latitude {get;set;}

		[DataMember(Name="longitude")]
		public double Longitude {get;set;}

		[DataMember(Name="photos")]
		public List<TRCottagePhoto> Photos {get;set;}
	}
}

