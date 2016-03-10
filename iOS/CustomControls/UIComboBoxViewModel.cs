using System;
using UIKit;
using System.Collections.Generic;

namespace ThisRoofN.iOS
{
	public class UIComboBoxViewModel<TItem> : UIPickerViewModel
	{
		public TItem SelectedItem { get; private set; }
		public int SelectedIndex { get; private set; }

		IList<TItem> _items;
		public IList<TItem> Items {
			get { return _items; }
			set { _items = value; Selected(null, 0, 0); }
		}

		public void SetupSelected(int index) {
			SelectedIndex = index;
			SelectedItem = Items[index];
		}

		public UIComboBoxViewModel() {
		}

		public UIComboBoxViewModel(IList<TItem> items) {
			Items = items;
		}

		public override nint GetRowsInComponent(UIPickerView picker, nint component) {
			return NoItem () ? 1 : Items.Count;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component) {
			return NoItem((int)row) ? "" : GetTitleForItem(Items[(int)row]);
		}

		public override void Selected(UIPickerView picker, nint row, nint component) {
			SelectedIndex = -1;
			if (NoItem((int)row)) SelectedItem = default(TItem);
			else SelectedItem = Items[SelectedIndex=(int)row];

		}

		public override nint GetComponentCount(UIPickerView picker) {
			return 1;
		}

		public virtual string GetTitleForItem(TItem item) {
			if (item == null) {
				return null;
			}
			return item.ToString();
		}

		bool NoItem(int row = 0) {
			return (Items == null) || (row >= Items.Count);
		}
	}
}

