﻿using System;
using System.Collections.Generic;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class ElevationResponse :
	ResponseBase {
	public IEnumerable<ElevationResult> Results { get; set; }
}