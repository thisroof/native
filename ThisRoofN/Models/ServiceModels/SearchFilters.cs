using System;
using System.Runtime.Serialization;
using System.Linq;
using ThisRoofN.Database;
using ThisRoofN.Models.App;
using ThisRoofN.Extensions;
using Newtonsoft.Json;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;
using ThisRoofN.Database.Entities;

namespace ThisRoofN.Models.Service
{
	[DataContract]
	public class SearchFilters : TREntityBase
	{
		#region Default Properties
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
		#endregion

		#region Additional Fields
		public string JsonStringForUpdate {
			get {
				var json = new {
					//				mobile_num = "035ed031-1cb1-46aa-ba72-b0943d1896da",
					mobile_num = MobileNum,
					first_name = "",
					last_name = "",
					latitude = GeoLat.ToString(),
					longitude = GeoLng.ToString(),
					email = "",
					altitude = "0",
					address = Address ?? "",
					city = City ?? "",
					state = State ?? "",
					zip_code = Zip ?? "0",
					country = "US",//usersAppPreferences.Country ?? "",
					value = Value.ToString () ,
					bedrooms = Bedrooms.ToString (),
					baths_full = BathsFull.ToString (),
					min_square_footage_structure = MinSquareFootageStructure.ToString (),
					max_square_footage_structure = MaxSquareFootageStructure.ToString (),
					min_lot_square_footage = MinLotSquareFootage.ToString (),
					max_lot_square_footage = MaxLotSquareFootage.ToString (),
					max_budget = MaxBudget,
					min_beds = MinBeds.ToString(),
					min_baths = MinBaths.ToString(),
					start_zip = StartZip ?? "0",
					search_type = SearchType.ToString (),
					// search_dist = Utils.GetSearchDistanceValue(usersAppPreferences.SearchDistance),
					search_dist = SearchDistance.ToString(),
					metricus = MetricUS ?? "0",
					proptypes = PropertyTypes.GetPropertyTypeValue(),
					dislike_zipcodes = DislikeZipcodes,
					traffic_type = TrafficType.ToString (),
					travel_mode = TravelMode.ToString (),
					state_filters = StateFilters,
					year_built = YearBuilt.ToString(),
					days_on_market = DaysOnMarket.ToString(),
					has_pool = HasPool,
					view_types = ViewTypes,
					foreclosure_status = ForeclosureStatus,
					sort_by = SortBy,
					keywords = Keywords
				};

				return JsonConvert.SerializeObject (json);
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Get saved Search Property from database
		/// </summary>
		/// <returns>The latest from database.</returns>
		public static SearchFilters FetchLatestFromDatabase()
		{
			IUserPreference mUserPref =  Mvx.Resolve<IUserPreference> ();
			var filter = TRDatabase.Instance.GetSearchFilter (mUserPref.GetValue(TRConstant.UserPrefUserIDKey, 0));
			if (filter != null) {
				return filter;
			} else {
				return new SearchFilters ();
			}
		}

		#endregion
	}
}

