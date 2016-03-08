using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.RestService;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;

namespace ThisRoofN.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		protected TRService m_TRService;
		protected IUserPreference userPref;
		private MvxCommand _closeCommand;


		public BaseViewModel ()
		{
			m_TRService = new TRService ();
			userPref = Mvx.Resolve<IUserPreference> ();
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

