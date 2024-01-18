﻿using System;
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
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.ViewModel;

namespace Tchoukball_Scoreboard_MJ.View
{
    /// <summary>
    /// Interaction logic for KeyboardSettingsWindowView.xaml
    /// </summary>
    public partial class KeyboardSettingsWindowView : Window
    {
        private KeyboardSettingsWindowViewModel _viewModel;
        public KeyboardSettingsWindowView(KeyboardSettingsWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += ScoreboardWindow_Loaded;
        }

        private async void ScoreboardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void OnKeyPressHandler(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void PreviewKeyPressHandler(object sender, KeyEventArgs e)
        {
            if (!(e.Key == Key.Return))
            {
                var a = (System.Windows.Controls.TextBox)sender;
                _viewModel.OnKeyPressHandler(a.Name, e.Key);
            }
        }
    }
}
