using System;
using MvvmCross.Platform.Converters;
using UIKit;

namespace ThisRoofN.iOS.ValueConverters
{
	public class CheckmarkConverter:MvxValueConverter<bool, UIImage>
	{
		protected override UIImage Convert (bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(value) {
				return UIImage.FromBundle ("icon_checkbox_tick");
			} else {
				return UIImage.FromBundle ("icon_checkbox_emp");
			}
		}
	}
}

