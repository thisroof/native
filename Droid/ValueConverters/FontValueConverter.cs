using System;
using MvvmCross.Platform.Converters;
using Android.Graphics;
using Android.App;
using MvvmCross.Platform;

namespace ThisRoofN.Droid.ValueConverter
{
	public class FontValueConverter:MvxValueConverter<string, Typeface>
	{
		protected override Typeface Convert(string fontName, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Typeface formattedString = GetTypeface(fontName);
			return formattedString;
		}

		private Typeface GetTypeface(string fontName)
		{
			Typeface tf;
			try
			{
				string fontPath = "Fonts/Helvetica_" + fontName + ".otf";
				tf = Typeface.CreateFromAsset(Application.Context.Assets, fontPath);
			}
			catch (Exception e)
			{
				Mvx.Trace ("Could not get Typeface: {0} Error: {1}", fontName, e);
				return Typeface.Default;
			}

			if (tf == null) 
				return Typeface.Default; 

			return tf;
		}
	}
}

