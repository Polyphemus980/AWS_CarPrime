﻿# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY CarPrime.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port (default for ASP.NET Core)
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "CarPrime.dll"]