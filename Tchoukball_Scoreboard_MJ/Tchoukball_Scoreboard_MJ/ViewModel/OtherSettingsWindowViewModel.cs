using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tchoukball_Scoreboard_MJ.Command;

namespace Tchoukball_Scoreboard_MJ.ViewModel;

public class OtherSettingsWindowViewModel : ViewModelBase
{
    private OtherSettingsItemViewModel _model;

    public OtherSettingsWindowViewModel(OtherSettingsItemViewModel otherSettingsItemViewModel)
    {
        _model = otherSettingsItemViewModel;
        SaveCommand = new DelegateCommand(Save);
        ResetCommand = new DelegateCommand(Reset);
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

    public OtherSettingsItemViewModel OtherSettings
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
