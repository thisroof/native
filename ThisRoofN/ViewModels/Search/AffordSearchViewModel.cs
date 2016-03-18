using System;
using ThisRoofN.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.Models.Service;
using Acr.UserDialogs;

namespace ThisRoofN.ViewModels
{
	public class AffordSearchViewModel : BaseViewModel
	{
		private double _annualIncome;
		private double _monthlyDebts;
		private double _downPayment;
		private int _loanTerm;
		private bool _includeTax;
		private double _propertyTax;
		private int _insurance;
		private int _hoaDues;
		private MvxCommand _calcCommand;

		public AffordSearchViewModel ()
		{
			LoanTerm = 3;
			_propertyTax = 1.0;
		}

		public string AnnualIncome
		{
			get {
				if (this._annualIncome == 0) {
					return string.Empty;
				}

				return this._annualIncome.ToString ("#,##0");
			} 
			set {
				double dValue;
				double.TryParse (value, out dValue);
				this._annualIncome = dValue;
				RaisePropertyChanged (() => AnnualIncome);
			}
		}

		public string MonthlyDebts
		{
			get {
				if (this._monthlyDebts == 0) {
					return string.Empty;
				}

				return this._monthlyDebts.ToString ("#,##0");
			} 
			set {
				double dValue;
				double.TryParse (value, out dValue);
				this._monthlyDebts = dValue;
				RaisePropertyChanged (() => MonthlyDebts);
			}
		}

		public string DownPayment
		{
			get {
				if (this._downPayment == 0) {
					return string.Empty;
				}

				return this._downPayment.ToString ("#,##0");
			} 
			set {
				double dValue;
				double.TryParse (value, out dValue);
				this._downPayment = dValue;
				RaisePropertyChanged (() => DownPayment);
			}
		}

		public int LoanTerm {
			get {
				return _loanTerm;
			} 
			set {
				_loanTerm = value;
				RaisePropertyChanged (() => LoanTerm);
			}
		}

		public bool IncludeTax {
			get {
				return this._includeTax;
			} 
			set {
				this._includeTax = value;
				RaisePropertyChanged (() => IncludeTax);
			}
		}

		public string PropertyTax
		{
			get
			{
				return string.Format("{0:0.000}", _propertyTax);
			}
			set
			{
				double.TryParse(value, out _propertyTax);
				RaisePropertyChanged (() => PropertyTax);
			}
		}
		public string PropertyInsurance
		{
			get
			{
				return string.Format("${0}", _insurance);
			}
			set
			{
				int.TryParse(value.Substring(1), out _insurance);
				RaisePropertyChanged (() => PropertyInsurance);
			}
		}
		public string HoaDues
		{
			get
			{
				return string.Format("${0}", _hoaDues);
			}
			set
			{
				int.TryParse(value.Substring(1), out _hoaDues);
				RaisePropertyChanged (() => HoaDues);
			}
		}

		public ICommand CalcCommand
		{
			get {
				_calcCommand = _calcCommand ?? new MvxCommand (DoCalc);
				return _calcCommand;
			}
		}

		private async void DoCalc()
		{
			if (DoValidate ()) {
				AffordSearchRequest request = new AffordSearchRequest () {
					mobileNum = mDeviceInfo.GetUniqueIdentifier(),
					annualIncome = (int)_annualIncome,
					down = (int)_downPayment,
					monthlyDebts = (int)_monthlyDebts,
					term = TRConstant.LoanTerms[LoanTerm] * 12,
					schedule = "yearly",
					propertyTax = _propertyTax,
					hazard = _insurance.ToString(),
					estimate = _includeTax,
					hoa = _hoaDues
				};

				UserDialogs.Instance.ShowLoading ();
				AffordSearchResponse result = await mTRService.CalcAfford (request);
				UserDialogs.Instance.HideLoading ();

				if (result != null && result.data != null && result.success) {
					ShowViewModel<AffordResultViewModel> (new {amount=result.data.Amount} );
				} else {
					if (result == null) {
						UserDialogs.Instance.Alert ("Request Failed");
					} else {
						UserDialogs.Instance.Alert (result.message);
					}
				}
			}
		}

		private bool DoValidate()
		{
			if (_annualIncome < TRConstant.MinValidIncome || _annualIncome > TRConstant.MaxValidIncome) {
				UserDialogs.Instance.Alert(string.Format("Annual Income value should be {0} - {1}", TRConstant.MinValidIncome, TRConstant.MaxValidIncome));
				return false;
			}

			if (_monthlyDebts < TRConstant.MinValidDebts || _monthlyDebts > TRConstant.MaxValidDebts) {
				UserDialogs.Instance.Alert(string.Format("Monthly Debts value should be {0} - {1}", TRConstant.MinValidDebts, TRConstant.MaxValidDebts));
				return false;
			}

			if (_downPayment < TRConstant.MinValidDownPayment || _downPayment > TRConstant.MaxValidDownPayment) {
				UserDialogs.Instance.Alert(string.Format("Down Payment value should be {0} - {1}", TRConstant.MinValidDownPayment, TRConstant.MaxValidDownPayment));
				return false;
			}

			if (_propertyTax < 1 || _propertyTax > 5)
			{
				UserDialogs.Instance.Alert("Property Tax should be value between 1% and 5%");
				return false;
			}

			if (_insurance < 0 || _insurance > 25000)
			{
				UserDialogs.Instance.Alert("Property Insuarance should be value between 0 and 25000");
				return false;
			}

			if (_hoaDues < 0 || _hoaDues > 10000)
			{
				UserDialogs.Instance.Alert("Hoa Dues should be value between 0 and 10000");
				return false;
			}

			return true;
		}
	}
}

