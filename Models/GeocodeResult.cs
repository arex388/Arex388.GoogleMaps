using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class GeocodeResult {
		[JsonProperty("address_components")]
		public IEnumerable<Component> Components { get; set; }
		[JsonProperty("formatted_address")]
		public string FormattedAddress { get; set; }
		public GeocodeGeometry Geometry { get; set; }
		[JsonProperty("partial_match")]
		public bool PartialMatch { get; set; }
		[JsonProperty("place_id")]
		public string PlaceId { get; set; }
		//[JsonProperty("postcode_localities")]
		//public IEnumerable<string> PostalCodeLocalities { get; set; }
		[JsonProperty("plus_code")]
		public PlusCode PlusCode { get; set; }
		public IEnumerable<string> Types { get; set; }
	}
}