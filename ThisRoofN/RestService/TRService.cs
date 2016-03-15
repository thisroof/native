using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ModernHttpClient;
using System.Collections.Generic;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;
using ThisRoofN.Models;
using ThisRoofN.Interfaces;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace ThisRoofN.RestService
{
	public class TRService : TRBaseService
	{
		private const string testTokenMethod = "api/v5/test_api";
		private const string loginMethod = "api/v5/login";
		private const string signupMethod = "api/v5/signup";
		private const string createMethod = "api/v5/userapp/create";
		private const string searchMethod = "api/v5/search";
		private const string likeDislikeMethod = "api/v5/like";
		private const string getLikedPropertiesMethod = "api/v5/get_likes";
		private const string clearLikeDislikeMethod = "api/v5/clear";
		private const string morgageAffordability = "api/v5/mortgage_afford";
		private const string getPolygonMethod = "api/v5/commute_polygon";
		private const string getUserAppPreferences = "api/v5/userapp";

		public TRService ()
		{
			baseURL = TRConstant.TRServiceBaseURL;
		}

		public async Task<bool> IsTokenValid()
		{
			bool res = false;
			string response = string.Empty;
			string testTokenURL = string.Format ("{0}?api_key={1}", testTokenMethod, TRConstant.TRServiceKey);

			try
			{
				response = await CallRestAPI (testTokenURL, null, HTTP_METHOD.GET);
				var definition = new {Success = false};
				var formattedResponse = JsonConvert.DeserializeAnonymousType(response, definition, jsonSerializeSetting);
				if(formattedResponse != null) {
					res = formattedResponse.Success;
				}
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during IsTokenValid"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during IsTokenValid API Target:{0}", response);
			}

			return res;
		}

		public async Task<TRUser> Login(string email, string password)
		{
			TRUser result = null;
			string response = String.Empty;
			MvxTrace.Trace ("Login with email:{0}, password:{1}", email, password); 
			var request = new {
				email = email,
				password = password,
				provider = TRConstant.TRDefaultOAuthProvider
			};

			try
			{
				response = await CallRestAPI (loginMethod, JsonConvert.SerializeObject (request));
				result = JsonConvert.DeserializeObject<TRUser>(response, jsonSerializeSetting);
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during login"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during Loing API Target:{0}", response);
			}

			return result;
		}

		public async Task<TRUser> FacebookLogin(FBUserInfo fbUserInfo)
		{
			TRUser result = null;
			string response = String.Empty;
			MvxTrace.Trace ("Facebook Loing with email:{0}", fbUserInfo.UserEmail); 
			fbUserInfo.Provider = TRConstant.TRFBOAuthProvider;

			try
			{
				response = await CallRestAPI (loginMethod, JsonConvert.SerializeObject (fbUserInfo));
				result = JsonConvert.DeserializeObject<TRUser>(response, jsonSerializeSetting);
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during fblogin"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during FBLogin API Target:{0}", response);
			}

			return result;
		}

		public async Task<TRUser> Signup(string email, string password)
		{
			TRUser result = null;
			string response = String.Empty;
			MvxTrace.Trace ("Signup with email:{0}, password:{1}", email, password); 
			var request = new {
				email = email,
				password = password,
				provider = TRConstant.TRDefaultOAuthProvider
			};

			try
			{
				response = await CallRestAPI (signupMethod, JsonConvert.SerializeObject (request));
				result = JsonConvert.DeserializeObject<TRUser>(response, jsonSerializeSetting);
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during signup"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during Signup API Target:{0}", response);
			}

			return result;
		}

		public async Task<TRUserSearchProperty> UpdateUserSearchProperty(TRUserSearchProperty searchProperty)
		{
			TRUserSearchProperty result = null;
			string response = String.Empty;

			var json = new {
//				mobile_num = "035ed031-1cb1-46aa-ba72-b0943d1896da",
				mobile_num = searchProperty.MobileNum,
				first_name = "",
				last_name = "",
				latitude = searchProperty.GeoLat.ToString(),
				longitude = searchProperty.GeoLng.ToString(),
				email = "",
				altitude = "0",
				address = searchProperty.Address ?? "",
				city = searchProperty.City ?? "",
				state = searchProperty.State ?? "",
				zip_code = searchProperty.Zip ?? "0",
				country = "US",//usersAppPreferences.Country ?? "",
				value = searchProperty.Value.ToString () ,
				bedrooms = searchProperty.Bedrooms.ToString (),
				baths_full = searchProperty.BathsFull.ToString (),
				min_square_footage_structure = searchProperty.MinSquareFootageStructure.ToString (),
				max_square_footage_structure = searchProperty.MaxSquareFootageStructure.ToString (),
				min_lot_square_footage = searchProperty.MinLotSquareFootage.ToString (),
				max_lot_square_footage = searchProperty.MaxLotSquareFootage.ToString (),
				max_budget = searchProperty.MaxBudget,
				min_beds = searchProperty.MinBeds.ToString(),
				min_baths = searchProperty.MinBaths.ToString(),
				start_zip = searchProperty.StartZip ?? "0",
				search_type = searchProperty.SearchType.ToString (),
				// search_dist = Utils.GetSearchDistanceValue(usersAppPreferences.SearchDistance),
				search_dist = searchProperty.SearchDistance.ToString(),
				metricus = searchProperty.MetricUS ?? "0",
				proptypes = searchProperty.PropertyTypes.GetPropertyTypeValue(),
				dislike_zipcodes = searchProperty.DislikeZipcodes,
				traffic_type = searchProperty.TrafficType.ToString (),
				travel_mode = searchProperty.TravelMode.ToString (),
				state_filters = searchProperty.StateFilters,
				year_built = searchProperty.YearBuilt.ToString(),
				days_on_market = searchProperty.DaysOnMarket.ToString(),
				has_pool = searchProperty.HasPool,
				view_types = searchProperty.ViewTypes,
				foreclosure_status = searchProperty.ForeclosureStatus,
				sort_by = searchProperty.SortBy,
				keywords = searchProperty.Keywords
			};

			try
			{
				string serialized = JsonConvert.SerializeObject (json);
				response = await CallRestAPI (createMethod, serialized);
				result = JsonConvert.DeserializeObject<TRUserSearchProperty>(response, jsonSerializeSetting);
				result.UserID = result.ID;
				result.ID = searchProperty.ID;
				searchProperty.UserID = result.ID;
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during UpdateUserSearchProperty"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during UpdateUserSearchProperty Target:{0}", response);
			}
				
			return result;
		}

		public async Task<List<TRCottage>> GetSearchResults(string deviceID, int resultsPerRequest = 10, int page = 1, bool isAutoSearch =false){
			string response = string.Empty;
			List<TRCottage> result = new List<TRCottage>();

			var json = new {
//				mobile_num = "035ed031-1cb1-46aa-ba72-b0943d1896da",
				mobile_num = deviceID,
				page = page.ToString (),
				query_count = resultsPerRequest.ToString (),
				mode = isAutoSearch ? "1" : "0" };
			
			try {
				string serialized = JsonConvert.SerializeObject (json);
				response = await CallRestAPI(searchMethod, serialized);
				result = JsonConvert.DeserializeObject<List<TRCottage>> (response, jsonSerializeSetting);
			} catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during GetSearchResults"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during GetSearchResults, Target:{0}", response);
			}

			return result;
		}

		public async Task<TRCottageDetail> GetCottageDetail(string deviceID, string propertyID) {
			TRCottageDetail detail = new TRCottageDetail();

			string response = string.Empty;
			string searchDetailURL = string.Format ("{0}/{1}", searchMethod, propertyID);

			try
			{
				response = await CallRestAPI (searchDetailURL, null, HTTP_METHOD.GET);
				detail = JsonConvert.DeserializeObject<TRCottageDetail>(response);
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during GetCottageDetail"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during GetCottageDetail, Target:{0}", response);
			}

			return detail;
		}

		public async Task<bool> LikeProperty(TRSetLikeRequest request)
		{
			TRLikeStatus res = null;
			string response = string.Empty;
			try
			{
				var serialized = JsonConvert.SerializeObject(request);
				response = await CallRestAPI(likeDislikeMethod, serialized);
				res = JsonConvert.DeserializeObject<TRLikeStatus>(response, jsonSerializeSetting);
			}
			catch (Exception ex)
			{
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during SetLikeProperty"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during SetLikeProperty, Target:{0}", response);
			}


			if (res != null && res.PropertyID != null) {
				return true;
			} else {
				return false;
			}
		}

		public async Task<bool> DislikeProperty(TRSetDisLikeRequest request)
		{
			TRLikeStatus res = null;
			string response = string.Empty;

			try {
				var serialized = JsonConvert.SerializeObject(request);
				response = await CallRestAPI(likeDislikeMethod, serialized);
				res = JsonConvert.DeserializeObject<TRLikeStatus>(response, jsonSerializeSetting);
			}
			catch (Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during SetLikeProperty"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during SetLikeProperty, Target:{0}", response);
			}

			if (res != null && res.PropertyID != null) {
				return true;
			} else {
				return false;
			}
		}

		public async Task<bool> ClearLikeDislike(string deviceID, int userID, bool likeDislike, string propertyID) {
			bool res = false;
			string response = string.Empty;

			try {
				var json = new {
					user_id = userID.ToString(),
					mobile_num = deviceID,
					property_id = propertyID,
					like_dislike = likeDislike
				};

				var serialized = JsonConvert.SerializeObject(json);
				response = await CallRestAPI(clearLikeDislikeMethod, serialized);

				var definition = new {status = string.Empty, data = string.Empty};
				var formatRes = JsonConvert.DeserializeAnonymousType(response, definition, jsonSerializeSetting);
				if(formatRes.status.Equals("success"))
				{
					res = true;
				}
			}
			catch(Exception ex) {
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during ClearLikeDislike"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during ClearLikeDislike, Target:{0}", response);
			}

			return res;
		}

		public async Task<List<IPosition>> GetPolygon(string deviceID)
		{
			string response = string.Empty;
			List<IPosition> positions = new List<IPosition>();

			var json = new {
				mobile_num = deviceID,
			};

			var serialized = JsonConvert.SerializeObject(deviceID);

			try
			{
				response = await CallRestAPI(getPolygonMethod, serialized);
				var feature = JsonConvert.DeserializeObject<Feature>(serialized);
				Polygon polygon = feature.Geometry as Polygon;
				positions = polygon.Coordinates[0].Coordinates;
			}
			catch (Exception ex)
			{
				Xamarin.Insights.Report (ex, new Dictionary<string, string> {
					{"Exception Time", DateTime.Now.ToString() },
					{"Description", "Parse Failed during GetPolygon"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during GetPolygon, Target:{0}", response);
			}

			return positions;
		}
	}
}

