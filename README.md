# MeetingTimeFinder

This project was generated .NET Core cli version 3.1.101
The purpose of this project was to learn development .NET Core using
.NET Core cli and to solver a problem by using the .NET Core.

## Code scaffolding

The solution and the project was generated using the following command:
- dotnet new sln -o MeetingTmeFinder
- dotnet new console -o src\MeetingTimeFinderApp
- dotnet new console -o src\MeetingTimeFinder
- dotnet new xunit -o src\MeetingTimeFinder.Tests
- dotnet sln add src\MeetingTimeFinderApp
- dotnet sln add src\MeetingTimeFinder
- dotnet sln add src\MeetingTimeFinder.Tests
- dotnet "add" - "c:\repos\MeetingTimeFinder\src\MeetingTimeFinderApp\MeetingTimeFinderApp.csproj" "reference" "c:\repos\MeetingTimeFinder\src\MeetingTimeFinder\MeetingTimeFinder.csproj"
- dotnet "add" "c:\repos\MeetingTimeFinder\src\MeetingTimeFinder.Tests\MeetingTimeFinder.Tests.csproj" "reference" "c:\repos\MeetingTimeFinder\src\MeetingTimeFinder\MeetingTimeFinder.csproj"

## Run

Run `dotnet run --project src/MeetingTimeFinderApp`

## Build

Run `dotnet build` to build the solution.

## Running unit tests

Run `dotnet test` to execute the unit tests.
