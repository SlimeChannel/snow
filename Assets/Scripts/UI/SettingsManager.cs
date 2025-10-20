namespace snow.UI
{
    using UnityEngine;
    using UnityEngine.Localization;
    using UnityEngine.Localization.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine.Audio;

    public static class SettingsManager
    {
        // All possible states of all standart settings (a.k.a. the ones changed via Selector.cs)
        public static Dictionary<string, List<string>> SettingsData = new()
        {
            { "language", new() { "en", "eo", "ru" } },
            { "fullscreen", new() { "Windowed", "ExclusiveFullScreen", "FullScreenWindow" } },
            { "resolution", new() { "1280x720", "1366x768", "1920x1080"  } },
            { "refresh_rate", new() { "30", "60", "120", "144", "160", "165", "170", "175", "180", "240", "360", "9999" } },
            { "master_volume", new() { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" } }
        };

        // Current states of all standart settings 
        public static Dictionary<string, string> SettingsConfig = new()
        {
            { "language", "en" },
            { "fullscreen", "FullScreenWindow" },
            { "resolution", "1920x1080" },
            { "refresh_rate", "60" },
            { "master_volume", "50%" }
        };

        // All possible system languages (used for auto language detection)
        public static Dictionary<SystemLanguage, string> LanguageCodes = new()
        {
            { SystemLanguage.English, "en" },
            { SystemLanguage.Russian, "ru" }
        };
        private static Dictionary<string, Locale> _localeCache = new();
        private static List<string> SettingsKeys = new();
        private static AudioMixer _audioMixer;

        public static void Initialize()
        {
            _audioMixer = Resources.Load<AudioMixer>("Audio/MainAudioMixer");
            SettingsKeys = SettingsConfig.Keys.ToList();
            PrecacheLocales();
            // Automatically change settings to ones most appropriate to the user system
            foreach (string key in SettingsKeys)
            {
                if (PlayerPrefs.HasKey(key))
                {
                    SettingsConfig[key] = PlayerPrefs.GetString(key);
                    ApplySettings(key, SettingsData[key].IndexOf(SettingsConfig[key]));
                }
                else
                {
                    switch (key)
                    {
                        case "language":
                            if (LanguageCodes.ContainsKey(Application.systemLanguage))
                                SettingsConfig["language"] = LanguageCodes[Application.systemLanguage];
                            else
                                SettingsConfig["language"] = "en";
                            if (_localeCache.TryGetValue(SettingsConfig["language"], out Locale targetLocale))
                                LocalizationSettings.SelectedLocale = targetLocale;
                            else
                                SettingsConfig["language"] = "en";
                            break;
                        case "resolution":
                            Resolution res = Screen.currentResolution;
                            SettingsConfig["resolution"] = res.width + "x" + res.height;
                            string[] resolution = SettingsConfig["resolution"].Split("x");
                            Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), Enum.Parse<FullScreenMode>(SettingsConfig["fullscreen"]));
                            break;
                        case "refresh_rate":
                            res = Screen.currentResolution;
                            double refresh_rate = res.refreshRateRatio.value;
                            if (SettingsData["refresh_rate"].Contains(refresh_rate.ToString()))
                                SettingsConfig["refresh_rate"] = refresh_rate.ToString();
                            else
                            {
                                // Finding the closest value to the user's system framerate
                                List<double> diff = new();
                                for (int i = 0; i < SettingsData["refresh_rate"].Count; i++)
                                    diff[i] = Math.Abs(refresh_rate - Convert.ToDouble(SettingsData["refresh_rate"][i]));
                                SettingsConfig["refresh_rate"] = SettingsData["refresh_rate"][diff.IndexOf(diff.Min())];
                            }
                            Application.targetFrameRate = int.Parse(SettingsConfig["refresh_rate"]);
                            break;
                    }
                    PlayerPrefs.SetString(key, SettingsConfig[key]);
                }
                MainMenuManager.Singleton.SelectorUpdate(key, SettingsData[key].IndexOf(SettingsConfig[key]));
            }
            PlayerPrefs.Save();
        }

        // Automatically create [(string) locale code|(Locale) locale] dictionary for all existing locales
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
            if (!SettingsData.ContainsKey(setting)) return;
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
                case "refresh_rate":
                    Application.targetFrameRate = int.Parse(value);
                    break;
                case "master_volume":
                    _audioMixer.SetFloat("MasterVolume", int.Parse(value.Split("%")[0]) - 80);
                    break;
            }
        }

        public static void SaveSettings()
        {
            foreach (string key in SettingsKeys)
            {
                PlayerPrefs.SetString(key, SettingsConfig[key]);
            }
            PlayerPrefs.Save();
        }
        
        public static void DiscardSettings()
        {
            foreach (string key in SettingsKeys)
            {
                if (!PlayerPrefs.HasKey(key)) return;
                SettingsConfig[key] = PlayerPrefs.GetString(key);
                ApplySettings(key, SettingsData[key].IndexOf(SettingsConfig[key]));
                MainMenuManager.Singleton.SelectorUpdate(key, SettingsData[key].IndexOf(SettingsConfig[key]));
            }
        }
    }
}