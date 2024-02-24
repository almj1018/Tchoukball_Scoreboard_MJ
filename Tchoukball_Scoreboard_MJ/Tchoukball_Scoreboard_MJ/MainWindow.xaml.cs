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
using System.Windows.Threading;

namespace Tchoukball_Scoreboard_MJ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private DispatcherTimer _timer;

        double panelWidth;
        bool hidden;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            _timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Do you want to close the scoreboard?", "Confirm Exit?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _viewModel._scoreboardControlViewModel!.Export(null);
                // Stop the timer, wait for up to 1 sec for current event to finish,
                //  if it does not finish within this time abort the timer thread
                _viewModel._timerViewModel!.DisposeMicroTimer();
                Application.Current.Shutdown();
            }
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
            cleanedInput = cleanedInput.Length > 6 ? cleanedInput.Substring(0, 6) : cleanedInput;

            // Add colons to format as mm:ss.ff
            if (cleanedInput.Length >= 2)
            {
                cleanedInput = cleanedInput.Insert(2, ":");
                cleanedInput = cleanedInput.Insert(5, ".");
            }

            // Update the TextBox text
            textBox.Text = cleanedInput;
            // Restore the caret index
            textBox.CaretIndex = Math.Min(caretIndex, textBox.Text.Length);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sidePanel.Width += 1;
                if (sidePanel.Width >= panelWidth)
                {
                    _timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 1;
                if (sidePanel.Width <= 35)
                {
                    _timer.Stop();
                    hidden = true;
                }
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to close the scoreboard?", "Confirm Exit?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _viewModel._scoreboardControlViewModel!.Export(null);
                // Stop the timer, wait for up to 1 sec for current event to finish,
                //  if it does not finish within this time abort the timer thread
                _viewModel._timerViewModel!.DisposeMicroTimer();
                Application.Current.Shutdown();
            }
            else
                e.Handled = true;
        }

        private void MainGridOnClick(object sender, MouseButtonEventArgs e)
        {
            keyboardButton.IsChecked = false;
        }
    }
}