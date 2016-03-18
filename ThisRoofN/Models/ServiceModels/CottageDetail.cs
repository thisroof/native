using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;
using ThisRoofN.Database;
using ThisRoofN.Database.Entities;

namespace ThisRoofN.Models.Service
{
	[DataContract]
	public class CottageDetail
	{
		#region Default Fields
		[DataMember(Name="data")]
		public CottageDetailData Data {get;set;}
		#endregion

		#region Additional Fields
		public string Address {
			get { 
				return this.Data.AddressData.FullStreetAddress;
			}

		}
		public string PrimaryPhotoUrl {
			get { 
				if (this.Data.Photos != null && this.Data.Photos.Count > 0)
				{
					return this.Data.Photos[0].MediaURL;
				}
				else
				{
					return string.Empty;
				}
			}

		}

		public string GalleryUrl {
			get { 
				return this.Data.MatrixUniqueID;
			}
		}

		public int LotSquareSize {
			get {
				// Property sub type is not working for now, so use acres without any validation.
				double value = 0;
				double.TryParse(this.Data.Acres, out value);
				return (int)value;
			}
		}

		public string LicenseNumber
		{
			get
			{
				try
				{
					return Data.Participants[0].Licenses.License.LicenseNumber;
				}
				catch(NullReferenceException ex)
				{
					return string.Empty;
				}
			}
		}
		#endregion
	}

	#region DataContract Classes
	[DataContract]
	public class CottageDetailData
	{
		[DataMember(Name="photos")]
		public List<CottagePhoto> Photos {get;set;}

		[DataMember(Name="address")]
		public CottageAddress AddressData {get;set;}

		[DataMember(Name="offices")]
		public List<CottageOffice> Offices {get;set;}

		[DataMember(Name="bedrooms")]
		public int Bedrooms { get; set; }

		[DataMember(Name="location")]
		public CottageLocation Location {get;set;}

		[DataMember(Name="lot_size")]
		public string Acres{get;set;}

		[DataMember(Name="bathrooms")]
		public double Bathrooms { get; set; }

		[DataMember(Name="list_price")]
		public double ListPrice { get; set; }

		[DataMember(Name="listing_key")]
		public string MatrixUniqueID { get; set; }

		[DataMember(Name="living_area")]
		public string SquareFootageStructure { get; set; }

		[DataMember(Name="listing_description")]
		public string PropertyDescription { get; set; }

		[DataMember(Name="listing_participants")]
		public List<CottageParticipant> Participants {get;set;}
	}

	[DataContract]
	public class CottageAddress
	{
		[DataMember(Name="city")]
		public string City { get; set; }

		[DataMember(Name="country")]
		public string Country { get; set; }

		[DataMember(Name="state_or_province")]
		public string State { get; set; }

		[DataMember(Name="full_street_address")]
		public string FullStreetAddress { get; set; }

		[DataMember(Name="postal_code")]
		public string ZipCode { get; set; }
	}

	[DataContract]
	public class CottageLocation
	{
		[DataMember(Name="county")]
		public string County { get; set; }

		[DataMember(Name="latitude")]
		public float Latitude { get; set; }

		[DataMember(Name="longitude")]
		public float Longitude { get; set; }
	}


	[DataContract]
	public class CottageOffice
	{
		[DataMember(Name="name")]
		public string Name { get; set; }
	}

	[DataContract]
	public class CottageParticipant
	{
		[DataMember(Name="licenses")]
		public CottageLicense Licenses {get; set;}

		[DataMember(Name="first_name")]
		public string FirstName { get; set; }

		[DataMember(Name="last_name")]
		public string LastName { get; set; }

		[DataMember(Name="office_phone")]
		public string OfficePhone { get; set; }
	}

	[DataContract]
	public class CottageLicense
	{
		[DataMember(Name="license")]
		public CottageLicenseNumber License {get;set;}
	}

	[DataContract]
	public class CottageLicenseNumber
	{
		[DataMember(Name="license_number")]
		public string LicenseNumber {get;set;}
	}
	#endregion
}

