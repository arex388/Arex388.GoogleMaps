using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Arex388.GoogleMaps;

/// <summary>
/// Google Maps API client.
/// </summary>
public sealed class GoogleMapsClient :
	IGoogleMapsClient {
	private readonly bool _debug;
	private readonly HttpClient _httpClient;
	private readonly string _key;

	/// <summary>
	/// Google Maps API client.
	/// </summary>
	public GoogleMapsClient(
		HttpClient httpClient,
		string key,
		bool debug = false) {
		_debug = debug;
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_key = key ?? throw new ArgumentNullException(nameof(key));
	}

	/// <summary>
	/// Returns travel distance and time for a matrix of origins and destinations.
	/// </summary>
	/// <param name="origin">The origin address.</param>
	/// <param name="destination">The destination address.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>DistanceMatrix.Response</returns>
	public Task<DistanceMatrix.Response> DistanceMatrixAsync(
		string origin,
		string destination,
		CancellationToken cancellationToken = default) => DistanceMatrixAsync(new DistanceMatrix.Request {
			Destinations = new[] {
				destination
			},
			Origins = new[] {
				origin
			}
		}, cancellationToken);

	/// <summary>
	/// Returns travel distance and time for a matrix of origins and destinations.
	/// </summary>
	/// <param name="origins">The origin addresses.</param>
	/// <param name="destinations">The destination addresses.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>DistanceMatrix.Response</returns>
	public Task<DistanceMatrix.Response> DistanceMatrixAsync(
		IEnumerable<string> origins,
		IEnumerable<string> destinations,
		CancellationToken cancellationToken = default) => DistanceMatrixAsync(new DistanceMatrix.Request {
			Destinations = destinations,
			Origins = origins
		}, cancellationToken);

	/// <summary>
	/// Returns travel distance and time for a matrix of origins and destinations.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>DistanceMatrix.Response</returns>
	public Task<DistanceMatrix.Response> DistanceMatrixAsync(
		DistanceMatrix.Request request,
		CancellationToken cancellationToken = default) => RequestAsync<DistanceMatrix.Response>(request, cancellationToken);

	/// <summary>
	/// Returns the elevation of points on the Earth.
	/// </summary>
	/// <param name="point">The point to check.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Elevation.Response</returns>
	public Task<Elevation.Response> ElevationAsync(
		string point,
		CancellationToken cancellationToken = default) => ElevationAsync(new Elevation.Request {
			Points = new[] {
				point
			}
		}, cancellationToken);

	/// <summary>
	/// Returns the elevation of points on the Earth.
	/// </summary>
	/// <param name="points">The points to check.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Elevation.Response</returns>
	public Task<Elevation.Response> ElevationAsync(
		IEnumerable<string> points,
		CancellationToken cancellationToken = default) => ElevationAsync(new Elevation.Request {
			Points = points
		}, cancellationToken);

	/// <summary>
	/// Returns the elevation of points on the Earth.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Elevation.Response</returns>
	public Task<Elevation.Response> ElevationAsync(
		Elevation.Request request,
		CancellationToken cancellationToken = default) => RequestAsync<Elevation.Response>(request, cancellationToken);

	/// <summary>
	/// Returns the latitude and longitude point of an address.
	/// </summary>
	/// <param name="address">The address to geocode.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	public Task<Geocode.Response> GeocodeAsync(
		string address,
		CancellationToken cancellationToken = default) => GeocodeAsync(new Geocode.Request {
			Address = address
		}, cancellationToken);

	/// <summary>
	/// Returns the latitude and longitude point of an address.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	public Task<Geocode.Response> GeocodeAsync(
		Geocode.Request request,
		CancellationToken cancellationToken = default) => RequestAsync<Geocode.Response>(request, cancellationToken);

	//public Task<NearestRoads.Response> NearestRoadsAsync(
	//	decimal latitude,
	//	decimal longitude,
	//	CancellationToken cancellationToken = default) => NearestRoadsAsync(new NearestRoads.Request {
	//		Points = new[] {
	//			new NearestRoads.Point {
	//				Latitude = latitude,
	//				Longitude = longitude
	//			}
	//		}
	//	}, cancellationToken);

	//public Task<NearestRoads.Response> NearestRoadsAsync(
	//	IEnumerable<NearestRoads.Point> points,
	//	CancellationToken cancellationToken = default) => NearestRoadsAsync(new NearestRoads.Request {
	//		Points = points
	//	}, cancellationToken);

	//public Task<NearestRoads.Response> NearestRoadsAsync(
	//	NearestRoads.Request request,
	//	CancellationToken cancellationToken = default) => RequestAsync<NearestRoads.Response>(request, cancellationToken);

	//public Task<PlaceDetails.Response> PlaceDetailsAsync(
	//	string id,
	//	CancellationToken cancellationToken = default) => PlaceDetailsAsync(new PlaceDetails.Request {
	//		Id = id
	//	}, cancellationToken);

	//public Task<PlaceDetails.Response> PlaceDetailsAsync(
	//	PlaceDetails.Request request,
	//	CancellationToken cancellationToken = default) => RequestAsync<PlaceDetails.Response>(request, cancellationToken);

	/// <summary>
	/// Returns the address for a latitude and longitude point.
	/// </summary>
	/// <param name="point">The point to check.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	public Task<Geocode.Response> ReverseGeocodeAsync(
		string point,
		CancellationToken cancellationToken = default) => ReverseGeocodeAsync(new Geocode.Request {
			Point = point
		}, cancellationToken);

	/// <summary>
	/// Returns the address for a latitude and longitude point.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	public Task<Geocode.Response> ReverseGeocodeAsync(
		Geocode.Request request,
		CancellationToken cancellationToken = default) => RequestAsync<Geocode.Response>(request, cancellationToken);

	/// <summary>
	/// Returns the time zone and UTC offset for a latitude and longitude point.
	/// </summary>
	/// <param name="point">The point to check.</param>
	/// <param name="cancellationToken">Optional cancellation token</param>
	/// <returns>TimeZone.Response</returns>
	public Task<TimeZone.Response> TimeZoneAsync(
		string point,
		CancellationToken cancellationToken = default) => TimeZoneAsync(new TimeZone.Request {
			Point = point
		}, cancellationToken);

	/// <summary>
	/// Returns the time zone and UTC offset for a latitude and longitude point.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns></returns>
	public Task<TimeZone.Response> TimeZoneAsync(
		TimeZone.Request request,
		CancellationToken cancellationToken = default) => RequestAsync<TimeZone.Response>(request, cancellationToken);

	//	============================================================================
	//	Request
	//	============================================================================

	private Task<HttpResponseMessage> GetAsync(
		IRequest request,
		CancellationToken cancellationToken) => _httpClient.GetAsync($"{request.Endpoint}&key={_key}", cancellationToken);

	private Task<HttpResponseMessage> PostAsync(
		IRequest request,
		CancellationToken cancellationToken) => _httpClient.PostAsync($"{request.Endpoint}&key={_key}", null, cancellationToken);

	private async Task<T> RequestAsync<T>(
		IRequest? request,
		CancellationToken cancellationToken)
		where T : IResponse, new() {
		if (request is null) {
			return InvalidResponse<T>();
		}

		if (cancellationToken.IsCancellationRequested) {
			return CancelledResponse<T>();
		}

		try {
			var response = request.Method == HttpMethod.Get
				? await GetAsync(request, cancellationToken).ConfigureAwait(false)
				: await PostAsync(request, cancellationToken).ConfigureAwait(false);

			if (!response.Content.Headers.ContentType.MediaType.Contains("application/json")) {
				var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				return FailedResponse<T>(error);
			}

			var succcess = await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken).ConfigureAwait(false);

			if (succcess is null) {
				return FailedResponse<T>("Response is null.");
			}

			succcess.ResponseStatus = response.StatusCode == HttpStatusCode.OK
									  && !succcess.ResponseError.HasValue()
				? ResponseStatus.Succeeded
				: ResponseStatus.Failed;

			if (_debug) {
				succcess.ResponseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			}

			return succcess;
		} catch (HttpRequestException) {
			return FailedResponse<T>();
		} catch (TaskCanceledException) {
			return TimedOutResponse<T>();
		} catch (Exception e) {
			return FailedResponse<T>($"{e.Message}\n{e.InnerException?.Message}\n{e.StackTrace}");
		}
	}

	//	============================================================================
	//	Utilities
	//	============================================================================

	/// <summary>
	/// Returns a cancelled response of T.
	/// </summary>
	private static T CancelledResponse<T>()
		where T : IResponse, new() => new() {
			ResponseError = "The request was cancelled before completion.",
			ResponseStatus = ResponseStatus.Cancelled
		};

	/// <summary>
	/// Returns a failed response of T.
	/// </summary>
	private static T FailedResponse<T>(
		string error = "Unknown.")
		where T : IResponse, new() => new() {
			ResponseError = $"The request failed with the following error: {error}",
			ResponseStatus = ResponseStatus.Failed
		};

	/// <summary>
	/// Returns an invalid response of T.
	/// </summary>
	private static T InvalidResponse<T>()
		where T : IResponse, new() => new() {
			ResponseError = "The request is invalid.",
			ResponseStatus = ResponseStatus.Invalid
		};

	/// <summary>
	/// Returns a timed out response of T.
	/// </summary>
	private static T TimedOutResponse<T>()
		where T : IResponse, new() => new() {
			ResponseError = "The request timed out before completion.",
			ResponseStatus = ResponseStatus.TimedOut
		};
}