using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.GoogleMaps;

/// <summary>
/// TimeZone feature object.
/// </summary>
public sealed class TimeZone {
	//	https://developers.google.com/maps/documentation/timezone/requests-timezone

	//	============================================================================
	//	Request
	//	============================================================================

	/// <summary>
	/// TimeZone request object.
	/// </summary>
	public sealed class Request :
		IRequest {
		/// <summary>
		/// The language in which to return results.
		/// </summary>
		// ReSharper disable once MemberHidesStaticFromOuterClass
		public Language? Language { get; init; }

		/// <summary>
		/// A comma-separated latitude,longitude tuple, location=39.6034810,-119.6822510, representing the location to look up.
		/// </summary>
		public string Point { get; init; } = null!;

		/// <summary>
		/// The desired time as seconds since midnight, January 1, 1970 UTC.
		/// </summary>
		public int Timestamp { get; init; }

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
			var parameters = new HashSet<string> {
				$"location={request.Point}",
				$"timestamp={request.Timestamp}"
			};

			if (request.Language.HasValue) {
				parameters.Add($"language={GetLanguage(request)}");
			}

			return $"https://maps.googleapis.com/maps/api/timezone/json?{parameters.StringJoin("&")}";
		}

		private static string GetLanguage(
			Request request) => request.Language switch {
				TimeZone.Language.Arabic => "ar",
				TimeZone.Language.Belarusian => "be",
				TimeZone.Language.Bulgarian => "bg",
				TimeZone.Language.Bengali => "bn",
				TimeZone.Language.Catalan => "ca",
				TimeZone.Language.Czech => "cs",
				TimeZone.Language.Danish => "da",
				TimeZone.Language.German => "de",
				TimeZone.Language.Greek => "el",
				TimeZone.Language.English => "en",
				TimeZone.Language.EnglishAustralian => "en-AU",
				TimeZone.Language.EnglishGreatBritain => "en-GB",
				TimeZone.Language.Spanish => "es",
				TimeZone.Language.Basque => "eu",
				TimeZone.Language.Farsi => "fa",
				TimeZone.Language.Finnish => "fi",
				TimeZone.Language.Filipino => "fil",
				TimeZone.Language.French => "fr",
				TimeZone.Language.Galician => "gl",
				TimeZone.Language.Gujarati => "gu",
				TimeZone.Language.Hindi => "hi",
				TimeZone.Language.Croatian => "hr",
				TimeZone.Language.Hungarian => "hu",
				TimeZone.Language.Indonesian => "id",
				TimeZone.Language.Italian => "it",
				TimeZone.Language.Hebrew => "iw",
				TimeZone.Language.Japanese => "ja",
				TimeZone.Language.Kazakh => "kk",
				TimeZone.Language.Kannada => "kn",
				TimeZone.Language.Korean => "ko",
				TimeZone.Language.Kyrgyz => "ky",
				TimeZone.Language.Lithuanian => "lt",
				TimeZone.Language.Latvian => "lv",
				TimeZone.Language.Macedonian => "mk",
				TimeZone.Language.Malayalam => "ml",
				TimeZone.Language.Marathi => "mr",
				TimeZone.Language.Burmese => "my",
				TimeZone.Language.Dutch => "nl",
				TimeZone.Language.Norwegian => "no",
				TimeZone.Language.Punjabi => "pa",
				TimeZone.Language.Polish => "pl",
				TimeZone.Language.Portuguese => "pt",
				TimeZone.Language.PortugueseBrazil => "pt-BR",
				TimeZone.Language.PortuguesePortugal => "pt-PT",
				TimeZone.Language.Romanian => "ro",
				TimeZone.Language.Russian => "ru",
				TimeZone.Language.Slovak => "sk",
				TimeZone.Language.Slovenian => "sl",
				TimeZone.Language.Albanian => "sq",
				TimeZone.Language.Serbian => "sr",
				TimeZone.Language.Swedish => "sv",
				TimeZone.Language.Tamil => "ta",
				TimeZone.Language.Telugu => "te",
				TimeZone.Language.Thai => "th",
				TimeZone.Language.Tagalog => "tl",
				TimeZone.Language.Turkish => "tr",
				TimeZone.Language.Ukrainian => "uk",
				TimeZone.Language.Uzbek => "uz",
				TimeZone.Language.Vietnamese => "vi",
				TimeZone.Language.ChineseSimplified => "zh-CN",
				TimeZone.Language.ChineseTraditional => "zh-TW",
				_ => throw new InvalidOperationException($"Unable to transform {nameof(Language)} value.")
			};
	}

	//	============================================================================
	//	Response
	//	============================================================================

	/// <summary>
	/// TimeZone response object.
	/// </summary>
	public sealed class Response :
		IResponse {
		/// <summary>
		/// The offset for daylight-savings time in seconds. This will be zero if the time zone is not in Daylight Savings Time during the specified timestamp.
		/// </summary>
		[JsonPropertyName("dstOffset")]
		public int? DstOffsetInSeconds { get; init; }

		/// <summary>
		/// The offset from UTC (in seconds) for the given location. This does not take into effect daylight savings.
		/// </summary>
		[JsonPropertyName("rawOffset")]
		public int? RawOffsetInSeconds { get; init; }

		/// <summary>
		/// The status field within the Time Zone response object contains the status of the request.
		/// </summary>
		[JsonConverter(typeof(StatusJsonConverter))]
		public Status Status { get; init; } = Status.Unknown;

		/// <summary>
		/// A string containing the ID of the time zone, such as "America/Los_Angeles" or "Australia/Sydney".
		/// </summary>
		public string? TimeZoneId { get; init; }

		/// <summary>
		/// The long form name of the time zone.
		/// </summary>
		public string? TimeZoneName { get; init; }

		//	========================================================================
		//	IResponse
		//	========================================================================

		/// <summary>
		/// Detailed information about the reasons behind the given status code. Included if status other than Ok.
		/// </summary>
		[JsonPropertyName("errorMessage")]
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
	/// The status field within the Time Zone response object contains the status of the request.
	/// </summary>
	public enum Status :
		byte {
		/// <summary>
		/// Indicates that the request was malformed.
		/// </summary>
		InvalidRequest,

		/// <summary>
		/// Indicates that the request was successful.
		/// </summary>
		Ok,

		/// <summary>
		/// Indicates any of the following: The API key is missing or invalid; Billing has not been enabled on your account; A self-imposed usage cap has been exceeded; OR The provided method of payment is no longer valid (for example, a credit card has expired).
		/// </summary>
		OverDailyLimit,

		/// <summary>
		/// Indicates the requestor has exceeded quota.
		/// </summary>
		OverQueryLimit,

		/// <summary>
		/// Indicates that the API did not complete the request.
		/// </summary>
		RequestDenied,

		/// <summary>
		/// Indicates an unknown error.
		/// </summary>
		Unknown,

		/// <summary>
		/// Indicates that no time zone data could be found for the specified position or time.
		/// </summary>
		ZeroResults
	}

	private sealed class StatusJsonConverter :
		JsonConverter<Status> {
		public override Status Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
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
}