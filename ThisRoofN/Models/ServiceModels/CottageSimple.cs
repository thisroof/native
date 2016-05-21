using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ThisRoofN.Models.Service
{
	[DataContract]
	public class CottageSimple
	{
		[DataMember(Name="listing_key")]
		public string ID {get;set;}

		[DataMember(Name="list_price")]
		public double Price {get;set;}

		[DataMember(Name="title")]
		public string Title {get;set;}

		[DataMember(Name="latitude")]
		public double Latitude {get;set;}

		[DataMember(Name="longitude")]
		public double Longitude {get;set;}

		[DataMember(Name="photo")]
		public string Photo {get;set;}

		[DataMember(Name="address")]
		public string Address {get;set;}

		[DataMember(Name="city")]
		public string City {get;set;}

		[DataMember(Name="state")]
		public string State {get;set;}
	}

	[DataContract]
	public class CottagePhoto
	{
		[DataMember(Name="media_url")]
		public string MediaURL;

		[DataMember(Name="media_modification_timestamp")]
		public string ModificationTime;
	}
}

