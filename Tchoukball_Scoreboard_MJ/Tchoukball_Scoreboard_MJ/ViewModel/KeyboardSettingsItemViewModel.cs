using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.CustomEnum;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class KeyboardSettingsItemViewModel : ValidationViewModelBase
    {
        private readonly KeyboardSettings _model;
        private readonly IConfigurationSection _settings;

        public KeyboardSettingsItemViewModel()
        {
            _settings = new ConfigurationBuilder().AddJsonFile("D:\\Git Repository\\Tchoukball_Scoreboard_MJ\\Tchoukball_Scoreboard_MJ\\appsettings.json").Build().GetSection("KeyboardSettings");
            _model = new KeyboardSettings
            {
                TimerStartStopKey = ConvertSettingToKey("TimerStartStop"),
                HomeAddPointKey = ConvertSettingToKey("HomeAddPoint"),
                HomeMinusPointKey = ConvertSettingToKey("HomeMinusPoint"),
                AwayAddPointKey = ConvertSettingToKey("AwayAddPoint"),
                AwayMinusPointKey = ConvertSettingToKey("AwayMinusPoint"),
                AddPeriodKey = ConvertSettingToKey("AddPeriod"),
                MinusPeriodKey = ConvertSettingToKey("MinusPeriod"),
                SwitchPossesionKey = ConvertSettingToKey("ChangePossession"),
            };

            _model.TimerStartStop = GetStrFromKey(TimerStartStopKey);
            _model.HomeAddPoint = GetStrFromKey(HomeAddPointKey);
            _model.HomeMinusPoint = GetStrFromKey(HomeMinusPointKey);
            _model.AwayAddPoint = GetStrFromKey(AwayAddPointKey);
            _model.AwayMinusPoint = GetStrFromKey(AwayMinusPointKey);
            _model.AddPeriod = GetStrFromKey(AddPeriodKey);
            _model.MinusPeriod = GetStrFromKey(MinusPeriodKey);
            _model.SwitchPossesion = GetStrFromKey(SwitchPossesionKey);
        }

        public bool CanUpdateKey(Key key)
        {
            if (TimerStartStopKey == key)
                return false;
            else if(HomeAddPointKey == key)
                return false;
            else if(HomeMinusPointKey == key)
                return false;
            else if(AwayAddPointKey == key)
                return false;
            else if(AwayMinusPointKey == key)
                return false;
            else if(AddPeriodKey == key)
                return false;
            else if(MinusPeriodKey == key)
                return false;
            else if(SwitchPossesionKey == key)
                return false;
            else
                return true;
        }

        private Key ConvertSettingToKey(string settingName)
        {
            var b = _settings[settingName];
            int d;

            if (!int.TryParse(b, out d))
            {
                KeyboardEnum a = (KeyboardEnum)Enum.Parse(typeof(KeyboardEnum), settingName);
                switch (a)
                {
                    case KeyboardEnum.TimerStartStop:
                        return DefaultSettings.TimerStartStopKey;
                    case KeyboardEnum.HomeAddPoint:
                        return DefaultSettings.HomeAddPointKey;
                    case KeyboardEnum.HomeMinusPoint:
                        return DefaultSettings.HomeMinusPointKey;
                    case KeyboardEnum.AwayAddPoint:
                        return DefaultSettings.AwayAddPointKey;
                    case KeyboardEnum.AwayMinusPoint:
                        return DefaultSettings.AwayMinusPointKey;
                    case KeyboardEnum.AddPeriod:
                        return DefaultSettings.PeriodAddKey;
                    case KeyboardEnum.MinusPeriod:
                        return DefaultSettings.PeriodMinusKey;
                    case KeyboardEnum.ChangePossession:
                        return DefaultSettings.SwitchPossesionKey;
                    default:
                        return Key.None;
                }
            }
            else
            {
                var c = (Key)d;
                return c;
            }
        }

        public Key TimerStartStopKey
        {
            get => _model.TimerStartStopKey;
            set
            {
                _model.TimerStartStopKey = value;
                TimerStartStop = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key HomeAddPointKey
        {
            get => _model.HomeAddPointKey;
            set
            {
                _model.HomeAddPointKey = value;
                HomeAddPoint = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key HomeMinusPointKey
        {
            get => _model.HomeMinusPointKey;
            set
            {
                _model.HomeMinusPointKey = value;
                HomeMinusPoint = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key AwayAddPointKey
        {
            get => _model.AwayAddPointKey;
            set
            {
                _model.AwayAddPointKey = value;
                AwayAddPoint = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key AwayMinusPointKey
        {
            get => _model.AwayMinusPointKey;
            set
            {
                _model.AwayMinusPointKey = value;
                AwayMinusPoint = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key AddPeriodKey
        {
            get => _model.AddPeriodKey;
            set
            {
                _model.AddPeriodKey = value;
                AddPeriod = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key MinusPeriodKey
        {
            get => _model.MinusPeriodKey;
            set
            {
                _model.MinusPeriodKey = value;
                MinusPeriod = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public Key SwitchPossesionKey
        {
            get => _model.SwitchPossesionKey;
            set
            {
                _model.SwitchPossesionKey = value;
                SwitchPossesion = GetStrFromKey(value);
                RaisePropertyChanged();
            }
        }

        public String TimerStartStop
        {
            get
            {
                return _model.TimerStartStop;
            }
            set
            {
                _model.TimerStartStop = value;
                RaisePropertyChanged();
            }
        }

        public String HomeAddPoint
        {
            get
            {
                return _model.HomeAddPoint;
            }
            set
            {
                _model.HomeAddPoint = value;
                RaisePropertyChanged();
            }
        }

        public String HomeMinusPoint
        {
            get
            {
                return _model.HomeMinusPoint;
            }
            set
            {
                _model.HomeMinusPoint = value;
                RaisePropertyChanged();
            }
        }

        public String AwayAddPoint
        {
            get
            {
                return _model.AwayAddPoint;
            }
            set
            {
                _model.AwayAddPoint = value;
                RaisePropertyChanged();
            }
        }

        public String AwayMinusPoint
        {
            get
            {
                return _model.AwayMinusPoint;
            }
            set
            {
                _model.AwayMinusPoint = value;
                RaisePropertyChanged();
            }
        }

        public String AddPeriod
        {
            get
            {
                return _model.AddPeriod;
            }
            set
            {
                _model.AddPeriod = value;
                RaisePropertyChanged();
            }
        }

        public String MinusPeriod
        {
            get
            {
                return _model.MinusPeriod;
            }
            set
            {
                _model.MinusPeriod = value;
                RaisePropertyChanged();
            }
        }

        public String SwitchPossesion
        {
            get
            {
                return _model.SwitchPossesion;
            }
            set
            {
                _model.SwitchPossesion = value;
                RaisePropertyChanged();
            }
        }

        private string GetStrFromKey(Key key)
        {
            if (key >= Key.D0 && key <= Key.D9)
            {
                return (key.ToString().Replace("D", ""));
            }
            else if (key >= Key.NumPad0 && key <= Key.NumPad9)
            {
                return (key.ToString().Replace("NumPad", ""));
            }
            else
            {
                return (key.ToString().Replace("Oem", ""));
            }
        }
    }
}
