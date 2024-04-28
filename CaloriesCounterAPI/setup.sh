#!/bin/bash

# Run Entity Framework migrations and update the database
dotnet-ef database update

# Start the ASP.NET application
exec dotnet run --project CaloriesCounterAPI.csproj