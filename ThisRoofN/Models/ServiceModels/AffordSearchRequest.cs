using System;
using System.Runtime.Serialization;

namespace ThisRoofN.Models.Service
{
	[DataContract]
	public class AffordSearchRequest
	{
		[DataMember(Name="mobile_num")]
		public string mobileNum;

		[DataMember(Name="annualincome")]
		public int annualIncome;

		[DataMember(Name="down")]
		public int down;

		[DataMember(Name="monthlydebts")]
		public int monthlyDebts;

		//		[DataMember(Name="rate")]
		//		public int rate;

		[DataMember(Name="schedule")]
		public string schedule;

		[DataMember(Name="propertytax")]
		public double propertyTax;

		[DataMember(Name="hazard")]
		public string hazard;

		[DataMember(Name="term")]
		public int term;

		[DataMember(Name="estimate")]
		public bool estimate;

		[DataMember(Name="hoa")]
		public int hoa;

		[DataMember(Name="zip")]
		public string zip;
	}

	[DataContract]
	public class AffordSearchResponse
	{
		[DataMember(Name="success")]
		public bool success;

		[DataMember(Name="message")]
		public string message;

		[DataMember(Name="data")]
		public AffordAmount data;
	}

	[DataContract]
	public class AffordAmount
	{
		[DataMember(Name="affordabilityamount")]
		public int Amount;
	}
}

