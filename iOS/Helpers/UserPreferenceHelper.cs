using System;
using ThisRoofN.Interfaces;
using Foundation;

namespace ThisRoofN.iOS
{
	public class UserPreferenceHelper : IUserPreference
	{
		public string GetValue(string key, string defaultValue = "")
		{
			string result = NSUserDefaults.StandardUserDefaults.StringForKey (key);
			if (result == null) {
				return defaultValue;
			} else {
				return result;
			}
		}

		public int GetValue(string key, int defaultValue)
		{
			return (int)NSUserDefaults.StandardUserDefaults.IntForKey (key);
		}

		public bool GetValue(string key, bool defaultValue)
		{
			return NSUserDefaults.StandardUserDefaults.BoolForKey (key);
		}

		public void SetValue(string key, string value)
		{
			NSUserDefaults.StandardUserDefaults.SetString( value, key );
		}

		public void SetValue(string key, int value)
		{
			NSUserDefaults.StandardUserDefaults.SetInt( value, key );
		}

		public void SetValue(string key, bool value)
		{
			NSUserDefaults.StandardUserDefaults.SetBool (value, key);
		}

		public void RemovePreference( String key )
		{
			NSUserDefaults.StandardUserDefaults.RemoveObject( key );
		}
	}
}

