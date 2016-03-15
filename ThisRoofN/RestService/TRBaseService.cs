using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;
using System.Net.Http.Headers;
using MvvmCross.Platform.Platform;
using System.Collections.Generic;

namespace ThisRoofN
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

		protected async Task<string> CallRestAPI(string url, string body, HTTP_METHOD httpMethod = HTTP_METHOD.POST)
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
	}
}

