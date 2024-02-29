using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class TimerViewModel : ViewModelBase
    {
        private CountdownTimer _model;
        public event EventHandler<TimerEndEventArgs>? TimerEnd;
        private readonly MicroTimer _microTimer;
        private TimeSpan _timeInterval;
        private bool _countdownByMilliseconds;

        public TimerViewModel()
        {
            _model = new CountdownTimer();
            _countdownByMilliseconds = StaticSettings.CountdownByMilliseconds;

            _microTimer = new MicroTimer();
            _microTimer.MicroTimerElapsed += new MicroTimer.MicroTimerElapsedEventHandler(Timer_Tick);
            if (StaticSettings.CountdownByMilliseconds)
            {
                _timeInterval = TimeSpan.FromMilliseconds(10);
                _microTimer.Interval = 10000;
                _microTimer.IgnoreEventIfLateBy = 10000 / 2; 
            }
            else
            {
                _timeInterval = TimeSpan.FromSeconds(1);
                _microTimer.Interval = 1000000;
                _microTimer.IgnoreEventIfLateBy = 1000000 / 2;
            }

        }

        protected virtual void OnTimerEnded(TimerEndEventArgs e)
        {
            TimerEnd?.Invoke(this, e);
            UpdateTimer();
        }

        private async void Timer_Tick(object? sender, MicroTimerEventArgs e)
        {
            if (Timer.TotalMilliseconds > 0)
            {
                Timer = Timer.Subtract(_timeInterval);
            }
            else
            {
                StopTimer();
                OnTimerEnded(new TimerEndEventArgs { TimerEnded = true });
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

        public void StartTimer()
        {
            _microTimer.Start();
        }

        public void StopTimer()
        {
            _microTimer.Stop();
        }

        public bool IsTimerStarted
        {
            get => _microTimer.Enabled;
        }

        public void DisposeMicroTimer()
        {
            if (!_microTimer.StopAndWait(1000))
            {
                _microTimer.Abort();
            }
        }

        public void UpdateTimer()
        {
            if (_countdownByMilliseconds != StaticSettings.CountdownByMilliseconds)
            {
                _countdownByMilliseconds = StaticSettings.CountdownByMilliseconds;
                if (StaticSettings.CountdownByMilliseconds)
                {
                    _timeInterval = TimeSpan.FromMilliseconds(10);
                    _microTimer.Interval = 10000;
                    _microTimer.IgnoreEventIfLateBy = 10000 / 2;
                }
                else
                {
                    _timeInterval = TimeSpan.FromSeconds(1);
                    _microTimer.Interval = 1000000;
                    _microTimer.IgnoreEventIfLateBy = 1000000 / 2;
                }
            }
        }
    }
}
