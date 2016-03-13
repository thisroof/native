using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ThisRoofN
{
	[DataContract]
	public class TRSearchResult
	{
		public TRSearchResult ()
		{
		}

		public string Address {
			get { 
				return this.Listing.AddressData.FullStreetAddress;
			}

		}
		public string PrimaryPhotoUrl {
			get { 
				if (this.Listing.Photos != null && this.Listing.Photos.Count > 0)
				{
					return this.Listing.Photos[0].MediaURL;
				}
				else
				{
					return string.Empty;
				}
			}

		}

		public string GalleryUrl {
			get { 
				return this.Listing.MatrixUniqueID;
			}
		}

		public int LotSquareSize {
			get {
				// Property sub type is not working for now, so use acres without any validation.
				double value = 0;
				double.TryParse(this.Listing.Acres, out value);
				return (int)value;
			}
		}

		public string LicenseNumber
		{
			get
			{
				try
				{
					return Listing.Participants[0].Licenses.License.LicenseNumber;
				}
				catch(NullReferenceException ex)
				{
					return string.Empty;
				}
			}
		}

		[DataMember(Name="data")]
		public SearchResultListing Listing {get;set;}
	}

	[DataContract]
	public class SearchResultListing
	{
		[DataMember(Name="photos")]
		public List<PhotoDetail> Photos {get;set;}

		[DataMember(Name="address")]
		public SearchResultAddress AddressData {get;set;}

		[DataMember(Name="offices")]
		public List<SearchResultOffice> Offices {get;set;}

		[DataMember(Name="bedrooms")]
		public int Bedrooms { get; set; }

		[DataMember(Name="location")]
		public SearchResultLocation Location {get;set;}

		[DataMember(Name="lot_size")]
		public string Acres{get;set;}

		[DataMember(Name="bathrooms")]
		public double Bathrooms { get; set; }

		[DataMember(Name="list_price")]
		public double ListPrice { get; set; }

		// Now This field is error so I skip this field for now.
		//		[DataMember(Name="property_sub_type")]
		//		public string PropertySubType { get; set; }

		[DataMember(Name="listing_key")]
		public string MatrixUniqueID { get; set; }

		[DataMember(Name="living_area")]
		public string SquareFootageStructure { get; set; }

		[DataMember(Name="listing_description")]
		public string PropertyDescription { get; set; }

		[DataMember(Name="listing_participants")]
		public List<SearchResultParticipant> Participants {get;set;}
	}

	[DataContract]
	public class SearchResultAddress
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
	public class SearchResultLocation
	{
		[DataMember(Name="county")]
		public string County { get; set; }

		[DataMember(Name="latitude")]
		public float Latitude { get; set; }

		[DataMember(Name="longitude")]
		public float Longitude { get; set; }
	}


	[DataContract]
	public class SearchResultOffice
	{
		[DataMember(Name="name")]
		public string Name { get; set; }
	}

	[DataContract]
	public class SearchResultParticipant
	{
		[DataMember(Name="licenses")]
		public SearchResultLicenses Licenses {get; set;}

		[DataMember(Name="first_name")]
		public string FirstName { get; set; }

		[DataMember(Name="last_name")]
		public string LastName { get; set; }

		[DataMember(Name="office_phone")]
		public string OfficePhone { get; set; }
	}

	[DataContract]
	public class SearchResultLicenses
	{
		[DataMember(Name="license")]
		public SearchResultLicense License {get;set;}
	}

	[DataContract]
	public class SearchResultLicense
	{
		[DataMember(Name="license_number")]
		public string LicenseNumber {get;set;}
	}

	[DataContract]
	public class PhotoDetail
	{
		[DataMember(Name="media_url")]
		public string MediaURL {get;set;}

		[DataMember(Name="media_modification_timestamp")]
		public string ModificationTimeStamp {get;set;}
	}
}

