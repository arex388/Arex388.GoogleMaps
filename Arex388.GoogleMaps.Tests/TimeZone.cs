using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.GoogleMaps.Tests;

public sealed class TimeZone {
	private readonly IGoogleMapsClient _googleMaps;

	public TimeZone() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Geocode>().Build();
		var key = configuration["GoogleMapsKey"];

		_googleMaps = new GoogleMapsClient(new HttpClient(), key!, true);
	}

	[Fact]
	public async Task DoAsync() {
		var response = await _googleMaps.TimeZoneAsync("38.897675,-77.036547").ConfigureAwait(false);

		response.ResponseStatus.Should().Be(ResponseStatus.Succeeded);
		response.TimeZoneName.Should().Be("Eastern Standard Time");
	}
}