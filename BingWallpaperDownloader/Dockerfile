FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
# was USER app, i.e. uid 1654
USER 1000
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BingWallpaperDownloader/BingWallpaperDownloader.csproj", "BingWallpaperDownloader/"]
RUN dotnet restore "./BingWallpaperDownloader/BingWallpaperDownloader.csproj"
COPY . .
WORKDIR "/src/BingWallpaperDownloader"
RUN dotnet build "./BingWallpaperDownloader.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BingWallpaperDownloader.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

VOLUME /app/target

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BingWallpaperDownloader.dll"]