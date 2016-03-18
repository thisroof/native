using System;

using UIKit;
using MvvmCross.iOS.Views;
using ObjCRuntime;
using CoreGraphics;
using Foundation;
using ThisRoofN.iOS.Helpers;

namespace ThisRoofN.iOS
{
	public class BaseViewController : MvxViewController
	{
		private const int LOADING_GIF_WIDTH = 150; 
		private const int LOADING_GIF_HEIGHT = 83; 

		protected UIView loadingView;
		protected UIButton backButton;
		protected UIButton settingButton;


		public BaseViewController (IntPtr handle) : base (handle)
		{
			this.Initializes ();
		}

		public BaseViewController ()
		{
			this.Initializes ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			CreateLoaindingAnimator ();

			// iOS7 layout
			if (RespondsToSelector (new Selector ("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;
		}

		private void Initializes ()
		{
			EdgesForExtendedLayout = UIRectEdge.None;
		}

		protected void SetupNavigationBar (bool hasSetting = true)
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
			settingButton.SetTitle (string.Empty, UIControlState.Normal);
			if (hasSetting) {
				settingButton.SetImage (UIImage.FromBundle ("icon_setting"), UIControlState.Normal);
			}
			UIBarButtonItem rightButtonItem = new UIBarButtonItem (settingButton);
			this.NavigationItem.SetRightBarButtonItem (rightButtonItem, true);
		}

		private void CreateLoaindingAnimator ()
		{
			CGRect viewRect = UIScreen.MainScreen.Bounds;
			//Prepare Animator
			loadingView = new UIView (new CGRect (0, 0, viewRect.Width, viewRect.Height));

			UIView animatorView = new UIView(new CGRect(
				(viewRect.Width - (LOADING_GIF_WIDTH + 16)) / 2, 
				(viewRect.Height - (LOADING_GIF_HEIGHT + 20)) / 2 - 30,
				LOADING_GIF_WIDTH + 16,
				LOADING_GIF_HEIGHT + 20));
			NSUrl gifUrl = NSBundle.MainBundle.GetUrlForResource ("animator", "html");
			var request = new NSUrlRequest (gifUrl, NSUrlRequestCachePolicy.ReloadIgnoringLocalAndRemoteCacheData, 30.0);

			UIWebView gifWebView = new UIWebView (new CGRect (8, 0, LOADING_GIF_WIDTH, LOADING_GIF_HEIGHT));
			gifWebView.BackgroundColor = UIColor.Clear;
			gifWebView.Opaque = false;
			gifWebView.LoadRequest (request);

			CGRect loadingLabelFrame = new CGRect (8, LOADING_GIF_HEIGHT - 4, LOADING_GIF_WIDTH, 20);
			UILabel loadingTextView = new UILabel (loadingLabelFrame) {
				Text = "Loading...",
				BackgroundColor = UIColor.Clear,
				TextColor = TRColorHelper.LightBlue,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.FromName("HelveticaNeue", 16.0f)
			};

			animatorView.BackgroundColor = UIColor.White;
			animatorView.Add (gifWebView);
			animatorView.Add (loadingTextView);
			animatorView.Layer.BorderColor = TRColorHelper.LightBlue.CGColor;
			animatorView.Layer.BorderWidth = 1.0f;
			animatorView.Layer.CornerRadius = 5.0f;

			loadingView.Add (animatorView);
			loadingView.BackgroundColor = TRColorHelper.LoadingBack;
			this.View.Add (loadingView);

			loadingView.Hidden = true;
		}
	}
}


