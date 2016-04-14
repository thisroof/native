using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;

namespace ThisRoofN.Droid.CustomControls
{
	public delegate void KeyboardEventHandler (object sender, bool isShown);

	public class KeyboardLayout : LinearLayout
	{
		public event KeyboardEventHandler OnKeyboard;

		public KeyboardLayout (Context context) : base(context)
		{
		}

		public KeyboardLayout (Context context, IAttributeSet attrs) : base (context, attrs)
		{
		}

		public KeyboardLayout (Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			int proposedHeight = MeasureSpec.GetSize (heightMeasureSpec);
			int actualHeight = this.Height;

			if (actualHeight > proposedHeight) {
				OnKeyboard (this, true);
			} else if (actualHeight + 150 < proposedHeight) {
				OnKeyboard (this, false);
			}

			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
		}
	}
}

