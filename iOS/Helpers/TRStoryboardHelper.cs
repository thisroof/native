using System;

namespace ThisRoofN.iOS
{
	public class TRStoryboardHelper
	{
		private static string LoginFlowStoryboard = "LoginFlow";

		public static string GetStoryboardName(Type viewType)
		{
			if (viewType.Name == "WelcomeView") {
				return LoginFlowStoryboard;
			} else {
				return LoginFlowStoryboard;
			}
		}
	}
}

