
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;

namespace ThisRoofN.Droid.CustomControls
{
	public class SquareImageView : MvxImageView
	{
		public SquareImageView(Context context) : base(context)
		{
		}

		public SquareImageView (Context context, IAttributeSet attrs) : base (context, attrs)
		{
		}

		public SquareImageView (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
			SetMeasuredDimension (MeasuredWidth, MeasuredWidth);
		}
	}
}

