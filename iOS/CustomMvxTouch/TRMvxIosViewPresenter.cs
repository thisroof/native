using System;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;
using UIKit;
using System.Collections.Generic;
using ThisRoofN.iOS.Helpers;
using System.Linq;
using MvvmCross.iOS.Views;
using CoreAnimation;
using CoreGraphics;

namespace ThisRoofN.iOS
{
	public class TRMvxIosViewPresenter : MvxIosViewPresenter
	{
		private readonly Stack<UIViewController> _modalViewControllers = new Stack<UIViewController> ();

		public TRMvxIosViewPresenter (UIApplicationDelegate appDelegate, UIWindow window) : base(appDelegate, window)
		{
			
		}

		public override void Show (IMvxIosView view)
		{
			if (view is IMvxModalIosView) {
				PresentModalViewController (view as UIViewController, true);
			} else {
				base.Show (view);
			}
		}

		public override bool PresentModalViewController (UIViewController viewController, bool animated)
		{
			viewController.View.BackgroundColor = UIColor.Clear;
			this.MasterNavigationController.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;

			this.MasterNavigationController.PresentModalViewController (viewController, false);
			_modalViewControllers.Push (viewController);
			return true;
		}

		public override void Close (IMvxViewModel toClose)
		{
			if (_modalViewControllers.Any ()) {
				CloseModalViewController ();
				return;
			}
			base.Close (toClose);
		}

		public override void CloseModalViewController ()
		{
			var currentModal = _modalViewControllers.Pop ();

			currentModal.View.BackgroundColor = UIColor.Clear;
			UIView.Animate (0.4, () => {
				currentModal.View.Frame = new CGRect(currentModal.View.Frame.Width, 0, currentModal.View.Frame.Width, currentModal.View.Frame.Height);
			}, () => {
				currentModal.DismissViewController (false, null);
			});
		}

		public override void ChangePresentation (MvxPresentationHint hint)
		{
			if (hint is TRMvxPresentationHint) {
//				TRMvxPresentationHint pHint = (TRMvxPresentationHint)hint;
				this.MasterNavigationController.PopToRootViewController (true);
			} else {
				base.ChangePresentation (hint);
			}
		}
	}
}

