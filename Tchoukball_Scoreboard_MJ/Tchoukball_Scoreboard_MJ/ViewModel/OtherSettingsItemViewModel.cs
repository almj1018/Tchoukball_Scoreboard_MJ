using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Tchoukball_Scoreboard_MJ.CustomEnum;
using Tchoukball_Scoreboard_MJ.Helper;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel;

public class OtherSettingsItemViewModel : ValidationViewModelBase
{
    private readonly OtherSettings _model;
    private readonly SettingsHelper _settings;

    private bool previousEnableBreakTimerScreen;
    private bool previousSoundBuzzer;
    private bool previousDisableTimerResetButtonWhileTimerIsStarted;
    private bool previousAutoIncrementPeriod;
    private bool previousAutoSetBreakTimer;
    private bool previousAutoStartBreakTimer;
    private string? previousDefaultHomeName;
    private string? previousDefaultAwayName;
    private TimeSpan previousBreakTime;
    private TimeSpan previousPeriodTime;
    private bool previousCountdownByMilliseconds;

    public OtherSettingsItemViewModel(SettingsHelper settings)
    {
        _settings = settings;
        _model = _settings._otherSettings;

        previousEnableBreakTimerScreen = _model.EnableBreakTimerScreen;
        previousSoundBuzzer = _model.SoundBuzzer;
        previousDisableTimerResetButtonWhileTimerIsStarted = _model.DisableTimerResetButtonWhileTimerIsStarted;
        previousAutoIncrementPeriod = _model.AutoIncrementPeriod;
        previousAutoSetBreakTimer = _model.AutoSetBreakTimer;
        previousAutoStartBreakTimer = _model.AutoStartBreakTimer;
        previousDefaultHomeName = _model.DefaultHomeName;
        previousDefaultAwayName = _model.DefaultAwayName;
        previousBreakTime = _model.BreakTime;
        previousPeriodTime = _model.PeriodTime;
        previousCountdownByMilliseconds = _model.CountdownByMilliseconds;

        StaticSettings.CountdownByMilliseconds = _model.CountdownByMilliseconds;

        HasUnsavedChanges = false;
    }

    #region Model Properties
    public bool EnableBreakTimerScreen
    {
        get => _model.EnableBreakTimerScreen;
        set
        {
            _model.EnableBreakTimerScreen = value;
            RaisePropertyChanged();
        }
    }

    public bool SoundBuzzer
    {
        get => _model.SoundBuzzer;
        set
        {
            _model.SoundBuzzer = value;
            RaisePropertyChanged();
        }
    }

    public bool DisableTimerResetButtonWhileTimerIsStarted
    {
        get => _model.DisableTimerResetButtonWhileTimerIsStarted;
        set
        {
            _model.DisableTimerResetButtonWhileTimerIsStarted = value;
            RaisePropertyChanged();
        }
    }

    public bool AutoIncrementPeriod
    {
        get => _model.AutoIncrementPeriod;
        set
        {
            _model.AutoIncrementPeriod = value;
            RaisePropertyChanged();
        }
    }

    public bool AutoSetBreakTimer
    {
        get => _model.AutoSetBreakTimer;
        set
        {
            _model.AutoSetBreakTimer = value;
            RaisePropertyChanged();
        }
    }

    public bool AutoStartBreakTimer
    {
        get => _model.AutoStartBreakTimer;
        set
        {
            _model.AutoStartBreakTimer = value;
            RaisePropertyChanged();
        }
    }

    public string DefaultHomeName
    {
        get => _model.DefaultHomeName;
        set
        {
            _model.DefaultHomeName = value;
            RaisePropertyChanged();
        }
    }

    public string DefaultAwayName
    {
        get => _model.DefaultAwayName;
        set
        {
            _model.DefaultAwayName = value;
            RaisePropertyChanged();
        }
    }

    public TimeSpan BreakTime
    {
        get => _model.BreakTime;
        set
        {
            _model.BreakTime = value;
            RaisePropertyChanged();
        }
    }

    public TimeSpan PeriodTime
    {
        get => _model.PeriodTime;
        set
        {
            _model.PeriodTime = value;
            RaisePropertyChanged();
        }
    }

    public bool CountdownByMilliseconds
    {
        get => _model.CountdownByMilliseconds;
        set
        {
            _model.CountdownByMilliseconds = value;
            StaticSettings.CountdownByMilliseconds = value;
            RaisePropertyChanged();
        }
    }
    #endregion

    private bool _hasUnsavedChanges;
    public bool HasUnsavedChanges
    {
        get
        {
            return CheckForUnsavedChanges();
        }
        set
        {
            _hasUnsavedChanges = value;
        }
    }

    private bool CheckForUnsavedChanges()
    {
        bool result = false;

        if(_model.EnableBreakTimerScreen != previousEnableBreakTimerScreen)
        {
            result = true;
        }
        else if (_model.SoundBuzzer != previousSoundBuzzer)
        {
            result = true;
        }
        else if (_model.DisableTimerResetButtonWhileTimerIsStarted != previousDisableTimerResetButtonWhileTimerIsStarted)
        {
            result = true;
        }
        else if (_model.AutoIncrementPeriod != previousAutoIncrementPeriod)
        {
            result = true;
        }
        else if (_model.AutoSetBreakTimer != previousAutoSetBreakTimer)
        {
            result = true;
        }
        else if (_model.AutoStartBreakTimer != previousAutoStartBreakTimer)
        {
            result = true;
        }
        else if(_model.DefaultHomeName != previousDefaultHomeName)
        {
            result = true;
        }
        else if (_model.DefaultAwayName != previousDefaultAwayName)
        {
            result = true;
        }
        else if (_model.BreakTime != previousBreakTime)
        {
            result = true;
        }
        else if (_model.PeriodTime != previousPeriodTime)
        {
            result = true;
        }
        else if (_model.CountdownByMilliseconds != previousCountdownByMilliseconds)
        {
            result = true;
        }

        return result;
    }

    public void Save()
    {
        previousEnableBreakTimerScreen = _model.EnableBreakTimerScreen;
        previousSoundBuzzer = _model.SoundBuzzer;
        previousDisableTimerResetButtonWhileTimerIsStarted = _model.DisableTimerResetButtonWhileTimerIsStarted;
        previousAutoIncrementPeriod = _model.AutoIncrementPeriod;
        previousAutoSetBreakTimer = _model.AutoSetBreakTimer;
        previousAutoStartBreakTimer = _model.AutoStartBreakTimer;
        previousDefaultHomeName = _model.DefaultHomeName;
        previousDefaultAwayName = _model.DefaultAwayName;
        previousBreakTime = _model.BreakTime;
        previousPeriodTime = _model.PeriodTime;
        previousCountdownByMilliseconds = _model.CountdownByMilliseconds;

        _settings.SaveToFile(otherSettings: _model);

        HasUnsavedChanges = false;
    }

    public void Reset()
    {
        EnableBreakTimerScreen = DefaultSettings.EnableBreakTimerScreen;
        SoundBuzzer = DefaultSettings.SoundBuzzer;
        DisableTimerResetButtonWhileTimerIsStarted = DefaultSettings.DisableTimerResetButtonWhileTimerIsStarted;
        AutoIncrementPeriod = DefaultSettings.AutoIncrementPeriod;
        AutoSetBreakTimer = DefaultSettings.AutoSetBreakTimer;
        AutoStartBreakTimer = DefaultSettings.AutoStartBreakTimer;
        DefaultHomeName = DefaultSettings.DefaultHomeName;
        DefaultAwayName = DefaultSettings.DefaultAwayName;
        BreakTime = DefaultSettings.BreakTime;
        PeriodTime = DefaultSettings.PeriodTime;
        CountdownByMilliseconds = DefaultSettings.CountdownByMilliseconds;
        Save();
    }

    public void UndoToLastSetting()
    {
        _model.EnableBreakTimerScreen = previousEnableBreakTimerScreen;
        _model.SoundBuzzer = previousSoundBuzzer;
        _model.DisableTimerResetButtonWhileTimerIsStarted = previousDisableTimerResetButtonWhileTimerIsStarted;
        _model.AutoIncrementPeriod = previousAutoIncrementPeriod;
        _model.AutoSetBreakTimer = previousAutoSetBreakTimer;
        _model.AutoStartBreakTimer = previousAutoStartBreakTimer;
        _model.DefaultHomeName = previousDefaultHomeName;
        _model.DefaultAwayName = previousDefaultAwayName;
        _model.BreakTime = previousBreakTime;
        _model.PeriodTime = previousPeriodTime;
        _model.CountdownByMilliseconds = previousCountdownByMilliseconds;

        HasUnsavedChanges = false;
    }
}
