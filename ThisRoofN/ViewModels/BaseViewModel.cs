using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.RestService;

namespace ThisRoofN.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		protected TRService m_TRService;
		private MvxCommand _closeCommand;


		public BaseViewModel ()
		{
			m_TRService = new TRService ();
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

