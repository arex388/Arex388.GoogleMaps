using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Arex388.GoogleMaps {
	public sealed class DistanceMatrixRequest :
		RequestBase {
		public int? ArrivalTime { get; set; }
		public string AvoidType { get; set; }
		public int DepartureTime { get; set; } = DateTime.UtcNow.ToEpochSeconds();
		public IEnumerable<string> Destinations { get; set; } = Enumerable.Empty<string>();
		public override string Endpoint {
			get {
				var parameters = new HashSet<string> {
					$"departure_time={DepartureTime}"
				};

				if (ArrivalTime.HasValue) {
					parameters.Add($"arrival_time={ArrivalTime}");
				}

				if (!string.IsNullOrEmpty(AvoidType)) {
					parameters.Add($"avoid={AvoidType}");
				}

				if (Destinations.Any()) {
					var destinations = string.Join("|", Destinations.Select(WebUtility.UrlEncode));

					parameters.Add($"destinations={destinations}");
				}

				if (!string.IsNullOrEmpty(Language)) {
					parameters.Add($"language={Language}");
				}

				if (Origins.Any()) {
					var origins = string.Join("|", Origins.Select(WebUtility.UrlEncode));

					parameters.Add($"origins={origins}");
				}

				if (!string.IsNullOrEmpty(Region)) {
					parameters.Add($"region={Region}");
				}

				if (!string.IsNullOrEmpty(TrafficModel)) {
					parameters.Add($"traffic_model={TrafficModel}");
				}

				if (!string.IsNullOrEmpty(TransitMode)) {
					parameters.Add($"transit_mode={TransitMode}");
				}

				if (!string.IsNullOrEmpty(TransitRoutingPreference)) {
					parameters.Add($"transit_routing_preference={TransitRoutingPreference}");
				}

				if (!string.IsNullOrEmpty(TravelMode)) {
					parameters.Add($"mode={TravelMode}");
				}

				if (!string.IsNullOrEmpty(UnitType)) {
					parameters.Add($"units={UnitType}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/distancematrix/json?{query}";
			}
		}
		public string Language { get; set; } = Languages.English;
		public IEnumerable<string> Origins { get; set; } = Enumerable.Empty<string>();
		public string Region { get; set; }
		public string TrafficModel { get; set; } = TrafficModelType.BestGuess;
		public string TransitMode { get; set; }
		public string TransitRoutingPreference { get; set; }
		public string TravelMode { get; set; } = TravelModes.Driving;
		public string UnitType { get; set; } = UnitTypes.Imperial;
	}
}