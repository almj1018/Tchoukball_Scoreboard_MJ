using Tchoukball_Scoreboard_MJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardItemViewModel : ValidationViewModelBase
    {
        private readonly Scoreboard _model;
        private DispatcherTimer dispatcherTimer;
        public event EventHandler<TimerEndEventArgs>? TimerEnd;

        public ScoreboardItemViewModel()
        {
            _model = new Scoreboard
            {
                Period = 1,
                Timer = new TimeSpan(0, 0, 10),
                HomeName = "Home",
                GuestName = "Guest",
                HomePoints = 0,
                GuestPoints = 0
            };

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += Timer_Tick;
        }

        protected virtual void OnTimerEnded(TimerEndEventArgs e)
        {
            TimerEnd?.Invoke(this, e);
            Timer = new TimeSpan(0, 0, 20);
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

        public string? HomeName
        {
            get => _model.HomeName;
            set
            {
                _model.HomeName = value;
                RaisePropertyChanged();
            }
        }
        public string? GuestName
        {
            get => _model.GuestName;
            set
            {
                _model.GuestName = value;
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

        public int GuestPoints
        {
            get => _model.GuestPoints;
            set
            {
                if (value >= 0)
                {
                    _model.GuestPoints = value;
                    RaisePropertyChanged(); 
                }
            }
        }
    }
}
