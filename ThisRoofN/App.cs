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
		public override void Initialize ()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			RegisterAppStart<HomeViewModel> ();

			Mvx.RegisterSingleton<IUserDialogs> (() => UserDialogs.Instance);
		}
	}
}

