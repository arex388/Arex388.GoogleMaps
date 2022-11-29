using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class DistanceMatrixResponse :
	ResponseBase {
	[JsonProperty("destination_addresses")]
	public IEnumerable<string> DestinationAddresses { get; set; }

	[JsonProperty("origin_addresses")]
	public IEnumerable<string> OriginAddresses { get; set; }

	public IEnumerable<DistanceMatrixRow> Rows { get; set; }
}