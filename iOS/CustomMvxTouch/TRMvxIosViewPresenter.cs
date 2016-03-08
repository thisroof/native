using System;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;
using UIKit;

namespace ThisRoofN.iOS
{
	public class TRMvxIosViewPresenter : MvxIosViewPresenter
	{
		public TRMvxIosViewPresenter (UIApplicationDelegate appDelegate, UIWindow window) : base(appDelegate, window)
		{
			
		}

		public override void Show (MvxViewModelRequest request)
		{
			base.Show(request);
		}
	}
}

