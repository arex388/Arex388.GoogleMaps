using System;
using System.Collections.Generic;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class DistanceMatrixRow {
	public IEnumerable<DistanceMatrixElement> Elements { get; set; }
}