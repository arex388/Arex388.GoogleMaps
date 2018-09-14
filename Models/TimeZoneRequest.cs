using System;
using System.Collections.Generic;

namespace Arex388.GoogleMaps {
	public sealed class TimeZoneRequest :
		RequestBase {
		public override string Endpoint {
			get {
				var parameters = new HashSet<string> {
					$"timestamp={Timestamp}"
				};

				if (!string.IsNullOrEmpty(Language)) {
					parameters.Add($"language={Language}");
				}

				if (!string.IsNullOrEmpty(Location)) {
					parameters.Add($"location={Location}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/timezone/json?{query}";
			}
		}
		public string Language { get; set; } = Languages.English;
		public string Location { get; set; }
		public int Timestamp { get; set; } = DateTime.UtcNow.ToEpochSeconds();
	}
}