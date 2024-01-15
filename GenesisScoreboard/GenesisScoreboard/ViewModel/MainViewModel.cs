using GenesisScoreboard.Command;
using GenesisScoreboard.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisScoreboard.ViewModel;

public class MainViewModel :ViewModelBase
{
    private ViewModelBase? _selectedViewModel;

    public MainViewModel(ControlsViewModel controlsViewModel)
    {
        ControlsViewModel = controlsViewModel;
        SelectedViewModel = ControlsViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel);
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

    public ControlsViewModel ControlsViewModel { get; }
    public DelegateCommand SelectViewModelCommand { get; }

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
}
