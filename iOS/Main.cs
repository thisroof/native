using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace ThisRoofN.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// Initialize Xamarin Insights
			Xamarin.Insights.Initialize(TRConstant.InsightsApiKey);

			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
