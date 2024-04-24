using BingWallpaperDownloader.Data;
using BingWallpaperDownloader.Models;
using Newtonsoft.Json;

namespace BingWallpaperDownloader.Logic {

    public static class WallpaperUtils {

        public static async Task DownloadWallpaperAsync() {
            await Logger.LogAsync("Requesting wallpaper");
            using var wc = new HttpClient();

            var response = await wc.GetStringAsync("https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US");

            if (string.IsNullOrEmpty(response)) {
                await Logger.LogAsync("Response not received from wallpaper request");
                return;
            }

            var wallpaperObject = JsonConvert.DeserializeObject<WallpaperResponse>(response);

            if (wallpaperObject == null) {
                await Logger.LogAsync($"Unable to parse a valid wallpaper response for request: {response}");
                return;
            }

            using var db = new BWDDbContext();
            await db.WallpaperResponses.AddAsync(wallpaperObject);
            await db.SaveChangesAsync();

            if (wallpaperObject.Images == null) {
                await Logger.LogWarningAsync("No images returned");
                return;
            }

            foreach (var image in wallpaperObject.Images) {
                if (image.Url == null) {
                    await Logger.LogErrorAsync($"There is no url to download for the image: {image}");
                    continue;
                }

                var url = new Uri(image.Url);

                var bytes = await wc.GetByteArrayAsync(url);

                //      "url": "/th?id=OHR.TrilliumOntario_EN-US5180679465_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp",
                var query = System.Web.HttpUtility.ParseQueryString(url.Query);
                var filename = query["id"] ?? $"{Path.GetRandomFileName()}.jpg";

                var destination = Path.Combine(BWDOptions.TargetFolder, filename);

                File.WriteAllBytes(destination, bytes);

                var Download = new DownloadedFile() {
                    Filename = destination,
                    WallpaperResponse = wallpaperObject
                };

                await db.DownloadedFiles.AddAsync(Download);
                await db.SaveChangesAsync();
            }
        }
    }
}