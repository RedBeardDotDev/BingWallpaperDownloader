using BingWallpaperDownloader.Data;
using BingWallpaperDownloader.Models;
using Microsoft.Extensions.Logging;

namespace BingWallpaperDownloader.Logic {

    public static class Logger {
        private static ILogger? _logger = null;

        public static void SetLogger(ILogger log) {
            _logger = log;
        }

        public static async Task LogMeAsync() {
          await  LogAsync(LogLevel.Debug);
        }

        public static async Task LogCriticalAsync(string message, params object[] args) {
            await LogAsync(LogLevel.Critical, message, args);
        }

        public static async Task LogInformationAsync(string message, params object[] args) {
            await LogAsync(LogLevel.Information, message, args);
        }

        public static async Task LogWarningAsync(string message, params object[] args) {
            await LogAsync(LogLevel.Warning, message, args);
        }

        public static async Task LogErrorAsync(string message, params object[] args) {
            await LogAsync(LogLevel.Error, message, args);
        }

        public static async Task LogDebugAsync(string message, params object[] args) {
            await LogAsync(LogLevel.Debug, message, args);
        }

        public static async Task LogAsync(string message) {
            await LogAsync(LogLevel.Information, message);
        }

        private static async Task LogAsync(LogLevel level, string? msg = null, params object[] args) {
            if (msg == null) {
                msg = string.Empty;
            }

            msg = string.Format(msg, args);

            var logMessage = new LogMessage() {
                Level = level.ToString(),
                Message = msg
            };

            if (BingWallpaperOptions.LogToConsole) {
                var currColor = Console.ForegroundColor;

                switch (level) {
                    case LogLevel.Error:
                    case LogLevel.Critical:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;

                    case LogLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    default:
                        break;
                }

                Console.WriteLine(logMessage.ToString());

                Console.ForegroundColor = currColor;
            }

            if (BingWallpaperOptions.LogToDb) {
                using var db = new BingDbContext();

                await db.LogMessages.AddAsync(logMessage);
                await db.SaveChangesAsync();
            }
        }

        public static async Task LogProgressAsync(string message, int linesFromBottom = 0) {
            var originalPos = Console.CursorTop;
            var newPos = Console.WindowHeight - (linesFromBottom + 1);

            Console.CursorTop = newPos;
            Console.CursorLeft = 0;

            Console.Write(new string(' ', Console.WindowWidth));

            Console.CursorTop = newPos;
            Console.CursorLeft = 0;

            Console.Write(message);

            Console.CursorLeft = 0;
            Console.CursorTop = originalPos;
        }
    }
}