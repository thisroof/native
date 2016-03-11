using System;
using System.Collections;
using System.Linq;
using Foundation;
using MLP;
using UIKit;
using System.Collections.Generic;

namespace ThisRoofN.iOS.CustomControls
{
	[Register("UIAutoCompleteTextField")]
	public class UIAutoCompleteTextField : MLPAutoCompleteTextField
	{
		public IList Elements { get { return _elements; } set { _elements = value; OnElementsChanged(); } }

		public Object SelectedElement { get { return _selectedElement; } set { _selectedElement = value; Text = ElementText(value); } }
		public event EventHandler SelectedElementChanged;

		public bool AllowOpenText { get; set; }

		public delegate string ElementToTextDelegateType(Object element);
		public ElementToTextDelegateType ElementToTextDelegate { get; set; }

		IList _elements;
		AutoCompletionStringObject[] _objectElements;
		Object _selectedElement;

		public UIAutoCompleteTextField() { Initialize(); }
		public UIAutoCompleteTextField(IntPtr handler): base(handler) { Initialize(); }
		public UIAutoCompleteTextField(NSObjectFlag flag): base(flag) { }

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
			Initialize();
		}

		void Initialize() {
			BackgroundColor = UIColor.White;
			AutoCompleteTableBackgroundColor = UIColor.White;
			AutoCompleteTableBorderColor = UIColor.LightGray;
			AutoCompleteTableBorderWidth = 1;
			SortAutoCompleteSuggestionsByClosestMatch = true;
			ShowAutoCompleteTableWhenEditingBegins = true;
			ApplyBoldEffectToAutoCompleteSuggestions = true;
			ReverseAutoCompleteSuggestionsBoldEffect = true;
			MaximumNumberOfAutoCompleteRows = 3;

			this.RegisterAutoCompleteCellClass (new ObjCRuntime.Class(typeof(UIAutoCompleteTextCell)), "AutoCompleteCell");

			ShouldConfigureCell = (MLPAutoCompleteTextField textField, UITableViewCell cell, string autocompleteString, NSAttributedString boldedString, MLPAutoCompletionObject autocompleteObject, NSIndexPath indexPath) => {
				return true;
			};

			PossibleCompletionsForString = (textField, text) => _objectElements;
			DidSelectAutoCompleteString += (sender, e) =>
			{
				Object selectedElement = ((AutoCompletionStringObject)e.SelectedObject).Element;
				SelectElement(selectedElement);
			};
			EditingDidEnd += (sender, e) => {
				bool currentSelectionMatches = ElementText(SelectedElement) == Text;

				if (String.IsNullOrEmpty(Text) || (!currentSelectionMatches && !AllowOpenText))
					SelectElement(null);
			};
		}

		void OnElementsChanged()
		{
			_objectElements = (Elements ?? new List<Object>()).Cast<Object>().Select(o =>
				{
					var elementText = ElementText(o);
					return new AutoCompletionStringObject(elementText, o);
				}).ToArray();
		}

		string ElementText(Object element) {
			if (element == null)
				return null;
			return ElementToTextDelegate != null ? ElementToTextDelegate(element) : element.ToString();
		}

		void SelectElement(Object selectedElement) {
			bool hasChanged = selectedElement != SelectedElement;
			SelectedElement = selectedElement;
			if (hasChanged && SelectedElementChanged != null)
				SelectedElementChanged(this, null);
		}

		public class AutoCompletionStringObject : MLPAutoCompletionObject
		{
			public string Text { get; private set; }
			public object Element { get; private set; }

			public AutoCompletionStringObject(string text, object element)
			{
				Text = text;
				Element = element;
			}

			public override string AutocompleteString { get { return Text; } }
		}
	}
}

