// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ThisRoofN.iOS
{
	[Register ("SearchView")]
	partial class SearchView
	{
		[Outlet]
		UIKit.UIButton btn_viewResult { get; set; }

		[Outlet]
		UIKit.UICollectionView cv_search { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_viewResult != null) {
				btn_viewResult.Dispose ();
				btn_viewResult = null;
			}

			if (cv_search != null) {
				cv_search.Dispose ();
				cv_search = null;
			}
		}
	}
}
