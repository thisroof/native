using System;
using System.Collections.Specialized;
using System.Windows.Input;
using UIKit;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Platform.IoC;

namespace ThisRoofN.iOS
{
	// This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
	// are preserved in the deployed app
	[Foundation.Preserve(AllMembers = true)]
	public class LinkerPleaseInclude
	{
		public void Include(UIButton uiButton)
		{
			uiButton.TouchUpInside += (s, e) =>
				uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
		}

		public void Include(UIBarButtonItem barButton)
		{
			barButton.Clicked += (s, e) =>
				barButton.Title = barButton.Title + "";
		}

		public void Include(UITextField textField)
		{
			textField.Text = textField.Text + "";
			textField.EditingChanged += (sender, args) => { textField.Text = ""; };
		}

		public void Include(UITextView textView)
		{
			textView.Text = textView.Text + "";
			textView.Changed += (sender, args) => { textView.Text = ""; };
		}

		public void Include(UILabel label)
		{
			label.Text = label.Text + "";
			NSMutableAttributedString mAString = new NSMutableAttributedString ();
			mAString.Append (label.AttributedText);
			label.AttributedText = mAString;
		}

		public void Include(UIImageView imageView)
		{
			imageView.Image = new UIImage(imageView.Image.CGImage);
		}

		public void Include(MvxImageView imageView)
		{
			imageView.ImageUrl = imageView.ImageUrl + "";
		}

//		public void Include(UIDatePicker date)
//		{
//			date.Date = date.Date.AddSeconds(1);
//			date.ValueChanged += (sender, args) => { date.Date = RLDateTimeHelper.DateTimeToNSDate(DateTime.MaxValue); };
//		}

		public void Include(UISlider slider)
		{
			slider.Value = slider.Value + 1;
			slider.ValueChanged += (sender, args) => { slider.Value = 1; };
		}

		public void Include(UIProgressView progress)
		{
			progress.Progress = progress.Progress + 1;
		}

		public void Include(UISwitch sw)
		{
			sw.On = !sw.On;
			sw.ValueChanged += (sender, args) => { sw.On = false; };
		}

		public void Include(INotifyCollectionChanged changed)
		{
			changed.CollectionChanged += (s,e) => { string.Format("{0}{1}{2}{3}{4}", e.Action,e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex); } ;
		}

		public void Include(ICommand command)
		{
			command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
		}

		public void Include(MvxPropertyInjector injector)
		{
			injector = new MvxPropertyInjector ();
		} 

		public void Include(System.ComponentModel.INotifyPropertyChanged changed)
		{
			changed.PropertyChanged += (sender, e) =>  {
				var test = e.PropertyName;
			};
		}
	}
}