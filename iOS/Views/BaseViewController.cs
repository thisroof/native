using System;

using UIKit;
using MvvmCross.iOS.Views;
using ObjCRuntime;
using CoreGraphics;

namespace ThisRoofN.iOS
{
	public class BaseViewController : MvxViewController
	{
		protected UIButton backButton;
		protected UIButton settingButton;


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
			
		protected void SetupNavigationBar()
		{
			this.NavigationController.SetNavigationBarHidden (false, true);
			this.NavigationItem.SetHidesBackButton (true, true);
			UIImage logoImage = UIImage.FromBundle ("img_nav_title");
			UIImageView titleImageView = new UIImageView (new CGRect (0, 0, 0, 35.0f));
			titleImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			titleImageView.Image = logoImage;
			this.NavigationItem.TitleView = titleImageView;

			backButton = new UIButton (new CGRect (0, 0, 20, 20));
			backButton.SetImage (UIImage.FromBundle ("icon_arrow_back"), UIControlState.Normal);
			UIBarButtonItem barButtonItem = new UIBarButtonItem (backButton);
			this.NavigationItem.SetLeftBarButtonItem (barButtonItem, true);

			settingButton = new UIButton (new CGRect (0, 0, 20, 20));
			settingButton.SetImage (UIImage.FromBundle ("icon_setting"), UIControlState.Normal);
			UIBarButtonItem rightButtonItem = new UIBarButtonItem (settingButton);
			this.NavigationItem.SetRightBarButtonItem (rightButtonItem, true);
		}
	}
}


