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
	[Register ("SRDetailImageCell")]
	partial class SRDetailImageCell
	{
		[Outlet]
		MvvmCross.Binding.iOS.Views.MvxImageView img_detailItem { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (img_detailItem != null) {
				img_detailItem.Dispose ();
				img_detailItem = null;
			}
		}
	}
}
