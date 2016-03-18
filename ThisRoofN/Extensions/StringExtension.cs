using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace ThisRoofN.Extensions
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

		public static string GetPropertyTypeValue(this string selectedValue)
		{
			selectedValue = selectedValue ?? TRConstant.SearchPropertyTypes [0];

			var array = selectedValue.Split (',');
			List<string> newList = new List<string>();

			foreach (var text in array) {
				switch (text) {
				case "Single Family":
					newList.Add ("Single Family Detached");
					break;
				case "Townhouse":
					newList.Add ("Single Family Attached");
					newList.Add ("Townhouse");
					break;
				case "Condo":
					newList.Add ("Condominium");
					break;
				case "Duplex":
					newList.Add ("Duplex");
					newList.Add ("Quadruplex");
					break;
				case "Manufactured":
					newList.Add ("Manufactured Home");
					newList.Add ("Mobile Home");
					break;
				case "Lots/Land":
					newList.Add ("Farm");
					break;
				case "Timeshare":
					newList.Add ("Timeshare");
					break;
				}
			}
			return string.Join (",", newList);
		}
	}
}

