// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using RangeSlider;
using ThisRoofN.ViewModels;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;

namespace ThisRoofN.iOS
{
	public partial class SearchView : BaseViewController
	{
		private TRMovingLabelRangeSlider mPriceRangeSlider;

		public SearchView (IntPtr handle) : base (handle)
		{
		}

		public SearchViewModel ViewModelInstance {
			get {
				return (SearchViewModel)base.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SetupNavigationBar ();

			InitRangeSlider ();

			btn_lifeStyle.TitleLabel.Lines = 0;
			btn_homeDetails.TitleLabel.Lines = 0;
			btn_viewResult.Layer.BorderWidth = 1.0f;
			btn_viewResult.Layer.BorderColor = UIColor.LightGray.CGColor;
			btn_viewResult.Layer.CornerRadius = 0.4f;

			var bindingSet = this.CreateBindingSet<SearchView, SearchViewModel> ();
			bindingSet.Bind(mPriceRangeSlider.minLabel).To (vm => vm.MinBudgetString);
			bindingSet.Bind(mPriceRangeSlider.maxLabel).To (vm => vm.MaxBudgetString);
			bindingSet.Bind(mPriceRangeSlider.rangeSlider).For ("LeftValueChange").To (vm => vm.MinBudget);
			bindingSet.Bind(mPriceRangeSlider.rangeSlider).For ("RightValueChange").To (vm => vm.MaxBudget);
			bindingSet.Bind (backButton).To (vm => vm.CloseCommand);
			bindingSet.Bind (settingButton).To (vm => vm.SettingCommand);
			bindingSet.Bind (btn_searchArea).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.SearchArea);
			bindingSet.Bind (btn_inHome).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.InHome);
			bindingSet.Bind (btn_inArea).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.InArea);
			bindingSet.Bind (btn_location).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.Location);
			bindingSet.Bind (btn_architecture).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.Architecture);
			bindingSet.Bind (btn_lifeStyle).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.Lifestyle);
			bindingSet.Bind (btn_homeStructure).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.HomeStructure);
			bindingSet.Bind (btn_homeDetails).To (vm => vm.GotoModalCommand).CommandParameter (ThisRoofN.ViewModels.SearchViewModel.ModalType.HomeDetails);
			bindingSet.Apply ();

			btn_searchArea.TouchUpInside += (object sender, EventArgs e) => {
				Console.WriteLine ("asdf");
			};

			UIComboBox soryByComboBox = new UIComboBox (ViewModelInstance.SortTypes, 0, txt_sortBy, 
				                            (index, text) => {
					ViewModelInstance.SelectedSortType = text;
				});
		}

		private void InitRangeSlider ()
		{
			mPriceRangeSlider = new TRMovingLabelRangeSlider (
				new CGRect (0, 0, this.view_rangeSlider.Frame.Width, this.view_rangeSlider.Frame.Height), 
				0, 
				TRConstant.PriceStringValues.Count - 1);

			this.view_rangeSlider.AddSubview (mPriceRangeSlider);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (false, true);
			this.NavigationController.SetToolbarHidden (true, true);
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			if (mPriceRangeSlider != null) {
				mPriceRangeSlider.Frame = new CGRect (0, 0, this.view_rangeSlider.Frame.Width, this.view_rangeSlider.Frame.Height);
			}
		}
	}
}
