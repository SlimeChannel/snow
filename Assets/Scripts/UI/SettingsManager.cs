namespace snow.UI
{
    using UnityEngine;
    using UnityEngine.Localization;
    using UnityEngine.Localization.Settings;
    using System;
    using System.Collections.Generic;
    public static class SettingsManager
    {
        public static Dictionary<string, List<string>> SettingsData = new()
        {
            { "language", new() { "en", "eo", "ru" } },
            { "fullscreen", new() { "Windowed", "ExclusiveFullScreen", "FullScreenWindow" } },
            { "resolution", new() { "1920x1080", "1366x768", "1280x720" } },
            { "master_volume", new() { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" } }
        };
        public static Dictionary<string, string> SettingsConfig = new()
        {
            { "language", "en" },
            { "fullscreen", "Windowed" },
            { "resolution", "1280x720" },
            { "master_volume", "50%" }
        };
        private static Dictionary<string, Locale> _localeCache = new();
        static SettingsManager()
        {
            PrecacheLocales();
        }
        private static void PrecacheLocales()
        {
            _localeCache.Clear();
            var availableLocales = LocalizationSettings.AvailableLocales;
            
            if (availableLocales != null)
            {
                foreach (Locale locale in availableLocales.Locales)
                {
                    _localeCache[locale.Identifier.Code] = locale;
                }
            }
        }
        public static void ApplySettings(string setting, int index)
        {
            if (SettingsData.ContainsKey(setting))
            {
                string value = SettingsData[setting][index];
                SettingsConfig[setting] = value;
                switch (setting)
                {
                    case "language":
                        if (_localeCache.TryGetValue(value, out Locale targetLocale))
                            LocalizationSettings.SelectedLocale = targetLocale;
                        break;
                    case "fullscreen":
                        Screen.SetResolution(Screen.width, Screen.height, Enum.Parse<FullScreenMode>(value));
                        break;
                    case "resolution":
                        string[] resolution = value.Split("x");
                        Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), Screen.fullScreenMode);
                        break;
                    case "master_volume":
                        break;
                }
            }
        }
    }
}