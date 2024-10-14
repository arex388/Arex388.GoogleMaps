using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Arex388.GoogleMaps;

/// <summary>
/// AddressValidation feature object.
/// </summary>
public sealed class AddressValidation {
	//	https://developers.google.com/maps/documentation/address-validation/requests-validate-address

	//	============================================================================
	//	Request
	//	============================================================================

	/// <summary>
	/// AddressValidation request object.
	/// </summary>
	public sealed class Request :
		IRequest {
		/// <summary>
		/// The address being validated. Unformatted addresses should be submitted via addressLines.
		/// </summary>
		public PostalAddress Address { get; set; } = null!;

		/// <summary>
		/// Enables USPS CASS compatible mode.
		/// </summary>
		public bool EnableUspsCass { get; init; }

		/// <summary>
		/// This field must be empty for the first address validation request. If more requests are necessary to fully validate a single address (for example if the changes the user makes after the initial validation need to be re-validated), then each followup request must populate this field with the responseId from the very first response in the validation sequence.
		/// </summary>
		public string? PreviousResponseId { get; init; }

		//	========================================================================
		//	IRequest
		//	========================================================================

		[JsonIgnore]
		string IRequest.Endpoint => "https://addressvalidation.googleapis.com/v1:validateAddress?";

		[JsonIgnore]
		HttpMethod IRequest.Method => HttpMethod.Post;
	}

	//	============================================================================
	//	Response
	//	============================================================================

	/// <summary>
	/// AddressValidation response object.
	/// </summary>
	public sealed class Response :
		IResponse {
		//	========================================================================
		//	IResponse
		//	========================================================================

		/// <summary>
		/// 
		/// </summary>
		public string? ResponseError { get; init; }

		/// <summary>
		/// The raw response JSON if debugging was enabled.
		/// </summary>
		public string? ResponseJson { get; set; }

		/// <summary>
		/// The overall response status.
		/// </summary>
		public ResponseStatus ResponseStatus { get; set; }
	}

	//	============================================================================
	//	Models
	//	============================================================================

	/// <summary>
	/// Represents a postal address, e.g. for postal delivery or payments addresses.
	/// </summary>
	public sealed class PostalAddress {
		/// <summary>
		/// Highest administrative subdivision which is used for postal addresses of a country or region. For example, this can be a state, a province, an oblast, or a prefecture. Specifically, for Spain this is the province and not the autonomous community (e.g. "Barcelona" and not "Catalonia"). Many countries don't use an administrative area in postal addresses. E.g. in Switzerland this should be left unpopulated.
		/// </summary>
		public string? AdministrativeArea { get; init; }

		/// <summary>
		/// Unstructured address lines describing the lower levels of an address.
		/// </summary>
		public IEnumerable<string> AddressLines { get; init; } = Enumerable.Empty<string>();

		/// <summary>
		/// Generally refers to the city/town portion of the address. Examples: US city, IT comune, UK post town. In regions of the world where localities are not well defined or do not fit into this structure well, leave locality empty and use addressLines.
		/// </summary>
		public string? Locality { get; init; }

		/// <summary>
		/// Postal code of the address. Not all countries use or require postal codes to be present, but where they are used, they may trigger additional validation with other parts of the address (e.g. state/zip validation in the U.S.A.).
		/// </summary>
		public string? PostalCode { get; init; }

		/// <summary>
		/// CLDR region code of the country/region of the address.
		/// </summary>
		public string? RegionCode { get; init; }

		/// <summary>
		/// Additional, country-specific, sorting code. This is not used in most regions. Where it is used, the value is either a string like "CEDEX", optionally followed by a number (e.g. "CEDEX 7"), or just a number alone, representing the "sector code" (Jamaica), "delivery area indicator" (Malawi) or "post office indicator" (e.g. Côte d'Ivoire).
		/// </summary>
		public string? SortingCode { get; init; }

		/// <summary>
		/// Sublocality of the address. For example, this can be neighborhoods, boroughs, districts.
		/// </summary>
		public string? Sublocality { get; init; }
	}
}