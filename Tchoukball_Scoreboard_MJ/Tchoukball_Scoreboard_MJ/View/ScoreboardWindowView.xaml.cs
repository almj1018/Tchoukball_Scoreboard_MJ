using Tchoukball_Scoreboard_MJ.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace Tchoukball_Scoreboard_MJ.View
{
    /// <summary>
    /// Interaction logic for ScoreboardWindowView.xaml
    /// </summary>
    public partial class ScoreboardWindowView : Window
    {
        private ViewModelBase? _viewModel;
        public ScoreboardWindowView(ScoreboardWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += ScoreboardWindow_Loaded;
        }

        public void SwitchScoreboard(ScoreboardWindowViewModel viewModel)
        {
            ScoreboardWindowViewModel originalViewModel = _viewModel as ScoreboardWindowViewModel;
            viewModel.WindowState = originalViewModel.WindowState;
            viewModel.WindowStyle = originalViewModel.WindowStyle;
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void ScoreboardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel!.LoadAsync();
        }

        private void OnClosingHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
