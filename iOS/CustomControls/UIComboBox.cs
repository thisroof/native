using System;
using UIKit;
using System.Collections.Generic;

namespace ThisRoofN.iOS
{
	public class UIComboBox 
	{
		protected UIPickerView mPickerView;
		protected UIComboBoxViewModel<string> mListPickerViewModel;
		protected UITextField textField;
		protected Action<int, string> Selected;
		public	IList<string> Items { 
			get { 
				return mListPickerViewModel.Items; 
			} 
			set { 
				mListPickerViewModel.Items = value; 
				mPickerView.ReloadAllComponents ();
			} 
		}

		public	int SelectedIndex { get { return (mListPickerViewModel!=null)?mListPickerViewModel.SelectedIndex:-1; } }
		public	string SelectedText { get { return (mListPickerViewModel!=null)?mListPickerViewModel.SelectedItem:""; } }

		public UIComboBox (IList<string> items, int selectedIndex, UITextField aTextField, Action<int, string> selected)  {
			Selected = selected;
			mPickerView = new UIPickerView();
			mListPickerViewModel = new UIComboBoxViewModel<string> ();
			mListPickerViewModel.Items = items;
			mListPickerViewModel.SetupSelected (SelectedIndex);
			mPickerView.ShowSelectionIndicator = true;
			mPickerView.Model = mListPickerViewModel;

			if (selectedIndex < 0)
			{
				aTextField.Text = string.Empty;
			}
			else if(selectedIndex < items.Count)
			{
				aTextField.Text = items[selectedIndex];
			}

			textField = aTextField;
			UIToolbar toolbar = new UIToolbar();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.SizeToFit();
			toolbar.TintColor = UIColor.White;
			toolbar.Items = new UIBarButtonItem[] { 
				new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace, null, null), 
				new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, DoneClicked)
			};
			textField.InputView = mPickerView;
			textField.InputAccessoryView = toolbar;
		}

		private void DoneClicked(object sender, System.EventArgs args) {
			textField.ResignFirstResponder();
			if (Selected == null) return;
			textField.Text = mListPickerViewModel.SelectedItem;
			Selected(mListPickerViewModel.SelectedIndex, mListPickerViewModel.SelectedItem);
		}
	}
}

