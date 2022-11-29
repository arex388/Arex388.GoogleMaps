using Newtonsoft.Json;
using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public abstract class ResponseBase {
	[JsonProperty("error_message")]
	public string ErrorMessage { get; set; }

	public string Status { get; set; }
}