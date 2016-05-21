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
	[Register ("SearchResultStatesViewCell")]
	partial class SearchResultStatesViewCell
	{
		[Outlet]
		UIKit.UIButton btn_showall { get; set; }

		[Outlet]
		UIKit.UICollectionView cv_results { get; set; }

		[Outlet]
		MvvmCross.Binding.iOS.Views.MvxImageView img_state { get; set; }

		[Outlet]
		UIKit.UILabel lbl_state { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (img_state != null) {
				img_state.Dispose ();
				img_state = null;
			}

			if (lbl_state != null) {
				lbl_state.Dispose ();
				lbl_state = null;
			}

			if (btn_showall != null) {
				btn_showall.Dispose ();
				btn_showall = null;
			}

			if (cv_results != null) {
				cv_results.Dispose ();
				cv_results = null;
			}
		}
	}
}
