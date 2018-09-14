using System.Collections.Generic;

namespace Arex388.GoogleMaps {
	public sealed class GeocodeResponse :
		ResponseBase {
		public IEnumerable<GeocodeResult> Results { get; set; }
	}
}