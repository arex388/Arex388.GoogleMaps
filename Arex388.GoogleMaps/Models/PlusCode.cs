using Newtonsoft.Json;
using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class PlusCode {
	[JsonProperty("compound_code")]
	public string CompoundCode { get; set; }

	[JsonProperty("global_code")]
	public string GlobalCode { get; set; }
}