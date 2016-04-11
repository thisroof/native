
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
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Android.Graphics.Drawables;

namespace ThisRoofN.Droid
{
	public class InHomeModalView : MvxDialogFragment
	{
		public InHomeModalView() 
		{
			this.SetStyle (MvxDialogFragment.StyleNoTitle, Resource.Style.SearchModalStyle);
		}

		public InHomeModalViewModel ViewModelInstance
		{
			get {
				return (InHomeModalViewModel)base.ViewModel;
			}
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			Dialog.Window.SetBackgroundDrawable (new ColorDrawable (Android.Graphics.Color.Transparent));

			View view = this.BindingInflate (Resource.Layout.fragment_search_modal_inHome, null);

			LinearLayout modal_back = (LinearLayout)view.FindViewById (Resource.Id.layout_modal_back);
			modal_back.Click += (object sender, EventArgs e) => {
				ViewModelInstance.ModalCloseCommand.Execute(null);
			};

			ImageView img_back = (ImageView)view.FindViewById (Resource.Id.btn_back);
			img_back.Click += (object sender, EventArgs e) => {
				ViewModelInstance.ModalCloseCommand.Execute(null);
			};

			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.DialogAnimation;
		}

		public override void OnResume ()
		{
			base.OnResume ();
			Dialog.Window.SetLayout (RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);

		}
	}
}

