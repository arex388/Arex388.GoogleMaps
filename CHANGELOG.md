#### 2.1.0 (2022-11-29)

- **Removed:** All `Get*` methods and supporting code.



#### 2.0.0 (2022-11-28)

This is a major version that overhauls the implementation and deprecates the current method calls. New method implementations are introduced in their place. Cleaned up async implementation. Introduced an interface to use instead of the concrete class. Added a dependency injection extension package.

- **Added:** `IGoogleMapsClient` interface to use with dependency injection instead of the concrete `GoogleMapsClient` class.
- **Added:** New request/response container objects that isolate all response object dependencies.
- **Added:** New extension package for dependency injection.
- **Updated:** Async implementations to reduce memory usage and improve performance.
- **Updated:** Now targets .NET Standard 2.0.
- **Deprecated:** All `Get*` methods. Will remove in 2.1.0, use the new ones instead.

