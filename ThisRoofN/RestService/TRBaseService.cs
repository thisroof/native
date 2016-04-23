using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;
using System.Net.Http.Headers;
using MvvmCross.Platform.Platform;
using System.Collections.Generic;
using System.Net;

namespace ThisRoofN.RestService
{
	public class TRBaseService
	{
		public enum HTTP_METHOD
		{
			POST,
			GET,
			PUT,
			DELETE
		}

		public static string Token;
		protected string baseURL;
		protected JsonSerializerSettings jsonSerializeSetting;

		public TRBaseService() {
			jsonSerializeSetting = new JsonSerializerSettings () {
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		protected async Task<T> CallRestAPI<T> (string url, string body, HTTP_METHOD httpMethod = HTTP_METHOD.POST) {
			string jsonResponse = string.Empty;
			T result = default(T);

			try
			{
				jsonResponse = await CallRestAPI(url, body, httpMethod);

				if(!string.IsNullOrEmpty(jsonResponse)) {
					result = JsonConvert.DeserializeObject<T>(jsonResponse, jsonSerializeSetting);
				}
			}
			catch (Exception e) {
				Xamarin.Insights.Report (e, new Dictionary<string, string> {
					{"Error Description", "Exception while parsing service response"},
					{"Request Link", url },
					{"Response Data", jsonResponse }
				}, Xamarin.Insights.Severity.Error);

				MvxTrace.TaggedTrace("REST_API_PARSE_EXCEPTION", string.Format("\n\nURL: {0}", url));
			}

			return result;
		}

		protected async Task<string> CallRestAPI(string url, string body, HTTP_METHOD httpMethod = HTTP_METHOD.POST)
		{
			string result = string.Empty;
			HttpResponseMessage response = null;
			using (var httpClient = new HttpClient (new NativeMessageHandler())) {
				try
				{
					httpClient.BaseAddress = new Uri (baseURL);
					httpClient.Timeout = TimeSpan.FromSeconds(200);
					httpClient.DefaultRequestHeaders.Accept.Clear ();
					httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
					if (!string.IsNullOrEmpty (Token)) {
						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", Token);
					}
					MvxTrace.TaggedTrace("REST_API_REQUEST", string.Format("\n\nURL: {0}", url));
					System.Diagnostics.Debug.WriteLine(body);

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
					MvxTrace.TaggedTrace("REST_API_NORMAL_RESPONSE", string.Format("\n\nURL: {0}", url));
					System.Diagnostics.Debug.WriteLine(result);

					if(response.StatusCode == HttpStatusCode.OK) {
						return result;
					} else {
						return null;
					}
				} catch(Exception ex) {
					Xamarin.Insights.Report (ex, new Dictionary<string, string> {
						{"Error Description", "Exception while calling rest service"},
						{"Reqeust Link", url },
						{"Request Data", body }
					}, Xamarin.Insights.Severity.Error);

					MvxTrace.TaggedTrace("REST_API_EXCEPTON_RESPONSE", string.Format("\n\nURL: {0}\n Exception: {1}", url, ex.Message));
				}
			}

			return result;
		}
	}
}

