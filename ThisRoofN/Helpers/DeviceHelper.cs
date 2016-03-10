using System;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;

namespace ThisRoofN.Helpers
{
	public static class DeviceHelper
	{		
		public static string Platform(){
			IDevice device = Mvx.Resolve<IDevice> ();
			return device.Platform;
		}

		public static string GetUniqueDeviceId (){
			IDevice device = Mvx.Resolve<IDevice> ();
			return device.GetUniqueIdentifier ();
		}

		public static string GetOSVersion(){
			IDevice device = Mvx.Resolve<IDevice> ();
			return device.GetOSVersion();
		}

//		public static bool HasVisitedScreen (string screen){  	
//			TutorialVisitsManager manager = new TutorialVisitsManager ();
//			var screenName = string.Format ("visited_{0}", screen).ToString();
//			return manager.GetTutorialScreenShowed (screenName);
//
//			//			object hasVisited =  null;
//			//			Application.Current.Properties.TryGetValue (string.Format ("visited_{0}", screen).ToString(), out hasVisited);
//			//			if (hasVisited != null) {
//			//				return (bool)hasVisited;
//			//			}
//			//			return false;
//		}

//		public static void ScreenVisited (string screen, bool showFlag = true){
//			TutorialVisitsManager manager = new TutorialVisitsManager ();
//			var screenName = string.Format ("visited_{0}", screen).ToString();
//			manager.SetTutorialScreenShowed (screenName, showFlag);
//			//Application.Current.Properties [string.Format ("visited_{0}", screen)] = true;
//		}

//		public static void SetGooglePlacesApiCall (){
//			string propName = string.Format("GooglePlacesApiCalls_{0}", DateTime.Now.ToString("dd/MM/yyy"));
//			object calls =  null;
//			Application.Current.Properties.TryGetValue (propName, out calls);
//			if (calls != null) {
//				Application.Current.Properties [propName] = (int)calls + 1;
//			} else {
//				Application.Current.Properties [propName] = 1;
//			}		
//		}

//		public static int GetGooglePlacesApiCalls (DateTime date){
//			string propName = string.Format("GooglePlacesApiCalls_{0}", date.ToString("dd/MM/yyy"));
//			object calls =  null;
//			Application.Current.Properties.TryGetValue (propName, out calls);
//			if (calls != null) {
//				return (int)calls;
//			} else {
//				return 0;
//			}		
//		}

		public static string GetGoogleMapsApiKey(){
			IDevice device = Mvx.Resolve<IDevice> ();
			return device.GetGoogleMapsApiKey();
		}

//		public static bool HasResultLoaded (string screen){
//
//			object hasVisited =  null;
//			Application.Current.Properties.TryGetValue (string.Format ("loaded_{0}", screen).ToString(), out hasVisited);
//			if (hasVisited != null) {
//				return (bool)hasVisited;
//			}
//			return false;
//		}

//		public static void ResultLoaded (string screen, bool loaded){
//			Application.Current.Properties [string.Format ("loaded_{0}", screen)] = loaded;
//		}

		public static bool IsNetworkConnected(){
			IDevice device = Mvx.Resolve<IDevice> ();
			return device.IsNetworkConnected();
		}

		public static bool IsGpsEnabled(){
			IDevice device = Mvx.Resolve<IDevice> ();
			return device.IsGpsEnabled();
		}
	}
}

