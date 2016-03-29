// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;

namespace ThisRoofN.iOS
{
	public partial class InHomeModalView : BaseModalView
	{
		public InHomeModalView (IntPtr handle) : base (handle)
		{
		}

		public InHomeModalViewModel ViewModelInstance {
			get {
				return (InHomeModalViewModel)base.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Bind Item Tableview source
			var source = new HomeItemsTableViewSource (this, tbl_inHomeItems);
			tbl_inHomeItems.Source = source;
			tbl_inHomeItems.RowHeight = UITableView.AutomaticDimension;
			tbl_inHomeItems.TableFooterView = new UITableView (CGRect.Empty);
			tbl_inHomeItems.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			tbl_inHomeItems.ReloadData ();

			var bindingSet = this.CreateBindingSet<InHomeModalView, InHomeModalViewModel> ();
			bindingSet.Bind (btn_modalBack).To (vm => vm.ModalCloseCommand);
			bindingSet.Bind (source).To (vm => vm.Items);
			bindingSet.Apply ();

			this.view_side.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				ViewModelInstance.CloseCommand.Execute(null);
			}));
		}

		public class HomeItemsTableViewSource : MvxTableViewSource
		{
			InHomeModalView masterView;
			public HomeItemsTableViewSource (InHomeModalView _masterView, UITableView tv) : base (tv)
			{
				masterView = _masterView;
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60.0f;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				InHomeItemCell cell = (InHomeItemCell)tableView.DequeueReusableCell (InHomeItemCell.Identifier);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				masterView.ViewModelInstance.Items [indexPath.Row].Selected = !masterView.ViewModelInstance.Items [indexPath.Row].Selected;
			}
		}
	}
}
