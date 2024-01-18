using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardViewModel : ViewModelBase
    {
        private ScoreboardItemViewModel? _scoreboardItemViewModel;

        public ScoreboardViewModel(ScoreboardItemViewModel scoreboardItemViewModel)
        {
            _scoreboardItemViewModel = scoreboardItemViewModel;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScoreboardItemViewModel? Scoreboard
        {
            get
            {
                return _scoreboardItemViewModel;
            }
            set
            {
                _scoreboardItemViewModel = value;
                RaisePropertyChanged();
            }
        }
    }
}
