using System;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using ThisRoofN.Models.Service;
using ThisRoofN.Database.Entities;
using ThisRoofN.Database;
using System.Text.RegularExpressions;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;

namespace ThisRoofN.Models.App
{
	public class TRCottageDetail : TRCottageSimple
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ThisRoofN.Models.App.TRCottageDetail"/> class.
		/// Convert server side detail info to app side for easy usage in view model classes
		/// </summary>
		/// <param name="serverCottageDetail">server side cottage detail</param>
		/// /// <param name="simpleData">selected simple data</param>
		public TRCottageDetail(CottageDetail serverData, TRCottageSimple simpleData) {
			CottageID = simpleData.CottageID;
			PrimaryPhotoLink = simpleData.PrimaryPhotoLink;
			Latitude = simpleData.Latitude;
			Longitude = simpleData.Longitude;

			Price = serverData.Data.ListPrice;
			Description = serverData.Data.PropertyDescription;
			Bedrooms = serverData.Data.Bedrooms;
			Bathrooms = (int)serverData.Data.Bathrooms;

			double squareFootageStructure = 0;
			double.TryParse (serverData.Data.SquareFootageStructure, out squareFootageStructure);
			SquareFootageStructure = (int)squareFootageStructure;
			LotSquareSize = serverData.LotSquareSize;
			Acres = serverData.Data.Acres;
			Photos = serverData.Data.Photos;
			Address = serverData.Data.AddressData;

			if (serverData.Data.Participants != null && serverData.Data.Participants.Count > 0) {
				Participant = serverData.Data.Participants [0];
			} else {
				Participant = new CottageParticipant ();
			}
		}

		public TRCottageDetail(CottageDetail serverData) {
			CottageID = serverData.Data.MatrixUniqueID;
			PrimaryPhotoLink = serverData.PrimaryPhotoUrl;
			Latitude = serverData.Data.Location.Latitude;
			Longitude = serverData.Data.Location.Longitude;

			Price = serverData.Data.ListPrice;
			Description = serverData.Data.PropertyDescription;
			Bedrooms = serverData.Data.Bedrooms;
			Bathrooms = (int)serverData.Data.Bathrooms;

			double squareFootageStructure = 0;
			double.TryParse (serverData.Data.SquareFootageStructure, out squareFootageStructure);
			SquareFootageStructure = (int)squareFootageStructure;
			LotSquareSize = serverData.LotSquareSize;
			Acres = serverData.Data.Acres;
			Photos = serverData.Data.Photos;
			Address = serverData.Data.AddressData;

			if (serverData.Data.Participants != null && serverData.Data.Participants.Count > 0) {
				Participant = serverData.Data.Participants [0];
			} else {
				Participant = new CottageParticipant ();
			}
		}
			
		public double 					Price { get; set; }
		public string 					Description { get; set; }

		public int 						Bedrooms { get; set; }
		public int						Bathrooms { get; set; }
		public int						SquareFootageStructure {get;set;}
		public int						LotSquareSize {get;set;}
		public string 					Acres {get;set;}


		public List<CottagePhoto> 		Photos { get; set; }
		public CottageAddress			Address { get; set; }
		public CottageParticipant 		Participant {get;set;}

		public string FormattedPrice {
			get {
				return string.Format ("{0:C0}", this.Price);
			}
		}

		public string ShortenedDescription { 
			get {
				if (string.IsNullOrEmpty (Description))
					return string.Empty;
				string res = string.Empty;
				string description = Regex.Replace (Description, @"\s{2,}", " ");
				description.Replace ('\r', ' ');
				description.Replace ('\n', ' ');

				res = (!string.IsNullOrEmpty (description) && description.Length > 150) ? 
					description.Substring (0, 1) + description.Substring (1, 149).ToLower () + "..." : 
					description;			
				return res;
			}
		}
		public bool HasLargeDescription { 
			get {
				return !string.IsNullOrEmpty (Description) && Description.Length > 150;
			}
		}

		public string FormattedCityStateZip {
			get {
				return string.Format ("{0}, {1} {2}", Address.City, Address.State, Address.ZipCode) ;
			}
		}

		public string FormattedBedBath
		{
			get {
				return string.Format ("{0} BEDS, {1} BATHS", this.Bedrooms, this.Bathrooms);
			}
		}

		public string FormattedSquareFootageStructure
		{
			get {
				return string.Format ("{0:N0} SQFT", SquareFootageStructure);
			}
		}

		public string FormattedAcreValue {
			get {
				if (LotSquareSize >= 43000) {
					double acreValue = (double)LotSquareSize / (double)43000;
					return string.Format ("{0:N2}", acreValue);
				} else {
					return string.Empty;
				}
			}
		}

		public bool IsFormattedAcreValueShow {
			get {
				if (LotSquareSize >= 43000) {
					return true;
				} else {
					return false;
				}
			}
		}

		public bool IsFormattedAcreValueHide {
			get {
				return !IsFormattedAcreValueShow;
			}
		}

		public string FormattedAddress {
			get {
				return this.Address.FullStreetAddress.ToUpper ();
			}
		}

		public bool IsLotSquareVisible {
			get { return LotSquareSize > 0 && LotSquareSize < 43000; }
		}

		public bool IsAcresVisible {
			get { return LotSquareSize >= 43000; }
		}

		public bool Liked
		{
			get {
				IUserPreference mUserPref = Mvx.Resolve<IUserPreference> ();
				TREntityLikes item = TRDatabase.Instance.GetCottageLikeInfo (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), CottageID);
				if(item != null && item.LikeDislike == true) {
					return true;
				} else {
					return false;
				}
			}
		}

		public bool Disliked
		{
			get {
				IUserPreference mUserPref = Mvx.Resolve<IUserPreference> ();
				TREntityLikes item = TRDatabase.Instance.GetCottageLikeInfo (mUserPref.GetValue (TRConstant.UserPrefUserIDKey, 0), CottageID);
				if(item != null && item.LikeDislike == false) {
					return true;
				} else {
					return false;
				}
			}
		}
	}
}

