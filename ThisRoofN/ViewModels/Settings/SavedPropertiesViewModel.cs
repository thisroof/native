using System;
using System.Collections.Generic;
using ThisRoofN.Models.Service;
using Newtonsoft.Json;
using ThisRoofN.Helpers;
using ThisRoofN.Models.App;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace ThisRoofN.ViewModels
{
	public class SavedPropertiesViewModel : BaseViewModel
	{
		private MvxCommand<int> _detailCommand;
		private List<CottageDetail> savedProperties;

		public SavedPropertiesViewModel ()
		{
		}

		public void Init (string data) {
			savedProperties = JsonConvert.DeserializeObject<List<CottageDetail>> (data);	
		}

		public ICommand DetailCommand {
			get {
				_detailCommand = _detailCommand ?? new MvxCommand<int> (GotoDetail);
				return _detailCommand;
			}
		}

		private void GotoDetail (int index)
		{
			DataHelper.SelectedCottageDetail = new TRCottageDetail (savedProperties[index]);
			ShowViewModel<SearchResultDetailViewModel> ();
		}


		public List<CottageDetail> SavedProperties 
		{
			get {
				return savedProperties;
			}
			set {
				savedProperties = value;
			}
		}
	}
}

