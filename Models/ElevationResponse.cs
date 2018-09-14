using System.Collections.Generic;

namespace Arex388.GoogleMaps {
	public sealed class ElevationResponse :
		ResponseBase {
		public IEnumerable<ElevationResult> Results { get; set; }
	}
}