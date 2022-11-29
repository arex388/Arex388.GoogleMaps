using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.GoogleMaps;

//	2.0.0
/// <summary>
/// Elevation feature object.
/// </summary>
public sealed class Elevation {
	//	https://developers.google.com/maps/documentation/elevation/requests-elevation

	//	============================================================================
	//	Request
	//	============================================================================

	/// <summary>
	/// Elevation request object.
	/// </summary>
	public sealed class Request :
		IRequest {
		/// <summary>
		/// Defines the location(s) on the earth from which to return elevation data. This parameter takes either a single location as a comma-separated {latitude,longitude} pair (e.g. "40.714728,-73.998672") or multiple latitude/longitude pairs passed as an array or as an encoded polyline.
		/// </summary>
		public IEnumerable<string> Points { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// Defines a path on the earth for which to return elevation data. This parameter defines a set of two or more ordered {latitude,longitude} pairs defining a path along the surface of the earth. This parameter must be used in conjunction with the samples parameter described below.
		/// </summary>
		public IEnumerable<string> Path { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// Specifies the number of sample points along a path for which to return elevation data. The samples parameter divides the given path into an ordered set of equidistant points along the path.
		/// </summary>
		public int? Samples { get; init; }

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

			if (request.Points.Any()) {
				var locations = request.Points.StringJoin("|");

				parameters.Add($"locations={locations}");
			}

			if (request.Path.Any()) {
				var path = request.Path.StringJoin("|");

				parameters.Add($"path={path}");
			}

			if (request.Samples.HasValue) {
				parameters.Add($"samples={request.Samples}");
			}

			return $"https://maps.googleapis.com/maps/api/elevation/json?{parameters.StringJoin("&")}";
		}
	}

	//	============================================================================
	//	Response
	//	============================================================================

	/// <summary>
	/// Elevation response object.
	/// </summary>
	public sealed class Response :
		IResponse {
		/// <summary>
		/// An array of results returned by the API.
		/// </summary>
		public IEnumerable<Result> Results { get; init; } = Enumerable.Empty<Result>();

		/// <summary>
		/// Status codes returned by service.
		/// </summary>
		[JsonConverter(typeof(StatusJsonConverter))]
		public Status Status { get; init; } = Status.Unknown;

		//	========================================================================
		//	IResponse
		//	========================================================================

		/// <summary>
		/// When the service returns a status code other than OK, there may be an additional error_message field within the response object. This field contains more detailed information about thereasons behind the given status code. This field is not always returned, and its content is subject to change.
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
	/// An object describing a specific location with Latitude and Longitude in decimal degrees.
	/// </summary>
	public struct Point {
		/// <summary>
		/// Latitude in decimal degrees.
		/// </summary>
		[JsonPropertyName("lat")]
		public decimal Latitude { get; init; }

		/// <summary>
		/// Longitude in decimal degrees.
		/// </summary>
		[JsonPropertyName("lng")]
		public decimal Longitude { get; init; }
	}

	/// <summary>
	/// Elevation response result object.
	/// </summary>
	public sealed class Result {
		/// <summary>
		/// The elevation of the location in meters.
		/// </summary>
		public decimal Elevation { get; init; }

		/// <summary>
		/// A location element of the position for which elevation data is being computed. Note that for path requests, the set of location elements will contain the sampled points along the path.
		/// </summary>
		public Point Location { get; init; }

		/// <summary>
		/// The value indicating the maximum distance between data points from which the elevation was interpolated, in meters. This property will be missing if the resolution is not known. Note that elevation data becomes more coarse (larger resolution values) when multiple points are passed. To obtain the most accurate elevation value for a point, it should be queried independently.
		/// </summary>
		public decimal Resolution { get; init; }
	}

	/// <summary>
	/// Status codes returned by service.
	/// </summary>
	public enum Status :
		byte {
		/// <summary>
		/// Indicating that there's no available data for the input locations.
		/// </summary>
		DataNotAvailable,

		/// <summary>
		/// Indicating the API request was malformed.
		/// </summary>
		InvalidRequest,

		/// <summary>
		/// Indicating the API request was successful.
		/// </summary>
		Ok,

		/// <summary>
		/// IIndicating any of the following: The API key is missing or invalid; Billing has not been enabled on your account; A self-imposed usage cap has been exceeded; OR The provided method of payment is no longer valid (for example, a credit card has expired).
		/// </summary>
		OverDailyLimit,

		/// <summary>
		/// Indicating the requestor has exceeded quota.
		/// </summary>
		OverQueryLimit,

		/// <summary>
		/// Indicating the API did not complete the request.
		/// </summary>
		RequestDenied,

		/// <summary>
		/// Indicating an unknown error.
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
				"DATA_NOT_AVAILABLE" => Status.DataNotAvailable,
				"INVALID_REQUEST" => Status.InvalidRequest,
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
}