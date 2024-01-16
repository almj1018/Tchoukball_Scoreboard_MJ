using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.Data;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public ScoreboardWindowViewModel(ControlsViewModel controlsViewModel)
        {
            ScoreboardViewModel = new ScoreboardViewModel(controlsViewModel);
            BreakTimerViewModel = new BreakTimerViewModel(controlsViewModel);
            SelectedViewModel = ScoreboardViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        public ScoreboardViewModel ScoreboardViewModel { get; }
        public BreakTimerViewModel BreakTimerViewModel { get; }
        public DelegateCommand SelectViewModelCommand { get; }

        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
