using BingWallpaperDownloader.Data;
using Newtonsoft.Json;

namespace BingWallpaperDownloader.Logic {

    public static class WallpaperUtils {

        public static async Task RequestWallpaper() {
            using var wc = new HttpClient();

            var response = await wc.GetStringAsync("https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US");

            if (string.IsNullOrEmpty(response)) {
                Logger.Log("Response not received from wallpaper request");
                return;
            }

            var wallpaperObject = JsonConvert.DeserializeObject<WallpaperResponse>(response);

            if (wallpaperObject == null) {
                Logger.Log($"Unable to parse a valid wallpaper response for request: {response}");
                return;
            }

            using var db = new BingDbContext();
            await db.WallpaperResponses.AddAsync(wallpaperObject);
            await db.SaveChangesAsync();

            if (wallpaperObject.Images == null) {
                Logger.LogWarning("No images returned");
                return;
            }

            foreach (var image in wallpaperObject.Images) {
                if (image.Url == null) {
                    Logger.LogError($"There is no url to download for the image: {image}");
                    continue;
                }

                var url = new Uri(image.Url);

                var bytes = await wc.GetByteArrayAsync(url);

                var filename = "";
                var destination = Path.Combine(BingWallpaperOptions.TargetFolder, filename);

                File.WriteAllBytes(destination, bytes);
            }
        }
    }
}