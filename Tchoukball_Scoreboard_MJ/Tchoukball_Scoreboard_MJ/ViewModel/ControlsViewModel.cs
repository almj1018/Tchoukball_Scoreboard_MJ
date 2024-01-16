using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.Data;
using Tchoukball_Scoreboard_MJ.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ControlsViewModel : ViewModelBase
    {
        private readonly IScoreboardDataProvider _scoreboardDataProvider;
        private ScoreboardItemViewModel? _scoreboardItemViewModel;
        private ViewModelBase? _selectedViewModel;

        public ControlsViewModel(IScoreboardDataProvider scoreboardDataProvider)
        {
            _scoreboardDataProvider = scoreboardDataProvider;
            AddCommand = new DelegateCommand(Add);
            MinusCommand = new DelegateCommand(Minus);
            StartStopTimerCommand = new DelegateCommand(StartStop);
            ResetTimerCommand = new DelegateCommand(Reset);

            ScoreboardWindowView scoreboard = new ScoreboardWindowView();
            scoreboard.DataContext = this;
            scoreboard.Show();
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

        public async override Task LoadAsync()
        {
            var scoreboardData = await _scoreboardDataProvider.GetAsync();
            Scoreboard = new ScoreboardItemViewModel(scoreboardData!);
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand MinusCommand { get; }
        public DelegateCommand StartStopTimerCommand { get; }
        public DelegateCommand ResetTimerCommand { get; }

        private void Add(object? team)
        {
            var a = team!.ToString();
            if (a == "home")
            {
                Scoreboard!.HomePoints++;
            }
            else if (a == "guest")
            {
                Scoreboard!.GuestPoints++;
            }
            else if (a == "period")
            {
                Scoreboard!.Period++;
            }
        }

        private void Minus(object? team)
        {
            var a = team!.ToString();
            if (a == "home")
            {
                Scoreboard!.HomePoints--;
            }
            else if (a == "guest")
            {
                Scoreboard!.GuestPoints--;
            }
            else if (a == "period")
            {
                Scoreboard!.Period--;
            }
        }

        private void StartStop(object? o)
        {
            if (Scoreboard!.IsTimerStarted)
            {
                Scoreboard!.StopTimer();
            }
            else
            {
                Scoreboard!.StartTimer();
            }
        }

        private void Reset(object? o)
        {
            Scoreboard!.StopTimer();
            Scoreboard!.Timer = new TimeSpan(0, 15, 0);
        }
    }
}
