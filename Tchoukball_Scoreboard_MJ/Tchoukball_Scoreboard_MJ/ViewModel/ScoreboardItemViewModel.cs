using Tchoukball_Scoreboard_MJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using System.Windows.Navigation;
using System.Media;
using System.Windows;
using System.Data;
using System.Data.Common;
using System.IO;
using ClosedXML.Excel;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardItemViewModel : ValidationViewModelBase
    {
        private Scoreboard _model;
        private OtherSettingsItemViewModel _otherSettingsItemViewModel;
        public bool IsBreak = false;

        public ScoreboardItemViewModel(Scoreboard scoreboard, OtherSettingsItemViewModel otherSettingsItemViewModel)
        {
            _model = scoreboard;
            _otherSettingsItemViewModel = otherSettingsItemViewModel;
        }

        #region Other Settings Properties
        public bool EnableBreakTimerScreen => _otherSettingsItemViewModel.EnableBreakTimerScreen;
        public bool AutoIncrementPeriod => _otherSettingsItemViewModel.AutoIncrementPeriod;
        public bool SoundBuzzer => _otherSettingsItemViewModel.SoundBuzzer;
        public bool AutoSetBreakTimer => _otherSettingsItemViewModel.AutoSetBreakTimer;
        public bool AutoStartBreakTimer => _otherSettingsItemViewModel.AutoStartBreakTimer;
        public bool DisableTimerResetButtonWhileTimerIsStarted => _otherSettingsItemViewModel.DisableTimerResetButtonWhileTimerIsStarted;
        #endregion

        public void ResetScoreboard()
        {
            Period = 1;
            HomePoints = 0;
            AwayPoints = 0;
            HomeLogo = null;
            AwayLogo = null;
            HomePossession = true;
            AwayPossession = false;
            PeriodTimer = _otherSettingsItemViewModel.PeriodTime;
            BreakTimer = _otherSettingsItemViewModel.BreakTime;
            HomeName = _otherSettingsItemViewModel.DefaultHomeName;
            AwayName = _otherSettingsItemViewModel.DefaultAwayName;

            IsBreak = false;
        }

        #region Scoreboard Display Properties
        public string MatchNo
        {
            get => _model.MatchNo;
            set
            {
                _model.MatchNo = value;
                RaisePropertyChanged();
            }
        }

        public int Period
        {
            get => _model.Period;
            set
            {
                if (value >= 1)
                {
                    _model.Period = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TimeSpan PeriodTimer
        {
            get => _model.PeriodTimer;
            set
            {
                _model.PeriodTimer = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan BreakTimer
        {
            get => _model.BreakTimer;
            set
            {
                _model.BreakTimer = value;
                RaisePropertyChanged();
            }
        }

        public string? HomeName
        {
            get => _model.HomeName;
            set
            {
                _model.HomeName = value;
                RaisePropertyChanged();
            }
        }

        public string? AwayName
        {
            get => _model.AwayName;
            set
            {
                _model.AwayName = value;
                RaisePropertyChanged();
            }
        }

        public int HomePoints
        {
            get => _model.HomePoints;
            set
            {
                if (value >= 0)
                {
                    _model.HomePoints = value;
                    RecordScore();
                    RaisePropertyChanged();
                }
            }
        }

        public int AwayPoints
        {
            get => _model.AwayPoints;
            set
            {
                if (value >= 0)
                {
                    _model.AwayPoints = value;
                    RecordScore();
                    RaisePropertyChanged();
                }
            }
        }

        public string? HomeLogo
        {
            get => _model.HomeLogo;
            set
            {
                _model.HomeLogo = value;
                RaisePropertyChanged();
            }
        }

        public string? AwayLogo
        {
            get => _model.AwayLogo;
            set
            {
                _model.AwayLogo = value;
                RaisePropertyChanged();
            }
        }

        public bool HomePossession
        {
            get => _model.HomePossession;
            set
            {
                _model.HomePossession = value;
                RaisePropertyChanged();
            }
        }

        public bool AwayPossession
        {
            get => _model.AwayPossession;
            set
            {
                _model.AwayPossession = value;
                RaisePropertyChanged();
            }
        }

        public string CategoryName
        {
            get => _model.Category;
            set
            {
                _model.Category = value;
                RaisePropertyChanged();
            }
        }

        public string CategoryColor
        {
            get
            {
                if (string.IsNullOrEmpty(_model.CategoryColor))
                {
                    return "#FFD3D3D3";
                }
                else
                    return _model.CategoryColor;
            }
            set
            {
                _model.CategoryColor = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ObservableDictionary<int, PeriodScore> PeriodScores
        {
            get => _model.ScoreHistory;
            set
            {
                _model.ScoreHistory = value;
                RaisePropertyChanged();
            }
        }

        public async void RecordScore()
        {
            if (_model.ScoreHistory.ContainsKey(Period))
            {
                _model.ScoreHistory[Period] = new PeriodScore
                {
                    HomeScore = HomePoints,
                    AwayScore = AwayPoints
                };
            }
            else
            {
                _model.ScoreHistory.Add(Period, new PeriodScore
                {
                    HomeScore = HomePoints,
                    AwayScore = AwayPoints
                });
            }

            PeriodScores = _model.ScoreHistory;
        }

        public TimeSpan GetTimer(bool IsReset)
        {
            if (IsReset)
            {
                return IsBreak ? _model.BreakTime : _model.PeriodTime;
            }
            else
            {
                if (_otherSettingsItemViewModel.AutoSetBreakTimer)
                {
                    if (!IsBreak)
                    {
                        if (AutoIncrementPeriod)
                        {
                            Period++;
                        }
                        IsBreak = true;
                        return _model.BreakTime;
                    }
                }
                IsBreak = false;
                return _model.PeriodTime;
            }
        }
    }
}
