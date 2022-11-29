using Newtonsoft.Json;
using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class Coordinate {
	[JsonProperty("lat")]
	public decimal Latitude { get; set; }

	[JsonProperty("lng")]
	public decimal Longitude { get; set; }
}