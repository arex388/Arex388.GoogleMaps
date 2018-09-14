using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class PlusCode {
		[JsonProperty("compound_code")]
		public string CompoundCode { get; set; }
		[JsonProperty("global_code")]
		public string GlobalCode { get; set; }
	}
}