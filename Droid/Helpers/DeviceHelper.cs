using System;
using ThisRoofN.Interfaces;
using Android.Provider;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Locations;

namespace ThisRoofN.Droid.Helpers
{
	public class DeviceHelper : IDevice
	{		
		public string Platform
		{
			get{ 
				return "Android";
			}
		}

		public string GetUniqueIdentifier ()
		{
			return Settings.Secure.GetString(Application.Context.ContentResolver, Settings.Secure.AndroidId);
		}

		public string GetOSVersion()
		{
			return Android.OS.Build.VERSION.Release;
		}

		public string GetGoogleMapsApiKey ()
		{
			return TRConstant.GMapAndroidApiKey;
		}

		public bool IsNetworkConnected(){
			var connectivityManager = (ConnectivityManager)Application.Context.ApplicationContext.GetSystemService(Context.ConnectivityService);

			var activeConnection = connectivityManager.ActiveNetworkInfo;

			if ((activeConnection != null) && activeConnection.IsConnected)
			{
				return true;
			}

			return false;
		}

		public bool IsGpsEnabled(){
			var locationManager = (LocationManager)Application.Context.ApplicationContext.GetSystemService(Context.LocationService);
			return locationManager.IsProviderEnabled (LocationManager.GpsProvider);
		}
	}
}

