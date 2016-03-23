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
		//		private RangeSliderView squareSlider;
		//		private RangeSliderView homeAgeSlider;
		//		private RangeSliderView lotSizeSlider;
		private TRMovingLabelRangeSlider mSquareSlider;
		private TRMovingLabelRangeSlider mHomeAgeSlider;
		private TRMovingLabelRangeSlider mLotSizeSlider;

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

			bindingSet.Bind (mSquareSlider.minLabel).To (vm => vm.MinSquareFootageString);
			bindingSet.Bind (mSquareSlider.maxLabel).To (vm => vm.MaxSquareFootageString);
			bindingSet.Bind (mHomeAgeSlider.minLabel).To (vm => vm.MinAgeString);
			bindingSet.Bind (mHomeAgeSlider.maxLabel).To (vm => vm.MaxAgeString);
			bindingSet.Bind (mLotSizeSlider.minLabel).To (vm => vm.MinLotSizeString);
			bindingSet.Bind (mLotSizeSlider.maxLabel).To (vm => vm.MaxLotSizeString);

			bindingSet.Bind (mSquareSlider.rangeSlider).For ("LeftValueChange").To (vm => vm.MinSquareFootage);
			bindingSet.Bind (mSquareSlider.rangeSlider).For ("RightValueChange").To (vm => vm.MaxSquareFootage);
			bindingSet.Bind (mHomeAgeSlider.rangeSlider).For ("LeftValueChange").To (vm => vm.MinAge);
			bindingSet.Bind (mHomeAgeSlider.rangeSlider).For ("RightValueChange").To (vm => vm.MaxAge);
			bindingSet.Bind (mLotSizeSlider.rangeSlider).For ("LeftValueChange").To (vm => vm.MinLotSize);
			bindingSet.Bind (mLotSizeSlider.rangeSlider).For ("RightValueChange").To (vm => vm.MaxLotSize);

			bindingSet.Apply ();
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			mSquareSlider.Frame = new CGRect (0, 0, this.view_rangeSliderSquare.Frame.Width, this.view_rangeSliderSquare.Frame.Height);
			mHomeAgeSlider.Frame = new CGRect (0, 0, this.view_rangeSliderHomeAge.Frame.Width, this.view_rangeSliderHomeAge.Frame.Height);
			mLotSizeSlider.Frame = new CGRect (0, 0, this.view_rangeSliderLotSize.Frame.Width, this.view_rangeSliderLotSize.Frame.Height);
		}

		private void InitRangeSlider ()
		{
//			squareSlider = new RangeSliderView (new CGRect (0, 0, this.view_rangeSliderSquare.Frame.Width, this.view_rangeSliderSquare.Frame.Height));
//			squareSlider.MinValue = 0;
//			squareSlider.MaxValue = TRConstant.SquareFootageOptions.Count - 1;
//			this.view_rangeSliderSquare.AddSubview (squareSlider);

			mSquareSlider = new TRMovingLabelRangeSlider (
				new CGRect (0, 0, this.view_rangeSliderSquare.Frame.Width, this.view_rangeSliderSquare.Frame.Height), 
				0, 
				TRConstant.SquareFootageOptions.Count - 1);
			this.view_rangeSliderSquare.AddSubview (mSquareSlider);

			mHomeAgeSlider = new TRMovingLabelRangeSlider (
				new CGRect (0, 0, this.view_rangeSliderHomeAge.Frame.Width, this.view_rangeSliderHomeAge.Frame.Height), 
				0, 
				TRConstant.HomeAgeOptions.Count - 1);
			this.view_rangeSliderHomeAge.AddSubview (mHomeAgeSlider);

			mLotSizeSlider = new TRMovingLabelRangeSlider (
				new CGRect (0, 0, this.view_rangeSliderLotSize.Frame.Width, this.view_rangeSliderLotSize.Frame.Height), 
				0, 
				TRConstant.LotSizeOptions.Count - 1);
			this.view_rangeSliderLotSize.AddSubview (mLotSizeSlider);
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
