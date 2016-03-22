using System;
using MvvmCross.Binding.Bindings.Target;
using RangeSlider;
using MvvmCross.Binding;
using MvvmCross.Platform;

namespace ThisRoofN.iOS.CustomBinding
{
	public class RangeSliderRightTargetBinding : MvxConvertingTargetBinding
	{
		protected RangeSliderView RangeSliderView
		{
			get { return (RangeSliderView)Target; }
		}

		public RangeSliderRightTargetBinding (RangeSliderView target) : base(target)
		{
		}

		public override void SubscribeToEvents ()
		{
			RangeSliderView.RightValueChanged += RangeSliderView_RightValueChanged;
		}

		void RangeSliderView_RightValueChanged (nfloat value)
		{
			FireValueChanged (value);
		}

		protected override void SetValueImpl (object target, object value)
		{
		}

		public override void SetValue (object value)
		{
			RangeSliderView.RightValue = (float)value;
		}

		public override Type TargetType {
			get {
				return typeof(float);
			}
		}

		public override MvvmCross.Binding.MvxBindingMode DefaultMode {
			get {
				return MvxBindingMode.TwoWay;
			}
		}

		protected override void Dispose (bool isDisposing)
		{
			if (isDisposing) {
				var target = Target as RangeSliderView;
				if (target != null) {
					target.RightValueChanged -= RangeSliderView_RightValueChanged;
				}
			}
			base.Dispose (isDisposing);
		}
	}
}

