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
using ThisRoofN.Models.Service;
using ThisRoofN.Extensions;
using ThisRoofN.Models.App;
using ThisRoofN.Database;

namespace ThisRoofN.RestService
{
	public class TRService : TRBaseService
	{
		private const string endpoint_testAPI = "api/v5/test_api";
		private const string endpoint_login = "api/v5/login";
		private const string endpoint_signup = "api/v5/signup";
		private const string endpoint_registerUserSearchFilter = "api/v5/userapp/create";
		private const string endpoint_search = "api/v5/search";
		private const string endpoint_like = "api/v5/like";
		private const string endpoint_getLikes = "api/v5/get_likes";
		private const string endpoint_clear = "api/v5/clear";
		private const string endpoint_mortgageAfford = "api/v5/mortgage_afford";
		private const string endpoint_commutePolygon = "api/v5/commute_polygon";
		private const string endpoint_getUserSearchFilter = "api/v5/userapp";

		public TRService ()
		{
			baseURL = TRConstant.TRServiceBaseURL;
		}

		public async Task<bool> IsTokenValid()
		{
			TRSuccessModel response = await CallRestAPI<TRSuccessModel>(endpoint_testAPI, null, HTTP_METHOD.GET);
			return response.Success;
		}

		public async Task<TRUser> Login(string email, string password)
		{
			var request = new {
				email = email,
				password = password,
				provider = TRConstant.TRDefaultOAuthProvider
			};

			return await CallRestAPI<TRUser> (endpoint_login, JsonConvert.SerializeObject (request));
		}

		public async Task<TRUser> FacebookLogin(FBUserInfo fbUserInfo)
		{
			return await CallRestAPI<TRUser> (endpoint_login, JsonConvert.SerializeObject (fbUserInfo));
		}

		public async Task<TRUser> Signup(string email, string password)
		{
			var request = new {
				email = email,
				password = password,
				provider = TRConstant.TRDefaultOAuthProvider
			};

			return await CallRestAPI<TRUser> (endpoint_signup, JsonConvert.SerializeObject (request));
		}

		public async Task<SearchFilters> UpdateUserSearchProperty(SearchFilters searchProperty)
		{
			// First call Rest API
			SearchFilters result = await CallRestAPI<SearchFilters> (endpoint_registerUserSearchFilter, searchProperty.JsonStringForUpdate);

			searchProperty.UserID = result.UserID;
			// Second save to Database
			TRDatabase.Instance.SaveItem(searchProperty);

			return result;
		}

		public async Task<List<CottageSimple>> GetSearchResults(string deviceID, int resultsPerRequest = 10, int page = 1, bool isAutoSearch =false){

			var json = new {
//				mobile_num = "035ed031-1cb1-46aa-ba72-b0943d1896da",
				mobile_num = deviceID,
				page = page.ToString (),
				query_count = resultsPerRequest.ToString (),
				mode = isAutoSearch ? "1" : "0" };

			return await CallRestAPI<List<CottageSimple>> (endpoint_search, JsonConvert.SerializeObject (json));
		}

		public async Task<CottageDetail> GetCottageDetail(string deviceID, string propertyID) {
			string searchDetailURL = string.Format ("{0}/{1}", endpoint_search, propertyID);
			return await CallRestAPI<CottageDetail> (searchDetailURL, null, HTTP_METHOD.GET);
		}

		public async Task<CottageLikeInfo> LikeCottage(CottageLikeRequest request)
		{
			return await CallRestAPI<CottageLikeInfo> (endpoint_like, JsonConvert.SerializeObject (request));
		}

		public async Task<CottageLikeInfo> DislikeCottage(CottageDislikeRequest request)
		{
			return await CallRestAPI<CottageLikeInfo> (endpoint_like, JsonConvert.SerializeObject (request));
		}

		public async Task<bool> ClearLikeDislike(string deviceID, int userID, bool likeDislike, string propertyID) {
			var json = new {
				user_id = userID.ToString(),
				mobile_num = deviceID,
				property_id = propertyID,
				like_dislike = likeDislike
			};

			TRStatusDataModel response = await CallRestAPI<TRStatusDataModel>(endpoint_clear, JsonConvert.SerializeObject(json));
			if (response.status.Equals ("success")) {
				return true;
			} else {
				return false;
			}
		}

		public async Task<List<IPosition>> GetPolygon(string deviceID)
		{
			var feature = await CallRestAPI<Feature> (endpoint_commutePolygon, JsonConvert.SerializeObject (deviceID));

			Polygon polygon = feature.Geometry as Polygon;
			List<IPosition> positions = polygon.Coordinates[0].Coordinates;
			return positions;
		}

		public async Task<AffordSearchResponse> CalcAfford(AffordSearchRequest affordData)
		{
			return await CallRestAPI<AffordSearchResponse> (endpoint_mortgageAfford, JsonConvert.SerializeObject (affordData));
		}
	}
}

