using System;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace ThisRoofN.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		private MvxCommand _closeCommand;
		public BaseViewModel ()
		{
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

