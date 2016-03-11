using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;

namespace ThisRoofN.iOS
{
	partial class SearchTypeViewController : BaseViewController
	{
		public SearchTypeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			btn_affordSearch.TitleLabel.Lines = 0;
			btn_affordSearch.TitleLabel.TextAlignment = UITextAlignment.Center;
			btn_affordSearch.SetTitle("FIND OUT HOW MUCH\nYOU CAN AFFORD...", UIControlState.Normal);
			btn_affordSearch.LineBreakMode = UILineBreakMode.WordWrap;

			var set = this.CreateBindingSet<SearchTypeViewController, SearchTypeViewModel> ();
			set.Bind (btn_normalSearch).To (vm => vm.NormalSearchCommand);
			set.Bind (btn_affordSearch).To (vm => vm.AffordSearchCommand);
			set.Apply ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, true);
		}
	}
}
