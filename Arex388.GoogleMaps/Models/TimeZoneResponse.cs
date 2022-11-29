using Newtonsoft.Json;
using System;

namespace Arex388.GoogleMaps; 

[Obsolete("Will be removed in 2.1.0.")]
public sealed class TimeZoneResponse :
	ResponseBase {
	[JsonProperty("dstOffset")]
	public int DstOffsetInSeconds { get; set; }

	[JsonProperty("rawOffset")]
	public int RawOffsetInSeconds { get; set; }

	public string TimeZoneId { get; set; }
	public string TimeZoneName { get; set; }
}