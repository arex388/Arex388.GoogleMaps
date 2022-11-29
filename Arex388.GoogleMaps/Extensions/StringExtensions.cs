namespace System.Collections.Generic;

//	2.0.0
internal static class StringExtensions {
	public static bool HasValue(
		this string? value) => !string.IsNullOrEmpty(value);

	public static string StringJoin(
		this IEnumerable<string> values,
		string? separator = null) => string.Join(separator, values);
}