using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        _model.Save();
    }

    private void Reset(object? parameter)
    {
        _model.Reset();
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
