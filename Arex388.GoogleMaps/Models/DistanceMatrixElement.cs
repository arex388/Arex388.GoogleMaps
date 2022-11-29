using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class DistanceMatrixElement {
	public TextValuePair Distance { get; set; }
	public TextValuePair Duration { get; set; }
	public string Status { get; set; }
}