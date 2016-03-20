// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using MvvmCross.Binding.iOS.Views;

namespace ThisRoofN.iOS
{
	public partial class ArchitectureModalView : BaseModalView
	{
		public ArchitectureModalView (IntPtr handle) : base (handle)
		{
		}

		public ArchitectureModalViewModel ViewModelInstance
		{
			get {
				return (ArchitectureModalViewModel)base.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Bind Item Tableview source
			var source = new ArchtectureItemsTableViewSource (this, tbl_items);
			tbl_items.Source = source;
			tbl_items.RowHeight = UITableView.AutomaticDimension;
			tbl_items.TableFooterView = new UITableView (CGRect.Empty);
			tbl_items.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			tbl_items.ReloadData ();

			var bindingSet = this.CreateBindingSet<ArchitectureModalView, ArchitectureModalViewModel> ();
			bindingSet.Bind (btn_modalBack).To (vm => vm.ModalCloseCommand);
			bindingSet.Bind (source).To (vm => vm.Items);
			bindingSet.Apply ();
		}

		public class ArchtectureItemsTableViewSource : MvxTableViewSource
		{
			ArchitectureModalView masterView;
			public ArchtectureItemsTableViewSource (ArchitectureModalView _masterView, UITableView tv) : base (tv)
			{
				masterView = _masterView;
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60.0f;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				ArchtectureItemCell cell = (ArchtectureItemCell)tableView.DequeueReusableCell (ArchtectureItemCell.Identifier);
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
