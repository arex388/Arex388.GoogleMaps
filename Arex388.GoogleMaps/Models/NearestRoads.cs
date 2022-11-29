//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;

//namespace Arex388.GoogleMaps;

////	2.0.0
//public sealed class NearestRoads {
//	//	https://developers.google.com/maps/documentation/roads/nearest

//	//	============================================================================
//	//	Request
//	//	============================================================================

//	public sealed class Request :
//		IRequest {
//		public IEnumerable<Point> Points { get; set; } = Enumerable.Empty<Point>();

//		//	========================================================================
//		//	IRequest
//		//	========================================================================

//		public string Endpoint => GetEndpoint(this);
//		public HttpMethod Method => HttpMethod.Get;

//		//	========================================================================
//		//	Utilities
//		//	========================================================================

//		private static string GetEndpoint(
//			Request request) {
//			var points = request.Points.Select(
//				_ => $"{_.Latitude},{_.Longitude}").StringJoin("|");

//			return $"https://roads.googleapis.com/v1/nearestRoads?points={points}";
//		}
//	}

//	//	============================================================================
//	//	Response
//	//	============================================================================

//	public sealed class Response :
//		IResponse {
//		public IEnumerable<SnappedPoint> SnappedPoints { get; set; } = Enumerable.Empty<SnappedPoint>();

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

//	public struct Point {
//		public decimal Latitude { get; set; }
//		public decimal Longitude { get; set; }
//	}

//	public sealed class SnappedPoint {
//		public Point Location { get; set; }
//		public int? OriginalIndex { get; set; }
//		public string PlaceId { get; set; } = null!;
//	}
//}