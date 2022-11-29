using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.GoogleMaps.Tests;

public sealed class Elevation {
	private readonly IGoogleMapsClient _googleMaps;

	public Elevation() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Geocode>().Build();
		var key = configuration["GoogleMapsKey"];

		_googleMaps = new GoogleMapsClient(new HttpClient(), key!, true);
	}

	[Fact]
	public async Task DoAsync() {
		var response = await _googleMaps.ElevationAsync("38.897675,-77.036547").ConfigureAwait(false);
		var result = response.Results.First();

		response.ResponseStatus.Should().Be(ResponseStatus.Succeeded);
		result.Elevation.Should().Be(17.96859359741211M);
	}
}