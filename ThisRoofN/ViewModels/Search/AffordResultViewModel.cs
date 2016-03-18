using System;
using ThisRoofN.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace ThisRoofN.ViewModels
{
	public class AffordResultViewModel : BaseViewModel
	{
		private MvxCommand _startSearchCommand;
		private MvxCommand _getApprovedCommand;
		private int affordAmount;

		public AffordResultViewModel ()
		{
		}

		public void Init (int amount)
		{
			this.affordAmount = amount;
		}

		public string AffordAmount {
			get {
				return affordAmount.ToString ("$#,##0");
			}
		}

		public ICommand StartSearchCommand
		{
			get {
				_startSearchCommand = _startSearchCommand ?? new MvxCommand (DoStartSearch);
				return _startSearchCommand;
			}
		}

		public ICommand GetApprovedCommand
		{
			get {
				_getApprovedCommand = _getApprovedCommand ?? new MvxCommand (DoGetApprove);
				return _getApprovedCommand;
			}
		}

		public void DoStartSearch()
		{
			ShowViewModel<NormalSearchViewModel> ();
		}

		public void DoGetApprove()
		{
			ShowViewModel<TRWebViewModel> (new {link = TRConstant.PreApprovalLink});
		}

	}
}

