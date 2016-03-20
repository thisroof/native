using System;
using MvvmCross.iOS.Views;
using CoreAnimation;
using ThisRoofN.iOS.Helpers;

namespace ThisRoofN.iOS
{
	public class BaseModalView : BaseViewController, IMvxModalIosView
	{
		public BaseModalView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			CATransition transition = new CATransition ();
			transition.AnimationStopped += (object sender, CAAnimationStateEventArgs e) => {
				this.View.BackgroundColor = TRColorHelper.Black50Alpha;
			};

			transition.Duration = 0.4;
			transition.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
			transition.Type = CATransition.TransitionPush;
			transition.Subtype = CATransition.TransitionFromRight;
			this.View.Layer.AddAnimation (transition, null);
		}
	}
}

