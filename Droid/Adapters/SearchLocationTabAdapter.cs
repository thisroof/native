using System;
using Android.Support.V4.App;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Android.OS;

namespace ThisRoofN.Droid.Adapters
{
	public class SearchLocationTabAdapter : FragmentStatePagerAdapter
	{
		public List<MvxViewPagerFragmentAdapter.FragmentInfo> Fragments { get; private set;}

		public SearchLocationTabAdapter (FragmentManager fm, List<MvxViewPagerFragmentAdapter.FragmentInfo> items) : base(fm)
		{
			this.Fragments = items;
		}

		public override int Count {
			get {
				return Fragments.Count;
			}
		}

		public override int GetItemPosition (Java.Lang.Object objectValue)
		{
			return PositionNone;
		}

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String ((Fragments.ElementAt (position) as MvxViewPagerFragmentAdapter.FragmentInfo).Title);
		}

		public override Fragment GetItem (int position)
		{
			var frag = Fragments.ElementAt (position);
			var fragment = (MvxFragment)Activator.CreateInstance ((frag as MvxViewPagerFragmentAdapter.FragmentInfo).FragmentType);
			((MvxFragment)fragment).ViewModel = (frag as MvxViewPagerFragmentAdapter.FragmentInfo).ViewModel;
			return fragment;
		}

		public override IParcelable SaveState ()
		{
			return null;
		}
	}
}

