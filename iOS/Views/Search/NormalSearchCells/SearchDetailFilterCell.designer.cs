// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ThisRoofN.iOS
{
	[Register ("SearchDetailFilterCell")]
	partial class SearchDetailFilterCell
	{
		[Outlet]
		UIKit.UIView view_addtionalFilter { get; set; }

		[Outlet]
		UIKit.UIView view_filter4 { get; set; }

		[Outlet]
		UIKit.UIView view_filter6 { get; set; }

		[Outlet]
		UIKit.UIView view_fitler5 { get; set; }

		[Outlet]
		UIKit.UIView view_lifeStyleFilter { get; set; }

		[Outlet]
		UIKit.UIView view_propertyType { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (view_propertyType != null) {
				view_propertyType.Dispose ();
				view_propertyType = null;
			}

			if (view_lifeStyleFilter != null) {
				view_lifeStyleFilter.Dispose ();
				view_lifeStyleFilter = null;
			}

			if (view_addtionalFilter != null) {
				view_addtionalFilter.Dispose ();
				view_addtionalFilter = null;
			}

			if (view_filter4 != null) {
				view_filter4.Dispose ();
				view_filter4 = null;
			}

			if (view_fitler5 != null) {
				view_fitler5.Dispose ();
				view_fitler5 = null;
			}

			if (view_filter6 != null) {
				view_filter6.Dispose ();
				view_filter6 = null;
			}
		}
	}
}
