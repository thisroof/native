// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using CoreGraphics;
using RangeSlider;
using System.Linq;

namespace ThisRoofN.iOS
{
	public partial class HomeDetailModalView : BaseModalView
	{
		private RangeSliderView homeAgeSlider;

		public HomeDetailModalView (IntPtr handle) : base (handle)
		{
		}

		public HomeDetailModalViewModel ViewModelInstance {
			get {
				return (HomeDetailModalViewModel)base.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitRangeSlider ();

			// Bind Item Tableview source
			var source = new HomeDetailItemsTableViewSource (this, tbl_homeDetail);
			tbl_homeDetail.Source = source;
			tbl_homeDetail.RowHeight = UITableView.AutomaticDimension;
			tbl_homeDetail.TableFooterView = new UITableView (CGRect.Empty);
			tbl_homeDetail.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			tbl_homeDetail.ReloadData ();

			var bindingSet = this.CreateBindingSet<HomeDetailModalView, HomeDetailModalViewModel> ();
			bindingSet.Bind (btn_modalBack).To (vm => vm.ModalCloseCommand);
			bindingSet.Bind (source).To (vm => vm.Items);
			bindingSet.Bind (lbl_squareFootage).To (vm => vm.SquareFootageString);
			bindingSet.Bind (lbl_homeAge).To (vm => vm.AgeString);
			bindingSet.Bind (lbl_lotSize).To (vm => vm.LotSizeString);
			bindingSet.Apply ();
		}

		private void InitRangeSlider ()
		{
			slider_squareFootage.MinValue = 0;
			slider_squareFootage.MaxValue = TRConstant.MaxSquareFeetOptions.Count () - 1;
			slider_squareFootage.Value = ViewModelInstance.SquareFootage;
			slider_squareFootage.ValueChanged += (object sender, EventArgs e) => {
				ViewModelInstance.SquareFootage = (int)slider_squareFootage.Value;
			};

			slider_lotSize.MinValue = 0;
			slider_lotSize.MaxValue = TRConstant.MaxLotSizeOptions.Count () - 1;
			slider_lotSize.Value = ViewModelInstance.LotSize;
			slider_lotSize.ValueChanged += (object sender, EventArgs e) => {
				ViewModelInstance.LotSize = (int)slider_lotSize.Value;
			};

			homeAgeSlider = new RangeSliderView (new CGRect (0, 0, this.view_rangeSliderHomeAge.Frame.Width, this.view_rangeSliderHomeAge.Frame.Height));
			homeAgeSlider.MinValue = TRConstant.MinHomeAge;
			homeAgeSlider.MaxValue = TRConstant.MaxHomeAge;
			homeAgeSlider.LeftValue = ViewModelInstance.MinAge;
			homeAgeSlider.RightValue = ViewModelInstance.MaxAge;
			homeAgeSlider.LeftValueChanged += (nfloat value) => {
				ViewModelInstance.MinAge = (int)homeAgeSlider.LeftValue;
			};
			homeAgeSlider.RightValueChanged += (nfloat value) => {
				ViewModelInstance.MaxAge = (int)homeAgeSlider.RightValue;
			};
			this.view_rangeSliderHomeAge.AddSubview (homeAgeSlider);
		}

		public class HomeDetailItemsTableViewSource : MvxTableViewSource
		{
			private HomeDetailModalView masterView;
			public HomeDetailItemsTableViewSource (HomeDetailModalView _masterView, UITableView tv) : base (tv)
			{
				masterView = _masterView;
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60.0f;
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				HomeDetailItemCell cell = (HomeDetailItemCell)tableView.DequeueReusableCell (HomeDetailItemCell.Identifier);
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
