using System.ComponentModel;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class BreakTimerViewModel : ViewModelBase
    {
        private TimerViewModel? _timer;

        public BreakTimerViewModel(TimerViewModel timerModel)
        {
            _timer = timerModel;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimerViewModel Timer

        {
            get
            {
                return _timer!;
            }
            set
            {
                _timer = value;
                RaisePropertyChanged();
            }
        }
    }
}
