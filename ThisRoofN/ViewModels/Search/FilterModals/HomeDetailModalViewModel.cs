using System;
using ThisRoofN.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using System.Collections.Generic;

namespace ThisRoofN
{
	public class HomeDetailModalViewModel : BaseViewModel
	{
		private List<CheckboxItemModel> _items;
		public HomeDetailModalViewModel ()
		{
			InitItems ();
		}

		private void InitItems ()
		{
			_minAge = TRConstant.MinHomeAge;
			_maxAge = TRConstant.MaxHomeAge;

			_minSqareFootage = 0;
			_maxSquareFootage = TRConstant.SquareFootageOptions.Count - 1;

			_minLotSize = 0;
			_maxLotSize = TRConstant.LotSizeOptions.Count - 2;

			_minAge = 2;
			_maxAge = TRConstant.HomeAgeOptions.Count - 1;

			_items = new List<CheckboxItemModel> ();
			foreach (var item in TRConstant.HomeDetailItems) {
				_items.Add (new CheckboxItemModel {
					Title = item,
					Selected = false
				});
			}
		}

		private MvxCommand _modalCloseCommand;

		public ICommand ModalCloseCommand {
			get {
				_modalCloseCommand = _modalCloseCommand ?? new MvxCommand (() => {
					DoSaveAndClose ();
				});
				return _modalCloseCommand;
			}
		}

		private void DoSaveAndClose ()
		{
			// Save edited values

			Close (this);
		}

		private float _minSqareFootage;
		public float MinSquareFootage {
			get {
				return _minSqareFootage;
			} set {
				_minSqareFootage = value;
				RaisePropertyChanged (() => MinSquareFootage);
				RaisePropertyChanged (() => SquareFootageString);
			}
		}

		private float _maxSquareFootage;
		public float MaxSquareFootage {
			get {
				return _maxSquareFootage;
			} set {
				_maxSquareFootage = value;
				RaisePropertyChanged (() => MaxSquareFootage);
				RaisePropertyChanged (() => SquareFootageString);
			}
		}

		public string SquareFootageString {
			get {
				return string.Format("{0} to {1}", TRConstant.SquareFootageOptions[(int)MinSquareFootage], TRConstant.SquareFootageOptions[(int)MaxSquareFootage]);
			}
		}

		private float _minLotSize;
		public float MinLotSize {
			get {
				return _minLotSize;
			} set {
				_minLotSize = value;
				RaisePropertyChanged (() => MinLotSize);
				RaisePropertyChanged (() => LotSizeString);
			}
		}

		private float _maxLotSize;
		public float MaxLotSize {
			get {
				return _maxLotSize;
			} set {
				_maxLotSize = value;
				RaisePropertyChanged (() => MaxLotSize);
				RaisePropertyChanged (() => LotSizeString);
			}
		}

		public string LotSizeString {
			get {
				return string.Format("{0} to {1}", TRConstant.LotSizeOptions[(int)MinLotSize], TRConstant.LotSizeOptions[(int)MaxLotSize]);
			}
		}

		private float _minAge;
		public float MinAge {
			get {
				return _minAge;
			} set {
				_minAge = value;
				RaisePropertyChanged (() => MinAge);
				RaisePropertyChanged (() => AgeString);
			}
		}

		private float _maxAge;
		public float MaxAge {
			get {
				return _maxAge;
			} set {
				_maxAge = value;
				RaisePropertyChanged (() => MaxAge);
				RaisePropertyChanged (() => AgeString);
			}
		}

		public string AgeString {
			get {
				return string.Format ("{0} to {1}", TRConstant.HomeAgeOptions[(int)MinAge], TRConstant.HomeAgeOptions[(int)MaxAge]);
			}
		}

		public List<CheckboxItemModel> Items {
			get {
				return _items;
			}
		}
	}
}

