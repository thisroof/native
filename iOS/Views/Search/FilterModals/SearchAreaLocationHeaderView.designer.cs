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
	[Register ("SearchAreaLocationHeaderView")]
	partial class SearchAreaLocationHeaderView
	{
		[Outlet]
		UIKit.UIButton btn_clearAll { get; set; }

		[Outlet]
		UIKit.UIButton btn_selectAll { get; set; }

		[Outlet]
		UIKit.UISwitch switch_city { get; set; }

		[Outlet]
		UIKit.UISwitch switch_metro { get; set; }

		[Outlet]
		UIKit.UISwitch switch_rural { get; set; }

		[Outlet]
		UIKit.UISwitch switch_suburb { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (switch_city != null) {
				switch_city.Dispose ();
				switch_city = null;
			}

			if (switch_suburb != null) {
				switch_suburb.Dispose ();
				switch_suburb = null;
			}

			if (switch_metro != null) {
				switch_metro.Dispose ();
				switch_metro = null;
			}

			if (switch_rural != null) {
				switch_rural.Dispose ();
				switch_rural = null;
			}

			if (btn_selectAll != null) {
				btn_selectAll.Dispose ();
				btn_selectAll = null;
			}

			if (btn_clearAll != null) {
				btn_clearAll.Dispose ();
				btn_clearAll = null;
			}
		}
	}
}
