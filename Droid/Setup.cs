using System;
using MvvmCross.Droid.Platform;
using Android.Content;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Platform;
using ThisRoofN.Droid.CustomMvxDroid;
using MvvmCross.Droid.Views;
using System.Linq;
using ThisRoofN.Interfaces;
using ThisRoofN.Droid.Helpers;

namespace ThisRoofN.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup (Context applicationContext) : base(applicationContext)
		{
			
		}

		protected override MvvmCross.Core.ViewModels.IMvxApplication CreateApp ()
		{
			return new App ();
		}

		protected override MvvmCross.Droid.Views.IMvxAndroidViewPresenter CreateViewPresenter ()
		{
			var presenter = Mvx.IocConstruct<TRMvxDroidViewPresenter> ();
			Mvx.RegisterSingleton<IMvxAndroidViewPresenter> (presenter);
			return presenter;
		}

		protected override void InitializeIoC ()
		{
			base.InitializeIoC ();
			Mvx.ConstructAndRegisterSingleton<IFragmentTypeLookup, TRMvxFragmentTypeLookup> ();
		}

		protected override MvvmCross.Platform.Platform.IMvxTrace CreateDebugTrace ()
		{
			return new MvxDebugTrace ();
		}

		protected override void FillTargetFactories (MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories (registry);
		}

		protected override IEnumerable<Assembly> AndroidViewAssemblies {
			get {
				List<Assembly> toReturn = base.AndroidViewAssemblies.ToList ();
				toReturn.Add (this.GetType ().Assembly);
				return toReturn;
			}
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

