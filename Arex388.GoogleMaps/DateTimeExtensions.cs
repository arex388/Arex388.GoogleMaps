using System;

namespace Arex388.GoogleMaps;

[Obsolete("Will be removed in 2.1.0.")]
internal static class DateTimeExtensions {
	public static int ToEpochSeconds(
		this DateTime dateTime) {
		var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		return (int)dateTime.Subtract(epoch).TotalSeconds;
	}
}