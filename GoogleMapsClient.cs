﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arex388.GoogleMaps {
	public sealed class GoogleMapsClient {
		private HttpClient Client { get; }
		private string Key { get; }

		public GoogleMapsClient(
			HttpClient client,
			string key) {
			Client = client;
			Key = key;
		}

		public async Task<DistanceMatrixResponse> GetDistanceMatrixAsync(
			string origin,
			string destination) {
			return await GetDistanceMatrixAsync(new DistanceMatrixRequest {
				Destinations = new[] {
					destination
				},
				Origins = new[] {
					origin
				}
			});
		}

		public async Task<DistanceMatrixResponse> GetDistanceMatrixAsync(
			IEnumerable<string> origins,
			IEnumerable<string> destinations) {
			return await GetDistanceMatrixAsync(new DistanceMatrixRequest {
				Destinations = destinations,
				Origins = origins
			});
		}

		public async Task<DistanceMatrixResponse> GetDistanceMatrixAsync(
			DistanceMatrixRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<DistanceMatrixResponse>(response);
		}

		public async Task<ElevationResponse> GetElevationAsync(
			string location) {
			return await GetElevationAsync(new ElevationRequest {
				Locations = new[] {
					location
				}
			});
		}

		public async Task<ElevationResponse> GetElevationAsync(
			IEnumerable<string> locations) {
			return await GetElevationAsync(new ElevationRequest {
				Locations = locations
			});
		}

		public async Task<ElevationResponse> GetElevationAsync(
			ElevationRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<ElevationResponse>(response);
		}

		public async Task<GeocodeResponse> GetGeocodeAsync(
			string address) {
			return await GetGeocodeAsync(new GeocodeRequest {
				Address = address
			});
		}

		public async Task<GeocodeResponse> GetGeocodeAsync(
			GeocodeRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<GeocodeResponse>(response);
		}

		public async Task<GeocodeResponse> GetReverseGeocodeAsync(
			string location) {
			return await GetGeocodeAsync(new GeocodeRequest {
				Location = location
			});
		}

		public async Task<TimeZoneResponse> GetTimeZoneAsync(
			string location) {
			return await GetTimeZoneAsync(new TimeZoneRequest {
				Location = location
			});
		}

		public async Task<TimeZoneResponse> GetTimeZoneAsync(
			TimeZoneRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<TimeZoneResponse>(response);
		}

		private async Task<string> GetResponseAsync(
			RequestBase request) {
			var endpoint = $"{request.Endpoint}&key={Key}";

			try {
				if (request.Method == HttpMethod.Get) {
					var response = await Client.GetAsync(endpoint);

					return await response.Content.ReadAsStringAsync();
				}

				var json = JsonConvert.SerializeObject(request);

				using (var content = new StringContent(json, Encoding.UTF8, "application/json")) {
					using (var message = await Client.PostAsync(endpoint, content)) {
						return await message.Content.ReadAsStringAsync();
					}
				}
			} catch (HttpRequestException e) {
				return null;
			}
		}
	}
}