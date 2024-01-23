using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.View;
using System.ComponentModel;
using Microsoft.Win32;

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
        Scoreboard.PeriodTimer = OtherSettings!.PeriodTime;
        Scoreboard.BreakTimer = OtherSettings!.BreakTime;
        Scoreboard.Timer = OtherSettings!.PeriodTime;
        Scoreboard.HomeName = OtherSettings!.DefaultHomeName;
        Scoreboard.AwayName = OtherSettings!.DefaultAwayName;

        AddCommand = new DelegateCommand(Add);
        MinusCommand = new DelegateCommand(Minus);
        StartStopTimerCommand = new DelegateCommand(StartStop);
        ResetTimerCommand = new DelegateCommand(Reset);
        UploadLogoCommand = new DelegateCommand(UploadLogo);

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

    public OtherSettingsItemViewModel? OtherSettings
    {
        get => _otherSettingsItemViewModel;
        set
        {
            _otherSettingsItemViewModel = value!;
            RaisePropertyChanged();
        }
    }

    public DelegateCommand AddCommand { get; }
    public DelegateCommand MinusCommand { get; }
    public DelegateCommand StartStopTimerCommand { get; }
    public DelegateCommand ResetTimerCommand { get; }
    public DelegateCommand UploadLogoCommand { get; }

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

    private void UploadLogo(object? team)
    {
        var a = team!.ToString();
        if (a == "home")
        {
            Scoreboard!.HomeLogo = LoadImagePath();
        }
        else if (a == "away")
        {
            Scoreboard!.AwayLogo = LoadImagePath();
        }
    }

    private string? LoadImagePath()
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

        return null;
    }
}
