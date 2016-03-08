using System;

namespace ThisRoofN.iOS
{
	public class TRStoryboardHelper
	{
		private static string LoginFlowStoryboard = "LoginFlow";
		private static string SearchFlowStoryboard = "SearchFlow";

		public static string GetStoryboardName(Type viewType)
		{
			if (viewType.Name == "WelcomeView") {
				return LoginFlowStoryboard;
			} else if (viewType.Name == "SearchTypeViewController") {
				return SearchFlowStoryboard;
			} else {
				return LoginFlowStoryboard;
			}
		}
	}
}

