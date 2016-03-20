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
			_squareFootage = 2;
			_lotSize = 2;

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

		private int _squareFootage;
		public int SquareFootage {
			get {
				return _squareFootage;
			} set {
				_squareFootage = value;
				RaisePropertyChanged (() => SquareFootage);
				RaisePropertyChanged (() => SquareFootageString);
			}
		}

		public string SquareFootageString {
			get {
				return TRConstant.MaxSquareFeetOptions [SquareFootage];
			}
		}

		private int _lotSize;
		public int LotSize {
			get {
				return _lotSize;
			} set {
				_lotSize = value;
				RaisePropertyChanged (() => LotSize);
				RaisePropertyChanged (() => LotSizeString);
			}
		}

		public string LotSizeString {
			get {
				return TRConstant.MaxLotSizeOptions [LotSize];
			}
		}

		private int _minAge;
		public int MinAge {
			get {
				return _minAge;
			} set {
				_minAge = value;
				RaisePropertyChanged (() => MinAge);
				RaisePropertyChanged (() => AgeString);
			}
		}

		private int _maxAge;
		public int MaxAge {
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
				return string.Format ("{0} - {1}", MinAge, MaxAge);
			}
		}

		public List<CheckboxItemModel> Items {
			get {
				return _items;
			}
		}
	}
}

