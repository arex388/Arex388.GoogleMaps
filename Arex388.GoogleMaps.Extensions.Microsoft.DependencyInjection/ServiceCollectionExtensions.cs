using Arex388.GoogleMaps;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions {
	/// <summary>
	/// Add Google Maps API client as a singleton.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="key">Your Google Maps API key.</param>
	/// <param name="debug">Flag indicating if the raw JSON response should be appended to the response objects for debugging.</param>
	/// <returns>IServiceCollection</returns>
	public static IServiceCollection AddGoogleMaps(
		this IServiceCollection services,
		string? key,
		bool debug = false) {
		if (key is null) {
			throw new ArgumentNullException(nameof(key));
		}

		services.AddHttpClient<GoogleMapsClient>(nameof(GoogleMapsClient));

		return services.AddSingleton<IGoogleMapsClient>(
			_ => {
				var httpClientFactory = _.GetRequiredService<IHttpClientFactory>();
				var httpClient = httpClientFactory.CreateClient(nameof(GoogleMapsClient));

				return new GoogleMapsClient(httpClient, key, debug);
			});
	}
}