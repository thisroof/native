using System;
using System.Runtime.Serialization;
using ThisRoofN.Helpers;
using SQLite.Net.Attributes;

namespace ThisRoofN.Database.Entities
{
	public class TREntityBase
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}

