using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Data;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public ScoreboardWindowViewModel(ScoreboardItemViewModel model)
        {
            model.TimerEnd += SwitchView;
            ScoreboardViewModel = new ScoreboardViewModel(model);
            BreakTimerViewModel = new BreakTimerViewModel(model);
            SelectedViewModel = ScoreboardViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        private void SwitchView(object? sender, TimerEndEventArgs e)
        {
            if (_selectedViewModel!.GetType() == typeof(ScoreboardViewModel))
            {
                SelectViewModel(BreakTimerViewModel);
                ScoreboardViewModel!.Scoreboard!.Period++;
            }
            else
            {
                SelectViewModel(ScoreboardViewModel);
            }
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
