# DynaUpdater

DynaUpdater is a native cross-platform updater for dynamic content used in mobile applications and desktop programs. It provides an API for managing update packages, installing and caching those packages, and (in the future) rolling back installed packages. It is intended to be used for games that have frequently updated assets, and also require a cross-platform and centralized update management system.

The core updater logic is implemented in a Portable Class Library (PCL), supporting .NET, Android, Windows Phone, Windows, and iOS (Profile 111). Platform specific implementations are also required. Currently, platform libraries for .NET 4.5 and Xamarin.Android have been developed and tested.

There are two three demo programs available: a command line desktop program (Desktop.IntegrationTestRunner), an Android app (Android.IntegrationTestRunner), and a WPF program (DemoApp.Desktop).

This prototype was developed at UofT Hacks (www.uofthacks.com) over the course of a weekend. Time constraints (and sleep deprivation!) severely limited the amount of documentation that was created; more will likely come as the library develops.

To whomever may find this useful: enjoy!
