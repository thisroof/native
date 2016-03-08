using System;
using ThisRoofN.ViewModels;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace ThisRoofN.ViewModels
{
	public class SearchTypeViewModel : BaseViewModel
	{
		private MvxCommand _normalSearchCommand;
		private MvxCommand _affordSearchCommand;

		public SearchTypeViewModel ()
		{
		}

		public ICommand NormalSearchCommand
		{
			get {
				_normalSearchCommand = _normalSearchCommand ?? new MvxCommand (DoNormalSearch);
				return _normalSearchCommand;
			}
		}

		public ICommand AffordSearchCommand
		{
			get {
				_affordSearchCommand = _affordSearchCommand ?? new MvxCommand (DoAffordSearch);
				return _affordSearchCommand;
			}
		}

		private void DoNormalSearch()
		{
		}

		private void DoAffordSearch()
		{
		}
	}
}

