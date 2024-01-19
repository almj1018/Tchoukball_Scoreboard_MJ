using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.Model;

namespace Tchoukball_Scoreboard_MJ.ViewModel;

public class KeyboardSettingsWindowViewModel : ViewModelBase
{
    private KeyboardSettingsItemViewModel _model;

    public KeyboardSettingsWindowViewModel(KeyboardSettingsItemViewModel keyboardSettingsItemViewModel)
    {
        _model = keyboardSettingsItemViewModel;
        SaveCommand = new DelegateCommand(Save);
        ResetCommand = new DelegateCommand(Reset);
    }

    public async override Task LoadAsync()
    {

        await Task.Delay(0);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public DelegateCommand SaveCommand { get; }
    public DelegateCommand ResetCommand { get; }

    private void Save(object? parameter)
    {
        _model.Save();
    }

    private void Reset(object? parameter)
    {
        _model.Reset();
    }

    public void OnKeyPressHandler(string parameter, Key key)
    {
        KeyboardSettings.UpdateKey(parameter, key);
        //if (KeyboardSettings.CanUpdateKey(key))
        //{
        //    KeyboardSettings.hasUnsavedChanges = true;

        //    switch (parameter)
        //    {
        //        case "StartStopTimer":
        //            KeyboardSettings.TimerStartStopKey = key;
        //            break;
        //        case "HomeAddPoint":
        //            KeyboardSettings.HomeAddPointKey = key;
        //            break;
        //        case "HomeMinusPoint":
        //            KeyboardSettings.HomeMinusPointKey = key;
        //            break;
        //        case "AwayAddPoint":
        //            KeyboardSettings.AwayAddPointKey = key;
        //            break;
        //        case "AwayMinusPoint":
        //            KeyboardSettings.AwayMinusPointKey = key;
        //            break;
        //        case "AddPeriod":
        //            KeyboardSettings.AddPeriodKey = key;
        //            break;
        //        case "MinusPeriod":
        //            KeyboardSettings.MinusPeriodKey = key;
        //            break;
        //        case "SwitchPossesion":
        //            KeyboardSettings.SwitchPossesionKey = key;
        //            break;
        //    } 
        //}
    }

    public KeyboardSettingsItemViewModel KeyboardSettings
    {
        get
        {
            return _model;
        }
        set
        {
            _model = value;
            RaisePropertyChanged();
        }
    }
}
