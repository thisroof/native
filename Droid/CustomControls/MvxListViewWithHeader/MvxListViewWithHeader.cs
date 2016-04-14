using System;
using MvvmCross.Binding.Droid.Views;
using Android.Util;
using System.Collections.Generic;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Views;
using Android.Content;

namespace ThisRoofN.Droid.CustomControls
{
	public class MvxListViewWithHeader : MvxListView
	{
		/// <summary>
		/// The default id for the grid header/footer.  This means there is no header/footer
		/// </summary>
		private const int DEFAULT_HEADER_ID = -1;

		private int _footerId;
		private int _headerId;

		public MvxListViewWithHeader(Context context, IAttributeSet attrs)
			: base(context, attrs, null)
		{
			IMvxAdapter adapter = new MvxAdapter(context);

			ApplyAttributes(context, attrs);

			var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
			adapter.ItemTemplateId = itemTemplateId;

			var headers = GetHeaders();
			var footers = GetFooters();

			IMvxAdapter headerAdapter = new HeaderMvxAdapter(headers, footers, adapter);

			Adapter = headerAdapter;
		}

		private void ApplyAttributes(Context c, IAttributeSet attrs)
		{
			_headerId = DEFAULT_HEADER_ID;
			_footerId = DEFAULT_HEADER_ID;

			using (var attributes = c.ObtainStyledAttributes(attrs, Resource.Styleable.ListView))
			{
				_headerId = attributes.GetResourceId(Resource.Styleable.ListView_header, DEFAULT_HEADER_ID);
				_footerId = attributes.GetResourceId(Resource.Styleable.ListView_footer, DEFAULT_HEADER_ID);
			}
		}

		private IList<ListView.FixedViewInfo> GetFixedViewInfos(int id)
		{
			var viewInfos = new List<ListView.FixedViewInfo>();

			View view = GetBoundView(id);

			if (view != null)
			{
				var info = new ListView.FixedViewInfo(this)
				{
					Data = null,
					IsSelectable = true,
					View = view,
				};
				viewInfos.Add(info);
			}

			return viewInfos;
		}

		private IList<ListView.FixedViewInfo> GetFooters()
		{
			return GetFixedViewInfos(_footerId);
		}

		private IList<ListView.FixedViewInfo> GetHeaders()
		{
			return GetFixedViewInfos(_headerId);
		}

		private View GetBoundView(int id)
		{
			if (id == DEFAULT_HEADER_ID) return null;

			IMvxAndroidBindingContext bindingContext = MvxAndroidBindingContextHelpers.Current();
			var view = bindingContext.BindingInflate(id, null);

			return view;
		}
	}
}

