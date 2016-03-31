using System;
using Android.Support.V4.App;
using Android.Widget;
using V7Widget = Android.Support.V7.Widget;

namespace ThisRoofN.Droid.Helpers
{
	public class ToolbarHelper
	{
		public enum ToolbarType
		{
			None,
			Visible,
			BackOnly
		}

		private FragmentManager fragmentManager;
		private V7Widget.Toolbar mToolbar;

		private ImageView leftImage;
		private ImageView rightImage;

		private Action rightButtonAction;

		public Action RightButtonAction { 
			get {
				return this.rightButtonAction;
			}
			set {
				this.rightButtonAction = value;
			}
		}

		public ToolbarHelper (HomeView owner, FragmentManager fragmentManager)
		{
			this.fragmentManager = fragmentManager;

			this.mToolbar = owner.FindViewById<V7Widget.Toolbar> (Resource.Id.toolbar);
			this.mToolbar.SetContentInsetsAbsolute (0, 0);

			this.leftImage = mToolbar.FindViewById<ImageView> (Resource.Id.nav_back);
			this.rightImage = mToolbar.FindViewById<ImageView> (Resource.Id.nav_setting);

			this.leftImage.Click += (object sender, EventArgs e) => {
				int backStackEntrycount = fragmentManager.BackStackEntryCount;
				if (backStackEntrycount > 0) {
					fragmentManager.PopBackStack ();
				}
			};

			this.rightImage.Click += (object sender, EventArgs e) => {
				if (rightButtonAction != null) {
					rightButtonAction ();
				}
			};
		}

		public void SetToolbarType (ToolbarType type)
		{
			switch (type) {
			case ToolbarType.None:
				this.mToolbar.Visibility = Android.Views.ViewStates.Gone;
				break;
			case ToolbarType.Visible:
				this.mToolbar.Visibility = Android.Views.ViewStates.Visible;
				this.rightImage.Visibility = Android.Views.ViewStates.Visible;
				break;
			case ToolbarType.BackOnly:
				this.mToolbar.Visibility = Android.Views.ViewStates.Visible;
				this.rightImage.Visibility = Android.Views.ViewStates.Gone;
				break;
			default:
				break;
			}
		}
	}
}

