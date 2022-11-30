using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arex388.GoogleMaps;

/// <summary>
/// Google Maps API client extensions.
/// </summary>
public static class GoogleMapsClientExtensions {
	/// <summary>
	/// Get the most accurate latitude and longitude point of an address.
	/// </summary>
	/// <param name="googleMaps">The Google Maps API client instance.</param>
	/// <param name="address">The address to geocode.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Tupple of (latitude, longitude) as decimals.</returns>
	public static async Task<(
		decimal latitude,
		decimal longitude
	)?> GetPointAsync(
		this IGoogleMapsClient googleMaps,
		string address,
		CancellationToken cancellationToken) {
		if (!address.HasValue()) {
			return null;
		}

		var response = await googleMaps.GeocodeAsync(address, cancellationToken).ConfigureAwait(false);

		if (response.ResponseStatus != ResponseStatus.Succeeded
			|| response.Status != Geocode.Status.Ok) {
			return null;
		}

		var result = response.Results.FirstOrDefault();

		if (result is null) {
			return null;
		}

		return (
			result.Geometry.Point.Latitude,
			result.Geometry.Point.Longitude
		);
	}
}