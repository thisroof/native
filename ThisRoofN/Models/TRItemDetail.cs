using System;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;

namespace ThisRoofN.Models
{
	public class TRItemDetail : TRUserSearchProperty
	{
		private TRUserSearchProperty _userSearchProperty;

		public TRItemDetail ()
		{
		}

		public TRItemDetail (TRUserSearchProperty _userProperty)
		{
			_userSearchProperty = _userProperty;
		}

		public void SetCalculationParameterReference(TRUserSearchProperty _userProperty)
		{
			_userSearchProperty = _userProperty;
		}

		public string PropertyID { get; set; }
		public string PrimaryPhoto { get; set; }
		public string Description { get; set; }
		public string GalleryID { get; set; }
		public List<TileItemModel> Gallery { get; set; }
		public string ShortenedDescription { get; set; }
		public bool HasLargeDescription { get; set; }
		public bool Liked { get; set; } 
		public bool Disliked { get; set; } 
		public string ThumbnailPhoto { get; set;}

		#region Listing Agent Info
		public string LA_FirstName { get; set; } 
		public string LA_LastName { get; set; } 
		public string LA_DreLicenseNumber { get; set; } 
		public string LA_CompanyName { get; set; } 
		public string LA_ShowingContactName { get; set; } 
		public string LA_ShowingContactPhone { get; set; } 
		#endregion

		#region Formatted Values
		public string FormattedValue {
			get {
				return string.Format ("{0:C0}", this.Value);
			}
		}

		public string FormattedCityStateZip {
			get {
				return CityStateZip.ToUpper ();
			}
		}

		public string FormattedBedBath
		{
			get {
				return string.Format ("{0} BEDS, {1} BATHS", this.Bedrooms, this.BathsFull);
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
				if (LotSquareFootage >= 43000) {
					double acreValue = (double)LotSquareFootage / (double)43000;
					return string.Format ("{0:N2}", acreValue);
				} else {
					return string.Empty;
				}
			}
		}
		#endregion



		public bool HasLikedOrDislike
		{
			get{ return Liked || Disliked; }
		}

		public bool IsLotSquareVisible {
			get { return LotSquareFootage > 0 && LotSquareFootage < 43000; }
		}

		public bool IsAcresVisible {
			get { return LotSquareFootage >= 43000; }
		}



		#region Calculated Properties
		public string PriceDiff
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					NumberFormatInfo numberInfo = NumberFormatInfo.CurrentInfo.Clone() as NumberFormatInfo;
					numberInfo.CurrencyNegativePattern = 1;

					var calculatedValue = this.Value - _userSearchProperty.MaxBudget;
					if (calculatedValue >= 0)
						_colorPrice = Color.Red;

					result = (calculatedValue).ToString("C0", numberInfo);
				}
				return result; 
			}
		}
		Color _colorPrice = Color.Lime;
		public Color PriceTextColor{
			get{ return _colorPrice; }
		}
		public string PriceDiffPercentage
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					result = String.Format ("{0:+#%;-#%;0}", (Value - _userSearchProperty.MaxBudget) / (float)_userSearchProperty.MaxBudget);
				}
				return result; 
			}
		}

		Color _colorSqFt = Color.Red;
		public Color SqFtTextColor{
			get{ return _colorSqFt; }
		}
		bool _showSqFt = false;
		public bool ShowSqFt{
			get{ return _showSqFt; }
		}
		public string SqFtDiff
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					if (_userSearchProperty.MinSquareFootageStructure > 0 || _userSearchProperty.MaxSquareFootageStructure > 0) {
						var calculatedValue = this.SquareFootageStructure - (_userSearchProperty.MinSquareFootageStructure + _userSearchProperty.MaxSquareFootageStructure);
						if (calculatedValue >= 0f)
							_colorSqFt = Color.Lime;
						else
							_colorSqFt = Color.Red;

						if (App.ShowComparisons)
							_showSqFt = true;

						result = String.Format ("{0:+#,###;-#,###;0}", calculatedValue);
					}
				}
				return result;  
			}
		}

		public string SqFtDiffPercentage
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					if (_userSearchProperty.MinSquareFootageStructure > 0 || _userSearchProperty.MaxSquareFootageStructure > 0) {
						result = String.Format ("{0:+#%;-#%;0}", (this.SquareFootageStructure - _userSearchProperty.SquareFootageStructure) / ((float)(_userSearchProperty.MinSquareFootageStructure + _userSearchProperty.MaxSquareFootageStructure)));
					}
				}
				return result; 
			}
		}

		Color _colorBedroom = Color.Gray;
		public Color BedroomTextColor{
			get{ return _colorBedroom; }
		}
		bool _showBed = false;
		public bool ShowBed{
			get{ return _showBed; }
		}
		public string BedroomsDiff
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					if (_userSearchProperty.Bedrooms > 0) {
						var calculatedValue = this.Bedrooms - _userSearchProperty.Bedrooms;
						if (calculatedValue >= 0f)
							_colorBedroom = Color.Lime;
						else
							_colorBedroom = Color.Red;

						if(App.ShowComparisons && calculatedValue != 0)
							_showBed = true;

						result = String.Format ("{0:+#;-#;0}", calculatedValue);
					}
				}
				return result;  
			}
		}

		Color _colorBathroom = Color.Transparent;
		public Color BathroomTextColor{
			get{ return _colorBathroom; }
		}
		bool _showBath = false;
		public bool ShowBath{
			get{ return _showBath; }
		}
		public string BathroomsDiff
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					if (_userSearchProperty.BathsFull > 0) {
						double calculatedValue = this.BathsFull - _userSearchProperty.BathsFull;
						if (calculatedValue > 0)
							_colorBathroom = Color.Lime;
						else
							_colorBathroom = Color.Red;

						if (App.ShowComparisons && calculatedValue != 0)
							_showBath = true;

						result = String.Format ("{0:+0.0#;-0.0#;0}", calculatedValue);
					}
				}
				return result;  
			}
		}
		public bool ShowBedBath{
			get{ return _showBath || _showBed; }
		}
		Color _colorLotDiff = Color.Transparent;
		public Color LotDiffTextColor{
			get{ return _colorLotDiff; }
		}
		bool _showLot = false;
		public bool ShowLot{
			get{ return _showLot; }
		}
		public string LotDiff
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					if (_userSearchProperty.LotSquareFootage > 0) {
						var calculatedValue = this.LotSquareFootage - _userSearchProperty.LotSquareFootage;
						if (calculatedValue >= 0f)
							_colorLotDiff = Color.Lime;
						else
							_colorLotDiff = Color.Red;

						if (App.ShowComparisons)
							_showLot = true;

						result = String.Format ("{0:+#,###;-#,###;0}", calculatedValue);
					}
				}
				return result; 
			}
		}
		public string LotDiffPercentage
		{
			get{
				var result = "N/A";
				if (_userSearchProperty != null) {
					if (_userSearchProperty.LotSquareFootage > 0)
						result = String.Format ("{0:+#,###%;-#,###%;0}", (this.LotSquareFootage - _userSearchProperty.LotSquareFootage) / (float)_userSearchProperty.LotSquareFootage);
				}
				return result; 
			}
		}
		#endregion

	}
}

