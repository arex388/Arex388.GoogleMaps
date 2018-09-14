using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Arex388.GoogleMaps {
	public sealed class GeocodeRequest :
		RequestBase {
		public string Address { get; set; }
		public IEnumerable<string> Bounds { get; set; } = Enumerable.Empty<string>();
		public IEnumerable<string> Components { get; set; } = Enumerable.Empty<string>();
		public override string Endpoint {
			get {
				var parameters = new HashSet<string>();

				if (!string.IsNullOrEmpty(Address)) {
					var address = WebUtility.UrlEncode(Address);

					parameters.Add($"address={address}");
				}

				if (Bounds.Any()) {
					var bounds = string.Join("|", Bounds);

					parameters.Add($"bounds={bounds}");
				}

				if (Components.Any()) {
					var components = string.Join("|", Components);

					parameters.Add($"components={components}");
				}

				if (!string.IsNullOrEmpty(Language)) {
					parameters.Add($"language={Language}");
				}

				if (!string.IsNullOrEmpty(Location)) {
					parameters.Add($"latlng={Location}");
				}

				if (LocationTypes.Any()) {
					var locationTypes = string.Join("|", LocationTypes);

					parameters.Add($"location_type={locationTypes}");
				}

				if (!string.IsNullOrEmpty(Region)) {
					parameters.Add($"region={Region}");
				}

				if (ResultTypes.Any()) {
					var resultTypes = string.Join("|", ResultTypes);

					parameters.Add($"result_type={resultTypes}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/geocode/json?{query}";
			}
		}
		public string Language { get; set; } = Languages.English;
		public string Location { get; set; }
		public IEnumerable<string> LocationTypes { get; set; } = Enumerable.Empty<string>();
		public string Region { get; set; }
		public IEnumerable<string> ResultTypes { get; set; } = Enumerable.Empty<string>();
	}
}