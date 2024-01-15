using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardWindowViewModel : ViewModelBase
    {
        //private readonly IScoreboardDataProvider _scoreboardDataProvider;
        private ScoreboardItemViewModel? _scoreboardItemViewModel;
        private ControlsViewModel _controlsViewModel;
        //private TimerViewModel _timerViewModel;
        private ViewModelBase? _selectedViewModel;

        public ScoreboardWindowViewModel(ControlsViewModel controlsViewModel)
        {
            _controlsViewModel = controlsViewModel;
            _scoreboardItemViewModel = controlsViewModel.Scoreboard;
            //_scoreboardDataProvider = scoreboardDataProvider;
            //_timerViewModel = timerViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public ViewModelBase TimerViewModel { get { return _timerViewModel; } }

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

        //public async override Task LoadAsync()
        //{
        //    var scoreboardData = await _scoreboardDataProvider.GetAsync();
        //    Scoreboard = new ScoreboardItemViewModel(scoreboardData!);
        //}
    }
}
