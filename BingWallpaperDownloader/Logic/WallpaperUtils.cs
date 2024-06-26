﻿using Newtonsoft.Json;

namespace BingWallpaperDownloader.Logic {

    public static class WallpaperUtils {

        public static async Task DownloadWallpaperAsync() {
            Logger.Log("Requesting wallpaper");
            using var client = new HttpClient();

            var source = BWDOptions.WallpaperSourceUrl;
            var response = await client.GetStringAsync(source);

            // If you don't get a response from the request
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

            // Download each wallpaper image. There should usually only be one, butit is in a Json array.
            foreach (var image in wallpaperObject.Images) {
                if (image.Url == null) {
                    Logger.LogError($"There is no url to download for the image: {image}");
                    continue;
                }

                // using .TrimStart('/') as it makes the string interpolation nicer
                var url = new Uri($"https://bing.com/{image.Url.TrimStart('/')}");
                var query = System.Web.HttpUtility.ParseQueryString(url.Query);
                var filename = query["id"] ?? $"{Path.GetRandomFileName()}.jpg";

                var destination = Path.Combine(BWDOptions.TargetFolder, filename);

                // Don't need to overwrite
                if (File.Exists(destination)) {
                    Logger.Log($"Destionation file already exists. Not downloading again: {destination}");
                    return;
                }

                var dir = new DirectoryInfo(BWDOptions.TargetFolder);

                if (dir.Exists) {
                    Logger.Log($"Destination directory already exists. We don't need to create it: {BWDOptions.TargetFolder}");
                } else {
                    Logger.Log($"Directory doesn't exist, so creating it: {BWDOptions.TargetFolder}");
                    dir.Create();
                }

                Logger.Log($"Downloading wallpaper from: {url}");

                var bytes = await client.GetByteArrayAsync(url);

                // Try to get the filename from the URL/
                //      "url": "/th?id=OHR.TrilliumOntario_EN-US5180679465_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp",
                try {

                    Logger.Log($"Saving wallpaper as {destination}");

                    File.WriteAllBytes(destination, bytes);

                    Logger.Log($"Saving wallpaper as latest.jpg");
                    File.WriteAllBytes(Path.Combine(BWDOptions.TargetFolder, "latest.jpg"), bytes);
                    
                    Logger.Log("File download complete.");
                } catch (UnauthorizedAccessException) {
                    Logger.LogError($"There was a problem saving the file. Please check the target directory exists and you have access to it. On Linux under docker, the target directory might need to exist and be owned by UID 1654, prior to the container being created.");
                    Environment.Exit(1);
                }
            }
        }
    }
}