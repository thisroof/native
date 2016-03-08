using System;

namespace ThisRoofN
{
	public static class TRConstant
	{
		// Facebook API
		public static readonly string FacebookAppName 			= "ThisRoof";
		public static readonly string FacebookAppID 			= "196649054002336";
		public static readonly string FacebookAppSecret 		= "44ee40904108c207763f4efbccdccc71";

		// ThisRoof Service BaseURL
		public static readonly string TRServiceBaseURL 			= "http://api.thisroof.com/"; 
		public static readonly string TRDefaultOAuthProvider	= "thisroof";
		public static readonly string TRFBOAuthProvider			= "facebook";

		// User Preference Keys
		public static readonly string UserPrefUserEmailKey			= "user_email_key";
		public static readonly string UserPrefUserPasswordKey		= "user_pwd_key";
		public static readonly string UserPrefAccessTokenKey		= "access_token_key";
		public static readonly string UserPrefRefreshTokenKey		= "refresh_token_key";
	}
}

