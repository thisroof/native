using System;
using System.Collections.Generic;
using System.Linq;
using ThisRoofN.Models;
using System.Text.RegularExpressions;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace ThisRoofN.ViewModels
{
	public class SearchResultDetailViewModel : BaseViewModel
	{
		private TRItemDetail itemDetail;

		private MvxCommand _descMoreCommand;
		private MvxCommand _likeCommand;
		private MvxCommand<bool> _showDislikeCommand;
		private MvxCommand _dislikeCommand;

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
			if (DataHelper.SelectedDetail != null) {
				TRCottageDetail item = DataHelper.SelectedDetail;

				string la_firstName = string.Empty;
				string la_lastName = string.Empty;
				string la_showingContactName = string.Empty;
				string la_showingContactPhone = string.Empty;
				string la_companyName = string.Empty;

				if (item.Listing.Participants != null && item.Listing.Participants.Count > 0) {
					la_firstName = item.Listing.Participants [0].FirstName;
					la_lastName = item.Listing.Participants [0].LastName;
					la_showingContactName = string.Format ("{0} {1}", la_firstName, la_lastName);
					la_showingContactPhone = item.Listing.Participants [0].OfficePhone;
				}

				if (item.Listing.Offices != null && item.Listing.Offices.Count > 0) {
					la_companyName = item.Listing.Offices [0].Name;
				}

				List<TileItemModel> gallery = new List<TileItemModel> ();
				if (item.Listing.Photos != null && item.Listing.Photos.Count > 0) {
					gallery = item.Listing.Photos.
						Select (i => 
							new TileItemModel {
						ImageUrl = i.MediaURL
					}).ToList ();
				}

				double squareFootageStructure = 0;
				double.TryParse (item.Listing.SquareFootageStructure, out squareFootageStructure);

				itemDetail = new TRItemDetail () {
					PropertyID = item.Listing.MatrixUniqueID,
					Description = item.Listing.PropertyDescription,
					ShortenedDescription = this.GetShortenedDescription (item.Listing.PropertyDescription),
					HasLargeDescription = (!string.IsNullOrEmpty (item.Listing.PropertyDescription) && item.Listing.PropertyDescription.Length > 150),
					LA_FirstName = la_firstName,
					LA_LastName = la_lastName,
					LA_DreLicenseNumber = item.LicenseNumber,
					LA_CompanyName = la_companyName,
					LA_ShowingContactName = la_showingContactName,
					LA_ShowingContactPhone = la_showingContactPhone,
					Bedrooms = item.Listing.Bedrooms,
					BathsFull = item.Listing.Bathrooms,
					SquareFootageStructure = (int)squareFootageStructure,
					LotSquareFootage = item.LotSquareSize,
					Acres = item.Listing.Acres,
					Value = item.Listing.ListPrice,
					GeoLat = item.Listing.Location.Latitude,
					GeoLng = item.Listing.Location.Longitude,
					Address = item.Address,
					City = item.Listing.AddressData.City,
					State = item.Listing.AddressData.State,
					Zip = item.Listing.AddressData.ZipCode,
					Country = item.Listing.AddressData.Country, 
					PrimaryPhoto = item.PrimaryPhotoUrl,
					GalleryID = item.Listing.MatrixUniqueID,
					Gallery = gallery,
					//					Liked = propertyManager.IsLiked (item.Listing.MatrixUniqueID),
					//					Disliked = propertyManager.IsDisliked (item.Listing.MatrixUniqueID)
				};
			}
		}

		#region Reject Reason Properties

		public bool TooFar { 
			get { return this._tooFar; } 
			set { 
				this._tooFar = value;
				RaisePropertyChanged (() => TooFar);
			} }
		public bool TooClose { 
			get { return this._tooClose; } 
			set { 
				this._tooClose = value;
				RaisePropertyChanged (() => TooClose);
			} }

		public bool BadArea { 
			get { return this._badArea; } 
			set { 
				this._badArea = value;
				RaisePropertyChanged (() => BadArea);
			} }

		public bool TooSmall { 
			get { return this._tooSmall; } 
			set { 
				this._tooSmall = value;
				RaisePropertyChanged (() => TooSmall);
			} }

		public bool HouseTooBig { 
			get { return this._tooBig; } 
			set { 
				this._tooBig = value;
				RaisePropertyChanged (() => HouseTooBig);
			} }

		public bool LotTooBig { 
			get { return this._lotTooBig; } 
			set { 
				this._lotTooBig = value;
				RaisePropertyChanged (() => LotTooBig);
			} }

		public bool LotTooSmall { 
			get { return this._lotTooSmall; } 
			set { 
				this._lotTooSmall = value;
				RaisePropertyChanged (() => LotTooSmall);
			} }

		public bool Ugly{ 
			get { return this._ugly; } 
			set { 
				this._ugly = value;
				RaisePropertyChanged (() => Ugly);
			} }

		#endregion

		public TRItemDetail ItemDetail {
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

		public ICommand LikeCommand {
			get {
				_likeCommand = _likeCommand ?? new MvxCommand (DoLike);
				return _likeCommand;
			}
		}

		public ICommand ShowDislikeViewCommand {
			get {
				_showDislikeCommand = _showDislikeCommand ?? new MvxCommand<bool> ((isShow) => {
					IsDislikeShown = isShow;
				});
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
				if (await mTRService.ClearLikeDislike (mDeviceInfo.GetUniqueIdentifier (), mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), true, itemDetail.PropertyID)) {
					Liked = false;
				} 
			} else {
				if (await mTRService.LikeProperty (new TRSetLikeRequest () {
					UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
					DeviceID = mDeviceInfo.GetUniqueIdentifier (),
					PropertyID = itemDetail.PropertyID,
					Like = true
				})) {
					Liked = true;
					Disliked = false;
				}
			}
		}

		private async void DoDislike ()
		{
			if (_disliked) {
				if (await mTRService.ClearLikeDislike (mDeviceInfo.GetUniqueIdentifier (), mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), false, itemDetail.PropertyID)) {
					Liked = false;
				} 
			} else {
				if (await mTRService.DislikeProperty (new TRSetDisLikeRequest () {
					UserID = mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0),
					DeviceID = mDeviceInfo.GetUniqueIdentifier (),
					PropertyID = itemDetail.PropertyID,
					Like = false,
					RejectReason = "Rejected",
					TooFar = this.TooFar,
					TooClose = this.TooClose,
					BadArea = this.BadArea,
					TooSmall = this.TooSmall,
					HouseTooBig = this.HouseTooBig,
					LotTooBig = this.LotTooBig,
					Ugly = this.Ugly,
					LotTooSmall = this.LotTooSmall
				})) {
					Liked = false;
					Disliked = true;
				}
			}
		}

		public void DoDescMore ()
		{
			// Full description should be displayed
		}

		private string GetShortenedDescription (string description)
		{
			if (string.IsNullOrEmpty (description))
				return string.Empty;
			string res = string.Empty;
			description = Regex.Replace (description, @"\s{2,}", " ");
			description.Replace ('\r', ' ');
			description.Replace ('\n', ' ');

			res = (!string.IsNullOrEmpty (description) && description.Length > 150) ? 
				description.Substring (0, 1) + description.Substring (1, 149).ToLower () + "..." : 
				description;			
			return res;
		}
	}
}

