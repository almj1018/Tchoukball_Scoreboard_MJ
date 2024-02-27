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
    private ScoreboardWindowView scoreboardWindowView;
    public TimerViewModel _timerViewModel;
    public ScoreboardControlViewModel _scoreboardControlViewModel;
    public HomeViewModel _homeViewModel;
    public MatchHistoryViewModel _matchHistoryViewModel;
    public KeyboardShortcutsViewModel _keyboardShortcutsViewModel;

    private OtherSettingsWindowView _otherSettingsWindowView;

    public MainViewModel(KeyboardShortcutsViewModel keyboardShortcutsViewModel,
        KeyboardSettingsItemViewModel keyboardSettingsItemViewModel,
        KeyboardSettingsWindowView keyboardSettingsWindowView,
        OtherSettingsWindowView otherSettingsWindowView,
        HomeViewModel homeViewModel)
    {
        _keyboardSettingsView = keyboardSettingsWindowView;
        _keyboardSettingsItemViewModel = keyboardSettingsItemViewModel;
        _keyboardShortcutsViewModel = keyboardShortcutsViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        ShowSideBarCommand = new DelegateCommand(SelectSideBarViewModel);
        OpenKeyboardSettingsCommand = new DelegateCommand(OpenKeyboardSettings);

        _otherSettingsWindowView = otherSettingsWindowView;
        OpenOtherSettingsCommand = new DelegateCommand(OpenOtherSettings);

        NewCommand = new DelegateCommand(New);
        RefreshCommand = new DelegateCommand(Refresh);
        SaveMatchHistoryCommand = new DelegateCommand(SaveMatchHistory);

        _timerViewModel = new TimerViewModel();
        _homeViewModel = homeViewModel;
        SelectedViewModel = homeViewModel;
        SelectedSideBarViewModel = keyboardShortcutsViewModel;
        _homeViewModel.SwitchUC += OnSwitchControl;

        IsKeyboardSidePanel = false;

        _matchHistoryViewModel = new MatchHistoryViewModel(_homeViewModel);
        _scoreboardControlViewModel = new ScoreboardControlViewModel(HomeViewModel.SelectedMatch, _timerViewModel, _keyboardSettingsItemViewModel);
        scoreboardWindowView = new ScoreboardWindowView(new ScoreboardWindowViewModel(HomeViewModel.SelectedMatch, _timerViewModel));
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
            if (_selectedSideBarViewModel.GetType() == typeof(KeyboardShortcutsViewModel))
                IsKeyboardSidePanel = IsKeyboardSidePanel ? false : true;
            else
                IsKeyboardSidePanel = false;
            RaisePropertyChanged();
        }
    }

    public DelegateCommand SelectViewModelCommand { get; }
    public DelegateCommand OpenKeyboardSettingsCommand { get; }
    public DelegateCommand OpenOtherSettingsCommand { get; }
    public DelegateCommand NewCommand { get; }
    public DelegateCommand RefreshCommand { get; }
    public DelegateCommand SaveMatchHistoryCommand { get; }
    public DelegateCommand ShowSideBarCommand { get; }

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

    private void Refresh(object? parameter)
    {
        var result = MessageBox.Show("Everything will reset and template will be reloaded. Confirm?", "Refresh Scoreboard Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        if (result == MessageBoxResult.Yes)
        {
            _homeViewModel.Refresh();
        }
    }

    private void SaveMatchHistory(object? parameter)
    {
        bool? result = _homeViewModel.ExportScoreData();
        if (result != null)
        {
            if ((bool)result)
            {
                MessageBox.Show("Scores successfully exported to file: " + _homeViewModel.ScoreDataFileName, "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to export scores", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public void ExportMatchHistory()
    {
        SaveMatchHistory(null);
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

    public HomeViewModel? HomeViewModel
    {
        get => _homeViewModel;
        set
        {
            _homeViewModel = value;
            RaisePropertyChanged();
        }
    }

    public KeyboardShortcutsViewModel? KeyboardShortcutsViewModel
    {
        get => _keyboardShortcutsViewModel;
        set
        {
            _keyboardShortcutsViewModel = value;
            RaisePropertyChanged();
        }
    }

    public MatchHistoryViewModel? MatchHistoryViewModel
    {
        get => _matchHistoryViewModel;
        set
        {
            _matchHistoryViewModel = value;
            RaisePropertyChanged();
        }
    }

    private bool _isKeyboardSidePanel;
    public bool IsKeyboardSidePanel
    {
        get
        {
            return _isKeyboardSidePanel;
        }
        set
        {
            _isKeyboardSidePanel = value;
        }
    }

    protected virtual void OnSwitchControl(object? sender, SwitchUserControlEventArgs e)
    {
        if (!e.MatchScoreboard.IsBreak)
            e.MatchScoreboard.PeriodTimer = _timerViewModel.Timer;
        else
            e.MatchScoreboard.BreakTimer = _timerViewModel.Timer;

        ScoreboardControl = new ScoreboardControlViewModel(HomeViewModel.SelectedMatch, _timerViewModel, _keyboardSettingsItemViewModel);
        scoreboardWindowView.SwitchScoreboard(new ScoreboardWindowViewModel(HomeViewModel.SelectedMatch, _timerViewModel));

        SelectViewModel(ScoreboardControl);
    }
}
