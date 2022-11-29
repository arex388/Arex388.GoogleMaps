namespace Arex388.GoogleMaps;

//	2.0.0
public interface IResponse {
	string? ResponseError { get; init; }
	string? ResponseJson { get; set; }
	ResponseStatus ResponseStatus { get; set; }
}