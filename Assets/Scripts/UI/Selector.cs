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
        public string _type;
        public int _valueIndex;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _localizationTable = "Settings";

        public void Start()
        {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        public void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        public void OnLocaleChanged(Locale locale)
        {
            UpdateDisplay();
        }

        public void ChangeValue(int index)
        {
            _valueIndex = index;
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

        private void UpdateDisplay()
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
                case "music_volume":
                case "sound_volume":
                    _text.text = value;
                    break;
                case "language":
                    _text.text = GetLocalizedLanguageName(value);
                    break;
                case "fullscreen":
                    GetLocalizedSettingText(value);
                    break;
                case "refresh_rate":
                    _text.text = value + " FPS";
                    break;
            }
        }

        // Fuction for getting translated version of the current setting
        private void GetLocalizedSettingText(string value)
        {
            try
            {
                string localizationKey = $"{_type}_{value}";
                var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(_localizationTable, localizationKey);
                _text.text = localizedString;
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