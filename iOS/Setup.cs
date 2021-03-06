﻿using System;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;

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

		protected override void InitializeLastChance ()
		{
			base.InitializeLastChance ();
		}
	}
}

