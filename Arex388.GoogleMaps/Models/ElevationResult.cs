using Newtonsoft.Json;
using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class ElevationResult {
	[JsonProperty("elevation")]
	public decimal ElevationInMeters { get; set; }

	public Coordinate Location { get; set; }

	[JsonProperty("resolution")]
	public decimal ResolutionInMeters { get; set; }
}