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

namespace ThisRoofN.ViewModels
{
	public class SearchResultDetailViewModel : BaseViewModel
	{
		private TRCottageDetail itemDetail;

		private MvxCommand _descMoreCommand;
		private MvxCommand _likeCommand;
		private MvxCommand<bool> _showDislikeCommand;
		private MvxCommand _dislikeCommand;
		private MvxCommand _findAgentCommand;

		private bool _liked;
		private bool _disliked;
		private bool _isDislikeShown;

		public bool _tooFar { get; set; }

		public bool _tooClose { get; set; }

		public bool _badArea { get; set; }

		public bool _tooSmall { get; set; }

		public bool _tooBig { get; set; }

		public bool _lotTooBig { get; set; }

		public bool _lotTooSmall { get; set; }

		public bool _ugly { get; set; }

		public SearchResultDetailViewModel ()
		{
			itemDetail = DataHelper.SelectedCottageDetail;
			_liked = itemDetail.Liked;
			_disliked = itemDetail.Disliked;
		}

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

		public TRCottageDetail ItemDetail {
			get {
				return itemDetail;
			} 
			set {
				itemDetail = value;
				RaisePropertyChanged (() => ItemDetail);
			}
		}

		public ICommand DescMoreCommand {
			get {
				_descMoreCommand = _descMoreCommand ?? new MvxCommand (DoDescMore);
				return _descMoreCommand;
			}
		}

		public ICommand FindAgentCommand {
			get {
				_findAgentCommand = _findAgentCommand ?? new MvxCommand (() => {
					ShowViewModel<TRWebViewModel>(new {link = TRConstant.BuyersAgentLink});
				});
				return _findAgentCommand;
			}
		}

		public ICommand LikeCommand {
			get {
				_likeCommand = _likeCommand ?? new MvxCommand (DoLike);
				return _likeCommand;
			}
		}

		public ICommand ShowDislikeViewCommand {
			get {
				_showDislikeCommand = _showDislikeCommand ?? new MvxCommand<bool> (DoShowDislikeDialog);
				return _showDislikeCommand;
			}
		}

		public ICommand DisLikeCommand {
			get {
				_dislikeCommand = _dislikeCommand ?? new MvxCommand (DoDislike);
				return _dislikeCommand;
			}
		}

		public bool IsDislikeHidden {
			get {
				return !IsDislikeShown;
			}
		}

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

		public bool Liked {
			get {
				return _liked;
			}
			set {
				_liked = value;
				RaisePropertyChanged (() => Liked);
			}
		}

		public bool Disliked {
			get {
				return _disliked;
			}
			set {
				_disliked = value;
				RaisePropertyChanged (() => Disliked);
			}
		}


		private async void DoLike ()
		{
			if (_liked) {
				this.IsLoading = true;
				this.LoadingText = "Clearing Like";
				if (await mTRService.ClearLikeDislike (mDeviceInfo.GetUniqueIdentifier (), mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), true, itemDetail.CottageID)) {
					Liked = false;
					TRDatabase.Instance.RemoveCottageLikeInfo (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), itemDetail.CottageID);
				} 
				this.IsLoading = false;
			} else {

				// Set like to cottage
				CottageLikeRequest request = new CottageLikeRequest () {
					UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
					DeviceID = mDeviceInfo.GetUniqueIdentifier (),
					PropertyID = itemDetail.CottageID,
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
						Latitude = itemDetail.Latitude,
						Longitude = itemDetail.Longitude,
						PrimaryImageLink = itemDetail.PrimaryPhotoLink
					};

					TRDatabase.Instance.SaveItem (likeData);
				}

				Liked = true;
			}
		}

		private async void DoShowDislikeDialog (bool isShow)
		{
			if (isShow) {
				if (_disliked) {
					this.IsLoading = true;
					this.LoadingText = "Clearing Dislike";
					if (await mTRService.ClearLikeDislike (mDeviceInfo.GetUniqueIdentifier (), mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), false, itemDetail.CottageID)) {
						Disliked = false;
						TRDatabase.Instance.RemoveCottageLikeInfo (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), itemDetail.CottageID);
					} 

					this.IsLoading = false;
				} else {
					IsDislikeShown = true;
				}
			} else {
				IsDislikeShown = false;
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
				PropertyID = itemDetail.CottageID,
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
					Latitude = itemDetail.Latitude,
					Longitude = itemDetail.Longitude,
					PrimaryImageLink = itemDetail.PrimaryPhotoLink
				};

				TRDatabase.Instance.SaveItem (likeData);
			}

			Disliked = true;
			this.IsDislikeShown = false;
		}

		public void DoDescMore ()
		{
			// Full description should be displayed
		}


	}
}

