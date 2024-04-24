// See https://aka.ms/new-console-template for more information

using BingWallpaperDownloader.Data;
using BingWallpaperDownloader.Logic;

using var db = new BingDbContext();
await db.Database.EnsureCreatedAsync();

// initialise settings based on environment variables.
BingWallpaperOptions.InitializeOptions();
