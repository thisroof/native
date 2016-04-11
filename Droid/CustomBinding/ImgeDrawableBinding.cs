using System.Windows.Input;
using System;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Binding;

namespace ThisRoofN.Droid.CustomBinding
{
	public class ImageDrawableBinding : MvxAndroidTargetBinding
	{
		protected ImageView View
		{
			get { return (ImageView) Target; }
		}

		public ImageDrawableBinding (ImageView view)
			: base(view)
		{
		}

		protected override void SetValueImpl (object target, object value)
		{
			throw new NotImplementedException ();
		}
		public override void SetValue(object value)
		{
			var drawableID = (int)value;

			Drawable drawable = AndroidGlobals.ApplicationContext.Resources.GetDrawable(drawableID);

			View.SetImageDrawable(drawable);
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneWay; }
		}

		public override Type TargetType
		{
			get { return typeof (int); }
		}

		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
		}
	}
}

