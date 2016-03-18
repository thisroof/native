// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;

namespace ThisRoofN.iOS
{
	public partial class AffordResultView : BaseViewController
	{
		public AffordResultView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SetupNavigationBar ();

			this.View.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.View.EndEditing(true);
			}));

			var bindingSet = this.CreateBindingSet<AffordResultView, AffordResultViewModel> ();
			bindingSet.Bind (backButton).To (vm => vm.CloseCommand);
			bindingSet.Bind (settingButton).To (vm => vm.SettingCommand);
			bindingSet.Bind (lbl_budget).To (vm => vm.AffordAmount);
			bindingSet.Bind (btn_startSearch).To (vm => vm.StartSearchCommand);
			bindingSet.Bind (btn_getPreApprove).To (vm => vm.GetApprovedCommand);
			bindingSet.Apply ();
		}
	}
}
