using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tchoukball_Scoreboard_MJ.ViewModel;

namespace Tchoukball_Scoreboard_MJ.View
{
    /// <summary>
    /// Interaction logic for OtherSettingsWindowView.xaml
    /// </summary>
    public partial class OtherSettingsWindowView : Window
    {
        private OtherSettingsWindowViewModel _viewModel;

        public OtherSettingsWindowView(OtherSettingsWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += OtherSettingsWindow_Loaded;
        }

        private async void OtherSettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void OnClosingHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            if (_viewModel.OtherSettings.HasUnsavedChanges)
            {
                var result = MessageBox.Show("Do want to save changes to Other Settings?", "Confirm Close?", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.SaveCommand.Execute("");
                    this.Hide();
                }
                else if (result == MessageBoxResult.No)
                {
                    _viewModel.OtherSettings.UndoToLastSetting();
                    this.Hide();
                }
                else if (result == MessageBoxResult.Cancel)
                    return;
            }
            this.Hide();
        }
    }
}
