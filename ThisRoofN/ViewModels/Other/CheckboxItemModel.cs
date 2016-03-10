using System;
using MvvmCross.Core.ViewModels;

namespace ThisRoofN
{
	public class CheckboxItemModel : MvxViewModel
	{
		private string _title;
		public string Title
		{
			get {
				return _title;
			}
			set {
				_title = value;
			}
		}

		private bool _selected;
		public bool Selected
		{
			get {
				return _selected;
			}
			set {
				_selected = value;
				RaisePropertyChanged (() => Selected);
			}
		}
	}
}

