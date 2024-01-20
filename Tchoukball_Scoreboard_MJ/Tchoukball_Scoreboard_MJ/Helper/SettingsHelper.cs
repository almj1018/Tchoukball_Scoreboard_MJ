using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.CustomEnum;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.Helper
{
    public class SettingsHelper
    {
        private readonly IConfigurationSection _keyboardSectionSettings;
        private readonly IConfigurationSection _otherSectionSettings;
        private readonly string _appSettingsFilePath = AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";
        private const string _keyboardSettingsSection = "KeyboardSettings";
        private const string _otherSettingsSection = "OtherSettings";
        public KeyboardSettings _keyboardSettings { get; set; }
        public OtherSettings _otherSettings { get; set; }

        public SettingsHelper()
        {
            _keyboardSectionSettings = new ConfigurationBuilder().AddJsonFile(_appSettingsFilePath).Build().GetSection(_keyboardSettingsSection);
            _otherSectionSettings = new ConfigurationBuilder().AddJsonFile(_appSettingsFilePath).Build().GetSection(_otherSettingsSection);
            LoadKeyboardSettingsModel();
            LoadOtherSettingsModel();
        }

        private void LoadKeyboardSettingsModel()
        {
            _keyboardSettings = new KeyboardSettings
            {
                TimerStartStopKey = GetKeyValue("TimerStartStopKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.TimerStartStopKey,
                HomeAddPointKey = GetKeyValue("HomeAddPointKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.HomeAddPointKey,
                HomeMinusPointKey = GetKeyValue("HomeMinusPointKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.HomeMinusPointKey,
                AwayAddPointKey = GetKeyValue("AwayAddPointKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.AwayAddPointKey,
                AwayMinusPointKey = GetKeyValue("AwayMinusPointKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.AwayMinusPointKey,
                AddPeriodKey = GetKeyValue("AddPeriodKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.PeriodAddKey,
                MinusPeriodKey = GetKeyValue("MinusPeriodKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.PeriodMinusKey,
                SwitchPossesionKey = GetKeyValue("SwitchPossesionKey", SettingsEnum.KeyboardSettings) ?? DefaultSettings.SwitchPossesionKey,
            };
        }

        private void LoadOtherSettingsModel()
        {
            _otherSettings = new OtherSettings
            {
                EnableBreakTimerScreen = GetBooleanValue("EnableBreakTimerScreen", SettingsEnum.OtherSettings) ?? DefaultSettings.EnableBreakTimerScreen,
                DisableTimerResetButtonWhileTimerIsStarted = GetBooleanValue("DisableTimerResetButtonWhileTimerIsStarted", SettingsEnum.OtherSettings) ?? DefaultSettings.DisableTimerResetButtonWhileTimerIsStarted,
                AutoIncrementPeriod = GetBooleanValue("AutoIncrementPeriod", SettingsEnum.OtherSettings) ?? DefaultSettings.AutoIncrementPeriod,

                DefaultHomeName = GetStringValue("DefaultHomeName", SettingsEnum.OtherSettings) ?? DefaultSettings.DefaultHomeName,
                DefaultAwayName = GetStringValue("DefaultAwayName", SettingsEnum.OtherSettings) ?? DefaultSettings.DefaultAwayName,
                BreakTime = GetTimeSpanValue("BreakTime", SettingsEnum.OtherSettings) ?? DefaultSettings.PeriodTime,
                PeriodTime = GetTimeSpanValue("PeriodTime", SettingsEnum.OtherSettings) ?? DefaultSettings.BreakTime,
            };
        }

        public bool? GetBooleanValue(string settingKey, SettingsEnum settingSection)
        {
            var a = settingSection == SettingsEnum.KeyboardSettings ? _keyboardSectionSettings[settingKey] : _otherSectionSettings[settingKey];

            if (string.IsNullOrEmpty(a))
            {
                return null;
            }
            else
            {
                if (bool.TryParse(a, out var result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public string? GetStringValue(string settingKey, SettingsEnum settingSection)
        {
            var a = settingSection == SettingsEnum.KeyboardSettings ? _keyboardSectionSettings[settingKey] : _otherSectionSettings[settingKey];

            if (string.IsNullOrEmpty(a))
            {
                return null;
            }
            else
            {
                return a;
            }
        }

        public TimeSpan? GetTimeSpanValue(string settingKey, SettingsEnum settingSection)
        {
            var a = settingSection == SettingsEnum.KeyboardSettings ? _keyboardSectionSettings[settingKey] : _otherSectionSettings[settingKey];

            if (string.IsNullOrEmpty(a))
            {
                return null;
            }
            else
            {
                if (TimeSpan.TryParseExact(a, "mm:ss", CultureInfo.InvariantCulture, out var result))
                {
                    return result;
                }
                else { return null; }
            }
        }

        public Key? GetKeyValue(string settingKey, SettingsEnum settingSection)
        {
            var a = settingSection == SettingsEnum.KeyboardSettings ? _keyboardSectionSettings[settingKey] : _otherSectionSettings[settingKey];

            if (string.IsNullOrEmpty(a))
            {
                return null;
            }
            else
            {
                if (int.TryParse(a, out var result))
                {
                    return (Key)result;
                }
                else { return null; }
            }
        }

        public bool SaveToFile(KeyboardSettings? keyboardSettings = null, OtherSettings? otherSettings = null)
        {
            if (keyboardSettings != null) { _keyboardSettings = keyboardSettings; }
            if (otherSettings != null) { _otherSettings = otherSettings; }

            var config = new Configuration
            {
                KeyboardSettings = _keyboardSettings,
                OtherSettings = _otherSettings
            };

            var b = JsonConvert.SerializeObject(config, Formatting.Indented, new JsonConverter[] { new JsonHelper(), new JsonTimeSpanConverter() });
            try
            {
                File.WriteAllText(_appSettingsFilePath, b);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
