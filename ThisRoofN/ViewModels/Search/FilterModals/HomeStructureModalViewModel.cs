﻿using System;
using ThisRoofN.ViewModels;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace ThisRoofN
{
	public class HomeStructureModalViewModel : BaseViewModel
	{
		private List<CheckboxItemModel> _items;
		public HomeStructureModalViewModel ()
		{
			InitItems ();
		}

		private void InitItems ()
		{
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

		public List<CheckboxItemModel> Items {
			get {
				return _items;
			}
		}
	}
}

