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


		// Normal Search Property Values
		public static readonly int BudgetStep = 10000;
		public static readonly int MinValidBudget = 10000;
		public static readonly int MaxValidBudget = 200000000;

		public static readonly int OneAcreToSquareFoot = 43560;

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
	}
}

