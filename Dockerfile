# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything from repo root into /src in container
COPY . .

# Restore & publish only the Presentation project
WORKDIR /src/WitcherProject.PresentationLayer
RUN dotnet restore WitcherProject.PresentationLayer.csproj
RUN dotnet publish WitcherProject.PresentationLayer.csproj -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WitcherProject.PresentationLayer.dll"]
