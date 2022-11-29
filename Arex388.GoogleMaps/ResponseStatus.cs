namespace Arex388.GoogleMaps;

#pragma warning disable CS1591
public enum ResponseStatus :
#pragma warning restore CS1591
	byte {
	/// <summary>
	/// The request was cancelled.
	/// </summary>
	Cancelled,

	/// <summary>
	/// The request failed.
	/// </summary>
	Failed,

	/// <summary>
	/// The request is invalid.
	/// </summary>
	Invalid,

	/// <summary>
	/// The request succeeded.
	/// </summary>
	Succeeded,

	/// <summary>
	/// The request timed out.
	/// </summary>
	TimedOut
}