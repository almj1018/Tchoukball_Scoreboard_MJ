using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tchoukball_Scoreboard_MJ.ViewModel;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _selectedViewModel;
    private KeyboardSettingsWindowView _keyboardSettingsView;
    private KeyboardSettingsItemViewModel? _keyboardSettingsItemViewModel;

    private OtherSettingsWindowView _otherSettingsWindowView;
    private OtherSettingsItemViewModel _otherSettingsItemViewModel;

    public MainViewModel(ScoreboardItemViewModel scoreboardItemViewModel, 
        KeyboardSettingsItemViewModel keyboardSettingsItemViewModel, 
        KeyboardSettingsWindowView keyboardSettingsWindowView, 
        OtherSettingsItemViewModel otherSettingsItemViewModel, 
        OtherSettingsWindowView otherSettingsWindowView)
    {
        _keyboardSettingsView = keyboardSettingsWindowView;
        _keyboardSettingsItemViewModel = keyboardSettingsItemViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        OpenKeyboardSettingsCommand = new DelegateCommand(OpenKeyboardSettings);

        _otherSettingsWindowView = otherSettingsWindowView;
        _otherSettingsItemViewModel = otherSettingsItemViewModel;
        OpenOtherSettingsCommand = new DelegateCommand(OpenOtherSettings);

        Scoreboard = scoreboardItemViewModel;
        AddCommand = new DelegateCommand(Add);
        MinusCommand = new DelegateCommand(Minus);
        StartStopTimerCommand = new DelegateCommand(StartStop);
        ResetTimerCommand = new DelegateCommand(Reset);

        var scoreboardWindowView = new ScoreboardWindowView(new ScoreboardWindowViewModel(Scoreboard));
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

    public DelegateCommand SelectViewModelCommand { get; }
    public DelegateCommand OpenKeyboardSettingsCommand { get; }
    public DelegateCommand OpenOtherSettingsCommand { get; }

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

    private void OpenKeyboardSettings(object? parameter)
    {
        _keyboardSettingsView.Show();
    }

    private void OpenOtherSettings(object? parameter)
    {
        _otherSettingsWindowView.Show();
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
        else if (a == "away")
        {
            Scoreboard!.AwayPoints++;
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
        else if (a == "away")
        {
            Scoreboard!.AwayPoints--;
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
