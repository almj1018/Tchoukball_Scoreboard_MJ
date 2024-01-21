using Tchoukball_Scoreboard_MJ.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Tchoukball_Scoreboard_MJ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Do you want to close the scoreboard?", "Confirm Exit?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            else
                e.Cancel = true;
        }

        private void On_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Focus();
        }

        private void TimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Store the current caret index
            int caretIndex = textBox.CaretIndex;

            // Remove any non-numeric characters
            string cleanedInput = Regex.Replace(textBox.Text, @"[^0-9]", "");

            // Ensure the input doesn't exceed 4 characters
            cleanedInput = cleanedInput.Length > 4 ? cleanedInput.Substring(0, 4) : cleanedInput;

            // Add colons to format as mm:ss
            if (cleanedInput.Length >= 2)
            {
                cleanedInput = cleanedInput.Insert(2, ":");
            }

            // Update the TextBox text
            textBox.Text = cleanedInput;
            // Restore the caret index
            textBox.CaretIndex = Math.Min(caretIndex, textBox.Text.Length);
        }
    }
}