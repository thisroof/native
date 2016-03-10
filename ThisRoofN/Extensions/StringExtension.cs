using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace ThisRoofN
{
	public static class StringExtension
	{
		private static readonly Regex s_emailRegex =
			new Regex(
				@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z" );

		public static Boolean IsValidEmail( this String email )
		{
			if(email == null)
			{
				return false;
			}

			if(Regex.Match( email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ).Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static string GetKString(this double value)
		{
			if (value / 1000000 < 1)
			{
				return string.Format("${0}K", (int)value / 1000);
			}
			else
			{
				return string.Format("${0:0.0}M", value / 1000000);
			}
		}

		public static string GetDurationString(this int value)
		{
			if (value == 0)
				return "Any";
			int hrs = (int) value / 60;
			int mins = value - hrs * 60;
			return (hrs != 0 ? string.Format ("{0} hr ", hrs) : "") + (mins != 0 ? string.Format ("{0} min ", mins) : "");
		}

		public static string GetDistanceString(this int value)
		{
			if (value == 0)
				return "Any";
			return string.Format ("{0} mile", value);
		}


		public static string ExtractNumber(this string original)
		{
			return new string(original.ToCharArray().Where(c => Char.IsDigit(c)).ToArray());
		}
	}
}

