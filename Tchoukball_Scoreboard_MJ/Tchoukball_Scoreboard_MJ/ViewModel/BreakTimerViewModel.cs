using System.ComponentModel;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class BreakTimerViewModel : ViewModelBase
    {
        private ScoreboardItemViewModel? _scoreboard;

        public BreakTimerViewModel(ScoreboardItemViewModel timerModel)
        {
            _scoreboard = timerModel;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScoreboardItemViewModel Scoreboard
        {
            get
            {
                return _scoreboard!;
            }
            set
            {
                _scoreboard = value;
                RaisePropertyChanged();
            }
        }
    }
}
