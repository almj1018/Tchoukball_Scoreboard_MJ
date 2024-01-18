using Tchoukball_Scoreboard_MJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardItemViewModel : ValidationViewModelBase
    {
        private readonly Scoreboard _model;
        private DispatcherTimer dispatcherTimer;

        public ScoreboardItemViewModel()
        {
            _model = new Scoreboard
            {
                Period = 0,
                Timer = new TimeSpan(0, 15, 0),
                HomeName = "Home",
                GuestName = "Guest",
                HomePoints = 0,
                GuestPoints = 0
            };

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += Timer_Tick;
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
                _model.Period = value;
                RaisePropertyChanged();
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
                _model.HomePoints = value;
                RaisePropertyChanged();
            }
        }

        public int GuestPoints
        {
            get => _model.GuestPoints;
            set
            {
                _model.GuestPoints = value;
                RaisePropertyChanged();
            }
        }

    }
}
