using System;
using System.IO;
using System.Runtime.Caching;
using Newtonsoft.Json;

namespace Parser
{
    internal class AppSettingsLoader
    {
        private const string DEFAULT_FILENAME = "settings.json";

        public static Settings Load(string filename = DEFAULT_FILENAME)
        {
            return File.Exists(DEFAULT_FILENAME)
                ? JsonConvert.DeserializeObject<Settings>(File.ReadAllText(DEFAULT_FILENAME))
                : null;
        }
    }

    public class Settings
    {
        public string MailTo { get; set; }

        public string MailFrom { get; set; }

        public string MailFromPass { get; set; }

        public int GmailPort { get; set; }

        public string CarSavePath { get; set; }
    }

    public class Config
    {
        private static MemoryCache cache = MemoryCache.Default;

        private Config() { }

        public static Settings Get()
        {
            if (cache.Contains("settings"))
                return cache.Get("settings") as Settings;

            var settings = AppSettingsLoader.Load();

            cache.Add("settings", settings, DateTimeOffset.Now.Add(TimeSpan.FromMinutes(2)));

            return settings;
        }

    }
}