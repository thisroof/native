using System;
using MvvmCross.Binding.Bindings.Target;
using RangeSlider;
using MvvmCross.Binding;
using MvvmCross.Platform;

namespace ThisRoofN.Droid.CustomBinding
{
	public class RangeSliderLeftTargetBinding : MvxConvertingTargetBinding
	{
		protected RangeSliderView RangeSliderView
		{
			get { return (RangeSliderView)Target; }
		}

		public RangeSliderLeftTargetBinding (RangeSliderView target) : base(target)
		{
		}

		public override void SubscribeToEvents ()
		{
			RangeSliderView.LeftValueChanged += RangeSliderView_LeftValueChanged;
		}

		void RangeSliderView_LeftValueChanged (float value)
		{
			FireValueChanged (value);
		}

		protected override void SetValueImpl (object target, object value)
		{
		}

		public override void SetValue (object value)
		{
			RangeSliderView.LeftValue = (float)value;
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
					target.LeftValueChanged -= RangeSliderView_LeftValueChanged;
				}
			}
			base.Dispose (isDisposing);
		}
	}
}

