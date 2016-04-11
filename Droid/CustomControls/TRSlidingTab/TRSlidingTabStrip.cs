using System;
using Android.Widget;
using Android.Graphics;
using Android.Content;
using Android.Util;
using Android.Views;

namespace ThisRoofN.Droid.CustomControls.TRSlidingTab
{
	public class TRSlidingTabStrip : LinearLayout {

		private static int DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS = 0;
		private static byte DEFAULT_BOTTOM_BORDER_COLOR_ALPHA = 0x26;
		private static int SELECTED_INDICATOR_THICKNESS_DIPS = 3;
		private static int DEFAULT_SELECTED_INDICATOR_COLOR = 0xffffff;

		private int mBottomBorderThickness;
		private Paint mBottomBorderPaint;

		private int mSelectedIndicatorThickness;
		private Paint mSelectedIndicatorPaint;

		private int mDefaultBottomBorderColor;

		private int mSelectedPosition;
		private float mSelectionOffset;

		private TRSlidingTabLayout.TabColorizer mCustomTabColorizer;
		private SimpleTabColorizer mDefaultTabColorizer;

		public TRSlidingTabStrip(Context context, IAttributeSet attrs) : base(context, attrs) {
			SetWillNotDraw (false);

			float density = Resources.DisplayMetrics.Density;
			TypedValue outValue = new TypedValue ();
			context.Theme.ResolveAttribute (Resource.Attribute.colorPrimary, outValue, true);
			int themeForegroundColor = outValue.Data;

			mDefaultBottomBorderColor = setColorAlpha (themeForegroundColor, DEFAULT_BOTTOM_BORDER_COLOR_ALPHA);
			mDefaultTabColorizer = new SimpleTabColorizer ();
			mDefaultTabColorizer.SetIndicatorColors (new int[]{ DEFAULT_SELECTED_INDICATOR_COLOR });

			mBottomBorderThickness = (int)(DEFAULT_BOTTOM_BORDER_THICKNESS_DIPS * density);
			mBottomBorderPaint = new Paint ();
			mBottomBorderPaint.Color = Color.Rgb(Color.GetRedComponent(mDefaultBottomBorderColor), Color.GetGreenComponent(mDefaultBottomBorderColor), Color.GetBlueComponent(mDefaultBottomBorderColor));

			mSelectedIndicatorThickness = (int)(SELECTED_INDICATOR_THICKNESS_DIPS * density);
			mSelectedIndicatorPaint = new Paint ();
		}

		public void SetCustomTabColorizer(TRSlidingTabLayout.TabColorizer customTabColorizer) {
			mCustomTabColorizer = customTabColorizer;
			Invalidate ();
		}

		public void SetSelectedIndicatorColors(int[] colors) {
			mCustomTabColorizer = null;
			mDefaultTabColorizer.SetIndicatorColors (colors);
			Invalidate ();
		}

		public void OnViewPagerPageChanged(int position, float positionOffset) {
			mSelectedPosition = position;
			mSelectionOffset = positionOffset;
			Invalidate ();
		}

		protected override void OnDraw (Canvas canvas)
		{
			int height = Height;
			int childCount = ChildCount;
			TRSlidingTabLayout.TabColorizer tabColorizer = mCustomTabColorizer != null
				? mCustomTabColorizer 
				: mDefaultTabColorizer;

			// Thick colored underline below the current selection
			if (childCount > 0) {
				View selectedTitle = GetChildAt (mSelectedPosition);
				int left = selectedTitle.Left;
				int right = selectedTitle.Right;
				int color = tabColorizer.GetIndicatorColor (mSelectedPosition);

				if (mSelectionOffset > 0 && mSelectedPosition < ChildCount - 1) {
					int nextColor = tabColorizer.GetIndicatorColor (mSelectedPosition + 1);
					if (color != nextColor) {
						color = BlendColors (nextColor, color, mSelectionOffset);
					}

					// Draw the selection partway between the tabs
					View nextTitle = GetChildAt(mSelectedPosition + 1);
					left = (int)(mSelectionOffset * nextTitle.Left + (1.0f - mSelectionOffset) * left);
					right = (int)(mSelectionOffset * nextTitle.Right + (1.0f - mSelectionOffset) * right);
				}

				mSelectedIndicatorPaint.Color = setColor(color);
				canvas.DrawRect (left, height - mSelectedIndicatorThickness, right, height, mSelectedIndicatorPaint);
			}

			canvas.DrawRect (0, height - mBottomBorderThickness, Width, height, mBottomBorderPaint);
		}

		private static Color setColor(int color) {
			return Color.Rgb (Color.GetRedComponent(color), Color.GetGreenComponent (color), Color.GetBlueComponent (color));
		}

		private static int setColorAlpha(int color, byte alpha) {
			return Color.Argb (alpha, Color.GetRedComponent(color), Color.GetGreenComponent (color), Color.GetBlueComponent (color));
		}

		private static int BlendColors(int color1, int color2, float ratio) {
			float inverseRation = 1 - ratio;
			float r = Color.GetRedComponent (color1) * ratio + Color.GetRedComponent (color2) * inverseRation;
			float g = Color.GetGreenComponent (color1) * ratio + Color.GetGreenComponent (color2) * inverseRation;
			float b = Color.GetBlueComponent (color1) * ratio + Color.GetBlueComponent (color2) * inverseRation;
			return Color.Rgb ((int)r, (int)g, (int)b);
		}

		public class SimpleTabColorizer : TRSlidingTabLayout.TabColorizer {
			private int[] mIndicatorColors;

			public int GetIndicatorColor(int position) {
				return mIndicatorColors [position % mIndicatorColors.Length];
			}

			public void SetIndicatorColors(int[] colors) {
				mIndicatorColors = colors;
			}
		}
	}


}

