using System;

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
		public static readonly string UserPrefUserEmailKey			= "user_email_key";
		public static readonly string UserPrefUserPasswordKey		= "user_pwd_key";
		public static readonly string UserPrefAccessTokenKey		= "access_token_key";
		public static readonly string UserPrefRefreshTokenKey		= "refresh_token_key";
	}
}

