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
