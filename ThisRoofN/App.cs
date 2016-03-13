using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using ThisRoofN.ViewModels;
using Acr.UserDialogs;
using MvvmCross.Platform;

namespace ThisRoofN
{
	public class App : MvxApplication
	{
		public static bool ShowComparisons = false; // not sure.. This needs to be arranged

		public override void Initialize ()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			RegisterAppStart<HomeViewModel> ();
//			RegisterAppStart<SearchTypeViewModel> ();

			Mvx.RegisterSingleton<IUserDialogs> (() => UserDialogs.Instance);
		}
	}
}

