# BingWallpaperDownloader

This is a little application intending to download the daily Bing wallpaper locally.

The source URL is:
- https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US

It will save each file with the original 
filename, and as `latest.jpg`. This means if you have something that 
wants to access the latest wallpaper image, it only needs to reference
`latest.jpg`. 

## docker compose

You can use a fairly simple `docker-compose.yml` file to download the 
wallpaper to a given folder.

```yaml
---
services:
  bingwallpaperdownloader:
    image: redbearddotdev/bingwallpaperdownloader
    volumes:
      - /opt/bingwallpaper:/app/target
    environment:
      - CHECK_FREQUENCY_HOURS=6
```

## Environment Variables

The following variables can be used to control the application:

- LOG_TO_CONSOLE 
  - Whether or not logging is sent to the console
  - Default: true
- TARGET_FOLDER
  - The locaiton that the wallpaper is downloaded to
  - Default: Current working directory
- CHECK_FREQUENCY_HOURS
  - The number of hours between each check
  - Default: 24
- LOG_CODE_VALUES
  - Whether or not to add code method, line, position, etc. to Log messages
  - Default: false

## Known Issues

~~Currently, as mentioned [this](https://devblogs.microsoft.com/dotnet/securing-containers-with-rootless/), 
the target folder needs to be owned (or write-accessible) by UID 1654.~~

The Dockerfile and docker-compose.yml have been updated to use uid:gid 1000:1000. At this stage the folder needs to pre-exist with that uid:gid.

