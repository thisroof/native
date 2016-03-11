using System;
using UIKit;
using Foundation;

namespace ThisRoofN.iOS
{
	public class UIAutoCompleteTextCell : UITableViewCell
	{
		public UIAutoCompleteTextCell ()
		{
			Initialize ();
		}

		public UIAutoCompleteTextCell(IntPtr handler): base(handler) { Initialize(); }
		public UIAutoCompleteTextCell(NSObjectFlag flag): base(flag) { }

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			Initialize ();
		}

		private void Initialize()
		{
			this.BackgroundColor = UIColor.LightGray;
			this.TextLabel.TextColor = UIColor.White;
		}
	}
}

