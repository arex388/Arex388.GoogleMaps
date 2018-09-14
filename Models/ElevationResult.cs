using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class ElevationResult {
		[JsonProperty("elevation")]
		public decimal ElevationInMeters { get; set; }
		public Coordinate Location { get; set; }
		[JsonProperty("resolution")]
		public decimal ResolutionInMeters { get; set; }
	}
}