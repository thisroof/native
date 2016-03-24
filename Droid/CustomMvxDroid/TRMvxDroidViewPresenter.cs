using System;
using MvvmCross.Droid.Views;
using MvvmCross.Core.ViewModels;
using Android.Content;
using Android.Support.V4.App;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;

namespace ThisRoofN.Droid.CustomMvxDroid
{
	public class TRMvxDroidViewPresenter : MvxAndroidViewPresenter
	{
		private IMvxViewModelLoader _viewModelLoader;
		private IFragmentTypeLookup _fragmentTypeLookup;
		private Context _context;
		private FragmentManager _fragmentManager;

		public TRMvxDroidViewPresenter (IMvxViewModelLoader viewModelLoader, IFragmentTypeLookup fragmentTypeLookup)
		{
			_fragmentTypeLookup = fragmentTypeLookup;
			_viewModelLoader = viewModelLoader;
		}

		public void RegisterFragmentManager (FragmentManager fragmentManager, Context context)
		{
			_fragmentManager = fragmentManager;
			_context = context;
		}

		public override void Show (MvxViewModelRequest request)
		{
			Type fragmentType;

			if (_fragmentManager == null) {
				base.Show (request);
				return;
			}


			TRFragmentType type = _fragmentTypeLookup.TryGetFragmentType (request.ViewModelType, out fragmentType);
			switch (type) {
			case TRFragmentType.MvxFragment:
				var fragment = (MvxFragment)Activator.CreateInstance (fragmentType);
				fragment.ViewModel = _viewModelLoader.LoadViewModel (request, null);
				ShowFragment (fragment, true);
				break;
			case TRFragmentType.MvxDialogFragment:
				var dialogFragment = (MvxDialogFragment)Activator.CreateInstance (fragmentType);
				dialogFragment.ViewModel = _viewModelLoader.LoadViewModel (request, null);
				ShowDialogFragment (dialogFragment);
				break;
			case TRFragmentType.None:
				base.Show (request);
				break;
			}
		}

		private void ShowFragment (MvxFragment fragment, bool addToBackStack)
		{
			Fragment existFragment = _fragmentManager.FindFragmentByTag (fragment.GetType ().Name);
			if (existFragment != null && existFragment.IsVisible) {
				return;
			}

			CommitFragmentTransaction (fragment, addToBackStack);
		}

		private void CommitFragmentTransaction (MvxFragment fragment, bool addToBackStack)
		{
			var transaction = _fragmentManager.BeginTransaction ();

			transaction.SetCustomAnimations (Resource.Animation.activity_slidein_left, Resource.Animation.activity_slideout_left, Resource.Animation.activity_slidein_right, Resource.Animation.activity_slideout_right);
			if (addToBackStack)
				transaction.AddToBackStack (fragment.GetType ().Name);
			transaction
					.Replace (Resource.Id.mainContentLayout, fragment, fragment.GetType ().Name)
					.Commit ();
		}

		private void ShowDialogFragment (MvxDialogFragment dialogFragment)
		{
			dialogFragment.Show (_fragmentManager, dialogFragment.GetType ().Name);
		}
	}
}

