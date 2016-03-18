using System;
using MvvmCross.Core.ViewModels;

namespace ThisRoofN
{
	public class TRMvxPresentationHint : MvxPresentationHint
	{
		public enum TRPresentationType
		{
			Default,
			GotoRoot,
		}

		public TRMvxPresentationHint (TRPresentationType changeType)
		{
			ChangeType = changeType;
		}

		public TRPresentationType ChangeType { get; private set; }
	}
}

