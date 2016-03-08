using System;

namespace ThisRoofN.Interfaces
{
	public interface IUserPreference
	{
		string GetValue (string key, string defaultValue = "");

		int GetValue(string key, int defaultValue);

		bool GetValue (string key, bool defaultValue);

		void SetValue (string key, string value);

		void SetValue(string key, int value);

		void SetValue (string key, bool value);

		void RemovePreference( string key );
	}
}

