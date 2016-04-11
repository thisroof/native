using System;
using System.Collections;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.App;
using Android.OS;
using MvvmCross.Core.ViewModels;
using System.Linq;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;

namespace ThisRoofN.Droid.Adapters
{
	/// <summary>
	/// Mvx ViewPager Fragment Adapter
	/// @see RoverLink.Android.Views.DashboardHomeView
	/// </summary>
	public class MvxViewPagerFragmentAdapter : FragmentStatePagerAdapter
	{
		public class FragmentInfo
		{
			public string Title { get; set;}
			public Type FragmentType { get; set;}
			public IMvxViewModel ViewModel { get; set; }
		}

		public List<FragmentInfo> Fragments { get; private set; }

		public MvxViewPagerFragmentAdapter (Context context, FragmentManager fragmentManager, List<FragmentInfo> fragments) : base(fragmentManager)
		{
			Fragments = fragments;
		}

		public override int Count {
			get {
				return Fragments.Count ();
			}
		}

		public override int GetItemPosition (Java.Lang.Object objectValue)
		{
			return PositionNone;
		}	

		public override Fragment GetItem (int position)
		{
			var frag = Fragments.ElementAt (position);
			var fragment = (MvxFragment)Activator.CreateInstance ((frag as FragmentInfo).FragmentType);
			((MvxFragment)fragment).ViewModel = (frag as FragmentInfo).ViewModel;
			return fragment;
		}

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String ((Fragments.ElementAt (position) as FragmentInfo).Title);
		}

		public void RemoveFragmentAt(int position)
		{
			Fragments.RemoveAt(position);
			NotifyDataSetChanged();
		}

		public void AddFragment(FragmentInfo info)
		{
			Fragments.Add(info);
			NotifyDataSetChanged();
		}

		public override IParcelable SaveState ()
		{
			return null;
		}
	}
}

