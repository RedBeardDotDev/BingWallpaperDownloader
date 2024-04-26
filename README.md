# BingWallpaperDownloader

This is a little application intending to download the daily Bing wallpaper locally.

The source URL is:
- https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US

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

## Sample Response

This is the sample response, like you might receive:

```json
 {
    "images": [
        {
            "startdate": "20240424",
            "fullstartdate": "202404240700",
            "enddate": "20240425",
            "url": "/th?id=OHR.TrilliumOntario_EN-US5180679465_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp",
            "urlbase": "/th?id=OHR.TrilliumOntario_EN-US5180679465",
            "copyright": "White trilliums blooming in Ontario, Canada (© Jun Zhang/Getty Images)",
            "copyrightlink": "https://www.bing.com/search?q=White+trillium&form=hpcapt&filters=HpDate%3a%2220240424_0700%22",
            "title": "Hey, how's it growing today?",
            "quiz": "/search?q=Bing+homepage+quiz&filters=WQOskey:%22HPQuiz_20240424_TrilliumOntario%22&FORM=HPQUIZ",
            "wp": true,
            "hsh": "2d2c49df10a1c386f159beb5bf9d674f",
            "drk": 1,
            "top": 1,
            "bot": 1,
            "hs": []
        }
    ],
    "tooltips": {
        "loading": "Loading...",
        "previous": "Previous image",
        "next": "Next image",
        "walle": "This image is not available to download as wallpaper.",
        "walls": "Download this image. Use of this image is restricted to wallpaper only."
    }
}
```

## Known Issues

Currently due to [this](https://github.com/dotnet/dotnet-docker/discussions/4995#:~:text=The%20.NET%20Linux%20container%20images%20include%20a%20new%20non%2Droot%20user%20named%20app%20with%20the%20UID%201654.) issue, 
the target folder needs to be owned (or write-accessible) by UID 1654.