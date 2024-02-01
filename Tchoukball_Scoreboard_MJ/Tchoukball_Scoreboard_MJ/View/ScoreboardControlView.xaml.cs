using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tchoukball_Scoreboard_MJ.View
{
    /// <summary>
    /// Interaction logic for ScoreboardControlView.xaml
    /// </summary>
    public partial class ScoreboardControlView : UserControl
    {
        public ScoreboardControlView()
        {
            InitializeComponent();
            //Focusable = true;
            //Loaded += (s, e) => Keyboard.Focus(this);
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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Window window = Window.GetWindow(this);
            //foreach (InputBinding ib in this.InputBindings)
            //{
            //    window.InputBindings.Add(ib);
            //}
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Focus();
        }
    }
}
