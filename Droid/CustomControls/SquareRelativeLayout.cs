using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;

namespace ThisRoofN.Droid
{
	public class SquareRelativeLayout : RelativeLayout
	{
		public SquareRelativeLayout (Context context) : base(context)
		{
		}

		public SquareRelativeLayout (Context context, IAttributeSet attrs) : base (context, attrs)
		{
		}

		public SquareRelativeLayout (Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			int width = MeasureSpec.GetSize (widthMeasureSpec);
			base.OnMeasure (MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly), 
				MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly));
		}
	}
}

