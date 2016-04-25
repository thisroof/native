using System;
using System.Collections.Generic;
using System.Linq;
using ThisRoofN.Models;
using System.Text.RegularExpressions;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using ThisRoofN.Models.App;
using ThisRoofN.Helpers;
using ThisRoofN.Models.Service;
using ThisRoofN.Database;
using ThisRoofN.Database.Entities;
using MvvmCross.Platform;
using ThisRoofN.Interfaces;

namespace ThisRoofN.ViewModels
{
	public class SearchResultDetailViewModel : BaseViewModel
	{
		private IDevice deviceInfo;


		public SearchResultDetailViewModel ()
		{
			deviceInfo = Mvx.Resolve<IDevice> ();
		}

		public void Init(int index, bool savedProperty)
		{
			propertyIndex = index;

			InitDetail (savedProperty);
		}

		private async void InitDetail(bool savedProperty)
		{
			if (savedProperty) {
				RaisePropertyChanged (() => ItemDetail);
			} else {
				this.IsLoading = true;
				this.LoadingText = "Loading Detail";
				CottageDetail detail = await mTRService.GetCottageDetail (deviceInfo.GetUniqueIdentifier (), DataHelper.SearchResults[propertyIndex].CottageID);

				DataHelper.SelectedCottage =  DataHelper.SearchResults[propertyIndex];
				DataHelper.SelectedCottageDetail = new TRCottageDetail (detail);

				this.IsLoading = false;
				RaisePropertyChanged (() => ItemDetail);
			}

			ImageIndex = 0;
			Liked = ItemDetail.Liked;
			Disliked = ItemDetail.Disliked;
		}

		public TRCottageDetail ItemDetail {
			get {
				return DataHelper.SelectedCottageDetail;
			} 
		}

		public string ImageLink 
		{
			get {
				return ItemDetail.Photos [ImageIndex].MediaURL;
			}
		}

		#region Next/Prev Image/Property
		private int imageIndex;
		public int ImageIndex 
		{
			get {
				return imageIndex;
			} set {
				imageIndex = value;
				RaisePropertyChanged (() => ImageIndex);
				RaisePropertyChanged (() => ImageLink);
			}
		}

		private MvxCommand<bool> _nextImageCommand;
		public ICommand NextImageCommand {
			get {
				_nextImageCommand = _nextImageCommand ?? new MvxCommand<bool> (DoNextImage);
				return _nextImageCommand;
			}
		}

		private void DoNextImage(bool isNext)
		{
			if (ItemDetail.Photos == null) {
				return;
			}

			if (isNext) {
				if (ImageIndex < ItemDetail.Photos.Count - 1) {
					ImageIndex++;
				} else {
					ImageIndex = 0;
				}
			} else {
				if (ImageIndex == 0) {
					ImageIndex = ItemDetail.Photos.Count - 1;
				} else {
					ImageIndex--;
				}
			}
		}

		private int propertyIndex;
		public int PropertyIndex 
		{
			get {
				return propertyIndex;
			} set {
				propertyIndex = value;
				RaisePropertyChanged (() => PropertyIndex);
				RaisePropertyChanged (() => ItemDetail);
			}
		}

		private MvxCommand<bool> _nextPropertyCommand;
		public ICommand NextPropertyCommand {
			get {
				_nextPropertyCommand = _nextPropertyCommand ?? new MvxCommand<bool> (DoNextProperty);
				return _nextPropertyCommand;
			}
		}

		private void DoNextProperty(bool isNext)
		{
			if (isNext) {
				if (PropertyIndex < DataHelper.SearchResults.Count - 1) {
					PropertyIndex++;
				} else {
					PropertyIndex = 0;
				}
			} else {
				if (PropertyIndex == 0) {
					PropertyIndex = DataHelper.SearchResults.Count - 1;
				} else {
					PropertyIndex--;
				}
			}

			InitDetail (false);
		}
		#endregion


		private MvxCommand _goMapCommand;
		public ICommand GoMapCommand {
			get {
				_goMapCommand = _goMapCommand ?? new MvxCommand (DoGoMap);
				return _goMapCommand;
			}
		}

		private void DoGoMap()
		{
			ShowViewModel<SearchResultDetailMapViewModel> ();
		}

		#region Like/Dislike Part
		private bool _tooFar;
		private bool _tooClose;
		private bool _badArea;
		private bool _tooSmall;
		private bool _tooBig;
		private bool _lotTooBig;
		private bool _lotTooSmall;
		private bool _ugly;

		#region Reject Reason Properties

		public bool TooFar { 
			get { return this._tooFar; } 
			set { 
				this._tooFar = value;
				RaisePropertyChanged (() => TooFar);
			}
		}

		public bool TooClose { 
			get { return this._tooClose; } 
			set { 
				this._tooClose = value;
				RaisePropertyChanged (() => TooClose);
			}
		}

		public bool BadArea { 
			get { return this._badArea; } 
			set { 
				this._badArea = value;
				RaisePropertyChanged (() => BadArea);
			}
		}

		public bool TooSmall { 
			get { return this._tooSmall; } 
			set { 
				this._tooSmall = value;
				RaisePropertyChanged (() => TooSmall);
			}
		}

		public bool HouseTooBig { 
			get { return this._tooBig; } 
			set { 
				this._tooBig = value;
				RaisePropertyChanged (() => HouseTooBig);
			}
		}

		public bool LotTooBig { 
			get { return this._lotTooBig; } 
			set { 
				this._lotTooBig = value;
				RaisePropertyChanged (() => LotTooBig);
			}
		}

		public bool LotTooSmall { 
			get { return this._lotTooSmall; } 
			set { 
				this._lotTooSmall = value;
				RaisePropertyChanged (() => LotTooSmall);
			}
		}

		public bool Ugly { 
			get { return this._ugly; } 
			set { 
				this._ugly = value;
				RaisePropertyChanged (() => Ugly);
			}
		}
		#endregion

		private bool _isDislikeShown;
		public bool IsDislikeShown {
			get {
				return this._isDislikeShown;
			}
			set {
				_isDislikeShown = value;
				RaisePropertyChanged (() => IsDislikeShown);
				RaisePropertyChanged (() => IsDislikeHidden);
			}
		}

		public bool IsDislikeHidden {
			get {
				return !IsDislikeShown;
			}
		}

		private bool _liked;
		public bool Liked {
			get {
				return _liked;
			}
			set {
				_liked = value;
				RaisePropertyChanged (() => Liked);
			}
		}

		private bool _disliked;
		public bool Disliked {
			get {
				return _disliked;
			}
			set {
				_disliked = value;
				RaisePropertyChanged (() => Disliked);
			}
		}

		private MvxCommand _likeCommand;
		public ICommand LikeCommand {
			get {
				_likeCommand = _likeCommand ?? new MvxCommand (DoLike);
				return _likeCommand;
			}
		}

		private MvxCommand _dislikeCommand;
		public ICommand DisLikeCommand {
			get {
				_dislikeCommand = _dislikeCommand ?? new MvxCommand (DoDislike);
				return _dislikeCommand;
			}
		}

		private MvxCommand<bool> _showDislikeCommand;
		public ICommand ShowDislikeViewCommand {
			get {
				_showDislikeCommand = _showDislikeCommand ?? new MvxCommand<bool> (DoShowDislikeDialog);
				return _showDislikeCommand;
			}
		}


		private async void DoLike ()
		{
			if (_liked) {
				this.IsLoading = true;
				this.LoadingText = "Clearing Like";
				if (await mTRService.ClearLikeDislike (mDeviceInfo.GetUniqueIdentifier (), mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), true, ItemDetail.CottageID)) {
					Liked = false;
					TRDatabase.Instance.RemoveCottageLikeInfo (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), ItemDetail.CottageID);
				} 
				this.IsLoading = false;
			} else {

				// Set like to cottage
				CottageLikeRequest request = new CottageLikeRequest () {
					UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
					DeviceID = mDeviceInfo.GetUniqueIdentifier (),
					PropertyID = ItemDetail.CottageID,
					Like = "1"
				};

				this.IsLoading = true;
				this.LoadingText = "Setting Like";

				// Set request to server
				CottageLikeInfo info = await mTRService.LikeCottage (request);

				this.IsLoading = false;

				// Set to database
				if (info != null) {
					TREntityLikes likeData = new TREntityLikes () {
						UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
						PropertyID = info.PropertyID,
						LikeDislike = true,
						Price = ItemDetail.Price,
						PrimaryPhotoURL = ItemDetail.PrimaryPhotoLink,
						Address = ItemDetail.Address.FullStreetAddress,
						CityStateZip = ItemDetail.FormattedAddress
					};

					TRDatabase.Instance.SaveItem (likeData);
				}

				Liked = true;
				Disliked = false;
			}
		}


		private async void DoDislike ()
		{
			this.IsLoading = true;
			this.LoadingText = "Setting Dislike";
			// dislike request to server
			CottageLikeInfo likeInfo = await mTRService.DislikeCottage (new CottageDislikeRequest () {
				UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
				DeviceID = mDeviceInfo.GetUniqueIdentifier (),
				PropertyID = ItemDetail.CottageID,
				Like = "0",
				RejectReason = "Rejected",
				TooFar = this.TooFar,
				TooClose = this.TooClose,
				BadArea = this.BadArea,
				TooSmall = this.TooSmall,
				HouseTooBig = this.HouseTooBig,
				LotTooBig = this.LotTooBig,
				Ugly = this.Ugly,
				LotTooSmall = this.LotTooSmall
			});

			this.IsLoading = false;

			// set to database
			if (likeInfo != null) {
				TREntityLikes likeData = new TREntityLikes () {
					UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
					PropertyID = likeInfo.PropertyID,
					LikeDislike = false,
					Price = ItemDetail.Price,
					PrimaryPhotoURL = ItemDetail.PrimaryPhotoLink,
					Address = ItemDetail.Address.FullStreetAddress,
					CityStateZip = ItemDetail.FormattedAddress
				};

				TRDatabase.Instance.SaveItem (likeData);
			}

			Liked = false;
			Disliked = true;
			this.IsDislikeShown = false;

			DataHelper.SearchResults.Remove (DataHelper.SearchResults.Where (i => i.CottageID == ItemDetail.CottageID).FirstOrDefault());
		}

		private async void DoShowDislikeDialog (bool isShow)
		{
			if (isShow) {
				if (_disliked) {
					this.IsLoading = true;
					this.LoadingText = "Clearing Dislike";
					if (await mTRService.ClearLikeDislike (mDeviceInfo.GetUniqueIdentifier (), mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), false, ItemDetail.CottageID)) {
						Disliked = false;
						TRDatabase.Instance.RemoveCottageLikeInfo (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), ItemDetail.CottageID);
					} 

					this.IsLoading = false;
				} else {
					IsDislikeShown = true;
				}
			} else {
				IsDislikeShown = false;
			}
		}
		#endregion
	}
}

