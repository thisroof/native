using System;

namespace ThisRoofN.iOS
{
	public class TRStoryboardHelper
	{
		private static string LoginFlowStoryboard = "LoginFlow";
		private static string SearchFlowStoryboard = "SearchFlow";
		private static string SearchResultStoryboard = "SearchResult";

		public static string GetStoryboardName(Type viewType)
		{
			if (viewType.Name == "HomeViewController" || 
				viewType.Name == "LoginViewController" || 
				viewType.Name == "SignupViewController" ) {
				return LoginFlowStoryboard;
			} else if (viewType.Name == "SearchTypeViewController" || 
				viewType.Name == "NormalSearchViewController") {
				return SearchFlowStoryboard;
			} else {
				return SearchResultStoryboard;
			}
		}
	}
}

