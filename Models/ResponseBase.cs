using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public abstract class ResponseBase {
		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }
		public string Status { get; set; }
	}
}