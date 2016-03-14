using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.RestService;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;
using System.Collections.Generic;

namespace ThisRoofN.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		protected TRService mTRService;
		protected IUserPreference mUserPref;
		protected IDevice mDeviceInfo;

		private MvxCommand _closeCommand;

		public BaseViewModel ()
		{
			mTRService = new TRService ();
			mUserPref = Mvx.Resolve<IUserPreference> ();
			mDeviceInfo = Mvx.Resolve<IDevice> ();
		}

		public ICommand CloseCommand
		{
			get {
				_closeCommand = _closeCommand ?? new MvxCommand (DoClose);
				return _closeCommand;
			}
		}

		public void DoClose()
		{
			Close (this);
		}
	}
}

