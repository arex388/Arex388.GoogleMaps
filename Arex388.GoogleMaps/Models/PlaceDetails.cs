//using System.Net.Http;

//namespace Arex388.GoogleMaps;

////	2.0.0
//public sealed class PlaceDetails {
//	//	https://developers.google.com/maps/documentation/places/web-service/details

//	//	============================================================================
//	//	Request
//	//	============================================================================

//	public sealed class Request :
//		IRequest {
//		public string Id { get; set; } = null!;

//		//	========================================================================
//		//	IRequest
//		//	========================================================================

//		public string Endpoint => $"https://maps.googleapis.com/maps/api/place/details/json?place_id={Id}";
//		public HttpMethod Method => HttpMethod.Get;
//	}

//	//	============================================================================
//	//	Response
//	//	============================================================================

//	public sealed class Response :
//		IResponse {
//		//	========================================================================
//		//	IResponse
//		//	========================================================================

//		public string? ResponseError { get; set; }
//		public string? ResponseJson { get; set; }
//		public ResponseStatus ResponseStatus { get; set; }
//	}

//	//	============================================================================
//	//	Models
//	//	============================================================================
//}