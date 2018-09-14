using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class Coordinate {
		[JsonProperty("lat")]
		public decimal Latitude { get; set; }
		[JsonProperty("lng")]
		public decimal Longitude { get; set; }
	}
}