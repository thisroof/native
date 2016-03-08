using System;
using ThisRoofN.Interfaces;
using Security;
using Foundation;
using UIKit;
using CoreLocation;

namespace ThisRoofN.iOS
{
	public class DeviceHelper : IDevice
	{		
		public string GetUniqueIdentifier ()
		{
			var query = new SecRecord(SecKind.GenericPassword);
			query.Service = NSBundle.MainBundle.BundleIdentifier;
			query.Account = "UniqueID";

			NSData uniqueId = SecKeyChain.QueryAsData(query);
			if(uniqueId == null) {
				query.ValueData = NSData.FromString(System.Guid.NewGuid().ToString());
				var err = SecKeyChain.Add (query);
				if (err != SecStatusCode.Success && err != SecStatusCode.DuplicateItem)
					throw new Exception("Cannot store Unique ID");

				return query.ValueData.ToString();
			}
			else {
				return uniqueId.ToString();
			}
		}

		public string Platform
		{
			get{ 
				return "iOS";
			}
		}

		public string GetOSVersion(){
			var device = UIDevice.CurrentDevice;
			return device.SystemVersion;
		}


		public string GetGoogleMapsApiKey ()
		{
			return TRConstant.GMapIosApiKey;
		}

		public bool IsNetworkConnected(){
			var networkStatus = Reachability.InternetConnectionStatus ();
			return networkStatus == NetworkStatus.ReachableViaCarrierDataNetwork || networkStatus == NetworkStatus.ReachableViaWiFiNetwork ;
		}

		public bool IsGpsEnabled(){
			return CLLocationManager.LocationServicesEnabled;
		}
	}
}

