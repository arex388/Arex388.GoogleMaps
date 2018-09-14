using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class GeocodeGeometry {
		public Coordinate Location { get; set; }
		[JsonProperty("location_type")]
		public string Type { get; set; }
		public GeocodeViewport Viewport { get; set; }
	}
}