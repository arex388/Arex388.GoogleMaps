using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public sealed class Component {
	[JsonProperty("long_name")]
	public string LongName { get; set; }

	[JsonProperty("short_name")]
	public string ShortName { get; set; }

	public IEnumerable<string> Types { get; set; }
}