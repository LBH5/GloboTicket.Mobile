# GloboTicket Mobile

GloboTicket Mobile is a .NET MAUI administration app for managing events and categories. The repository also includes the ASP.NET Core API used by the app and an xUnit test project.

## Projects

- `GloboTicket.Admin.API` - ASP.NET Core API with Swagger/OpenAPI in development.
- `GloboTicket.Admin.Mobile` - .NET MAUI administration application.
- `GloboTicket.Admin.Mobile.Tests` - xUnit tests for the mobile application.

## Prerequisites

- .NET 10 SDK
- .NET MAUI workload for the platform you want to run
- An Android emulator, iOS simulator, Mac Catalyst, or Windows target as appropriate

Install the MAUI workload with:

```bash
dotnet workload install maui
```

## Build

Restore and build the solution from the repository root:

```bash
dotnet build GloboTicket.Mobile.slnx
```

## Run the API

Start the API in a separate terminal:

```bash
dotnet run --project GloboTicket.Admin.API/GloboTicket.Admin.API.csproj
```

In development, Swagger is available at `http://localhost:5191/swagger`.

## Run the mobile app

Build or run the MAUI project for a supported target. For example:

```bash
dotnet build GloboTicket.Admin.Mobile/GloboTicket.Admin.Mobile.csproj -f net10.0-android
dotnet build GloboTicket.Admin.Mobile/GloboTicket.Admin.Mobile.csproj -f net10.0
```

The app's API client is configured for port `5191`. Start the API before launching the app. Android and other platforms use the base URLs configured in `MauiProgram.cs`.

## Run tests

Run the test project with:

```bash
dotnet test GloboTicket.Admin.Mobile.Tests/GloboTicket.Admin.Mobile.Tests.csproj
```

## Repository layout

```text
GloboTicket.Admin.API/          ASP.NET Core API
GloboTicket.Admin.Mobile/       .NET MAUI application
GloboTicket.Admin.Mobile.Tests/ Automated tests
GloboTicket.Mobile.slnx         Solution file
```
