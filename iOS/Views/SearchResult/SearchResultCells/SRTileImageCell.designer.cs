// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;
using MvvmCross.Binding.iOS.Views;

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
