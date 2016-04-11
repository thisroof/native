using System;
using MvvmCross.Platform.Converters;

namespace ThisRoofN.Droid.ValueConverters
{
	public class CheckImageValueConverter:MvxValueConverter<bool, int>
	{
		protected override int Convert (bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(value) {
				return Resource.Drawable.icon_checkbox_tick;
			} else {
				return Resource.Drawable.icon_checkbox_emp;
			}
		}
	}
}

