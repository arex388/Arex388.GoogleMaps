using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.GoogleMaps;

//	2.0.0
/// <summary>
/// Geocode feature object.
/// </summary>
public sealed class Geocode {
	//	https://developers.google.com/maps/documentation/geocoding/requests-geocoding

	//	============================================================================
	//	Request
	//	============================================================================

	/// <summary>
	/// Geocode request object.
	/// </summary>
	public sealed class Request :
		IRequest {
		/// <summary>
		/// The street address or plus code that you want to geocode. Specify addresses in accordance with the format used by the national postal service of the country concerned. Additional address elements such as business names and unit, suite or floor numbers should be avoided.
		/// </summary>
		public string? Address { get; init; }

		/// <summary>
		/// The bounding box of the viewport within which to bias geocode results more prominently. This parameter will only influence, not fully restrict, results from the geocoder.
		/// </summary>
		// ReSharper disable once MemberHidesStaticFromOuterClass
		public IEnumerable<string> Bounds { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// A components filter with elements separated by a pipe (|). The components filter is also accepted as an optional parameter if an address is provided. Each element in the components filter consists of a component:value pair, and fully restricts the results from the geocoder.
		/// </summary>
		public IEnumerable<string> Components { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// The language in which to return results.
		/// </summary>
		// ReSharper disable once MemberHidesStaticFromOuterClass
		public Language? Language { get; init; }

		/// <summary>
		/// The latitude and longitude values specifying the location for which you wish to obtain the closest, human-readable address.
		/// </summary>
		// ReSharper disable once MemberHidesStaticFromOuterClass
		public string? Point { get; init; }

		/// <summary>
		/// The region code, specified as a ccTLD ("top-level domain") two-character value. This parameter will only influence, not fully restrict, results from the geocoder.
		/// </summary>
		public string? Region { get; init; }

		//	========================================================================
		//	IRequest
		//	========================================================================

		string IRequest.Endpoint => GetEndpoint(this);
		HttpMethod IRequest.Method => HttpMethod.Get;

		//	========================================================================
		//	Utilities
		//	========================================================================

		private static string GetEndpoint(
			Request request) {
			var parameters = new HashSet<string>();

			if (request.Address.HasValue()) {
				parameters.Add($"address={WebUtility.UrlEncode(request.Address)}");
			}

			if (request.Bounds.Any()) {
				parameters.Add($"bounds={request.Bounds.StringJoin("|")}");
			}

			if (request.Components.Any()) {
				parameters.Add($"components={request.Components.StringJoin("|")}");
			}

			if (request.Language.HasValue) {
				parameters.Add($"language={GetLanguage(request)}");
			}

			if (request.Point.HasValue()) {
				parameters.Add($"latlng={request.Point}");
			}

			if (request.Region.HasValue()) {
				parameters.Add($"region={request.Region}");
			}

			return $"https://maps.googleapis.com/maps/api/geocode/json?{parameters.StringJoin("&")}";
		}

		private static string GetLanguage(
			Request request) => request.Language switch {
				Geocode.Language.Arabic => "ar",
				Geocode.Language.Belarusian => "be",
				Geocode.Language.Bulgarian => "bg",
				Geocode.Language.Bengali => "bn",
				Geocode.Language.Catalan => "ca",
				Geocode.Language.Czech => "cs",
				Geocode.Language.Danish => "da",
				Geocode.Language.German => "de",
				Geocode.Language.Greek => "el",
				Geocode.Language.English => "en",
				Geocode.Language.EnglishAustralian => "en-AU",
				Geocode.Language.EnglishGreatBritain => "en-GB",
				Geocode.Language.Spanish => "es",
				Geocode.Language.Basque => "eu",
				Geocode.Language.Farsi => "fa",
				Geocode.Language.Finnish => "fi",
				Geocode.Language.Filipino => "fil",
				Geocode.Language.French => "fr",
				Geocode.Language.Galician => "gl",
				Geocode.Language.Gujarati => "gu",
				Geocode.Language.Hindi => "hi",
				Geocode.Language.Croatian => "hr",
				Geocode.Language.Hungarian => "hu",
				Geocode.Language.Indonesian => "id",
				Geocode.Language.Italian => "it",
				Geocode.Language.Hebrew => "iw",
				Geocode.Language.Japanese => "ja",
				Geocode.Language.Kazakh => "kk",
				Geocode.Language.Kannada => "kn",
				Geocode.Language.Korean => "ko",
				Geocode.Language.Kyrgyz => "ky",
				Geocode.Language.Lithuanian => "lt",
				Geocode.Language.Latvian => "lv",
				Geocode.Language.Macedonian => "mk",
				Geocode.Language.Malayalam => "ml",
				Geocode.Language.Marathi => "mr",
				Geocode.Language.Burmese => "my",
				Geocode.Language.Dutch => "nl",
				Geocode.Language.Norwegian => "no",
				Geocode.Language.Punjabi => "pa",
				Geocode.Language.Polish => "pl",
				Geocode.Language.Portuguese => "pt",
				Geocode.Language.PortugueseBrazil => "pt-BR",
				Geocode.Language.PortuguesePortugal => "pt-PT",
				Geocode.Language.Romanian => "ro",
				Geocode.Language.Russian => "ru",
				Geocode.Language.Slovak => "sk",
				Geocode.Language.Slovenian => "sl",
				Geocode.Language.Albanian => "sq",
				Geocode.Language.Serbian => "sr",
				Geocode.Language.Swedish => "sv",
				Geocode.Language.Tamil => "ta",
				Geocode.Language.Telugu => "te",
				Geocode.Language.Thai => "th",
				Geocode.Language.Tagalog => "tl",
				Geocode.Language.Turkish => "tr",
				Geocode.Language.Ukrainian => "uk",
				Geocode.Language.Uzbek => "uz",
				Geocode.Language.Vietnamese => "vi",
				Geocode.Language.ChineseSimplified => "zh-CN",
				Geocode.Language.ChineseTraditional => "zh-TW",
				_ => throw new InvalidOperationException($"Unable to transform {nameof(Language)} value.")
			};
	}

	//	============================================================================
	//	Response
	//	============================================================================

	/// <summary>
	/// Geocode response object.
	/// </summary>
	public sealed class Response :
		IResponse {
		/// <summary>
		/// Contains an array of geocoded address information and geometry information.
		/// </summary>
		public IEnumerable<Result> Results { get; init; } = Enumerable.Empty<Result>();

		/// <summary>
		/// The "status" field within the Geocoding response object contains the status of the request, and may contain debugging information to help you track down why geocoding is not working.
		/// </summary>
		[JsonConverter(typeof(StatusJsonConverter))]
		public Status Status { get; init; } = Status.Unknown;

		//	========================================================================
		//	IResponse
		//	========================================================================

		/// <summary>
		/// When the geocoder returns a status code other than OK, there may be an additional error_message field within the Geocoding response object. This field contains more detailed information about the reasons behind the given status code.
		/// </summary>
		[JsonPropertyName("error_message")]
		public string? ResponseError { get; init; }

		/// <summary>
		/// The raw response JSON if debugging was enabled.
		/// </summary>
		public string? ResponseJson { get; set; }

		/// <summary>
		/// The overall response status.
		/// </summary>
		public ResponseStatus ResponseStatus { get; set; }
	}

	//	============================================================================
	//	Models
	//	============================================================================

	/// <summary>
	/// The bounding box of the viewport within which to bias geocode results more prominently. This parameter will only influence, not fully restrict, results from the geocoder.
	/// </summary>
	public sealed class Bounds {
#pragma warning disable CS1591
		public Point Northeast { get; init; }
		public Point Southwest { get; init; }
#pragma warning restore CS1591
	}

	/// <summary>
	/// Geocode response component object.
	/// </summary>
	public sealed class Component {
		/// <summary>
		/// The full text description or name of the address component as returned by the Geocoder.
		/// </summary>
		[JsonPropertyName("long_name")]
		public string LongName { get; init; } = null!;

		/// <summary>
		/// An abbreviated textual name for the address component, if available. For example, an address component for the state of Alaska may have a long_name of "Alaska" and a short_name of "AK" using the 2-letter postal abbreviation.
		/// </summary>
		[JsonPropertyName("short_name")]
		public string ShortName { get; init; } = null!;

		/// <summary>
		/// An array indicating the type of the address component.
		/// </summary>
		public IEnumerable<string> Types { get; init; } = Enumerable.Empty<string>();
	}

	/// <summary>
	/// Geocode response geometry object.
	/// </summary>
	public sealed class Geometry {
		/// <summary>
		/// Stores the bounding box which can fully contain the returned result.
		/// </summary>
		public Bounds Bounds { get; init; } = null!;

		/// <summary>
		/// Contains the geocoded latitude, longitude value.
		/// </summary>
		[JsonPropertyName("location")]
		public Point Point { get; init; }

		/// <summary>
		/// Additional data about the specified location.
		/// </summary>
		[JsonConverter(typeof(LocationTypeJsonConverter)), JsonPropertyName("location_type")]
		// ReSharper disable once MemberHidesStaticFromOuterClass
		public LocationType Type { get; init; }

		/// <summary>
		/// Contains the recommended viewport for displaying the returned result, specified as two latitude,longitude values defining the southwest and northeast corner of the viewport bounding box.
		/// </summary>
		public Bounds ViewPort { get; init; } = null!;
	}

	/// <summary>
	/// The language in which to return results.
	/// </summary>
	public enum Language :
		byte {
#pragma warning disable CS1591
		Arabic,
		Belarusian,
		Bulgarian,
		Bengali,
		Catalan,
		Czech,
		Danish,
		German,
		Greek,
		English,
		EnglishAustralian,
		EnglishGreatBritain,
		Spanish,
		Basque,
		Farsi,
		Finnish,
		Filipino,
		French,
		Galician,
		Gujarati,
		Hindi,
		Croatian,
		Hungarian,
		Indonesian,
		Italian,
		Hebrew,
		Japanese,
		Kazakh,
		Kannada,
		Korean,
		Kyrgyz,
		Lithuanian,
		Latvian,
		Macedonian,
		Malayalam,
		Marathi,
		Burmese,
		Dutch,
		Norwegian,
		Punjabi,
		Polish,
		Portuguese,
		PortugueseBrazil,
		PortuguesePortugal,
		Romanian,
		Russian,
		Slovak,
		Slovenian,
		Albanian,
		Serbian,
		Swedish,
		Tamil,
		Telugu,
		Thai,
		Tagalog,
		Turkish,
		Ukrainian,
		Uzbek,
		Vietnamese,
		ChineseSimplified,
		ChineseTraditional
#pragma warning restore CS1591
	}

	/// <summary>
	/// Additional data about the specified location.
	/// </summary>
	public enum LocationType :
		byte {
		/// <summary>
		/// Indicates that the returned result is approximate.
		/// </summary>
		Approximate,

		/// <summary>
		/// Indicates that the returned result is the geometric center of a result such as a polyline (for example, a street) or polygon (region).
		/// </summary>
		GeometricCenter,

		/// <summary>
		/// Indicates that the returned result reflects an approximation (usually on a road) interpolated between two precise points (such as intersections).
		/// </summary>
		RangeInterpolated,

		/// <summary>
		/// Indicates that the returned result is a precise geocode for which we have location information accurate down to street address precision.
		/// </summary>
		Rooftop
	}

	private sealed class LocationTypeJsonConverter :
		JsonConverter<LocationType> {
		public override LocationType Read(
			ref Utf8JsonReader reader,
			System.Type typeToConvert,
			JsonSerializerOptions options) {
			var value = reader.GetString();

			return value switch {
				"APPROXIMATE" => LocationType.Approximate,
				"GEOMETRIC_CENTER" => LocationType.GeometricCenter,
				"RANGE_INTERPOLATED" => LocationType.RangeInterpolated,
				"ROOFTOP" => LocationType.Rooftop,
				_ => throw new InvalidOperationException($"Unable to transform \"{value}\" to {nameof(LocationType)}.")
			};
		}

		public override void Write(
			Utf8JsonWriter writer,
			LocationType value,
			JsonSerializerOptions options) => throw new NotImplementedException();
	}

	/// <summary>
	/// Contains the geocoded latitude, longitude value.
	/// </summary>
	public struct Point {
#pragma warning disable CS1591
		[JsonPropertyName("lat")]
		public decimal Latitude { get; init; }

		[JsonPropertyName("lng")]
		public decimal Longitude { get; init; }
#pragma warning restore CS1591
	}

	/// <summary>
	/// Geocode response result object.
	/// </summary>
	public sealed class Result {
		/// <summary>
		/// An array containing the separate components applicable to this address.
		/// </summary>
		[JsonPropertyName("address_components")]
		public IEnumerable<Component> AddressComponents { get; init; } = Enumerable.Empty<Component>();

		/// <summary>
		/// A string containing the human-readable address of this location.
		/// </summary>
		[JsonPropertyName("formatted_address")]
		public string FormattedAddress { get; init; } = null!;

		/// <summary>
		/// Contains location, type, viewport and bounds.
		/// </summary>
		public Geometry Geometry { get; init; } = null!;

		/// <summary>
		/// A unique identifier that can be used with other Google APIs. For example, you can use the place_id in a Places API request to get details of a local business, such as phone number, opening hours, user reviews, and more.
		/// </summary>
		[JsonPropertyName("place_id")]
		public string PlacedId { get; init; } = null!;

		/// <summary>
		/// Array indicates the type of the returned result. This array contains a set of zero or more tags identifying the type of feature returned in the result.
		/// </summary>
		public IEnumerable<Type> Types { get; init; } = Enumerable.Empty<Type>();
	}

	/// <summary>
	/// The "status" field within the Geocoding response object contains the status of the request, and may contain debugging information to help you track down why geocoding is not working.
	/// </summary>
	public enum Status :
		byte {
		/// <summary>
		/// Generally indicates that the query (address, components or latlng) is missing.
		/// </summary>
		InvalidRequest,

		/// <summary>
		/// Indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
		/// </summary>
		Ok,

		/// <summary>
		/// Indicates any of the following: The API key is missing or invalid; Billing has not been enabled on your account; A self-imposed usage cap has been exceeded; OR The provided method of payment is no longer valid (for example, a credit card has expired).
		/// </summary>
		OverDailyLimit,

		/// <summary>
		/// Indicates that you are over your quota.
		/// </summary>
		OverQueryLimit,

		/// <summary>
		/// Indicates that your request was denied.
		/// </summary>
		RequestDenied,

		/// <summary>
		/// Indicates that the request could not be processed due to a server error. The request may succeed if you try again.
		/// </summary>
		Unknown,

		/// <summary>
		/// Indicates that the geocode was successful but returned no results.
		/// </summary>
		ZeroResults
	}

	private sealed class StatusJsonConverter :
		JsonConverter<Status> {
		public override Status Read(
			ref Utf8JsonReader reader,
			System.Type typeToConvert,
			JsonSerializerOptions options) {
			var value = reader.GetString();

			return value switch {
				"INVALID_REQUEST" => Status.InvalidRequest,
				"OK" => Status.Ok,
				"OVER_DAILY_LIMIT" => Status.OverDailyLimit,
				"OVER_QUERY_LIMIT" => Status.OverQueryLimit,
				"REQUEST_DENIED" => Status.RequestDenied,
				"UNKNOWN_ERROR" => Status.Unknown,
				"ZERO_RESULTS" => Status.ZeroResults,
				_ => throw new InvalidOperationException($"Unable to transform \"{value}\" to {nameof(Status)}.")
			};
		}

		public override void Write(
			Utf8JsonWriter writer,
			Status value,
			JsonSerializerOptions options) => throw new NotImplementedException();
	}

	/// <summary>
	/// The address type.
	/// </summary>
	[JsonConverter(typeof(TypeJsonConverter))]
	public enum Type :
		byte {
		/// <summary>
		/// Indicates a first-order civil entity below the country level. Within the United States, these administrative levels are states. Not all nations exhibit these administrative levels. In most cases, administrative_area_level_1 short names will closely match ISO 3166-2 subdivisions and other widely circulated lists; however this is not guaranteed as our geocoding results are based on a variety of signals and location data.
		/// </summary>
		AdministrativeAreaLevel1,

		/// <summary>
		/// Indicates a second-order civil entity below the country level. Within the United States, these administrative levels are counties. Not all nations exhibit these administrative levels.
		/// </summary>
		AdministrativeAreaLevel2,

		/// <summary>
		/// Indicates a third-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
		/// </summary>
		AdministrativeAreaLevel3,

		/// <summary>
		/// Idicates a fourth-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
		/// </summary>
		AdministrativeAreaLevel4,

		/// <summary>
		/// Indicates a fifth-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
		/// </summary>
		AdministrativeAreaLevel5,

		/// <summary>
		/// Indicates a sixth-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
		/// </summary>
		AdministrativeAreaLevel6,

		/// <summary>
		/// Indicates a seventh-order civil entity below the country level. This type indicates a minor civil division. Not all nations exhibit these administrative levels.
		/// </summary>
		AdministrativeAreaLevel7,

		/// <summary>
		/// Indicates an airport.
		/// </summary>
		Airport,

		/// <summary>
		/// Indicates the location of a bus stop.
		/// </summary>
		BusStation,

		/// <summary>
		/// Indicates a commonly-used alternative name for the entity.
		/// </summary>
		ColloquialArea,

		/// <summary>
		/// Indicates the national political entity, and is typically the highest order type returned by the Geocoder.
		/// </summary>
		Country,

		/// <summary>
		/// Typically indicates a place that has not yet been categorized.
		/// </summary>
		Establishment,

		/// <summary>
		/// Indicates the floor of a building address.
		/// </summary>
		Floor,

		/// <summary>
		/// Indicates a major intersection, usually of two major roads.
		/// </summary>
		Intersection,

		/// <summary>
		/// Indicates a nearby place that is used as a reference, to aid navigation.
		/// </summary>
		Landmark,

		/// <summary>
		/// Indicates an incorporated city or town political entity.
		/// </summary>
		Locality,

		/// <summary>
		/// Indicates a prominent natural feature.
		/// </summary>
		NaturalFeature,

		/// <summary>
		/// Indicates a named neighborhood.
		/// </summary>
		Neighborhood,

		/// <summary>
		/// Indicates a named park.
		/// </summary>
		Park,

		/// <summary>
		/// Indicates a parking lot or parking structure.
		/// </summary>
		Parking,

		/// <summary>
		/// Indicates an encoded location reference, derived from latitude and longitude. Plus codes can be used as a replacement for street addresses in places where they do not exist (where buildings are not numbered or streets are not named).
		/// </summary>
		PlusCode,

		/// <summary>
		/// Indicates a named point of interest. Typically, these "POI"s are prominent local entities that don't easily fit in another category, such as "Empire State Building" or "Eiffel Tower".
		/// </summary>
		PointOfInterest,

		/// <summary>
		/// Indicates a political entity. Usually, this type indicates a polygon of some civil administration.
		/// </summary>
		Political,

		/// <summary>
		/// Indicates a specific postal box.
		/// </summary>
		PostBox,

		/// <summary>
		/// Indicates a postal code as used to address postal mail within the country.
		/// </summary>
		PostalCode,

		/// <summary>
		/// Indicates a grouping of geographic areas, such as locality and sublocality, used for mailing addresses in some countries.
		/// </summary>
		PostalTown,

		/// <summary>
		/// Indicates a named location, usually a building or collection of buildings with a common name
		/// </summary>
		Premise,

		/// <summary>
		/// Indicates the room of a building address.
		/// </summary>
		Room,

		/// <summary>
		/// Indicates a named route (such as "US 101").
		/// </summary>
		Route,

		/// <summary>
		/// Indicates a precise street address.
		/// </summary>
		StreetAddress,

		/// <summary>
		/// Indicates the precise street number.
		/// </summary>
		StreetNumber,

		/// <summary>
		/// Indicates a first-order civil entity below a locality. For some locations may receive one of the additional types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
		/// </summary>
		Sublocality,

		/// <summary>
		/// Indicates a first-order civil entity below a locality. For some locations may receive one of the additional types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
		/// </summary>
		SublocalityLevel1,

		/// <summary>
		/// Indicates a first-order civil entity below a locality. For some locations may receive one of the additional types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
		/// </summary>
		SublocalityLevel2,

		/// <summary>
		/// Indicates a first-order civil entity below a locality. For some locations may receive one of the additional types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
		/// </summary>
		SublocalityLevel3,

		/// <summary>
		/// Indicates a first-order civil entity below a locality. For some locations may receive one of the additional types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
		/// </summary>
		SublocalityLevel4,

		/// <summary>
		/// Indicates a first-order civil entity below a locality. For some locations may receive one of the additional types: sublocality_level_1 to sublocality_level_5. Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
		/// </summary>
		SublocalityLevel5,

		/// <summary>
		/// Indicates a first-order entity below a named location, usually a singular building within a collection of buildings with a common name.
		/// </summary>
		Subpremise,

		/// <summary>
		/// Indicates the location of a train stop.
		/// </summary>
		TrainStation,

		/// <summary>
		/// Indicates the location of a transit stop.
		/// </summary>
		TransitStation
	}

	private sealed class TypeJsonConverter :
		JsonConverter<Type> {
		public override Type Read(
			ref Utf8JsonReader reader,
			System.Type typeToConvert,
			JsonSerializerOptions options) {
			var value = reader.GetString();

			return value switch {
				"administrative_area_level_1" => Type.AdministrativeAreaLevel1,
				"administrative_area_level_2" => Type.AdministrativeAreaLevel2,
				"administrative_area_level_3" => Type.AdministrativeAreaLevel3,
				"administrative_area_level_4" => Type.AdministrativeAreaLevel4,
				"administrative_area_level_5" => Type.AdministrativeAreaLevel5,
				"administrative_area_level_6" => Type.AdministrativeAreaLevel6,
				"administrative_area_level_7" => Type.AdministrativeAreaLevel7,
				"airport" => Type.Airport,
				"bus_station" => Type.BusStation,
				"colloquial_area" => Type.ColloquialArea,
				"country" => Type.Country,
				"establishment" => Type.Establishment,
				"floor" => Type.Floor,
				"intersection" => Type.Intersection,
				"landmark" => Type.Landmark,
				"locality" => Type.Locality,
				"natural_feature" => Type.NaturalFeature,
				"neighborhood" => Type.Neighborhood,
				"park" => Type.Park,
				"parking" => Type.Parking,
				"plus_code" => Type.PlusCode,
				"point_of_interest" => Type.PointOfInterest,
				"political" => Type.Political,
				"postal_code" => Type.PostalCode,
				"postal_town" => Type.PostalTown,
				"post_box" => Type.PostBox,
				"premise" => Type.Premise,
				"room" => Type.Room,
				"route" => Type.Route,
				"street_address" => Type.StreetAddress,
				"street_number" => Type.StreetNumber,
				"sublocality" => Type.Sublocality,
				"sublocality_level_1" => Type.SublocalityLevel1,
				"sublocality_level_2" => Type.SublocalityLevel2,
				"sublocality_level_3" => Type.SublocalityLevel3,
				"sublocality_level_4" => Type.SublocalityLevel4,
				"sublocality_level_5" => Type.SublocalityLevel5,
				"subpremise" => Type.Subpremise,
				"train_station" => Type.TrainStation,
				"transit_station" => Type.TransitStation,
				_ => throw new InvalidOperationException($"Unable to transform \"{value}\" to {nameof(Type)}.")
			};
		}

		public override void Write(
			Utf8JsonWriter writer,
			Type value,
			JsonSerializerOptions options) => throw new NotImplementedException();
	}
}