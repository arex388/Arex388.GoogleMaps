using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.GoogleMaps.Tests;

public sealed class DistanceMatrix {
	private readonly IGoogleMapsClient _googleMaps;

	public DistanceMatrix() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Geocode>().Build();
		var key = configuration["GoogleMapsKey"];

		_googleMaps = new GoogleMapsClient(new HttpClient(), key!, true);
	}

	[Fact]
	public async Task DoAsync() {
		var response = await _googleMaps.DistanceMatrixAsync("1600 Pennsylvania Ave NW, Washington, DC 20500", "East Capitol St NE & First St SE, Washington, DC 20004").ConfigureAwait(false);
		var row = response.Rows.First();
		var element = row.Elements.First();

		response.ResponseStatus.Should().Be(ResponseStatus.Succeeded);
		element.Distance!.Value.Should().Be(3782);
		element.Duration!.Value.Should().Be(721);
	}
}