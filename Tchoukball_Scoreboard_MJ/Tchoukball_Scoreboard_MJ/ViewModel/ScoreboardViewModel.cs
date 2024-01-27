using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchoukball_Scoreboard_MJ.Command;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardViewModel : ViewModelBase
    {
        private ScoreboardItemViewModel? _scoreboardItemViewModel;
        private TimerViewModel _timerViewModel;

        public ScoreboardViewModel(ScoreboardItemViewModel scoreboardItemViewModel, TimerViewModel timerViewModel)
        {
            _scoreboardItemViewModel = scoreboardItemViewModel;
            _timerViewModel = timerViewModel;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimerViewModel Timer
        {
            get => _timerViewModel;
            set
            {
                _timerViewModel = value;
                RaisePropertyChanged();
            }
        }

        public ScoreboardItemViewModel? Scoreboard
        {
            get => _scoreboardItemViewModel;
            set
            {
                _scoreboardItemViewModel = value;
                RaisePropertyChanged();
            }
        }
    }
}
