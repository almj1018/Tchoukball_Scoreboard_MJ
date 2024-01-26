using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Windows;
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
        try
        {
            _model.Save();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to save settings. Error: " + ex.Message, "Saved", MessageBoxButton.OK);
        }
        MessageBox.Show("Settings saved successfully", "Saved", MessageBoxButton.OK);
    }

    private void Reset(object? parameter)
    {
        try
        {
            _model.Reset();

        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to reset settings. Error: " + ex.Message, "Reset", MessageBoxButton.OK);
        }
        MessageBox.Show("Settings reset successfully", "Reset", MessageBoxButton.OK);
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
