// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace ThisRoofN.iOS
{
	public partial class SRDetailDescCell : UITableViewCell
	{
		public static string Identifier = "SRDetailDescCell";
		private SearchResultDetailView masterView;
		private nfloat cellHeight;

		public SRDetailDescCell (IntPtr handle) : base (handle)
		{
		}

		public nfloat CellHeight 
		{
			get
			{
				return cellHeight;
			}
		}

		public void BindData(SearchResultDetailView _masterView)
		{
			this.masterView = _masterView;

			_masterView.BindingSet.Bind (lbl_desc).To (vm => vm.ItemDetail.ShortenedDescription);
			_masterView.BindingSet.Bind (btn_more).To (vm => vm.DescMoreCommand);

			InitUI ();
		}

		private void InitUI()
		{
			cellHeight = view_descBack.Frame.Bottom;
		}
	}
}
