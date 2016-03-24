using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Platform.IoC;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;

namespace ThisRoofN.Droid.CustomMvxDroid
{
	public enum TRFragmentType
	{
		MvxFragment,
		MvxDialogFragment,
		None,
	}

	public interface IFragmentTypeLookup
	{
		TRFragmentType TryGetFragmentType (Type viewModelType, out Type fragmentType);	
	}

	public class TRMvxFragmentTypeLookup : IFragmentTypeLookup
	{
		private readonly IDictionary<string, Type> _fragmentLookup = new Dictionary<string, Type> ();
		private readonly IDictionary<string, Type> _dialogFragmentLookup = new Dictionary<string, Type> ();

		public TRMvxFragmentTypeLookup ()
		{
			_fragmentLookup = (from type in GetType ().Assembly.ExceptionSafeGetTypes ()
			                   where !type.IsAbstract
			                       && !type.IsInterface
			                       && typeof(MvxFragment).IsAssignableFrom (type)
			                       && type.Name.EndsWith ("View")
			                   select type).ToDictionary (getStrippedName);
			
			_dialogFragmentLookup = (from type in GetType ().Assembly.ExceptionSafeGetTypes ()
				where !type.IsAbstract
				&& !type.IsInterface
				&& typeof(MvxDialogFragment).IsAssignableFrom (type)
				&& type.Name.EndsWith ("View")
				select type).ToDictionary (getStrippedName);
		}

		public TRFragmentType TryGetFragmentType(Type viewModelType, out Type fragmentType) {
			var strippedName = getStrippedName (viewModelType);

			if (!_fragmentLookup.ContainsKey (strippedName)) {

				if (!_dialogFragmentLookup.ContainsKey (strippedName)) {
					fragmentType = null;
					return TRFragmentType.None;
				}

				fragmentType = _dialogFragmentLookup [strippedName];
				return TRFragmentType.MvxDialogFragment;
			}

			fragmentType = _fragmentLookup [strippedName];
			return TRFragmentType.MvxFragment;
		}

		private string getStrippedName(Type type)
		{
			return type.Name
				.TrimEnd ("View".ToCharArray ())
				.TrimEnd ("ViewModel".ToCharArray ());
		}
	}
}

