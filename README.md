# Arex388.GoogleMaps

A C# client for Google Maps API.

Yet another implementation of the Google Maps API. To use, just create an instance of `GoogleMapsClient` and pass in an `HttpClient` instance along with your API key. The original documentation can be found [here](https://developers.google.com/maps/documentation). For more information please visit [arex388.com](https://arex388.com ).

Available as a NuGet package [here](https://www.nuget.org/packages/Arex388.GoogleMaps ).



## Important



#### v2.1.0

Version 2.1.0 is the same as v2.0.0, except it removes all code marked as deprecated in v2.0.0, **so it is a version with breaking changes**.



#### v2.0.0

Version 2.0.0 is a rewrite of the client implementation and sets up the package for breaking changes in 2.1.0. Specifically, all `Get*` methods are deprecated and will be removed in 2.1.0. Switch to the new methods as directed to benefit from the memory and performance improvements.



## How to Use

You can either create an instance of `GoogleMapsClient` or inject it using the `IGoogleMapsClient` interface through dependency injection. A dependency injection package is now available to make it easy to do that.



```c#
var googleMaps = new GoogleMapsClient(
    httpClient,
    "{key}"
);
```



#### Distance Matrix

```c#
var distanceMatrix = await googleMaps.DistanceMatrixAsync(
    "1600 Pennsylvania Ave NW, Washington, DC 20500",
    "East Capitol St NE & First St SE, Washington, DC 20004"
);
```



#### Elevation

```c#
var elevation = await googleMaps.ElevationAsync(
    "38.897675,-77.036547"
);
```



#### Geocode

```c#
var geocode = await googleMaps.GeocodeAsync(
    "1600 Pennsylvania Ave NW, Washington, DC 20500"
);
```



#### Reverse Geocode

```c#
var reverseGeocode = await googleMaps.ReverseGeocodeAsync(
    "38.897675,-77.036547"
);
```



#### Time Zone

```c#
var timeZone = await googleMaps.TimeZoneAsync(
    "38.897675,-77.036547"
);
```



