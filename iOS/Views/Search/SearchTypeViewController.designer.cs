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
	[Register ("SearchTypeViewController")]
	partial class SearchTypeViewController
	{
		[Outlet]
		UIKit.UIButton btn_affordSearch { get; set; }

		[Outlet]
		UIKit.UIButton btn_normalSearch { get; set; }

		[Outlet]
		UIKit.UIView video_view { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btn_affordSearch != null) {
				btn_affordSearch.Dispose ();
				btn_affordSearch = null;
			}

			if (btn_normalSearch != null) {
				btn_normalSearch.Dispose ();
				btn_normalSearch = null;
			}

			if (video_view != null) {
				video_view.Dispose ();
				video_view = null;
			}
		}
	}
}
