using System;
using ThisRoofN.ViewModels;

namespace ThisRoofN.ViewModels
{
	public class TRWebViewModel : BaseViewModel
	{
		private string _contentLink;

		public TRWebViewModel ()
		{
		}

		public void Init(string link) {
			this.ContentLink = link;
		}

		public string ContentLink
		{
			get
			{
				return this._contentLink;
			}
			set {
				this._contentLink = value;
				RaisePropertyChanged (() => ContentLink);
			}
		}
	}
}

