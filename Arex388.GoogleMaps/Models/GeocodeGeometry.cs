using Newtonsoft.Json;
using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class GeocodeGeometry {
	public Coordinate Location { get; set; }

	[JsonProperty("location_type")]
	public string Type { get; set; }

	public GeocodeViewport Viewport { get; set; }
}