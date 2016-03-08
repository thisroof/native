using System;

using UIKit;
using MvvmCross.iOS.Views;
using ObjCRuntime;

namespace ThisRoofN.iOS
{
	public class BaseViewController : MvxViewController
	{
		public BaseViewController(IntPtr handle) : base (handle)
		{
			this.Initializes ();
		}

		public BaseViewController()
		{
			this.Initializes ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// iOS7 layout
			if (RespondsToSelector (new Selector ("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;
		}

		private void Initializes()
		{
			EdgesForExtendedLayout = UIRectEdge.None;
		}
	}
}


