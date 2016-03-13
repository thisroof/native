using System;
using System.Collections.Generic;
using System.Linq;
using ThisRoofN.Models;
using System.Text.RegularExpressions;

namespace ThisRoofN.ViewModels
{
	public class SearchResultDetailViewModel : BaseViewModel
	{
		private TRItemDetail itemDetail;

		public SearchResultDetailViewModel ()
		{
		}

		public void Init (string selectedPropertyID)
		{
			if (DataHelper.SearchResults != null) {
				TRSearchResult item = DataHelper.SearchResults
					.Where (i => i.Listing.MatrixUniqueID == selectedPropertyID)
					.FirstOrDefault ();

				string la_firstName = string.Empty;
				string la_lastName = string.Empty;
				string la_showingContactName = string.Empty;
				string la_showingContactPhone = string.Empty;
				string la_companyName = string.Empty;

				if(item.Listing.Participants != null && item.Listing.Participants.Count > 0) {
					la_firstName = item.Listing.Participants[0].FirstName;
					la_lastName = item.Listing.Participants[0].LastName;
					la_showingContactName = string.Format("{0} {1}", la_firstName, la_lastName);
					la_showingContactPhone = item.Listing.Participants[0].OfficePhone;
				}

				if(item.Listing.Offices != null && item.Listing.Offices.Count > 0) {
					la_companyName = item.Listing.Offices[0].Name;
				}

				List<TileItemModel> gallery = new List<TileItemModel>();
				if(item.Listing.Photos != null && item.Listing.Photos.Count > 0) {
					gallery = item.Listing.Photos.
						Select(i => 
							new TileItemModel {
								ImageUrl = i.MediaURL
							}).ToList();
				}

				double squareFootageStructure = 0;
				double.TryParse(item.Listing.SquareFootageStructure, out squareFootageStructure);

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

		public TRItemDetail ItemDetail {
			get {
				return itemDetail;
			} 
			set {
				itemDetail = value;
				RaisePropertyChanged (() => ItemDetail);
			}
		}

		private string GetShortenedDescription(string description)
		{
			if (string.IsNullOrEmpty (description))
				return string.Empty;
			string res = string.Empty;
			description = Regex.Replace (description, @"\s{2,}", " ");
			description.Replace('\r', ' ');
			description.Replace('\n', ' ');

			res = (!string.IsNullOrEmpty (description) && description.Length > 150) ? 
				description.Substring (0, 1) + description.Substring (1, 149).ToLower () + "..." : 
				description;			
			return res;
		}
	}
}

