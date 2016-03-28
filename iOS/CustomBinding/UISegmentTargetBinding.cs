using System;
using MvvmCross.Binding.Bindings.Target;
using RangeSlider;
using MvvmCross.Binding;
using MvvmCross.Platform;
using UIKit;

namespace ThisRoofN.iOS.CustomBinding
{
	public class UISegmentTargetBinding : MvxConvertingTargetBinding
	{
		protected UISegmentedControl segment
		{
			get { return (UISegmentedControl)Target; }
		}

		public UISegmentTargetBinding (UISegmentedControl target) : base(target)
		{
		}

		public override void SubscribeToEvents ()
		{
			segment.ValueChanged += Segment_ValueChanged;
		}

		void Segment_ValueChanged (object sender, EventArgs e)
		{
			FireValueChanged (segment.SelectedSegment);
		}

		protected override void SetValueImpl (object target, object value)
		{
		}

		public override void SetValue (object value)
		{
			segment.SelectedSegment = (int)value;
		}

		public override Type TargetType {
			get {
				return typeof(int);
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
				var target = Target as UISegmentedControl;
				if (target != null) {
					target.ValueChanged -= Segment_ValueChanged;
				}
			}
			base.Dispose (isDisposing);
		}
	}
}

