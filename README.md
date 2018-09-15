# Arex388.GoogleMaps

Google Maps API Implementation in C#

Yet another implementation of the Google Maps API. To use, just create an instance of `GoogleMapsClient` and pass in an `HttpClient` instance along with your API key. The original documentation can be found [here][0]. For more information please visit [arex388.com][1].

Available as a NuGet package [here][2].

    var googleMaps = new GoogleMapsClient(httpClient, "{key}");

**Get Geocode**

    var geocode = await googleMaps.GetGeocodeAsync("1600 Pennsylvania Ave NW, Washington, DC 20500");

**Get Reverse Geocoding**

    var reverseGeocode = await googleMaps.GetReverseGeocodeAsync("38.897675,-77.036547");

**Get Elevation**

    var elevation = await googleMaps.GetElevationAsync("38.897675,-77.036547");

**Get Time Zone**

    var timeZone = await googleMaps.GetTimeZoneAsync("38.897675,-77.036547");

**Get Distance Matrix**

    var distanceMatrix = await googleMaps.GetDistanceMatrixAsync(
        "East Capitol St NE & First St SE, Washington, DC 20004",
        "1600 Pennsylvania Ave NW, Washington, DC 20500"
    );

[0]:https://developers.google.com/maps/documentation
[1]:https://arex388.com
[2]:https://www.nuget.org/packages/Arex388.GoogleMaps