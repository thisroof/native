using System;
using MvvmCross.Platform.Converters;

namespace ThisRoofN.Droid.ValueConverters
{
	public class DislikeImageValueConverter:MvxValueConverter<bool, int>
	{
		protected override int Convert (bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(value) {
				return Resource.Drawable.icon_dislike_set;
			} else {
				return Resource.Drawable.icon_dislike;
			}
		}
	}
}

