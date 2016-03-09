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
	[Register ("SearchPropertyTypeCell")]
	partial class SearchPropertyTypeCell
	{
		[Outlet]
		UIKit.UIView cell_viewTitle { get; set; }

		[Outlet]
		UIKit.UICollectionView cv_propertyTypes { get; set; }

		[Outlet]
		UIKit.UIImageView img_expandMarker { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (cell_viewTitle != null) {
				cell_viewTitle.Dispose ();
				cell_viewTitle = null;
			}

			if (img_expandMarker != null) {
				img_expandMarker.Dispose ();
				img_expandMarker = null;
			}

			if (cv_propertyTypes != null) {
				cv_propertyTypes.Dispose ();
				cv_propertyTypes = null;
			}
		}
	}
}
