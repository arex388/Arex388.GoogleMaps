using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.GoogleMaps.Tests;

public sealed class Geocode {
	private readonly IGoogleMapsClient _googleMaps;

	public Geocode() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Geocode>().Build();
		var key = configuration["GoogleMapsKey"];

		_googleMaps = new GoogleMapsClient(new HttpClient(), key!, true);
	}

	[Fact]
	public async Task DoAsync() {
		var response = await _googleMaps.GeocodeAsync("1600 Pennsylvania Ave NW, Washington, DC 20500").ConfigureAwait(false);
		var result = response.Results.First();

		response.ResponseStatus.Should().Be(ResponseStatus.Succeeded);
		result.FormattedAddress.Should().Be("1600 Pennsylvania Avenue NW, Washington, DC 20500, USA");
		result.Geometry.Point.Latitude.Should().Be(38.8976633M);
		result.Geometry.Point.Longitude.Should().Be(-77.03657389999999M);
	}
}