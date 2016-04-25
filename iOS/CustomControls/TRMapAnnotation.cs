using System;
using MapKit;
using CoreLocation;

namespace ThisRoofN.iOS
{
	public class TRMapAnnotation : MKAnnotation
	{
		public static string Identifier = "TRMapAnnotation";

		private string propertyID;
		private string title;
		private string price;
		private CLLocationCoordinate2D propertyLocation;

		public TRMapAnnotation (string _propertyID, string _title, string _price, CLLocationCoordinate2D _propertyLocation)
		{
			this.propertyID = _propertyID;
			this.title = _title;
			this.price = _price;
			this.propertyLocation = _propertyLocation;
		}

		public override CLLocationCoordinate2D Coordinate {
			get {
				return propertyLocation;
			}
		}

		public override string Title {
			get {
				return price;
			}
		}

		public override string Subtitle {
			get {
				return title;
			}
		}

		public string PropertyID {
			get {
				return propertyID;
			}
		}
	}
}

