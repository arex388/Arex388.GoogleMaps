using System.Collections.Generic;
using System.Linq;

namespace Arex388.GoogleMaps {
	public sealed class ElevationRequest :
		RequestBase {
		public override string Endpoint {
			get {
				var parameters = new HashSet<string>();

				if (Locations.Any()) {
					var locations = string.Join("|", Locations);

					parameters.Add($"locations={locations}");
				}

				if (Path.Any()) {
					var path = string.Join("|", Path);

					parameters.Add($"path={path}");
				}

				if (Samples.HasValue) {
					parameters.Add($"samples={Samples}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/elevation/json?{query}";
			}
		}
		public IEnumerable<string> Locations { get; set; } = Enumerable.Empty<string>();
		public IEnumerable<string> Path { get; set; } = Enumerable.Empty<string>();
		public int? Samples { get; set; }
	}
}