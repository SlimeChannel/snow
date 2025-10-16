namespace snow.UI
{
    using UnityEngine;
    using UnityEngine.Localization;
    using UnityEngine.Localization.Settings;
    using TMPro;
    using System;
    using System.Threading.Tasks;

    public class Selector : MonoBehaviour
    {
        [SerializeField] private string _type;
        [SerializeField] private int _valueIndex;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _localizationTable = "Settings";
        void Start()
        {
            // Preload current settings values
            _valueIndex = SettingsManager.SettingsData[_type].IndexOf(SettingsManager.SettingsConfig[_type]);
            UpdateDisplay();
            if (_type == "language") LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }
        void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }
        private void OnLocaleChanged(Locale locale)
        {
            UpdateDisplay();
        }
        public void SwitchLeft()
        {
            _valueIndex = Math.Max(0, _valueIndex - 1);
            Switch();
        }
        public void SwitchRight()
        {
            _valueIndex = Math.Min(SettingsManager.SettingsData[_type].Count - 1, _valueIndex + 1);
            Switch();
        }
        private void Switch()
        {
            UpdateDisplay();
            SettingsManager.ApplySettings(_type, _valueIndex);
        }
        private async void UpdateDisplay()
        {
            // Stop update if the data is invalid
            if (!SettingsManager.SettingsData.ContainsKey(_type) || _valueIndex >= SettingsManager.SettingsData[_type].Count)
                return;
            string value = SettingsManager.SettingsData[_type][_valueIndex];
            // Custom settings output depending on the setting type
            switch (_type)
            {
                case "resolution":
                case "master_volume":
                    _text.text = value;
                    break;
                case "language":
                    _text.text = GetLocalizedLanguageName(value);
                    break;
                case "fullscreen":
                    await GetLocalizedSettingText(value);
                    break;
                case "refresh_rate":
                    _text.text = value + " FPS";
                    break;
            }
        }
        // Fuction for getting translated version of the current setting
        private async Task GetLocalizedSettingText(string value)
        {
            try
            {
                string localizationKey = $"{_type}_{value}";
                var operation = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_localizationTable, localizationKey);
                await operation.Task;
                _text.text = operation.Result;
            }
            catch
            {
                _text.text = value;
            }
        }
        // Separate function specifically for language setting
        private string GetLocalizedLanguageName(string languageCode)
        {
            return languageCode switch
            {
                "en" => "English",
                "eo" => "Esperanto",
                "ru" => "Русский",
                _ => languageCode
            };
        }
    }
}