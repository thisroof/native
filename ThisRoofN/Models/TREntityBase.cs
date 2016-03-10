using System;
using System.Runtime.Serialization;
using ThisRoofN.Helpers;
using SQLite.Net.Attributes;

namespace ThisRoofN.Models
{
	[DataContract]
	public class TREntityBase
	{
		[DataMember(Name="id")]
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }


		public string UniqueMobileIdentifier { 
			get{ 
				return DeviceHelper.GetUniqueDeviceId ();
			} 
		}

		public string OSVersion { 
			get{ 
				return DeviceHelper.GetOSVersion();
			} 
		}

		public string Platform {
			get{ 
				return DeviceHelper.Platform();
			}
		}
	}
}

