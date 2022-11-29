using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.GoogleMaps;

/// <summary>
/// DistanceMatrix feature object.
/// </summary>
public sealed class DistanceMatrix {
	//	https://developers.google.com/maps/documentation/distance-matrix/distance-matrix

	//	============================================================================
	//	Request
	//	============================================================================

	/// <summary>
	/// DistanceMatrix request object.
	/// </summary>
	public sealed class Request :
		IRequest {
		/// <summary>
		/// Specifies the desired time of arrival for transit directions, in seconds since midnight, January 1, 1970 UTC.
		/// </summary>
		public int? ArrivalTime { get; init; }

		/// <summary>
		/// Distances may be calculated that adhere to certain restrictions. Restrictions are indicated by use of the avoid parameter, and an argument to that parameter indicating the restriction to avoid.
		/// </summary>
		public AvoidType? Avoid { get; init; }

		/// <summary>
		/// Specifies the desired time of departure. You can specify the time as an integer in seconds since midnight, January 1, 1970 UTC.
		/// </summary>
		public int? DepartureTime { get; init; }

		/// <summary>
		/// One or more locations to use as the finishing point for calculating travel distance and time.
		/// </summary>
		public IEnumerable<string> Destinations { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// The language in which to return results.
		/// </summary>
		// ReSharper disable once MemberHidesStaticFromOuterClass
		public Language? Language { get; init; }

		/// <summary>
		/// For the calculation of distances and directions, you may specify the transportation mode to use. By default, DRIVING mode is used. By default, directions are calculated as driving directions.
		/// </summary>
		public ModeType? Mode { get; init; }

		/// <summary>
		/// The starting point for calculating travel distance and time. You can supply one or more locations separated by the pipe character (|), in the form of a place ID, an address, or latitude/longitude coordinates.
		/// </summary>
		public IEnumerable<string> Origins { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// The region code, specified as a ccTLD ("top-level domain") two-character value.
		/// </summary>
		public string? Region { get; init; }

		/// <summary>
		/// Specifies the assumptions to use when calculating time in traffic. This setting affects the value returned in the duration_in_traffic field in the response, which contains the predicted time in traffic based on historical averages.
		/// </summary>
		public TrafficModelType? TrafficModel { get; init; }

		/// <summary>
		/// Specifies one or more preferred modes of transit. This parameter may only be specified for transit directions.
		/// </summary>
		public TransitModeType? TransitMode { get; init; }

		/// <summary>
		/// Specifies preferences for transit routes. Using this parameter, you can bias the options returned, rather than accepting the default best route chosen by the API. This parameter may only be specified for transit directions.
		/// </summary>
		public TransitRoutingPreferenceType? TransitRoutingPreference { get; init; }

		/// <summary>
		/// Specifies the unit system to use when displaying results.
		/// </summary>
		public UnitType? Units { get; init; }

		//	========================================================================
		//	IRequest
		//	========================================================================

		string IRequest.Endpoint => GetEndpoint(this);
		HttpMethod IRequest.Method => HttpMethod.Get;

		//	========================================================================
		//	Utilities
		//	========================================================================

		private static string GetAvoidType(
			Request request) => request.Avoid switch {
				AvoidType.Ferries => nameof(AvoidType.Ferries).ToLower(),
				AvoidType.Highways => nameof(AvoidType.Highways).ToLower(),
				AvoidType.Indoor => nameof(AvoidType.Indoor).ToLower(),
				AvoidType.Tolls => nameof(AvoidType.Tolls).ToLower(),
				_ => throw new InvalidOperationException($"Unable to transform {nameof(AvoidType)} value.")
			};

		private static string GetEndpoint(
			Request request) {
			var parameters = new HashSet<string>();

			if (request.ArrivalTime.HasValue) {
				parameters.Add($"arrival_time={request.ArrivalTime}");
			}

			if (request.Avoid.HasValue) {
				parameters.Add($"avoid={GetAvoidType(request)}");
			}

			if (request.DepartureTime.HasValue) {
				parameters.Add($"departure_time={request.DepartureTime}");
			}

			var destinations = request.Destinations.Select(WebUtility.UrlEncode).StringJoin("|");

			parameters.Add($"destinations={destinations}");

			if (request.Language.HasValue) {
				parameters.Add($"language={GetLanguage(request)}");
			}

			if (request.Mode.HasValue) {
				parameters.Add($"mode={GetModeType(request)}");
			}

			var origins = request.Origins.Select(WebUtility.UrlEncode).StringJoin("|");

			parameters.Add($"origins={origins}");

			if (request.Region.HasValue()) {
				parameters.Add($"region={request.Region}");
			}

			if (request.TrafficModel.HasValue) {
				parameters.Add($"traffic_model={GetTrafficModelType(request)}");
			}

			if (request.TransitMode.HasValue) {
				parameters.Add($"transit_mde={GetTransitModeType(request)}");
			}

			if (request.TransitRoutingPreference.HasValue) {
				parameters.Add($"transit_routing_preference={GetTransitRoutingPreferenceType(request)}");
			}

			if (request.Units.HasValue) {
				parameters.Add($"units={GetUnitType(request)}");
			}

			return $"https://maps.googleapis.com/maps/api/distancematrix/json?{parameters.StringJoin("&")}";
		}

		private static string GetLanguage(
			Request request) => request.Language switch {
				DistanceMatrix.Language.Arabic => "ar",
				DistanceMatrix.Language.Belarusian => "be",
				DistanceMatrix.Language.Bulgarian => "bg",
				DistanceMatrix.Language.Bengali => "bn",
				DistanceMatrix.Language.Catalan => "ca",
				DistanceMatrix.Language.Czech => "cs",
				DistanceMatrix.Language.Danish => "da",
				DistanceMatrix.Language.German => "de",
				DistanceMatrix.Language.Greek => "el",
				DistanceMatrix.Language.English => "en",
				DistanceMatrix.Language.EnglishAustralian => "en-AU",
				DistanceMatrix.Language.EnglishGreatBritain => "en-GB",
				DistanceMatrix.Language.Spanish => "es",
				DistanceMatrix.Language.Basque => "eu",
				DistanceMatrix.Language.Farsi => "fa",
				DistanceMatrix.Language.Finnish => "fi",
				DistanceMatrix.Language.Filipino => "fil",
				DistanceMatrix.Language.French => "fr",
				DistanceMatrix.Language.Galician => "gl",
				DistanceMatrix.Language.Gujarati => "gu",
				DistanceMatrix.Language.Hindi => "hi",
				DistanceMatrix.Language.Croatian => "hr",
				DistanceMatrix.Language.Hungarian => "hu",
				DistanceMatrix.Language.Indonesian => "id",
				DistanceMatrix.Language.Italian => "it",
				DistanceMatrix.Language.Hebrew => "iw",
				DistanceMatrix.Language.Japanese => "ja",
				DistanceMatrix.Language.Kazakh => "kk",
				DistanceMatrix.Language.Kannada => "kn",
				DistanceMatrix.Language.Korean => "ko",
				DistanceMatrix.Language.Kyrgyz => "ky",
				DistanceMatrix.Language.Lithuanian => "lt",
				DistanceMatrix.Language.Latvian => "lv",
				DistanceMatrix.Language.Macedonian => "mk",
				DistanceMatrix.Language.Malayalam => "ml",
				DistanceMatrix.Language.Marathi => "mr",
				DistanceMatrix.Language.Burmese => "my",
				DistanceMatrix.Language.Dutch => "nl",
				DistanceMatrix.Language.Norwegian => "no",
				DistanceMatrix.Language.Punjabi => "pa",
				DistanceMatrix.Language.Polish => "pl",
				DistanceMatrix.Language.Portuguese => "pt",
				DistanceMatrix.Language.PortugueseBrazil => "pt-BR",
				DistanceMatrix.Language.PortuguesePortugal => "pt-PT",
				DistanceMatrix.Language.Romanian => "ro",
				DistanceMatrix.Language.Russian => "ru",
				DistanceMatrix.Language.Slovak => "sk",
				DistanceMatrix.Language.Slovenian => "sl",
				DistanceMatrix.Language.Albanian => "sq",
				DistanceMatrix.Language.Serbian => "sr",
				DistanceMatrix.Language.Swedish => "sv",
				DistanceMatrix.Language.Tamil => "ta",
				DistanceMatrix.Language.Telugu => "te",
				DistanceMatrix.Language.Thai => "th",
				DistanceMatrix.Language.Tagalog => "tl",
				DistanceMatrix.Language.Turkish => "tr",
				DistanceMatrix.Language.Ukrainian => "uk",
				DistanceMatrix.Language.Uzbek => "uz",
				DistanceMatrix.Language.Vietnamese => "vi",
				DistanceMatrix.Language.ChineseSimplified => "zh-CN",
				DistanceMatrix.Language.ChineseTraditional => "zh-TW",
				_ => throw new InvalidOperationException($"Unable to transform {nameof(Language)} value.")
			};

		private static string GetModeType(
			Request request) => request.Mode switch {
				ModeType.Bicycling => nameof(ModeType.Bicycling).ToLower(),
				ModeType.Driving => nameof(ModeType.Driving).ToLower(),
				ModeType.Transit => nameof(ModeType.Transit).ToLower(),
				ModeType.Walking => nameof(ModeType.Walking).ToLower(),
				_ => throw new InvalidOperationException($"Unable to transform {nameof(ModeType)} value.")
			};

		private static string GetTrafficModelType(
			Request request) => request.TrafficModel switch {
				TrafficModelType.BestGuess => "best_guess",
				TrafficModelType.Optimistic => nameof(TrafficModelType.Optimistic).ToLower(),
				TrafficModelType.Pessimistic => nameof(TrafficModelType.Pessimistic).ToLower(),
				_ => throw new InvalidOperationException($"Unable to transform {nameof(TrafficModelType)} value.")
			};

		private static string GetTransitModeType(
			Request request) => request.TransitMode switch {
				TransitModeType.Bus => nameof(TransitModeType.Bus).ToLower(),
				TransitModeType.Rail => nameof(TransitModeType.Rail).ToLower(),
				TransitModeType.Subway => nameof(TransitModeType.Subway).ToLower(),
				TransitModeType.Train => nameof(TransitModeType.Train).ToLower(),
				TransitModeType.Tram => nameof(TransitModeType.Tram).ToLower(),
				_ => throw new InvalidOperationException($"Unable to transform {nameof(TransitModeType)} value.")
			};

		private static string GetTransitRoutingPreferenceType(
			Request request) => request.TransitRoutingPreference switch {
				TransitRoutingPreferenceType.FewerTransfers => "fewer_transfers",
				TransitRoutingPreferenceType.LessWalking => "less_walking",
				_ => throw new InvalidOperationException($"Unable to transform {nameof(TransitRoutingPreference)} value.")
			};

		private static string GetUnitType(
			Request request) => request.Units switch {
				UnitType.Imperial => nameof(UnitType.Imperial).ToLower(),
				UnitType.Metric => nameof(UnitType.Metric).ToLower(),
				_ => throw new InvalidOperationException($"Unable to transform {nameof(UnitType)} value.")
			};
	}

	//	============================================================================
	//	Response
	//	============================================================================

	/// <summary>
	/// DistanceMatrix response object.
	/// </summary>
	public sealed class Response :
		IResponse {
		/// <summary>
		/// An array of addresses as returned by the API from your original request.
		/// </summary>
		[JsonPropertyName("destination_addresses")]
		public IEnumerable<string> Destinations { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// An array of addresses as returned by the API from your original request.
		/// </summary>
		[JsonPropertyName("origin_addresses")]
		public IEnumerable<string> Origins { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// An array of elements, which in turn each contain a status, duration, and distance element.
		/// </summary>
		public IEnumerable<Row> Rows { get; init; } = Enumerable.Empty<Row>();

		/// <summary>
		/// Contains the status of the request, and may contain debugging information to help you track down why the request failed.
		/// </summary>
		[JsonConverter(typeof(StatusJsonConverter))]
		public Status Status { get; init; } = Status.Unknown;

		//	========================================================================
		//	IResponse
		//	========================================================================

		/// <summary>
		/// A string containing the human-readable text of any errors encountered while the request was being processed.
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
	/// Distances may be calculated that adhere to certain restrictions. Restrictions are indicated by use of the avoid parameter, and an argument to that parameter indicating the restriction to avoid.
	/// </summary>
	public enum AvoidType :
		byte {
		/// <summary>
		/// Indicates that the calculated route should avoid ferries.
		/// </summary>
		Ferries,

		/// <summary>
		/// Indicates that the calculated route should avoid highways.
		/// </summary>
		Highways,

		/// <summary>
		/// Indicates that the calculated route should avoid indoor steps for walking and transit directions.
		/// </summary>
		Indoor,

		/// <summary>
		/// Indicates that the calculated route should avoid toll roads/bridges.
		/// </summary>
		Tolls
	}

	/// <summary>
	/// DistanceMatrix response element object.
	/// </summary>
	public sealed class Element {
		/// <summary>
		/// The total distance of this route, expressed in meters (value) and as text. The textual value uses the unit system specified with the unit parameter of the original request, or the origin's region.
		/// </summary>
		public TextValuePair? Distance { get; init; }

		/// <summary>
		/// The length of time it takes to travel this route, expressed in seconds (the value field) and as text. The textual representation is localized according to the query's language parameter.
		/// </summary>
		public TextValuePair? Duration { get; init; }

		/// <summary>
		/// The length of time it takes to travel this route, based on current and historical traffic conditions.
		/// </summary>
		public TextValuePair? DurationInTraffic { get; init; }

		/// <summary>
		/// If present, contains the total fare (that is, the total ticket costs) on this route. This property is only returned for transit requests and only for transit providers where fare information is available.
		/// </summary>
		public Fare? Fare { get; init; }

		/// <summary>
		/// A status for the element.
		/// </summary>
		[JsonConverter(typeof(ElementStatusJsonConverter))]
		public ElementStatus Status { get; init; }
	}

	/// <summary>
	/// An element which contains a status, duration
	/// </summary>
	public enum ElementStatus :
		byte {
		/// <summary>
		/// Indicates the requested route is too long and cannot be processed.
		/// </summary>
		MaxRouteLengthExceeded,

		/// <summary>
		/// Indicates that the origin and/or destination of this pairing could not be geocoded.
		/// </summary>
		NotFound,

		/// <summary>
		/// Indicates the response contains a valid result.
		/// </summary>
		Ok,

		/// <summary>
		/// Indicates no route could be found between the origin and destination.
		/// </summary>
		ZeroResults
	}

	private sealed class ElementStatusJsonConverter :
		JsonConverter<ElementStatus> {
		public override ElementStatus Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) {
			var value = reader.GetString();

			return value switch {
				"MAX_ROUTE_LENGTH_EXCEEDED" => ElementStatus.MaxRouteLengthExceeded,
				"NOT_FOUND" => ElementStatus.NotFound,
				"OK" => ElementStatus.Ok,
				"ZERO_RESULTS" => ElementStatus.ZeroResults,
				_ => throw new InvalidOperationException($"Unable to transform \"{value}\" to {nameof(ElementStatus)}.")
			};
		}

		public override void Write(
			Utf8JsonWriter writer,
			ElementStatus value,
			JsonSerializerOptions options) => throw new NotImplementedException();
	}

	/// <summary>
	/// DistanceMatrix response fare object.
	/// </summary>
	public sealed class Fare {
		/// <summary>
		/// An ISO 4217 currency code indicating the currency that the amount is expressed in.
		/// </summary>
		public string Currency { get; init; } = null!;

		/// <summary>
		/// The total fare amount, formatted in the requested language.
		/// </summary>
		public string Text { get; init; } = null!;

		/// <summary>
		/// The total fare amount, in the currency specified.
		/// </summary>
		public decimal Value { get; init; }
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
	/// For the calculation of distances and directions, you may specify the transportation mode to use. By default, DRIVING mode is used. By default, directions are calculated as driving directions.
	/// </summary>
	public enum ModeType :
		byte {
		/// <summary>
		/// Requests bicycling directions or distance via bicycle paths & preferred streets (where available).
		/// </summary>
		Bicycling,

		/// <summary>
		/// Indicates standard driving directions or distance using the road network (default).
		/// </summary>
		Driving,

		/// <summary>
		/// Requests directions or distance via public transit routes (where available).
		/// </summary>
		Transit,

		/// <summary>
		/// Requests walking directions or distance via pedestrian paths & sidewalks (where available).
		/// </summary>
		Walking
	}

	/// <summary>
	/// DistanceMatrix response row object.
	/// </summary>
	public sealed class Row {
		/// <summary>
		/// Each row array contains one or more element entries, which in turn contain the information about a single origin-destination pairing.
		/// </summary>
		public IEnumerable<Element> Elements { get; init; } = Enumerable.Empty<Element>();
	}

	/// <summary>
	/// Status codes returned by service.
	/// </summary>
	public enum Status :
		byte {
		/// <summary>
		/// Indicates that the provided request was invalid.
		/// </summary>
		InvalidRequest,

		/// <summary>
		/// Indicates that the number of origins or destinations exceeds the per-query limit.
		/// </summary>
		MaxDimensionsExceeded,

		/// <summary>
		/// Indicates that the product of origins and destinations exceeds the per-query limit.
		/// </summary>
		MaxElementsExceeded,

		/// <summary>
		/// Indicates the response contains a valid result.
		/// </summary>
		Ok,

		/// <summary>
		/// Indicates any of the following: The API key is missing or invalid; Billing has not been enabled on your account; A self-imposed usage cap has been exceeded; OR The provided method of payment is no longer valid (for example, a credit card has expired).
		/// </summary>
		OverDailyLimit,

		/// <summary>
		/// Indicates the service has received too many requests from your application within the allowed time period.
		/// </summary>
		OverQueryLimit,

		/// <summary>
		/// Indicates that the service denied use of the Distance Matrix service by your application.
		/// </summary>
		RequestDenied,

		/// <summary>
		/// Indicates a Distance Matrix request could not be processed due to a server error. The request may succeed if you try again.
		/// </summary>
		Unknown
	}

	private sealed class StatusJsonConverter :
		JsonConverter<Status> {
		public override Status Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) {
			var value = reader.GetString();

			return value switch {
				"IVALID_REQUEST" => Status.InvalidRequest,
				"MAX_DIMENSIONS_EXCEEDED" => Status.MaxDimensionsExceeded,
				"MAX_ELEMENTS_EXCEEDED" => Status.MaxElementsExceeded,
				"OK" => Status.Ok,
				"OVER_DAILY_LIMIT" => Status.OverDailyLimit,
				"OVER_QUERY_LIMIT" => Status.OverQueryLimit,
				"REQUEST_DENIED" => Status.RequestDenied,
				"UNKNOWN_ERROR" => Status.Unknown,
				_ => throw new InvalidOperationException($"Unable to transform \"{value}\" to {nameof(Status)}.")
			};
		}

		public override void Write(
			Utf8JsonWriter writer,
			Status value,
			JsonSerializerOptions options) => throw new NotImplementedException();
	}

	/// <summary>
	/// An object containing a numeric value and its formatted text representation.
	/// </summary>
	public sealed class TextValuePair {
		/// <summary>
		/// String value.
		/// </summary>
		public string Text { get; init; } = null!;

		/// <summary>
		/// Numeric value.
		/// </summary>
		public decimal Value { get; init; }
	}

	/// <summary>
	/// Specifies the assumptions to use when calculating time in traffic. This setting affects the value returned in the duration_in_traffic field in the response, which contains the predicted time in traffic based on historical averages.
	/// </summary>
	public enum TrafficModelType :
		byte {
		/// <summary>
		/// Indicates that the returned duration_in_traffic should be the best estimate of travel time given what is known about both historical traffic conditions and live traffic (default).
		/// </summary>
		BestGuess,

		/// <summary>
		/// Indicates that the returned duration_in_traffic should be shorter than the actual travel time on most days, though occasional days with particularly good traffic conditions may be faster than this value.
		/// </summary>
		Optimistic,

		/// <summary>
		/// Indicates that the returned duration_in_traffic should be longer than the actual travel time on most days, though occasional days with particularly bad traffic conditions may exceed this value.
		/// </summary>
		Pessimistic
	}

	/// <summary>
	/// Specifies one or more preferred modes of transit. This parameter may only be specified for transit directions.
	/// </summary>
	public enum TransitModeType :
		byte {
		/// <summary>
		/// Indicates that the calculated route should prefer travel by bus.
		/// </summary>
		Bus,

		/// <summary>
		/// Indicates that the calculated route should prefer travel by tram and light rail.
		/// </summary>
		Rail,

		/// <summary>
		/// Indicates that the calculated route should prefer travel by subway.
		/// </summary>
		Subway,

		/// <summary>
		/// Indicates that the calculated route should prefer travel by train.
		/// </summary>
		Train,

		/// <summary>
		/// Indicates that the calculated route should prefer travel by train, tram, light rail, and subway.
		/// </summary>
		Tram
	}

	/// <summary>
	/// Specifies preferences for transit routes. Using this parameter, you can bias the options returned, rather than accepting the default best route chosen by the API. This parameter may only be specified for transit directions.
	/// </summary>
	public enum TransitRoutingPreferenceType :
		byte {
		/// <summary>
		/// Indicates that the calculated route should prefer a limited number of transfers.
		/// </summary>
		FewerTransfers,

		/// <summary>
		/// Indicates that the calculated route should prefer limited amounts of walking.
		/// </summary>
		LessWalking
	}

	/// <summary>
	/// Specifies the unit system to use when displaying results.
	/// </summary>
	public enum UnitType :
		byte {
#pragma warning disable CS1591
		Imperial,
		Metric
#pragma warning restore CS1591
	}
}