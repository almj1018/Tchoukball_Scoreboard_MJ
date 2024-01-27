﻿using Tchoukball_Scoreboard_MJ.Command;
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
    private KeyboardSettingsWindowView _keyboardSettingsView;
    private KeyboardSettingsItemViewModel? _keyboardSettingsItemViewModel;
    private TimerViewModel _timerViewModel;

    private OtherSettingsWindowView _otherSettingsWindowView;

    public MainViewModel(ScoreboardItemViewModel scoreboardItemViewModel, 
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
        PlayBuzzerCommand = new DelegateCommand(PlayBuzzer);
        ExportCommand = new DelegateCommand(Export);

        Scoreboard = scoreboardItemViewModel;
        Timer = timerViewModel;
        Timer.Timer = Scoreboard!.PeriodTimer;
        timerViewModel.TimerEnd += OnTimerEnded;

        AddCommand = new DelegateCommand(Add);
        MinusCommand = new DelegateCommand(Minus);
        StartStopTimerCommand = new DelegateCommand(StartStop);
        ResetTimerCommand = new DelegateCommand(Reset);
        UploadLogoCommand = new DelegateCommand(UploadLogo);
        SwitchPossessionCommand = new DelegateCommand(SwitchPossession);

        var scoreboardWindowView = new ScoreboardWindowView(new ScoreboardWindowViewModel(Scoreboard, timerViewModel));
        scoreboardWindowView.Show();
    }

    protected virtual void OnTimerEnded(object? sender, TimerEndEventArgs e)
    {
        if (Scoreboard!.SoundBuzzer)
        {
            SoundPlayer player = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Sounds\\buzzer.wav");
            try
            {
                player.Play();
            }
            catch (Exception)
            {
            }
        }
        _scoreboardItemViewModel!.RecordScore();
        if (Scoreboard!.AutoSetBreakTimer)
        {
            _timerViewModel.Timer = _scoreboardItemViewModel!.GetTimer(false); 
        }

        if (Scoreboard!.AutoStartBreakTimer)
        {
            if (_scoreboardItemViewModel.IsBreak)
            {
                Task.Delay(1500).ContinueWith(t => StartStop(null)); 
            }
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

    public DelegateCommand SelectViewModelCommand { get; }
    public DelegateCommand OpenKeyboardSettingsCommand { get; }
    public DelegateCommand OpenOtherSettingsCommand { get; }
    public DelegateCommand NewCommand { get; }
    public DelegateCommand PlayBuzzerCommand { get; }
    public DelegateCommand ExportCommand { get; }

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

    private void New(object? parameter)
    {
        var result = MessageBox.Show("Everything will be reset to their default values. Confirm?", "New Scoreboard Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        if (result == MessageBoxResult.Yes)
        {
            Scoreboard!.ResetScoreboard();
            Reset(null);
        }
    }

    private void PlayBuzzer(object? parameter)
    {
        SoundPlayer player = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Sounds\\buzzer.wav");
        try
        {
            player.Play();
        }
        catch (Exception)
        {
            MessageBox.Show("Error playing sound. Sound file 'buzzer.wav' is not found or corrupted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void Export(object? parameter)
    {
        bool? exportResult = _scoreboardItemViewModel!.ExportScoreData();

        if (exportResult != null)
        {
            if ((bool)exportResult)
            {
                MessageBox.Show("Scores successfully exported to file: " + _scoreboardItemViewModel.ScoreDataFileName, "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to export scores", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
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

    public TimerViewModel? Timer
    {
        get => _timerViewModel;
        set
        {
            _timerViewModel = value;
            RaisePropertyChanged();
        }
    }

    public DelegateCommand AddCommand { get; }
    public DelegateCommand MinusCommand { get; }
    public DelegateCommand StartStopTimerCommand { get; }
    public DelegateCommand ResetTimerCommand { get; }
    public DelegateCommand UploadLogoCommand { get; }
    public DelegateCommand SwitchPossessionCommand { get; }

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
        if (Timer!.IsTimerStarted)
        {
            Timer!.StopTimer();
        }
        else
        {
            Timer!.StartTimer();
        }
    }

    private void Reset(object? o)
    {
        Timer!.StopTimer();
        Timer.Timer = Scoreboard!.GetTimer(true);
    }

    private void UploadLogo(object? team)
    {
        var a = team!.ToString();
        if (a == "home")
        {
            Scoreboard!.HomeLogo = LoadImagePath(a);
        }
        else if (a == "away")
        {
            Scoreboard!.AwayLogo = LoadImagePath(a);
        }
    }

    private string? LoadImagePath(string? team)
    {
        OpenFileDialog open = new OpenFileDialog();
        open.DefaultExt = (".png");
        open.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";

        if (open.ShowDialog() == true)
            return open.FileName;

        return team == "home" ? Scoreboard!.HomeLogo : Scoreboard!.AwayLogo;
    }

    public void SwitchPossession(object? param)
    {
        Scoreboard!.HomePossession = !Scoreboard.HomePossession;
        Scoreboard!.AwayPossession = !Scoreboard.AwayPossession;
    }
}
