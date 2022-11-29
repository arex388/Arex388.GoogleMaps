using System.Net.Http;

namespace Arex388.GoogleMaps;

internal interface IRequest {
	string Endpoint { get; }
	HttpMethod Method { get; }
}