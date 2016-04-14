using System;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding;
using MvvmCross.Platform;
using Android.Widget;

namespace ThisRoofN.Droid.CustomBinding
{
	public class SeekBarBinding : MvxConvertingTargetBinding
	{
		protected SeekBar seekbar
		{
			get { return (SeekBar)Target; }
		}

		public SeekBarBinding (SeekBar target) : base(target)
		{
		}

		public override void SubscribeToEvents ()
		{
			seekbar.ProgressChanged += Seekbar_ValueChanged;
		}

		void Seekbar_ValueChanged (object sender, SeekBar.ProgressChangedEventArgs e)
		{
			FireValueChanged (e.Progress);
		}

		protected override void SetValueImpl (object target, object value)
		{
		}

		public override void SetValue (object value)
		{
			seekbar.Progress = (int)value;
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
				var target = Target as SeekBar;
				if (target != null) {
					target.ProgressChanged -= Seekbar_ValueChanged;
				}
			}
			base.Dispose (isDisposing);
		}
	}
}