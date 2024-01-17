﻿using System.ComponentModel;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class BreakTimerViewModel : ViewModelBase
    {
        private ControlsViewModel _controlsViewModel;
        private ScoreboardItemViewModel? _scoreboardItemViewModel;

        public BreakTimerViewModel()
        {
            //_scoreboardItemViewModel = controlsViewModel.Scoreboard;
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