// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using ThisRoofN.ViewModels;
using CoreLocation;
using MapKit;
using ThisRoofN.Models.App;
using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using MvvmCross.Binding.BindingContext;

namespace ThisRoofN.iOS
{
	public partial class SearchResultMapView : BaseViewController, IMKMapViewDelegate
	{
		public SearchResultMapViewModel ViewModelInstance
		{
			get {
				return this.ViewModel as SearchResultMapViewModel;
			}
		}

		public SearchResultMapView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var bindingSet = this.CreateBindingSet<SearchResultMapView, SearchResultMapViewModel> ();
			bindingSet.Bind (loadingView).For(i => i.Hidden).To (vm => vm.IsHideLoading);
			bindingSet.Bind (loadingLabel).To (vm => vm.LoadingText);
			bindingSet.Apply ();

			map_results.Delegate = this;
			map_results.MapType = MKMapType.Standard;
			map_results.ZoomEnabled = true;
			map_results.ScrollEnabled = true;
			seg_mapMode.SelectedSegment = 1;
			seg_mapMode.ValueChanged += (object sender, EventArgs e) => {
				switch(seg_mapMode.SelectedSegment) {
				case 0:
					map_results.MapType = MKMapType.Satellite;
					break;
				case 1:
					map_results.MapType = MKMapType.Standard;
					break;
				case 2:
					map_results.MapType = MKMapType.Hybrid;
					break;
				}
			};

			AddAnnotation ();
			AddPolygonOverlay ();
		}

		private void AddPolygonOverlay() {
			List<CLLocationCoordinate2D> coords = new List<CLLocationCoordinate2D>();
			if(ViewModelInstance.MapRange != null && ViewModelInstance.MapRange.Count > 0) {
				foreach (IPosition position in ViewModelInstance.MapRange) {
					GeographicPosition posItem = position as GeographicPosition;
					coords.Add (new CLLocationCoordinate2D (posItem.Latitude, posItem.Longitude));
				}
			}

			MKPolygon polygon = MKPolygon.FromCoordinates (coords.ToArray ());
			map_results.AddOverlay (polygon);
//			map_results.SetVisibleMapRect (polygon.BoundingMapRect, new UIEdgeInsets (-10, -10, -10, -10), true);
		}

		private void AddAnnotation() {
			CLLocationCoordinate2D topLeftCoord;
			topLeftCoord.Latitude = -90;
			topLeftCoord.Longitude = 180;

			CLLocationCoordinate2D bottomRightCoord;
			bottomRightCoord.Latitude = 90;
			bottomRightCoord.Longitude = -180;

			if (ViewModelInstance.MapItems != null && ViewModelInstance.MapItems.Count > 0) {
				foreach (TRCottageSimple item in ViewModelInstance.MapItems) {
					TRMapAnnotation annotation = new TRMapAnnotation (item);
					topLeftCoord.Longitude = Math.Min (topLeftCoord.Longitude, annotation.Coordinate.Longitude);
					topLeftCoord.Latitude = Math.Max (topLeftCoord.Latitude, annotation.Coordinate.Latitude);

					bottomRightCoord.Longitude = Math.Max (bottomRightCoord.Longitude, annotation.Coordinate.Longitude);
					bottomRightCoord.Latitude = Math.Min (bottomRightCoord.Latitude, annotation.Coordinate.Latitude);
					map_results.AddAnnotation (annotation);
				}

				MKCoordinateRegion region;
				region.Center.Latitude = topLeftCoord.Latitude - (topLeftCoord.Latitude - bottomRightCoord.Latitude) * 0.5;
				region.Center.Longitude = topLeftCoord.Longitude + (bottomRightCoord.Longitude - topLeftCoord.Longitude) * 0.5;
				region.Span.LatitudeDelta = Math.Abs (topLeftCoord.Latitude - bottomRightCoord.Latitude) * 1.1;
				region.Span.LongitudeDelta = Math.Abs (bottomRightCoord.Longitude - topLeftCoord.Longitude) * 1.1;
				region = map_results.RegionThatFits (region);
				map_results.SetRegion (region, true);
			}
		}

		[Export ("mapView:viewForAnnotation:")]
		public MapKit.MKAnnotationView GetViewForAnnotation (MapKit.MKMapView mapView, MapKit.IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = mapView.DequeueReusableAnnotation (TRMapAnnotation.Identifier);

			if (annotationView == null) {
				annotationView = new MKPinAnnotationView (annotation, TRMapAnnotation.Identifier);
			} else {
				annotationView.Annotation = annotation;
			}

			annotationView.CanShowCallout = true;
			((MKPinAnnotationView)annotationView).AnimatesDrop = true;
			((MKPinAnnotationView)annotationView).PinColor = MKPinAnnotationColor.Green;
			annotationView.Selected = true;

			UIButton detailButton = UIButton.FromType (UIButtonType.DetailDisclosure);
			detailButton.TouchUpInside += (object sender, EventArgs e) => {
				ViewModelInstance.DetailCommand.Execute(annotation.GetTitle());
			};

			annotationView.RightCalloutAccessoryView = detailButton;


			return annotationView;
		}

		[Export ("mapView:viewForOverlay:")]
		public MapKit.MKOverlayView GetViewForOverlay (MapKit.MKMapView mapView, MapKit.IMKOverlay overlay)
		{
			MKPolygon polygon = overlay as MKPolygon;
			MKPolygonView polygonView = new MKPolygonView (polygon);

			polygonView.LineWidth = 10;
			polygonView.FillColor = UIColor.FromRGB (0, 162, 252).ColorWithAlpha (0.5f);
			polygonView.StrokeColor = UIColor.FromRGB (0, 253, 2);

			return polygonView;
		}

		public class TRMapAnnotation : MKAnnotation
		{
			public static string Identifier = "TRMapAnnotation";
			private TRCottageSimple item;
			public TRMapAnnotation(TRCottageSimple itemData)
			{
				item = itemData;
			}

			public override CLLocationCoordinate2D Coordinate {
				get {
					return new CLLocationCoordinate2D (item.Latitude, item.Longitude);
				}
			}

			public override string Title {
				get {
					return item.CottageID;
				}
			}
		}
	}
}
