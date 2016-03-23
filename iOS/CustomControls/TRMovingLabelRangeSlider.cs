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

			rangeSlider.LeftValueChanged += LeftValueChanged;
			rangeSlider.RightValueChanged += RightValueChanged;
		}

		private void LeftValueChanged (nfloat value) {
			minLabel.SizeToFit();
			CGRect frame = minLabel.Frame;
			frame.X = - frame.Width / 2 + GetOffSetX(value);
			frame.Y = rangeSlider.Frame.Bottom;

			if(frame.Right + 8 < maxLabel.Frame.X ) {
				minLabel.Frame = frame;
			}
		}

		private void RightValueChanged (nfloat value) {
			maxLabel.SizeToFit();
			CGRect frame = maxLabel.Frame;
			frame.X = -frame.Width / 2 + GetOffSetX(value);
			frame.Y = rangeSlider.Frame.Bottom;

			if(frame.Left > minLabel.Frame.Right + 8) {
				maxLabel.Frame = frame;
			}
		}

		public override CoreGraphics.CGRect Frame {
			get {
				return base.Frame;
			}
			set {
				base.Frame = value;
				if (rangeSlider != null) {
					rangeSlider.Frame = new CGRect (0, 0, value.Width, value.Height - 20);
					LeftValueChanged (rangeSlider.LeftValue);
					RightValueChanged (rangeSlider.RightValue);
				}
			}
		}

		private nfloat GetOffSetX (nfloat value) {
			nfloat offsetWidth = rangeSlider.Frame.Width * (value - rangeSlider.MinValue) / (rangeSlider.MaxValue - rangeSlider.MinValue);
			if(offsetWidth < THUMB_OFFSET) {
				offsetWidth = THUMB_OFFSET;
			} else if(offsetWidth > rangeSlider.Frame.Width - THUMB_OFFSET) {
				offsetWidth = rangeSlider.Frame.Width - THUMB_OFFSET;
			}

			return offsetWidth;
		}
	}
}

