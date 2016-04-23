using System;
using Android.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Content;
using Android.Views;
using Android.Graphics;

namespace ThisRoofN.Droid.CustomControls.TRSlidingTab
{
	public class TRIconSlidingTabLayout : HorizontalScrollView {

		private static int TITLE_OFFSET_DIPS = 24;
		private static int mTitleOffset;

		private int mTabViewLayoutId;
		private int mTabViewIconViewId;
		private bool mDistributeEvenly;

		private int[] mIconResourceArray;

		private static ViewPager mViewPager;
		private SparseArray<String> mContentDescriptions = new SparseArray<String>();
		private static ViewPager.IOnPageChangeListener mViewPagerPageChangeListener;

		private static TRSlidingTabStrip mTabStrip;

		private int tabViewPaddingDips = 16;
		private int tabViewTextSizeSp = 12;
		private int tabTitleTextSize = 14;
		public int TabViewTextSizeSp
		{
			get {
				return tabViewTextSizeSp;
			}
			set {
				tabViewTextSizeSp = value;
			}
		}

		public int TabTitleTextSize 
		{
			get {
				return tabTitleTextSize;
			}
			set {
				tabTitleTextSize = value;
			}
		}

		public int TabViewPaddingDips 
		{
			get {
				return tabViewPaddingDips;
			}
			set {
				tabViewPaddingDips = value;
			}
		}

		public int[] IconResourceArray
		{
			get {
				return mIconResourceArray;
			} set {
				mIconResourceArray = value;
			}
		}

		public TRIconSlidingTabLayout(Context context) : this(context, null) {
		}

		public TRIconSlidingTabLayout(Context context, IAttributeSet attrs) : this(context, attrs, 0) {
		}

		public TRIconSlidingTabLayout(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) {
			// Disable the TRSlidingTabLayout Bar
			HorizontalScrollBarEnabled = false;

			//Make sure that the Tab Strips fills this View
			FillViewport = true;

			mTitleOffset = (int)(TITLE_OFFSET_DIPS * Resources.DisplayMetrics.Density);
			mTabStrip = new TRSlidingTabStrip (context, null);
			AddView (mTabStrip, LayoutParams.MatchParent, LayoutParams.WrapContent);
		}

		public void SetCustomTabColorizer(TabColorizer tabColorizer) {
			mTabStrip.SetCustomTabColorizer (tabColorizer);
		}

		public void SetDistributeEvenly(bool distributeEvenly) {
			mDistributeEvenly = distributeEvenly;
		}

		public void SetSelectedIndicatorColors(int[] colors) {
			mTabStrip.SetSelectedIndicatorColors (colors);
		}

		public void SetOnPageChangeListener(ViewPager.IOnPageChangeListener listener) {
			mViewPagerPageChangeListener = listener;
		}

		public void SetCustomTabView(int layoutResId, int iconViewId) {
			mTabViewLayoutId = layoutResId;
			mTabViewIconViewId = iconViewId;
		}

		public void SetViewPager(ViewPager viewPager) {
			mTabStrip.RemoveAllViews ();
			mViewPager = viewPager;
			if (viewPager != null) {
				viewPager.AddOnPageChangeListener (new InternalViewPagerListener (this));
				populateTabStrip ();
			}
		}

		protected ImageView createDefaultTabView(Context context) {
			ImageView imageView = new ImageView (context);
			imageView.SetScaleType (ImageView.ScaleType.CenterInside);
			imageView.LayoutParameters = new LinearLayout.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent); 

			int padding = (int)(TabViewPaddingDips * Resources.DisplayMetrics.Density);
			imageView.SetPadding (padding, padding, padding, padding);

			return imageView;
		}

		private void populateTabStrip() {
			PagerAdapter adapter = mViewPager.Adapter;
			View.IOnClickListener tabClickListener = new TabClickListener ();

			for (int i = 0; i < adapter.Count; i++) {
				View tabView = null;
				ImageView tabIconView = null;

				if (mTabViewLayoutId != 0) {
					// If there is a custom tab view layout id set, and inflate it
					//View view = Inflate(Context, mTabViewLayoutId, mTabStrip);

					tabView = Inflate(Context, mTabViewLayoutId, mTabStrip);
					tabIconView = (ImageView)tabView.FindViewById (mTabViewIconViewId);
				}

				if (tabView == null) {
					tabView = createDefaultTabView (Context);
				}

				if (tabIconView == null && tabView is ImageView) {
					tabIconView = (ImageView)tabView;
				}

				if (mDistributeEvenly) {
					LinearLayout.LayoutParams lp = (LinearLayout.LayoutParams)tabView.LayoutParameters;
					lp.Width = 0;
					lp.Weight = 1;
				}

				tabIconView.SetImageDrawable(Context.Resources.GetDrawable(IconResourceArray[i]));
				tabView.SetOnClickListener (tabClickListener);

				string desc = mContentDescriptions.Get (i);
				if (desc != null) {
					tabView.ContentDescription = desc;
				}

				mTabStrip.AddView (tabView);
				if (i == mViewPager.CurrentItem) {
					tabView.Selected = true;
				}

//				tabTitleView.SetTextColor (Resources.GetColorStateList (Resource.Color.sliding_tab_selector));
//				tabTitleView.TextSize = TabTitleTextSize;
			}
		}

		public void SetContentDescription(int i, string desc) {
			mContentDescriptions.Put (i, desc);
		}

		protected override void OnAttachedToWindow ()
		{
			base.OnAttachedToWindow ();

			if (mViewPager != null) {
				ScrollToTab (mViewPager.CurrentItem, 0);
			}
		}

		private void ScrollToTab(int tabIndex, int positionOffset) {
			int tabStripChildCount = mTabStrip.ChildCount;
			if (tabStripChildCount == 0 || tabIndex < 0 || tabIndex >= tabStripChildCount) {
				return;
			}

			View selectedChild = mTabStrip.GetChildAt (tabIndex);
			if (selectedChild != null) {
				int targetScrollX = selectedChild.Left + positionOffset;

				if (tabIndex > 0 || positionOffset > 0) {
					targetScrollX -= mTitleOffset;
				}

				ScrollTo (targetScrollX, 0);
			}
		}

		public class InternalViewPagerListener :Java.Lang.Object, ViewPager.IOnPageChangeListener {
			private int mScrollState;
			TRIconSlidingTabLayout context;
			public InternalViewPagerListener(TRIconSlidingTabLayout instance) {
				context = instance;
			}

			public void OnPageScrolled (int position, float positionOffset, int positionOffsetPixels) {
				int tabStripChildCount = mTabStrip.ChildCount;
				if ((tabStripChildCount == 0) || (position < 0) || (position >= tabStripChildCount)) {
					return;
				}

				mTabStrip.OnViewPagerPageChanged (position, positionOffset);

				View selectedTitle = mTabStrip.GetChildAt (position);
				int extraOffset = (selectedTitle != null) 
					? (int)(positionOffset * selectedTitle.Width)
					: 0;

				context.ScrollToTab (position, extraOffset);

				if (mViewPagerPageChangeListener != null) {
					mViewPagerPageChangeListener.OnPageScrolled (position, positionOffset, positionOffsetPixels);
				}
			}

			public void OnPageScrollStateChanged (int state) {
				mScrollState = state;
				if (mViewPagerPageChangeListener != null) {
					mViewPagerPageChangeListener.OnPageScrollStateChanged (state);
				}
			}

			public void OnPageSelected (int position) {
				if (mScrollState == ViewPager.ScrollStateIdle) {
					mTabStrip.OnViewPagerPageChanged (position, 0);
					context.ScrollToTab (position, 0);
				}

				for (int i = 0; i < mTabStrip.ChildCount; i++) {
					mTabStrip.GetChildAt (i).Selected = position == i;
				}

				if (mViewPagerPageChangeListener != null) {
					mViewPagerPageChangeListener.OnPageSelected (position);
				}
			}
		}

		public class TabClickListener : Java.Lang.Object, View.IOnClickListener {
			public void OnClick (View v) {
				for (int i = 0; i < mTabStrip.ChildCount; i++) {
					if (v == mTabStrip.GetChildAt (i)) {
						mViewPager.SetCurrentItem (i, true);
						return;
					}
				}
			}
		}
	}
}

