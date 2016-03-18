// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using MvvmCross.Binding.iOS.Views;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ThisRoofN.iOS
{
	[Register ("SRTileImageCell")]
	partial class SRTileImageCell
	{
		[Outlet]
		MvxImageView img_item { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (img_item != null) {
				img_item.Dispose ();
				img_item = null;
			}
		}
	}
}
