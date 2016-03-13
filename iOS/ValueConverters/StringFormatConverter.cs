using System;
using MvvmCross.Platform.Converters;

namespace ThisRoofN.iOS
{
	public class StringFormatConverter:MvxValueConverter<string, string>
	{
		protected override string Convert (string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) {
				return null;
			}

			if (parameter == null) {
				return value;
			}

			return string.Format (parameter.ToString (), value);
		}
	}
}

