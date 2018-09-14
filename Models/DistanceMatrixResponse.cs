using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class DistanceMatrixResponse :
		ResponseBase {
		[JsonProperty("destination_addresses")]
		public IEnumerable<string> DestinationAddresses { get; set; }
		[JsonProperty("origin_addresses")]
		public IEnumerable<string> OriginAddresses { get; set; }
		public IEnumerable<DistanceMatrixRow> Rows { get; set; }
	}
}