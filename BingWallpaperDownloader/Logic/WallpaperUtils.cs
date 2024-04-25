﻿using Newtonsoft.Json;

namespace BingWallpaperDownloader.Logic {

    public static class WallpaperUtils {

        public static async Task DownloadWallpaperAsync() {
            Logger.Log("Requesting wallpaper");
            using var client = new HttpClient();

            var source = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";
            var response = await client.GetStringAsync(source);

            if (string.IsNullOrEmpty(response)) {
                Logger.Log("Response not received from wallpaper request");
                return;
            }

            var wallpaperObject = JsonConvert.DeserializeObject<WallpaperResponse>(response);

            if (wallpaperObject == null) {
                Logger.Log($"Unable to parse a valid wallpaper response for request: {response}");
                return;
            }

            if (wallpaperObject.Images == null) {
                Logger.LogWarning("No images returned");
                return;
            }

            foreach (var image in wallpaperObject.Images) {
                if (image.Url == null) {
                    Logger.LogError($"There is no url to download for the image: {image}");
                    continue;
                }

                var url = new Uri($"https://bing.com/{image.Url.TrimStart('/')}");
                var query = System.Web.HttpUtility.ParseQueryString(url.Query);
                var filename = query["id"] ?? $"{Path.GetRandomFileName()}.jpg";

                var destination = Path.Combine(BWDOptions.TargetFolder, filename);

                if (File.Exists(destination)) {
                    Logger.Log($"Destionation file already exists. Not downloading again: {destination}");
                    return;
                }

                if (!Directory.Exists(BWDOptions.TargetFolder)) {
                    Logger.Log($"Directory doesn't exist, so creating it: {BWDOptions.TargetFolder}");
                    Directory.CreateDirectory(BWDOptions.TargetFolder);
                }

                Logger.Log($"Downloading wallpaper from: {url}");

                var bytes = await client.GetByteArrayAsync(url);

                // Try to get the filename from the URL/
                //      "url": "/th?id=OHR.TrilliumOntario_EN-US5180679465_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp",

                Logger.Log($"Saving wallpaper as {destination}");

                File.WriteAllBytes(destination, bytes);

                Logger.Log("File download complete.");
            }
        }
    }
}