// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using MvvmCross.Binding.iOS.Views;

namespace ThisRoofN.iOS
{
	[Register ("SRDetailImageCell")]
	partial class SRDetailImageCell
	{
		[Outlet]
		MvxImageView img_detailItem { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (img_detailItem != null) {
				img_detailItem.Dispose ();
				img_detailItem = null;
			}
		}
	}
}
