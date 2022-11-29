using System.Net.Http;

namespace Arex388.GoogleMaps;

//	2.0.0
internal interface IRequest {
	string Endpoint { get; }
	HttpMethod Method { get; }
}

//internal abstract class RequestBase {
//	internal abstract string Endpoint { get; }
//	internal abstract HttpMethod Method { get; }
//}