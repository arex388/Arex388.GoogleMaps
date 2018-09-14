using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class Component {
		[JsonProperty("long_name")]
		public string LongName { get; set; }
		[JsonProperty("short_name")]
		public string ShortName { get; set; }
		public IEnumerable<string> Types { get; set; }
	}
}