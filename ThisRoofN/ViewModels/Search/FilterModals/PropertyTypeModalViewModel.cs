using System;
using ThisRoofN.ViewModels;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.Helpers;

namespace ThisRoofN
{
	public class PropertyTypeModalViewModel : BaseViewModel
	{
		private List<CheckboxItemModel> _items;
		public PropertyTypeModalViewModel ()
		{
			InitItems ();
		}

		private void InitItems ()
		{
			_items = new List<CheckboxItemModel> ();

			List<string> propertyTypesList = new List<string> ();
			if (!string.IsNullOrEmpty (DataHelper.CurrentSearchFilter.PropertyTypes)) {
				if (DataHelper.CurrentSearchFilter.PropertyTypes.Contains (",")) {
					propertyTypesList.AddRange (DataHelper.CurrentSearchFilter.PropertyTypes.Split (','));
				} else {
					propertyTypesList.Add (DataHelper.CurrentSearchFilter.PropertyTypes);
				}
			}

			foreach (var item in TRConstant.SearchPropertyTypes) {
				_items.Add (new CheckboxItemModel {
					Title = item,
					Selected = propertyTypesList.Contains(item)
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

			DataHelper.CurrentSearchFilter.PropertyTypes = GetSelectedCheckboxItems (Items);

			Close (this);
		}

		public List<CheckboxItemModel> Items {
			get {
				return _items;
			}
		}
	}
}

