// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.iOS.ValueConverters;

namespace ThisRoofN.iOS
{
	public partial class HomeDetailItemCell : MvxTableViewCell
	{
		public static string Identifier = "HomeDetailItemCell";
		public HomeDetailItemCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {
				var set = this.CreateBindingSet<HomeDetailItemCell, CheckboxItemModel>();
				set.Bind(iv_checkbox).To(vm => vm.Selected).WithConversion(new CheckmarkConverter());
				set.Bind(lbl_title).To(vm => vm.Title);
				set.Apply();
			});
		}
	}
}
