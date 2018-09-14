using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class TimeZoneResponse :
		ResponseBase {
		[JsonProperty("dstOffset")]
		public int DstOffsetInSeconds { get; set; }
		[JsonProperty("rawOffset")]
		public int RawOffsetInSeconds { get; set; }
		public string TimeZoneId { get; set; }
		public string TimeZoneName { get; set; }
	}
}