FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
WORKDIR "/src/WitcherProject.PresentationLayer"
RUN dotnet restore WitcherProject.Presentation/WitcherProject.Presentation.csproj
RUN dotnet publish WitcherProject.Presentation/WitcherProject.Presentation.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WitcherProject.Presentation.dll"]
