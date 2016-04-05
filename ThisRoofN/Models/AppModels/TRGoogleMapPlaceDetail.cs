﻿using System;
using System.Collections.Generic;

namespace ThisRoofN.Models.App
{
	public class AddressComponent
	{
		public string long_name { get; set; }
		public string short_name { get; set; }
		public List<string> types { get; set; }
	}

	public class Location
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Geometry
	{
		public Location location { get; set; }
	}

	public class Result
	{
		public List<AddressComponent> address_components { get; set; }
		public string adr_address { get; set; }
		public string formatted_address { get; set; }
		public Geometry geometry { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public string place_id { get; set; }
		public string reference { get; set; }
		public string scope { get; set; }
		public List<string> types { get; set; }
		public string url { get; set; }
		public string vicinity { get; set; }
	}

	public class TRGoogleMapPlaceDetail
	{
		public List<object> html_attributions { get; set; }
		public Result result { get; set; }
		public string status { get; set; }
	}

	public class TRGoogleMapGeocoding
	{
		public List<Result> results { get; set; }
		public string status { get; set; }
	}
}

