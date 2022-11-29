using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arex388.GoogleMaps;

/// <summary>
/// Google Maps API client.
/// </summary>
public interface IGoogleMapsClient {
	/// <summary>
	/// Returns travel distance and time for a matrix of origins and destinations.
	/// </summary>
	/// <param name="origin">The origin address.</param>
	/// <param name="destination">The destination address.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>DistanceMatrix.Response</returns>
	Task<DistanceMatrix.Response> DistanceMatrixAsync(
		string origin,
		string destination,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns travel distance and time for a matrix of origins and destinations.
	/// </summary>
	/// <param name="origins">The origin addresses.</param>
	/// <param name="destinations">The destination addresses.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>DistanceMatrix.Response</returns>
	Task<DistanceMatrix.Response> DistanceMatrixAsync(
		IEnumerable<string> origins,
		IEnumerable<string> destinations,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns travel distance and time for a matrix of origins and destinations.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>DistanceMatrix.Response</returns>
	Task<DistanceMatrix.Response> DistanceMatrixAsync(
		DistanceMatrix.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the elevation of points on the Earth.
	/// </summary>
	/// <param name="point">The point to check.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Elevation.Response</returns>
	Task<Elevation.Response> ElevationAsync(
		string point,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the elevation of points on the Earth.
	/// </summary>
	/// <param name="points">The points to check.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Elevation.Response</returns>
	Task<Elevation.Response> ElevationAsync(
		IEnumerable<string> points,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the elevation of points on the Earth.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Elevation.Response</returns>
	Task<Elevation.Response> ElevationAsync(
		Elevation.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the latitude and longitude point of an address.
	/// </summary>
	/// <param name="address">The address to geocode.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	Task<Geocode.Response> GeocodeAsync(
		string address,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the latitude and longitude point of an address.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	Task<Geocode.Response> GeocodeAsync(
		Geocode.Request request,
		CancellationToken cancellationToken = default);

	//Task<NearestRoads.Response> NearestRoadsAsync(
	//	decimal latitude,
	//	decimal longitude,
	//	CancellationToken cancellationToken);

	//Task<NearestRoads.Response> NearestRoadsAsync(
	//	IEnumerable<NearestRoads.Point> points,
	//	CancellationToken cancellationToken);

	//Task<NearestRoads.Response> NearestRoadsAsync(
	//	NearestRoads.Request request,
	//	CancellationToken cancellationToken);

	//Task<PlaceDetails.Response> PlaceDetailsAsync(
	//	string id,
	//	CancellationToken cancellationToken);

	//Task<PlaceDetails.Response> PlaceDetailsAsync(
	//	PlaceDetails.Request request,
	//	CancellationToken cancellationToken);

	/// <summary>
	/// Returns the address for a latitude and longitude point.
	/// </summary>
	/// <param name="point">The point to check.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	Task<Geocode.Response> ReverseGeocodeAsync(
		string point,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the address for a latitude and longitude point.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns>Geocode.Response</returns>
	Task<Geocode.Response> ReverseGeocodeAsync(
		Geocode.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the time zone and UTC offset for a latitude and longitude point.
	/// </summary>
	/// <param name="point">The point to check.</param>
	/// <param name="cancellationToken">Optional cancellation token</param>
	/// <returns>TimeZone.Response</returns>
	Task<TimeZone.Response> TimeZoneAsync(
		string point,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the time zone and UTC offset for a latitude and longitude point.
	/// </summary>
	/// <param name="request">The request object.</param>
	/// <param name="cancellationToken">Optional cancellation token.</param>
	/// <returns></returns>
	Task<TimeZone.Response> TimeZoneAsync(
		TimeZone.Request request,
		CancellationToken cancellationToken = default);
}