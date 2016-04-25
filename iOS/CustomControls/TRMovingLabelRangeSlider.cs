using System;
using UIKit;
using RangeSlider;
using CoreGraphics;
using Foundation;

namespace ThisRoofN.iOS
{
	public class TRMovingLabelRangeSlider : UIView
	{
		public RangeSliderView rangeSlider;
		public UILabel minLabel;
		public UILabel maxLabel;

		private const int THUMB_OFFSET = 12;

		public TRMovingLabelRangeSlider (CGRect rect, int minValue, int maxValue) : base(rect)
		{
			rangeSlider = new RangeSliderView (new CGRect (0, 0, rect.Width, rect.Height - 20));
			rangeSlider.MinValue = minValue;
			rangeSlider.MaxValue = maxValue;
			this.Add (rangeSlider);

			minLabel = new UILabel (new CGRect(-rect.Width / 2 + THUMB_OFFSET, rect.Height - 20, rect.Width, 20));
			minLabel.TextAlignment = UITextAlignment.Center;
			minLabel.TextColor = UIColor.LightGray;

			maxLabel = new UILabel (new CGRect(rect.Width / 2 - THUMB_OFFSET, rect.Height - 20, rect.Width, 20));
			maxLabel.TextAlignment = UITextAlignment.Center;
			maxLabel.TextColor = UIColor.LightGray;

			minLabel.Font = UIFont.FromName ("HelveticaNeue", 14.0f);
			maxLabel.Font = UIFont.FromName ("HelveticaNeue", 14.0f);
			this.Add (minLabel);
			this.Add (maxLabel);
		}

		public override CoreGraphics.CGRect Frame {
			get {
				return base.Frame;
			}
			set {
				base.Frame = value;
				if (rangeSlider != null) {
					rangeSlider.Frame = new CGRect (0, 0, value.Width, value.Height - 20);
					minLabel.Frame = new CGRect(-value.Width / 2 + THUMB_OFFSET, value.Height - 20, value.Width, 20);
					maxLabel.Frame = new CGRect(value.Width / 2 - THUMB_OFFSET, value.Height - 20, value.Width, 20);
				}
			}
		}
	}
}

