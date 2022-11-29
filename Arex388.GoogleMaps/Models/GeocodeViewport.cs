using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class GeocodeViewport {
	public Coordinate NorthEast { get; set; }
	public Coordinate SouthWest { get; set; }
}