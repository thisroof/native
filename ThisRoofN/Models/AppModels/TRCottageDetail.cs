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
		private CottageDetail serverDetailData;
		public TRCottageDetail(CottageDetail serverData) {
			serverDetailData = serverData;

			CottageID = serverData.Data.MatrixUniqueID;
			PrimaryPhotoLink = serverData.PrimaryPhotoUrl;
			Latitude = serverData.Data.Location.Latitude;
			Longitude = serverData.Data.Location.Longitude;
			Price = serverData.Data.ListPrice;
			Bedrooms = serverData.Data.Bedrooms;
			Bathrooms = (int)serverData.Data.Bathrooms;
			Description = serverData.Data.PropertyDescription;
			LotSquareSize = serverData.LotSquareSize;
			Sqft = (int)serverData.Sqft;

			Sataus = serverData.Data.Status;
			PropertyType = serverData.Data.PropertyType;
			MlsNumber = serverData.Data.MlsNumber;
			YearBuilt = serverData.Data.YearBuilt;

			Photos = serverData.Data.Photos;
			Address = serverData.Data.AddressData;

			if (serverData.Data.Participants != null && serverData.Data.Participants.Count > 0) {
				Participant = serverData.Data.Participants [0];
			} else {
				Participant = new CottageParticipant ();
			}
		}

		public string 					Description { get; set; }
		public int 						Bedrooms { get; set; }
		public int						Bathrooms { get; set; }
		public int						LotSquareSize {get;set;}
		public int 						Sqft { get; set; }

		public string 					Sataus { get; set; }
		public string 					PropertyType {get;set;}
		public string 					YearBuilt {get;set;}
		public string 					MlsNumber { get; set; }



		public List<CottagePhoto> 		Photos { get; set; }
		public CottageAddress			Address { get; set; }
		public CottageParticipant 		Participant {get;set;}

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

		public string FormattedAddress {
			get {
				return string.Format ("{0}\n{1}, {2} {3}", Address.FullStreetAddress, Address.City, Address.State, Address.ZipCode) ;
			}
		}

		public int DaysOnMarket {
			get {
				if (serverDetailData.Data.ListingDate.Year < 1900) {
					return 0;
				} else {
					TimeSpan difference = DateTime.Now - serverDetailData.Data.ListingDate;
					return (int)difference.TotalDays;
				}
			}
		}

		public string ListedBy { 
			get {
				if (serverDetailData.Data.Participants.Count > 0) {
					return serverDetailData.Data.Participants [0].FirstName + " " + serverDetailData.Data.Participants [0].LastName;
				} else {
					return "Unknown";
				}
			}
		}
	}
}

