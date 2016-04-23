using System;
using ThisRoofN.ViewModels;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace ThisRoofN
{
	public class InAreaModalViewModel : BaseViewModel
	{
		private List<CheckboxItemModel> _items;
		public InAreaModalViewModel ()
		{
			InitItems ();
		}

		private void InitItems ()
		{
			_items = new List<CheckboxItemModel> ();

			foreach (var item in TRConstant.InAreaItems) {
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

		public List<CheckboxItemModel> Items {
			get {
				return _items;
			}
		}

		private MvxCommand<CheckboxItemModel> _itemClickCommand;

		public ICommand ItemClickCommand {
			get {
				_itemClickCommand = _itemClickCommand ?? new MvxCommand<CheckboxItemModel> (DoItemSelect);
				return _itemClickCommand;
			}
		}

		private void DoItemSelect(CheckboxItemModel item) {
			item.Selected = !item.Selected;
		}
	}
}

