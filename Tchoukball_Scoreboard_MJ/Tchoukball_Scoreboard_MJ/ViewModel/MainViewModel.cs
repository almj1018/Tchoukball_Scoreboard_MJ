using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.View;
using System.ComponentModel;
using Microsoft.Win32;
using System.Windows;
using System.Media;
using DocumentFormat.OpenXml.Drawing.Charts;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Helper;

namespace Tchoukball_Scoreboard_MJ.ViewModel;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _selectedViewModel;
    private ViewModelBase? _selectedSideBarViewModel;
    private KeyboardSettingsWindowView _keyboardSettingsView;
    private KeyboardSettingsItemViewModel? _keyboardSettingsItemViewModel;
    public TimerViewModel _timerViewModel;
    public ScoreboardControlViewModel _scoreboardControlViewModel;

    private OtherSettingsWindowView _otherSettingsWindowView;

    public MainViewModel(ScoreboardControlViewModel scoreboardControlViewModel,
        KeyboardShortcutsViewModel keyboardShortcutsViewModel,
        ScoreboardItemViewModel scoreboardItemViewModel, 
        KeyboardSettingsItemViewModel keyboardSettingsItemViewModel, 
        KeyboardSettingsWindowView keyboardSettingsWindowView, 
        OtherSettingsWindowView otherSettingsWindowView,
        TimerViewModel timerViewModel)
    {
        _keyboardSettingsView = keyboardSettingsWindowView;
        _keyboardSettingsItemViewModel = keyboardSettingsItemViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        OpenKeyboardSettingsCommand = new DelegateCommand(OpenKeyboardSettings);

        _otherSettingsWindowView = otherSettingsWindowView;
        OpenOtherSettingsCommand = new DelegateCommand(OpenOtherSettings);

        NewCommand = new DelegateCommand(New);
        
        _scoreboardControlViewModel = scoreboardControlViewModel;
        _timerViewModel = timerViewModel;
        SelectedViewModel = scoreboardControlViewModel;
        SelectedSideBarViewModel = keyboardShortcutsViewModel;

        var scoreboardWindowView = new ScoreboardWindowView(new ScoreboardWindowViewModel(scoreboardItemViewModel, timerViewModel));
        scoreboardWindowView.Show();
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

    public ViewModelBase? SelectedSideBarViewModel
    {
        get => _selectedSideBarViewModel;
        set
        {
            _selectedSideBarViewModel = value;
            RaisePropertyChanged();
        }
    }

    public DelegateCommand SelectViewModelCommand { get; }
    public DelegateCommand OpenKeyboardSettingsCommand { get; }
    public DelegateCommand OpenOtherSettingsCommand { get; }
    public DelegateCommand NewCommand { get; }

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

    private async void SelectSideBarViewModel(object? parameter)
    {
        SelectedSideBarViewModel = parameter as ViewModelBase;
        await LoadAsync();
    }
    private void OpenKeyboardSettings(object? parameter)
    {
        _keyboardSettingsView.Show();
    }

    private void OpenOtherSettings(object? parameter)
    {
        _otherSettingsWindowView.Show();
    }

    private void New(object? parameter)
    {
        var result = MessageBox.Show("Everything will be reset to their default values. Confirm?", "New Scoreboard Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        if (result == MessageBoxResult.Yes)
        {
            _scoreboardControlViewModel!.Scoreboard!.ResetScoreboard();
            _scoreboardControlViewModel!.Reset(null);
        }
    }

    private ScoreboardItemViewModel? _scoreboardItemViewModel;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public KeyboardSettingsItemViewModel? KeyboardSettings
    {
        get
        {
            return _keyboardSettingsItemViewModel;
        }
        set
        {
            _keyboardSettingsItemViewModel = value;
        }
    }

    public ScoreboardControlViewModel? ScoreboardControl
    {
        get => _scoreboardControlViewModel;
        set
        {
            _scoreboardControlViewModel = value;
            RaisePropertyChanged();
        }
    }
}
