using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
public abstract class RequestBase {
	[JsonIgnore]
	protected string EndpointRoot = "https://maps.googleapis.com/maps/api";

	[JsonIgnore]
	public abstract string Endpoint { get; }

	[JsonIgnore]
	public virtual HttpMethod Method { get; } = HttpMethod.Get;
}