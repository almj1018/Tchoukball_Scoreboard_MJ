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
            StartTimerCommand = new DelegateCommand(Start);
            StopTimerCommand = new DelegateCommand(Stop);
            ResetTimerCommand = new DelegateCommand(Reset);

            var a = Application.Current.Windows;

            ScoreboardWindowView scoreboard = new ScoreboardWindowView();
            scoreboard.DataContext = this;
            //scoreboard.Owner = Application.Current.MainWindow;
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
        public DelegateCommand StartTimerCommand { get; }
        public DelegateCommand StopTimerCommand { get; }
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

        private void Start(object? o)
        {
            Scoreboard!.StartTimer();
        }

        private void Stop(object? o)
        {
            Scoreboard!.StopTimer();
        }

        private void Reset(object? o)
        {
            Stop(o);
            Scoreboard!.Timer = new TimeSpan(0, 15, 0);
        }
    }
}
