using System;
using ThisRoofN.Interfaces;
using Android.App;
using Android.Content;

namespace ThisRoofN.Droid.Helpers
{
	public class UserPreferenceHelper : IUserPreference
	{
		private const string PREF_KEY = "ThisRoofSharedPref";

		public string GetValue(string key, string defaultValue = "")
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			return prefs.GetString( key, defaultValue );
		}

		public int GetValue(string key, int defaultValue)
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			return prefs.GetInt( key, defaultValue );
		}

		public bool GetValue(string key, bool defaultValue)
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			return prefs.GetBoolean( key, defaultValue );
		}

		public void SetValue(string key, string value)
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			var prefsEditor = prefs.Edit();

			prefsEditor.PutString( key, value );
			prefsEditor.Commit();
		}

		public void SetValue(string key, int value)
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			var prefsEditor = prefs.Edit();

			prefsEditor.PutInt( key, value );
			prefsEditor.Commit();
		}

		public void SetValue(string key, bool value)
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			var prefsEditor = prefs.Edit();

			prefsEditor.PutBoolean( key, value );
			prefsEditor.Commit();
		}

		public void RemovePreference( String key )
		{
			var prefs = Application.Context.GetSharedPreferences( PREF_KEY, FileCreationMode.Private );
			var prefsEditor = prefs.Edit();

			prefsEditor.Remove( key );
			prefsEditor.Commit();
		}
	}
}

