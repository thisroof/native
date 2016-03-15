using System;
using System.Runtime.Serialization;
using System.Linq;

namespace ThisRoofN.Models
{
	[DataContract]
	public class TRUserSearchProperty : TREntityBase
	{
		[DataMember(Name = "id")]
		public int UserID { get; set; }

		[DataMember(Name = "mobile_num")]
		public string MobileNum{ get; set; }

		[DataMember(Name = "first_name")]
		public string FirstName{ get; set; }

		[DataMember(Name = "last_name")]
		public string LastName{ get; set; }

		[DataMember(Name = "email")]
		public string Email{ get; set; }

		[DataMember(Name = "altitude")]
		public double Altitude {get;set;}

		[DataMember(Name = "latitude")]
		public double GeoLat{ get; set; }

		[DataMember(Name = "longitude")]
		public double GeoLng{ get; set; }

		[DataMember(Name = "address")]
		public string Address { get; set; }

		[DataMember(Name = "city")]
		public string City { get; set; }

		[DataMember(Name = "state")]
		public string State { get; set; }

		[DataMember(Name = "zip_code")]
		public string Zip { get; set; }

		[DataMember(Name = "country")]
		public string Country { get; set; }

		public string CityStateZip{ get { return string.Format ("{0}, {1} {2}", City, State, Zip); } }

		[DataMember(Name = "value")]
		public double Value{ get; set; }

		[DataMember(Name = "bedrooms")]
		public double Bedrooms { get; set; }

		[DataMember(Name = "baths_full")]
		public double BathsFull { get; set; }

		[DataMember(Name = "min_square_footage_structure")]
		public int MinSquareFootageStructure{ get; set; }

		[DataMember(Name = "max_square_footage_structure")]
		public int MaxSquareFootageStructure{ get; set; }

		[DataMember(Name = "square_footage_structure")]
		public int SquareFootageStructure{ get; set; }

		[DataMember(Name = "min_lot_square_footage")]
		public int MinLotSquareFootage { get; set; }

		[DataMember(Name = "max_lot_square_footage")]
		public int MaxLotSquareFootage { get; set; }

		[DataMember(Name = "lot_square_footage")]
		public int LotSquareFootage { get; set; }

		[DataMember(Name = "acres")]
		public string Acres { get; set; }

		[DataMember(Name = "max_budget")]
		public double MaxBudget{ get; set; }

		[DataMember(Name = "min_budget")]
		public double MinBudget { get; set; }

		[DataMember(Name = "min_beds")]
		public double MinBeds{ get; set; }

		[DataMember(Name = "min_baths")]
		public double MinBaths { get; set; }

		[DataMember(Name = "start_zip")]
		public string StartZip{ get; set; }

		[DataMember(Name = "search_type")]
		public short SearchType{ get; set; }

		[DataMember(Name = "search_dist")]
		public int SearchDistance{ get; set; }

		[DataMember(Name = "metricus")]
		public string MetricUS{ get; set; }

		[DataMember(Name = "proptypes")]
		public string PropertyTypes{ get; set; }

		[DataMember(Name = "dislike_zipcodes")]
		public string DislikeZipcodes{ get; set; }

		[DataMember(Name = "traffic_type")]
		public int TrafficType{ get; set; }

		[DataMember(Name = "travel_mode")]
		public int TravelMode{ get; set; }

		[DataMember(Name = "state_filters")]
		public string StateFilters{ get; set; }

		[DataMember(Name = "year_built")]
		public int YearBuilt{ get; set; }

		[DataMember(Name = "days_on_market")]
		public int DaysOnMarket{ get; set; }

		[DataMember(Name = "has_pool")]
		public string HasPool{ get; set; }

		[DataMember(Name = "view_types")]
		public string ViewTypes{ get; set; }

		[DataMember(Name = "foreclosure_status")]
		public string ForeclosureStatus{ get; set; }

		[DataMember(Name = "sort_by")]
		public string SortBy{ get; set; }

		[DataMember(Name = "keywords")]
		public string Keywords{ get; set; }

		[DataMember(Name = "registration_date")]
		public DateTime RegistrationDate{ get; set; }

		[DataMember(Name = "modify_date")]
		public DateTime ModifyDate{ get{ return DateTime.Now; } }


		/// <summary>
		/// Get saved Search Property from database
		/// </summary>
		/// <returns>The latest from database.</returns>
		public static TRUserSearchProperty FetchLatestFromDatabase()
		{
			
			TRUserSearchProperty result;
			var searchPropertyList = TRDatabase.Instance.GetItems<TRUserSearchProperty> ();
			if (searchPropertyList.Count () > 0) {
				result = searchPropertyList.First ();
			} else {
				result = new TRUserSearchProperty ();
			}

			return result;
		}
	}
}

