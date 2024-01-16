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
        private ControlsViewModel _controlsViewModel;
        private ViewModelBase? _selectedViewModel;

        public ScoreboardViewModel(ControlsViewModel controlsViewModel)
        {
            _controlsViewModel = controlsViewModel;
            _scoreboardItemViewModel = controlsViewModel.Scoreboard;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
