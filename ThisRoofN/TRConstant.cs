using System;
using System.Collections.Generic;

namespace ThisRoofN
{
	public static class TRConstant
	{
		// Facebook API
		public static readonly string FacebookAppName 			= "ThisRoof";
		public static readonly string FacebookAppID 			= "196649054002336";
		public static readonly string FacebookAppSecret 		= "44ee40904108c207763f4efbccdccc71";

		// Xamarin.Insights
		public static readonly string InsightsApiKey 			= "c2ca12e603a2daa0deca1c39a7c62d7cd9fae812";

		// Google API KEY
		public static readonly string GMapAndroidApiKey 		= "AIzaSyASoYzYHkwODn11TzHjAJpE0eTGDgFGfLI";
		public static readonly string GMapIosApiKey 			= "AIzaSyDMLSurJ3zEsOtHk1GWNtL6LA2iL1gLc9w";
		public static readonly string GoogleAnalyticsID 		= "UA-68009456-1";// Craig Account

		// ThisRoof Service
		public static readonly string TRServiceBaseURL 			= "http://api.thisroof.com/"; 
		public static readonly string TRDefaultOAuthProvider	= "thisroof";
		public static readonly string TRFBOAuthProvider			= "facebook";
		public static readonly string TRServiceKey				= "5df6feba-4b48-4cb0-b3b4-c1e778d0c6ce";
	

		// User Preference Keys
		public static readonly string UserPrefUserIDKey				= "user_id_key";
		public static readonly string UserPrefUserEmailKey			= "user_email_key";
		public static readonly string UserPrefUserPasswordKey		= "user_pwd_key";
		public static readonly string UserPrefAccessTokenKey		= "access_token_key";
		public static readonly string UserPrefRefreshTokenKey		= "refresh_token_key";

		//Web pages with static content
		public static readonly string SupportPageLink = "http://thisroof.com/app-docs/";
		public static readonly string PrivacyPolicyPageLink = "http://thisroof.com/app-docs/pp/";
		public static readonly string TermsOfUsePageLink = "http://thisroof.com/app-docs/tos/";
		public static readonly string LicensePageLink = "http://thisroof.com/app-docs/lic/";
		public static readonly string PreApprovalLink = "http://thisroof.com/pre-approval/";

		// Normal Search Property Values
		public static readonly int BudgetStep = 10000;
		public static readonly int MinValidBudget = 10000;
		public static readonly int MaxValidBudget = 200000000;

		// Afford Constants
		public static readonly int MinValidIncome = 10000;
		public static readonly int MaxValidIncome = 50000000;
		public static readonly int MinValidDebts = 0;
		public static readonly int MaxValidDebts = 50000;
		public static readonly int MinValidDownPayment = 0;
		public static readonly int MaxValidDownPayment = 5000000;
		public static readonly int OneAcreToSquareFoot = 43560;

		#region SEARCH AREA consts
		public static readonly List<string> CommuteItems = new List<string>() {
			"Driving",
			"Carpool",
			"No traffic",
			"Rush Hour",
		};
		#endregion

		#region IN THE HOME consts
		public static readonly List<string> InHomeItems = new List<string>() {
			"Garage",
			"Swimming Pool",
			"Spa/Hot Tub",
			"Pond",
			"Yard/Garden",
			"Fireplace",
			"BBQ",
			"Walk in Closet",
			"Eco-Friendly",
			"Home Theater",
			"Basement",
			"Attic",
			"Green House",
			"Boat Doc",
			"Disabled Access",
			"Deck/Patio",
			"Bright & Airy",
			"RV Parking"
		};
		#endregion

		#region IN THE AREA consts
		public static readonly List<string> InAreaItems = new List<string>() {
			"Schools",
			"Top Rated Cafes/Restaurants",
			"Grocery Stores",
			"Shopping Malls/Plazas",
			"Entertainment",
			"Parks",
			"Golf Course",
			"Beaches",
			"Mueseums/Galleries",
			"Hospitals",
			"Theatres",
			"Sports Arenas",
			"Amusement Parks",
			"Salons",
			"Airport",
			"Public Library",
			"Banks",
			"Water Marina"
		};
		#endregion

		#region Search LOCATION consts
		public static readonly List<string> LocationItems = new List<string>() {
			"City/Urban",
			"Suburb",
			"Rural"
		};
		#endregion

		#region Search Archticture consts
		public static readonly List<string> ArchitectureItems = new List<string>() {
			"Modern",
			"Contemporary",
			"Loft",
			"Historic Style Homes",
			"Cottage",
			"Cape Code",
			"Craftsman",
			"Colonial",
			"Mid-Century Modern",
			"Italianate",
			"Prairie",
			"Ranch Style",
			"Neoclassical",
			"Spanish",
			"Tudor",
			"Mediterranean",
			"Oriental",
			"Victorian",
			"Art Deco",
		};
		#endregion

		#region Search LIFESTYLE consts
		public static readonly List<string> LifestyleItems = new List<string>() {
			"Dog Lover",
			"Fitness Junkie (gym/studios)",
			"Hikers",
			"Skiiers/Boarders",
			"Foodies",
			"Outdoor Junkie/Fishing",
			"Higher Rollers - Casino/Resorts",
			"Nightlife(bars)",
			"Golfers",
			"Ballers",
			"Tennis Enthusiasts",
			"Surfers",
			"Skaters - skate parks"
		};
		#endregion

		#region Search HOME STRUCTURE consts
		public static readonly List<string> HomeStructureItems = new List<string>() {
			"Single Family",
			"Condo",
			"Townhouse",
			"Duplex",
			"Manufactured",
			"Lots/Land",
			"Timeshare"
		};
		#endregion

		#region HOME DETAILS consts
		public static readonly int MinHomeAge = 1912;
		public static readonly int MaxHomeAge = 2016;

		public static readonly List<string> HomeDetailItems = new List<string>() {
			"Guest Quarters",
			"Forclosure/Bank Owned",
			"New construction",
			"Rennovated",
			"No HOA",
			"Farm/Horse"
		};
		#endregion

		#region OTHER
		public static readonly Dictionary<string, string> SortTypes = new Dictionary<string, string>{
			{"best_deal", "Best Deal"},
			{"list_price desc", "Price: High to Low"},
			{"list_price asc", "Price: Low to High"},
			{"listing_date", "Newly Listed"},
			{"bedrooms", "Bedrooms"},
			{"full_bathrooms", "Bathrooms"},
			{"living_area", "Square Feet"},
			{"lot_size", "Lot Size"},
			{"year_built", "Year Built"},
			{"modification_timestamp", "Recently Changed"}
		};

		public static readonly List<string> SearchPropertyTypes = new List<string> {
			"Single Family",
			"Townhouse",
			"Condo",
			"Duplex",
			"Manufactured",
			"Lots/Land",
			"Timeshare",
		};

		public static readonly List<string> MinSquareFeetOptions = new List<string>{
			"0",
			"500",
			"1000",
			"1500",
			"2000",
			"2500",
			"3000",
			"4000",
			"5000",
			"6000",
			"7000",
			"8000"
		};

		public static readonly List<string> MaxSquareFeetOptions = new List<string>{
			"500",
			"1000",
			"1500",
			"2000",
			"2500",
			"3000",
			"4000",
			"5000",
			"6000",
			"7000",
			"8000",
			"9000",
			"10000",
			"No Limit"
		};

		public static readonly List<string> MinLotSizeOptions = new List<string>{
			"0",
			"2500",
			"5000",
			"10000",
			".5 Acre",
			"1 Acre",
			"2 Acres",
			"5 Acres",
			"10 Acres",
			"20 Acres",
			"25 Acres",
			"50 Acres"
		};

		public static readonly List<string> MaxLotSizeOptions = new List<string>{
			"2500",
			"5000",
			"10000",
			".5 Acre",
			"1 Acre",
			"2 Acres",
			"5 Acres",
			"10 Acres",
			"20 Acres",
			"25 Acres",
			"50 Acres",
			"No Limit"
		};

		public static readonly List<string> YearBuiltOptions = new List<string>{
			"No Limit",
			"1 Year",
			"5 Years",
			"20 Years",
			"30 Years",
			"40 Years",
			"50 Years",
			"100 Years"
		};

		public static readonly List<string> DaysOnMarketOptions = new List<string>{
			"Any",
			"1 Day",
			"7 Days",
			"14 Days",
			"30 Days",
			"60 Days",
			"90 Days",
			"6 Months",
			"1 Year"
		};

		public static readonly List<string> PoolOptions = new List<string>{
			"Not Required",
			"Has Pool",
			"No Pool"
		};

		public static readonly List<string> SearchViewTypes = new List<string>{
			"Bridge",
			"City",
			"Vista",
			"Lake",
			"River",
			"Desert",
			"Valley",
			"Harbor",
			"Mountain",
			"Forest",
			"Airport",
			"Water",
			"Panorama",
			"Hills",
			"Park",
			"Ocean",
			"Golf Course",
			"Ravine",
			"Bluff",
			"Canyon",
			"Marina",
			"Territorial"
		};

		public static readonly List<string> StateFilters = new List<string>{
			"AL",
			"AK",
			"AZ",
			"AR",
			"CA",
			"CO",
			"CT",
			"DE",
			"FL",
			"GA",
			"HI",
			"ID",
			"IL",
			"IN",
			"IA",
			"KS",
			"KY",
			"LA",
			"ME",
			"MD",
			"MA",
			"MI",
			"MN",
			"MS",
			"MO",
			"MT",
			"NE",
			"NV",
			"NH",
			"NJ",
			"NM",
			"NY",
			"NC",
			"ND",
			"OH",
			"OK",
			"OR",
			"PA",
			"RI",
			"SC",
			"SD",
			"TN",
			"TX",
			"UT",
			"VT",
			"VA",
			"WA",
			"WV",
			"WI",
			"WY"
		};

		public static readonly List<int> SearchDistances = new List<int>{
			5,
			10,
			15,
			20,
			25,
			50,
			75,
			100,
			150,
			200,
			250,
			300,
			350,
			400,
			450,
			500
		};

		public static readonly List<int> SearchMinutesInt = new List<int>{
			5,
			15,
			30,
			45,
			60,
			75,
			90,
			105,
			120
		};

		public static readonly List<string> SearchMinutes = new List<string>{
			"5 min",
			"15 min",
			"30 min",
			"45 min",
			"1 hr",
			"1 hr 15 min",
			"1 hr 30 min",
			"1 hr 45 min",
			"2 hours"
		};

		public static readonly List<int> LoanTerms = new List<int>{
			15,
			20,
			25,
			30,
			35,
			40
		};
		#endregion
	}
}

