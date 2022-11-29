using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.GoogleMaps.Tests;

public sealed class ReverseGeocode {
	private readonly IGoogleMapsClient _googleMaps;

	public ReverseGeocode() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Geocode>().Build();
		var key = configuration["GoogleMapsKey"];

		_googleMaps = new GoogleMapsClient(new HttpClient(), key!, true);
	}

	[Fact]
	public async Task DoAsync() {
		var response = await _googleMaps.ReverseGeocodeAsync("38.897675,-77.036547").ConfigureAwait(false);
		var result = response.Results.First();

		response.ResponseStatus.Should().Be(ResponseStatus.Succeeded);
		result.FormattedAddress.Should().Be("1600 Pennsylvania Avenue NW, Washington, DC 20500, USA");
	}
}