using System;

namespace ThisRoofN.iOS
{
	public class TRStoryboardHelper
	{
		private static string LoginFlowStoryboard = "LoginFlow";
		private static string SearchFlowStoryboard = "SearchFlow";
		private static string SearchResultStoryboard = "SearchResult";
		private static string SettingsStoryboard = "Settings";

		public static string GetStoryboardName(Type viewType)
		{
			if (viewType.Name == "HomeViewController" || 
				viewType.Name == "LoginViewController" || 
				viewType.Name == "SignupViewController" ) {
				return LoginFlowStoryboard;
			} else if (viewType.Name == "SearchTypeViewController" || 
				viewType.Name == "NormalSearchViewController" ||
				viewType.Name == "AffordSearchView" ||
				viewType.Name == "AffordResultView") {
				return SearchFlowStoryboard;
			} else if (viewType.Name == "SettingsView" || 
				viewType.Name == "TRWebViewController") {
				return SettingsStoryboard;
			} else {
				return SearchResultStoryboard;
			}
		}
	}
}

