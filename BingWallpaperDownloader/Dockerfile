FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /app

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "BingWallpaperDownloader.dll" ]