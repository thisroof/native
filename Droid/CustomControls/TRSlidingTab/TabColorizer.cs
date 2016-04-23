using System;

namespace ThisRoofN.Droid.CustomControls.TRSlidingTab
{
	/**
     * Allows complete control over the colors drawn in the tab layout. Set with
     * {@link #setCustomTabColorizer(TabColorizer)}.
     */
	public interface TabColorizer
	{
		/**
         * @return return the color of the indicator used when {@code position} is selected.
         */
		int GetIndicatorColor(int position);
	}
}

