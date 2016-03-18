using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using ThisRoofN.Interfaces;
using MvvmCross.Platform;
using ThisRoofN.Models.ServiceModels;
using System.Linq;
using ThisRoofN.Models.App;

namespace ThisRoofN.RestService
{
	public class GeocodeService : TRBaseService
	{
		readonly string GoogleApiKey;// = "AIzaSyCSG2_n3pvNpcF8NRrRj_FoNDxBqjNXMWk";
		private IDevice deviceInfo;

		public GeocodeService ()
		{
			IDevice deviceInfo = Mvx.Resolve<IDevice> ();
			GoogleApiKey = deviceInfo.GetGoogleMapsApiKey ();
			baseURL = "https://maps.googleapis.com/maps/api/place/";
		}

		public async Task<List<TRGoogleMapPlace>> GetAutoCompleteSuggestionsAsync( string hint )
		{			
			var stringBuilder = new StringBuilder ();
			stringBuilder.Append("autocomplete/json?input=")
				.Append(hint).Append("&types=address&language=en&components=country:us&key=").Append(GoogleApiKey);
			var requestUri = stringBuilder.ToString();
			var places = new List<TRGoogleMapPlace>();

			if (hint.Length > 2) {
				var jsonString = await CallRestAPI(requestUri, null, HTTP_METHOD.GET);

//				deviceInfo.SetGooglePlacesApiCall ();

				var jsonObject = JsonConvert.DeserializeObject<AutoCompletePredictions> (jsonString, jsonSerializeSetting);
				if (jsonObject != null) {
					if (jsonObject.status == "OK") {
						places = jsonObject.predictions
							.Select (x => new TRGoogleMapPlace{
								FullAddress = x.description,
								NumAndStreet = x.description.Split(',')[0],
								City = x.description.Split(',').Length > 1 ? x.description.Split(',')[1] : "",
								PlaceId = x.place_id})
							.ToList();
					}
				}		
			}
			return places;
		}

		public async Task<TRGoogleMapPlaceDetail> GetPlaceDetailAsync( string placeId )
		{			
			var stringBuilder = new StringBuilder ();
			stringBuilder.Append("details/json?placeid=").Append(placeId).Append("&key=").Append(GoogleApiKey);
			var requestUri = stringBuilder.ToString ();
			var jsonString = await CallRestAPI (requestUri, null, HTTP_METHOD.GET);

			var jsonObject = JsonConvert.DeserializeObject<TRGoogleMapPlaceDetail> (jsonString, jsonSerializeSetting);
			if (jsonObject != null) {
				if (jsonObject.status == "OK") {
					return jsonObject;
				}
			}		

			return null;
		}
	}
}

