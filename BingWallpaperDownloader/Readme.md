# BingWallpaperDownloader

This is a little application intending to download the daily Bing wallpaper locally.

The source URL is:
- https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US

## Environment Variables

The following variables can be used to control the application:

- LOG_TO_DB
  - Whether or not logging is added to the database
  - Default: true
- LOG_TO_CONSOLE 
  - Whether or not logging is sent to the console
  - Default: true
- TARGET_FOLDER
  - The locaiton that the wallpaper is downloaded to
  - Default: Current working directory
- CHECK_FREQUENCY_HOURS
  - The number of hours between each check
  - Default: 24

## Sample Response

This is the sample response, like you might receive:

```json
/*
 {
    "images": [
        {
            "startdate": "20240424",
            "fullstartdate": "202404240700",
            "enddate": "20240425",
            "url": "/th?id=OHR.TrilliumOntario_EN-US5180679465_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp",
            "urlbase": "/th?id=OHR.TrilliumOntario_EN-US5180679465",
            "copyright": "White trilliums blooming in Ontario, Canada (� Jun Zhang/Getty Images)",
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
 */
```