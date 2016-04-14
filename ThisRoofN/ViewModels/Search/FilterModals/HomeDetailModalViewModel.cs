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

			_minAge = 0;
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

		private MvxCommand<CheckboxItemModel> _itemClickCommand;

		public ICommand ItemClickCommand {
			get {
				_itemClickCommand = _itemClickCommand ?? new MvxCommand<CheckboxItemModel> (DoItemSelect);
				return _itemClickCommand;
			}
		}

		private void DoItemSelect (CheckboxItemModel item)
		{
			item.Selected = !item.Selected;
		}

		private void DoSaveAndClose ()
		{
			// Save edited values

			Close (this);
		}

		public int MinSliderValue
		{
			get {
				return 0;
			}
		}


		private int _selectedBed;
		public int SelectedBed {
			get {
				return _selectedBed;
			} 
			set {
				_selectedBed = value;
				RaisePropertyChanged (() => SelectedBed);
				RaisePropertyChanged (() => BedsLabelText);
			}
		}

		public int BedOptionCount {
			get {
				return TRConstant.BedOptions.Count - 1;
			}
		}

		public string BedsLabelText {
			get {
				return TRConstant.BedOptions [SelectedBed];
			}
		}

		private int _selectedBath;
		public int SelectedBath {
			get {
				return _selectedBath;
			} 
			set {
				_selectedBath = value;
				RaisePropertyChanged (() => SelectedBath);
				RaisePropertyChanged (() => BathLabelText);
			}
		}

		public int BathOptionCount {
			get {
				return TRConstant.BathOptions.Count - 1;
			}
		}

		public string BathLabelText {
			get {
				return TRConstant.BathOptions [SelectedBath];
			}
		}

		private float _minSqareFootage;
		public float MinSquareFootage {
			get {
				return _minSqareFootage;
			}
			set {
				_minSqareFootage = value;
				RaisePropertyChanged (() => MinSquareFootageString);
				RaisePropertyChanged (() => MinSquareFootage);
			}
		}

		private float _maxSquareFootage;
		public float MaxSquareFootage {
			get {
				return _maxSquareFootage;
			}
			set {
				_maxSquareFootage = value;
				RaisePropertyChanged (() => MaxSquareFootageString);
				RaisePropertyChanged (() => MaxSquareFootage);
			}
		}

		public int SquareFootageCount {
			get {
				return TRConstant.SquareFootageOptions.Count - 1;
			}
		}

		public string MinSquareFootageString {
			get {
				return TRConstant.SquareFootageOptions [(int)Math.Round (MinSquareFootage, MidpointRounding.AwayFromZero)];
			}
		}

		public string MaxSquareFootageString {
			get {
				return TRConstant.SquareFootageOptions [(int)Math.Round (MaxSquareFootage, MidpointRounding.AwayFromZero)];
			}
		}

		private float _minLotSize;
		public float MinLotSize {
			get {
				return _minLotSize;
			}
			set {
				_minLotSize = value;
				RaisePropertyChanged (() => MinLotSizeString);
				RaisePropertyChanged (() => MinLotSize);
			}
		}

		public string MinLotSizeString {
			get {
				return TRConstant.LotSizeOptions [(int)Math.Round (MinLotSize, MidpointRounding.AwayFromZero)];
			}
		}

		private float _maxLotSize;
		public float MaxLotSize {
			get {
				return _maxLotSize;
			}
			set {
				_maxLotSize = value;
				RaisePropertyChanged (() => MaxLotSizeString);
				RaisePropertyChanged (() => MaxLotSize);
			}
		}

		public int LotSizeCount {
			get {
				return TRConstant.LotSizeOptions.Count - 1;
			}
		}

		public string MaxLotSizeString {
			get {
				return TRConstant.LotSizeOptions [(int)Math.Round (MaxLotSize, MidpointRounding.AwayFromZero)];
			}
		}

		private float _minAge;
		public float MinAge {
			get {
				return _minAge;
			}
			set {
				_minAge = value;
				RaisePropertyChanged (() => MinAgeString);
				RaisePropertyChanged (() => MinAge);

			}
		}

		public string MinAgeString {
			get {
				return TRConstant.HomeAgeOptions [(int)Math.Round (MinAge, MidpointRounding.AwayFromZero)];
			}
		}

		public int HomeAgeCount {
			get {
				return TRConstant.HomeAgeOptions.Count - 1;
			}
		}

		private float _maxAge;
		public float MaxAge {
			get {
				return _maxAge;
			}
			set {
				_maxAge = value;
				RaisePropertyChanged (() => MaxAgeString);
				RaisePropertyChanged (() => MaxAge);
			}
		}

		public string MaxAgeString {
			get {
				return TRConstant.HomeAgeOptions [(int)Math.Round (MaxAge, MidpointRounding.AwayFromZero)];
			}
		}

		public List<CheckboxItemModel> Items {
			get {
				return _items;
			}
		}
	}
}

