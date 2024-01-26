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

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardItemViewModel : ValidationViewModelBase
    {
        private Scoreboard _model;
        private DispatcherTimer dispatcherTimer;
        public event EventHandler<TimerEndEventArgs>? TimerEnd;
        public bool EnableBreakTimerScreen => _otherSettingsItemViewModel.EnableBreakTimerScreen;
        public bool AutoIncrementPeriod => _otherSettingsItemViewModel.AutoIncrementPeriod;
        private OtherSettingsItemViewModel _otherSettingsItemViewModel;
        private bool IsBreak = false;

        public ScoreboardItemViewModel(OtherSettingsItemViewModel otherSettingsItemViewModel)
        {
            _otherSettingsItemViewModel = otherSettingsItemViewModel;
            _model = new Scoreboard
            {
                Period = 1,
                HomePoints = 0,
                AwayPoints = 0,
                HomeLogo = null,
                AwayLogo = null,
                HomePossession = true,
                AwayPossession = false,
                Timer = _otherSettingsItemViewModel.PeriodTime,
                PeriodTimer = _otherSettingsItemViewModel.PeriodTime,
                BreakTimer = _otherSettingsItemViewModel.BreakTime,
                HomeName = _otherSettingsItemViewModel.DefaultHomeName,
                AwayName = _otherSettingsItemViewModel.DefaultAwayName
            };

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += Timer_Tick;
        }

        public void ResetScoreboard()
        {
            Period = 1;
            HomePoints = 0;
            AwayPoints = 0;
            HomeLogo = null;
            AwayLogo = null;
            HomePossession = true;
            AwayPossession = false;
            Timer = _otherSettingsItemViewModel.PeriodTime;
            PeriodTimer = _otherSettingsItemViewModel.PeriodTime;
            BreakTimer = _otherSettingsItemViewModel.BreakTime;
            HomeName = _otherSettingsItemViewModel.DefaultHomeName;
            AwayName = _otherSettingsItemViewModel.DefaultAwayName;

            IsBreak = false;
        }

        protected virtual void OnTimerEnded(TimerEndEventArgs e)
        {
            TimerEnd?.Invoke(this, e);
            if (_otherSettingsItemViewModel.AutoSetBreakTimer)
            {
                if (!IsBreak)
                {
                    if (AutoIncrementPeriod)
                    {
                        Period++;
                    }
                    Timer = _otherSettingsItemViewModel.BreakTime;
                    IsBreak = true;
                    if (_otherSettingsItemViewModel.AutoStartBreakTimer)
                    {
                        StartTimer(); 
                    }
                    return;
                }
            }
            Timer = _otherSettingsItemViewModel.PeriodTime;
            IsBreak = false;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (Timer.TotalSeconds > 0)
            {
                Timer = Timer.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                StopTimer();
                OnTimerEnded(new TimerEndEventArgs { TimerEnded = true });
            }
        }

        public bool IsTimerStarted { get { return dispatcherTimer.IsEnabled; } }

        public void StartTimer()
        {
            dispatcherTimer.Start();
        }

        public void StopTimer()
        {
            dispatcherTimer.Stop();
        }

        public void ResetTimer()
        {
            if (IsBreak)
            {
                Timer = _otherSettingsItemViewModel.BreakTime;
            }
            else
            {
                Timer = _otherSettingsItemViewModel.PeriodTime;
            }
        }

        public int Period
        {
            get => _model.Period;
            set
            {
                if (value >= 0)
                {
                    _model.Period = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TimeSpan Timer
        {
            get => _model.Timer;
            set
            {
                _model.Timer = value;
                RaisePropertyChanged();
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
    }
}
