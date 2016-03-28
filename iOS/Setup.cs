using System;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using ThisRoofN.Interfaces;
using MvvmCross.Binding.Bindings.Target.Construction;
using ThisRoofN.iOS.CustomBinding;
using RangeSlider;
using UIKit;

namespace ThisRoofN.iOS
{
	public class Setup : MvxIosSetup
	{
		protected IMvxApplicationDelegate mAppDelegate;
		protected IMvxIosViewPresenter mPresenter;

		public Setup (IMvxApplicationDelegate appDelegate, IMvxIosViewPresenter presenter) : base(appDelegate, presenter)
		{
			mAppDelegate = appDelegate;
			mPresenter = presenter;
		}

		protected override MvvmCross.Core.ViewModels.IMvxApplication CreateApp ()
		{
			return new App ();
		}

		protected override MvvmCross.Platform.Platform.IMvxTrace CreateDebugTrace ()
		{
			return new MvxDebugTrace ();
		}

		protected override MvvmCross.iOS.Views.IMvxIosViewsContainer CreateIosViewsContainer ()
		{
			return new TRMvxIosViewsContainer ();
		}

		protected override void FillTargetFactories (MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
		{
			registry.RegisterCustomBindingFactory<RangeSliderView> ("LeftValueChange", view => new RangeSliderLeftTargetBinding(view));
			registry.RegisterCustomBindingFactory<RangeSliderView> ("RightValueChange", view => new RangeSliderRightTargetBinding(view));
			registry.RegisterCustomBindingFactory<UISegmentedControl> ("SelectedIndex", view => new UISegmentTargetBinding(view));
			base.FillTargetFactories (registry);
		}

		protected override void InitializeFirstChance ()
		{
			Mvx.RegisterSingleton<IUserPreference>( new UserPreferenceHelper() );
			Mvx.RegisterSingleton<IDevice>( new DeviceHelper() );
			base.InitializeFirstChance ();
		}

		protected override void InitializeLastChance ()
		{
			base.InitializeLastChance ();
		}
	}
}

