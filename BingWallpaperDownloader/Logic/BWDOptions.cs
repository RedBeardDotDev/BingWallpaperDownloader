namespace BingWallpaperDownloader.Logic {

    public static class BWDOptions {
        public static bool LogToConsole { get; set; } = true;

        public static string TargetFolder { get; set; } = $"{Environment.CurrentDirectory}/target";

        public static int CheckFrequencyHours { get; set; } = 24;
        public static TimeSpan CheckFrequency => TimeSpan.FromHours(CheckFrequencyHours);

        public static bool StopRunning { get; set; } = false;

        public static bool LogCodeValues { get; set; } = false;

        public static string WallpaperSourceUrl => "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";

        public static void InitializeOptions() {
            var str_log_to_console = Environment.GetEnvironmentVariable("LOG_TO_CONSOLE");
            if (!string.IsNullOrEmpty(str_log_to_console)) {
                if (bool.TryParse(str_log_to_console, out var log_to_console)) {
                    LogToConsole = log_to_console;
                } else {
                    Logger.LogError($"Invalid option for LOG_TO_CONSOLE: {log_to_console}");
                }
            } else {
                Logger.Log($"Environment variable LOG_TO_CONSOLE not set. Using the value: {LogToConsole}");
            }

            // set target folder
            var target_folder = Environment.GetEnvironmentVariable("TARGET_FOLDER");
                        if (!string.IsNullOrEmpty(target_folder)) {
                var dir = new DirectoryInfo(target_folder);

                if (dir.Exists) {
                    TargetFolder = dir.FullName;
                } else {
                    Logger.LogError($"Invalid optin for TARGET_FOLDER: {target_folder}");
                }
            } else {
                Logger.Log($"Environment variable TARGET_FOLDER not set. Using the value: {TargetFolder}");
            }

            var str_check_frequency_hours = Environment.GetEnvironmentVariable("CHECK_FREQUENCY_HOURS");
            if (!string.IsNullOrEmpty(str_check_frequency_hours)) {
                if (int.TryParse(str_check_frequency_hours, out var check_frequency_hours)) {
                    if (check_frequency_hours < 1 || check_frequency_hours > (30 * 24)) {
                        Logger.LogWarning($"CHECK_FREQUENCY HOURS should be in the range of 1..{30 * 24}");
                    } else {
                        CheckFrequencyHours = check_frequency_hours;
                    }
                }
            } else {
                Logger.Log($"Environment variable CHECK_FREQUENCY_HOURS not set. Using the value: {CheckFrequencyHours} hours");
            }

            var str_log_code_values = Environment.GetEnvironmentVariable("LOG_CODE_VALUES");
            if (!string.IsNullOrEmpty(str_log_code_values)) {
                if (bool.TryParse(str_log_code_values, out var log_code_values)) {
                    LogCodeValues = log_code_values;
                } else {
                    Logger.LogError($"Unable to get a valid value for LOG_CODE_VALUES: {log_code_values}");
                }
            } else {
                Logger.Log($"Environment variable LOG_CODE_VALUES not set. Using the value: false");
            }
        }
    }
}