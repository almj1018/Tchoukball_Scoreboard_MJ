using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.CustomEnum;
using Tchoukball_Scoreboard_MJ.Helper;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class KeyboardSettingsItemViewModel : ValidationViewModelBase
    {
        private readonly KeyboardSettings _model;
        private readonly SettingsHelper _settings;
        public bool hasUnsavedChanges = false;

        private Key previousTimerStartStopKey;
        private Key previousHomeAddPointKey;
        private Key previousHomeMinusPointKey;
        private Key previousAwayAddPointKey;
        private Key previousAwayMinusPointKey;
        private Key previousAddPeriodKey;
        private Key previousMinusPeriodKey;
        private Key previousSwitchPossesionKey;

        public KeyboardSettingsItemViewModel(SettingsHelper settings)
        {
            _settings = settings;
            _model = settings._keyboardSettings;

            previousTimerStartStopKey = _model.TimerStartStopKey;
            previousHomeAddPointKey = _model.HomeAddPointKey;
            previousHomeMinusPointKey = _model.HomeMinusPointKey;
            previousAwayAddPointKey = _model.AwayAddPointKey;
            previousAwayMinusPointKey = _model.AwayMinusPointKey;
            previousAddPeriodKey = _model.AddPeriodKey;
            previousMinusPeriodKey = _model.MinusPeriodKey;
            previousSwitchPossesionKey = _model.SwitchPossesionKey;

            TimerStartStop = GetStrFromKey(TimerStartStopKey);
            HomeAddPoint = GetStrFromKey(HomeAddPointKey);
            HomeMinusPoint = GetStrFromKey(HomeMinusPointKey);
            AwayAddPoint = GetStrFromKey(AwayAddPointKey);
            AwayMinusPoint = GetStrFromKey(AwayMinusPointKey);
            AddPeriod = GetStrFromKey(AddPeriodKey);
            MinusPeriod = GetStrFromKey(MinusPeriodKey);
            SwitchPossesion = GetStrFromKey(SwitchPossesionKey);
        }

        private bool CanUpdateKey(Key key)
        {
            if (TimerStartStopKey == key)
                return false;
            else if (HomeAddPointKey == key)
                return false;
            else if (HomeMinusPointKey == key)
                return false;
            else if (AwayAddPointKey == key)
                return false;
            else if (AwayMinusPointKey == key)
                return false;
            else if (AddPeriodKey == key)
                return false;
            else if (MinusPeriodKey == key)
                return false;
            else if (SwitchPossesionKey == key)
                return false;
            else
                return true;
        }

        #region Key Properties
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
        #endregion

        #region Key Name Properties
        private string _timerStartStop;
        public String TimerStartStop
        {
            get
            {
                return _timerStartStop;
            }
            set
            {
                _timerStartStop = value;
                RaisePropertyChanged();
            }
        }

        private string _homeAddPoint;
        public String HomeAddPoint
        {
            get
            {
                return _homeAddPoint;
            }
            set
            {
                _homeAddPoint = value;
                RaisePropertyChanged();
            }
        }

        private string _homeMinusPoint;
        public String HomeMinusPoint
        {
            get
            {
                return _homeMinusPoint;
            }
            set
            {
                _homeMinusPoint = value;
                RaisePropertyChanged();
            }
        }

        private string _awayAddPoint;
        public String AwayAddPoint
        {
            get
            {
                return _awayAddPoint;
            }
            set
            {
                _awayAddPoint = value;
                RaisePropertyChanged();
            }
        }

        private string _awayMinusPoint;
        public String AwayMinusPoint
        {
            get
            {
                return _awayMinusPoint;
            }
            set
            {
                _awayMinusPoint = value;
                RaisePropertyChanged();
            }
        }

        private string _addPeriod;
        public String AddPeriod
        {
            get
            {
                return _addPeriod;
            }
            set
            {
                _addPeriod = value;
                RaisePropertyChanged();
            }
        }

        private string _minusPeriod;
        public String MinusPeriod
        {
            get
            {
                return _minusPeriod;
            }
            set
            {
                _minusPeriod = value;
                RaisePropertyChanged();
            }
        }

        private string _switchPossesion;
        public String SwitchPossesion
        {
            get
            {
                return _switchPossesion;
            }
            set
            {
                _switchPossesion = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        public void UpdateKey(string parameter, Key key)
        {
            if (CanUpdateKey(key))
            {
                hasUnsavedChanges = true;

                switch (parameter)
                {
                    case "StartStopTimer":
                        TimerStartStopKey = key;
                        break;
                    case "HomeAddPoint":
                        HomeAddPointKey = key;
                        break;
                    case "HomeMinusPoint":
                        HomeMinusPointKey = key;
                        break;
                    case "AwayAddPoint":
                        AwayAddPointKey = key;
                        break;
                    case "AwayMinusPoint":
                        AwayMinusPointKey = key;
                        break;
                    case "AddPeriod":
                        AddPeriodKey = key;
                        break;
                    case "MinusPeriod":
                        MinusPeriodKey = key;
                        break;
                    case "SwitchPossesion":
                        SwitchPossesionKey = key;
                        break;
                }
            }
        }

        public void Save()
        {
            previousTimerStartStopKey = _model.TimerStartStopKey;
            previousHomeAddPointKey = _model.HomeAddPointKey;
            previousHomeMinusPointKey = _model.HomeMinusPointKey;
            previousAwayMinusPointKey = _model.AwayMinusPointKey;
            previousAwayAddPointKey = _model.AwayAddPointKey;
            previousAddPeriodKey = _model.AddPeriodKey;
            previousMinusPeriodKey = _model.MinusPeriodKey;
            previousSwitchPossesionKey = _model.SwitchPossesionKey;

            _settings.SaveToFile(keyboardSettings: _model);

            hasUnsavedChanges = false;
        }

        public void Reset()
        {
            TimerStartStopKey = DefaultSettings.TimerStartStopKey;
            HomeAddPointKey = DefaultSettings.HomeAddPointKey;
            HomeMinusPointKey = DefaultSettings.HomeMinusPointKey;
            AwayMinusPointKey = DefaultSettings.AwayMinusPointKey;
            AwayAddPointKey = DefaultSettings.AwayAddPointKey;
            AddPeriodKey = DefaultSettings.PeriodAddKey;
            MinusPeriodKey = DefaultSettings.PeriodMinusKey;
            SwitchPossesionKey = DefaultSettings.SwitchPossesionKey;

            Save();
        }

        public void UndoToLastSetting()
        {
            TimerStartStopKey = previousTimerStartStopKey;
            HomeAddPointKey = previousHomeAddPointKey;
            HomeMinusPointKey = previousHomeMinusPointKey;
            AwayMinusPointKey = previousAwayMinusPointKey;
            AwayAddPointKey = previousAwayAddPointKey;
            AddPeriodKey = previousAddPeriodKey;
            MinusPeriodKey = previousMinusPeriodKey;
            SwitchPossesionKey = previousSwitchPossesionKey;

            hasUnsavedChanges = false;
        }
    }
}
