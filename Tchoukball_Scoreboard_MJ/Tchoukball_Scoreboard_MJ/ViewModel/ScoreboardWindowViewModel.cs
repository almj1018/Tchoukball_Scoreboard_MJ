using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Data;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public ScoreboardWindowViewModel(ScoreboardItemViewModel model, TimerViewModel timerViewModel)
        {
            timerViewModel.TimerEnd += SwitchView;
            ScoreboardViewModel = new ScoreboardViewModel(model, timerViewModel);
            BreakTimerViewModel = new BreakTimerViewModel(timerViewModel);
            SelectedViewModel = ScoreboardViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
            FullScreenCommand = new DelegateCommand(FullScreen);
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void SwitchView(object? sender, TimerEndEventArgs e)
        {
            if (ScoreboardViewModel!.Scoreboard!.EnableBreakTimerScreen)
            {
                if (_selectedViewModel!.GetType() == typeof(ScoreboardViewModel))
                {
                    SelectViewModel(BreakTimerViewModel);
                }
                else
                {
                    SelectViewModel(ScoreboardViewModel);
                }
            }
            else
            {
                if (_selectedViewModel!.GetType() == typeof(BreakTimerViewModel))
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
        public DelegateCommand FullScreenCommand { get; }

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

        public void FullScreen(object? parameter)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            WindowStyle = WindowStyle == WindowStyle.SingleBorderWindow ? WindowStyle.None : WindowStyle.SingleBorderWindow;
        }

        private WindowState _windowState;
        public WindowState WindowState 
        {
            get => _windowState;
            set
            {
                _windowState = value;
                RaisePropertyChanged();
            }
        }

        private WindowStyle _windowStyle;
        public WindowStyle WindowStyle 
        {
            get => _windowStyle;
            set
            {
                _windowStyle = value;
                RaisePropertyChanged();
            }
        }
    }
}
