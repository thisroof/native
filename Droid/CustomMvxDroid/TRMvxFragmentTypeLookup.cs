using System;

namespace ThisRoofN.Droid.CustomMvxDroid
{
	public enum TRFragmentType
	{
		TRMvxFragment,
		TrMvxDialogFragment,
		None,
	}

	public interface IFragmentTypeLookup
	{
		TRFragmentType TryGetFragmentType (Type viewModelType, out Type fragmentType);	
	}

	public class TRMvxFragmentTypeLookup : IFragmentTypeLookup
	{
		public TRMvxFragmentTypeLookup ()
		{
		}

		public TRFragmentType TryGetFragmentType(Type viewModelType, out Type fragmentType) {
			fragmentType = null;
			return TRFragmentType.TRMvxFragment;
		}
	}
}

