using System;

namespace ThisRoofN.Interfaces
{
	public interface IDevice
	{
		string Platform { get; }
		string GetUniqueIdentifier();
		string GetOSVersion ();
		string GetGoogleMapsApiKey ();
		bool IsNetworkConnected ();
		bool IsGpsEnabled ();
	}
}

