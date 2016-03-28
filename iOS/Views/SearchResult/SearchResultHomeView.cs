// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using ThisRoofN.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using CoreGraphics;
using ThisRoofN.iOS.Helpers;
using MvvmCross.Binding.BindingContext;

namespace ThisRoofN.iOS.Views
{
	public partial class SearchResultHomeView : BaseViewController
	{
		private UIViewController tileVC;
		private UIViewController mapVC;
		private int _curPage;

		public SearchResultHomeView (IntPtr handle) : base (handle)
		{
		}

		public SearchResultHomeView() : base() {
		}

		public int CurPage
		{
			get{
				return _curPage;
			}
			set {
				if (_curPage != value) {
					page_scroll.SetContentOffset (new CGPoint ((value * UIScreen.MainScreen.Bounds.Width), 0), true);
					_curPage = value;
				}
			}
		}

		public SearchResultHomeViewModel ViewModelInstance
		{
			get {
				return (SearchResultHomeViewModel)ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SetupNavigationBar ();

			page_scroll.ShowsHorizontalScrollIndicator = false;
			page_scroll.ShowsVerticalScrollIndicator = false;
			page_scroll.Bounces = false;
			page_scroll.PagingEnabled = true;

			tileVC = CreatePage (ViewModelInstance.TileViewModel);
			mapVC = CreatePage (ViewModelInstance.MapViewModel);

			page_scroll.AddSubview (tileVC.View);
			page_scroll.AddSubview (mapVC.View);

			SetSearchResultToolbar ();

			var bindingSet = this.CreateBindingSet<SearchResultHomeView, SearchResultHomeViewModel> ();
			bindingSet.Bind (backButton).To (vm => vm.CloseCommand);
			bindingSet.Bind (settingButton).To (vm => vm.SettingCommand);
			bindingSet.Apply ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (false, true);
			this.NavigationController.SetToolbarHidden (false, true);
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			tileVC.View.Frame = new CGRect (0, 0, page_scroll.Frame.Width, page_scroll.Frame.Height);
			mapVC.View.Frame = new CGRect (page_scroll.Frame.Width, 0, page_scroll.Frame.Width * 2, page_scroll.Frame.Height);
		}

		private UIViewController CreatePage(IMvxViewModel viewModel)
		{
			var controller = new UINavigationController ();
			var screen = this.CreateViewControllerFor (viewModel) as UIViewController;
			controller.PushViewController (screen, false);
			controller.NavigationBarHidden = true;
			return controller;
		}

		private void SetSearchResultToolbar()
		{
			CGRect toolbarFrame = this.NavigationController.Toolbar.Frame;
			nfloat oneItemWidth = (UIScreen.MainScreen.Bounds.Width - 36) / 2;
			nfloat itemHeight = toolbarFrame.Size.Height - 20;

			UIView tileItemView = new UIView (new CGRect (0, 0, oneItemWidth, toolbarFrame.Height));
			UIButton tileButton = new UIButton(new CGRect(0, 10, oneItemWidth, itemHeight));
			tileButton.Font = UIFont.FromName ("HelveticaNeue", 20.0f);
			tileButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			tileButton.SetTitle ("TILE", UIControlState.Normal);
			tileItemView.Add (tileButton);
			UIBarButtonItem tileItem = new UIBarButtonItem (tileItemView);


			UIView mapItemView = new UIView (new CGRect (0, 0, oneItemWidth, toolbarFrame.Height));
			UIButton mapButton = new UIButton(new CGRect(0, 10, oneItemWidth, itemHeight));
			mapButton.Font = UIFont.FromName ("HelveticaNeue", 20.0f);
			mapButton.SetTitleColor (UIColor.White.ColorWithAlpha(0.4f), UIControlState.Normal);
			mapButton.SetTitle ("MAP", UIControlState.Normal);
			mapItemView.Add (mapButton);
			UIBarButtonItem mapItem = new UIBarButtonItem (mapItemView);



			this.SetToolbarItems (new UIBarButtonItem[] {
				tileItem, mapItem
			}, true);

			this.NavigationController.SetToolbarHidden (false, true);

			UIToolbar toolbar = this.NavigationController.Toolbar;
			toolbar.Layer.BorderWidth = 2;
			toolbar.Layer.BorderColor = TRColorHelper.LightBlue.CGColor; 
			toolbar.Layer.ShadowColor = UIColor.Black.CGColor;
			toolbar.Layer.ShadowOffset = new CGSize(0, -1);
			toolbar.Layer.ShadowOpacity = .6f;

			tileButton.TouchUpInside += (object sender, EventArgs e) => {
				CurPage = 0;

				tileButton.SetTitleColor(UIColor.White.ColorWithAlpha(1.0f), UIControlState.Normal);
				mapButton.SetTitleColor(UIColor.White.ColorWithAlpha(0.4f), UIControlState.Normal);
			};

			mapButton.TouchUpInside += (object sender, EventArgs e) => {
				CurPage = 1;

				tileButton.SetTitleColor(UIColor.White.ColorWithAlpha(0.4f), UIControlState.Normal);
				mapButton.SetTitleColor(UIColor.White.ColorWithAlpha(1.0f), UIControlState.Normal);
			};
		}
	}
}
