using System.Net.Http;
using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public abstract class RequestBase {
		[JsonIgnore]
		protected string EndpointRoot = "https://maps.googleapis.com/maps/api";

		[JsonIgnore]
		public abstract string Endpoint { get; }
		[JsonIgnore]
		public virtual HttpMethod Method { get; } = HttpMethod.Get;
	}
}