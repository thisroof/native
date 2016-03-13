using System;
using MvvmCross.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using UIKit;
using ThisRoofN.iOS.Views;

namespace ThisRoofN.iOS
{
	public class TRMvxIosViewsContainer : MvxIosViewsContainer
	{
		protected override IMvxIosView CreateViewOfType (Type viewType, MvxViewModelRequest request)
		{
			MvxTrace.Trace (MvxTraceLevel.Diagnostic, viewType.Name);

//			if (viewType.Name == "SearchResultHomeView") {
//				return new SearchResultHomeView ();
//			}

			string storyboardName = TRStoryboardHelper.GetStoryboardName (viewType);
			return (IMvxIosView)UIStoryboard.FromName (storyboardName, null).InstantiateViewController (viewType.Name);
		}
	}
}

