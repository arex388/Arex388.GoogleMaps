namespace Arex388.GoogleMaps;

#pragma warning disable CS1591
public interface IResponse {
#pragma warning restore CS1591
	/// <summary>
	/// Response errors, if any.
	/// </summary>
	string? ResponseError { get; init; }

	/// <summary>
	/// The raw response JSON if debugging was enabled.
	/// </summary>
	string? ResponseJson { get; set; }

	/// <summary>
	/// The overall response status.
	/// </summary>
	ResponseStatus ResponseStatus { get; set; }
}