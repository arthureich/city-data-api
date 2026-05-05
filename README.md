# City Data API

## Description
ASP.NET Core academic/API study project with a REST API and a separate MVC web client for city data. The API exposes nested country/state/city routes, supports filtering by name and population range, and reads from a JSON-backed repository.

## Tech Stack
- C#
- ASP.NET Core
- MVC / Razor views
- JSON data storage
- Bootstrap and jQuery assets in the web client
- CSS and JavaScript

## Structure
- `myAPI/` contains the REST API, models, repository, JSON data file, and `CidadesController`.
- `WebClient/WebClient/` contains the MVC client, controllers, views, and static assets.
- `API.sln` and `WebClient/WebClient.sln` are the Visual Studio solutions.

## How to Run
From this folder, restore dependencies and run the desired project:

```bash
dotnet restore
dotnet run --project myAPI/myAPI.csproj
```

The API route inspected is `paises/{IdPais}/estados/{IdEstado}/cidades`.
