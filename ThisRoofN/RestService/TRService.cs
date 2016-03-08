using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ModernHttpClient;
using System.Collections.Generic;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;

namespace ThisRoofN.RestService
{
	public enum HTTP_METHOD
	{
		POST,
		GET,
		PUT,
		DELETE
	}

	public class TRService
	{
		public static string Token;

		private string baseURL;
		private const string testTokenMethod = "api/v5/test_api";
		private const string loginMethod = "api/v5/login";
		private const string signupMethod = "api/v5/signup";
		private const string searchMethod = "api/v5/search";
		private const string createMethod = "api/v5/userapp/create";
		private const string likeDislikeMethod = "api/v5/like";
		private const string getLikedPropertiesMethod = "api/v5/get_likes";
		private const string clearLikeDislikeMethod = "api/v5/clear";
		private const string morgageAffordability = "api/v5/mortgage_afford";
		private const string getPolygonMethod = "api/v5/commute_polygon";
		private const string getUserAppPreferences = "api/v5/userapp";

		private JsonSerializerSettings jsonSerializeSetting;


		public TRService ()
		{
			baseURL = TRConstant.TRServiceBaseURL;
			jsonSerializeSetting = new JsonSerializerSettings () {
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		private async Task<string> CallRestAPI(string url, string body, HTTP_METHOD httpMethod = HTTP_METHOD.POST)
		{
			string result = string.Empty;
			HttpResponseMessage response = null;
			using (var httpClient = new HttpClient (new NativeMessageHandler())) {
				try
				{
					httpClient.BaseAddress = new Uri (baseURL);
					httpClient.DefaultRequestHeaders.Accept.Clear ();
					httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
					if (!string.IsNullOrEmpty (Token)) {
						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", Token);
					}


					switch (httpMethod) {
					case HTTP_METHOD.GET:
						response = await httpClient.GetAsync(url);
						break;
					case HTTP_METHOD.POST:
						response = await httpClient.PostAsync(url, new StringContent(body, System.Text.Encoding.UTF8, "application/json"));
						break;
					case HTTP_METHOD.PUT:
						response = await httpClient.PutAsync(url, new StringContent(body, System.Text.Encoding.UTF8, "application/json"));
						break;
					case HTTP_METHOD.DELETE:
						response = await httpClient.DeleteAsync(url);
						break;
					}

					response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
					result = await response.Content.ReadAsStringAsync();
				} catch(Exception ex) {
					Xamarin.Insights.Report (ex, new Dictionary<string, string> {
						{"Exception Time", DateTime.Now.ToString() },
						{"URL", url },
						{"BODY", body }
					}, Xamarin.Insights.Severity.Error);

					MvxTrace.Trace(String.Format("Exception in RestService URL: {0},\nBody: {1}\nDescription:{2}", url, body, ex.Message));
				}
			}

			return result;
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
					{"Description", "Parse Failed during login"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during Loing API Target:{0}", response);
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
					{"Description", "Parse Failed during login"},
					{"Target String", response},
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.Trace ("Parse Failed during Loing API Target:{0}", response);
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


	}
}

