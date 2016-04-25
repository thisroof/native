using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Foundation;

namespace ThisRoofN.iOS
{
	public class GradientHelper
	{
		public static CAGradientLayer WhiteGradient
		{
			get {
				CAGradientLayer gradient = new CAGradientLayer ();
				gradient.NeedsDisplayOnBoundsChange = true;
				gradient.Colors = new CGColor[] { UIColor.White.ColorWithAlpha(0.1f).CGColor, UIColor.White.CGColor, UIColor.White.CGColor};

				NSNumber stepOne = new NSNumber(0.0f);
				NSNumber stepTwo = new NSNumber(0.4f);
				NSNumber stepThree = new NSNumber(1.0f);

				gradient.Locations = new NSNumber[]{ stepOne, stepTwo, stepThree };
				return gradient;
			}
		}


		public static CAGradientLayer TileCellGradient
		{
			get {
				CAGradientLayer gradient = new CAGradientLayer ();
				gradient.NeedsDisplayOnBoundsChange = true;
				gradient.Colors = new CGColor[] { UIColor.Clear.CGColor, UIColor.Black.ColorWithAlpha(0.7f).CGColor};

				NSNumber stepOne = new NSNumber(0.0f);
				NSNumber stepThree = new NSNumber(1.0f);

				gradient.Locations = new NSNumber[]{ stepOne, stepThree };
				return gradient;
			}
		}
	}
}

