using DocumentFormat.OpenXml.EMMA;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardControlViewModel : ViewModelBase
    {
        private ScoreboardItemViewModel _scoreboardItemViewModel;
        private TimerViewModel _timerViewModel;
        private KeyboardSettingsItemViewModel _keyboardSettingsItemViewModel;

        public ScoreboardControlViewModel(ScoreboardItemViewModel model, TimerViewModel timerViewModel, KeyboardSettingsItemViewModel keyboardSettingsItemViewModel)
        {
            Scoreboard = model;
            Timer = timerViewModel; 
            Timer.Timer = Scoreboard.PeriodTimer;
            timerViewModel.TimerEnd += OnTimerEnded;

            AddCommand = new DelegateCommand(Add);
            MinusCommand = new DelegateCommand(Minus);
            StartStopTimerCommand = new DelegateCommand(StartStop);
            ResetTimerCommand = new DelegateCommand(Reset);
            UploadLogoCommand = new DelegateCommand(UploadLogo);
            SwitchPossessionCommand = new DelegateCommand(SwitchPossession);
            PlayBuzzerCommand = new DelegateCommand(PlayBuzzer);
            ExportCommand = new DelegateCommand(Export);
        }

        protected virtual void OnTimerEnded(object? sender, TimerEndEventArgs e)
        {
            if (Scoreboard!.SoundBuzzer)
            {
                SoundPlayer player = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Sounds\\buzzer.wav");
                try
                {
                    player.Play();
                }
                catch (Exception)
                {
                }
            }
            _scoreboardItemViewModel!.RecordScore();
            if (Scoreboard!.AutoSetBreakTimer)
            {
                _timerViewModel.Timer = _scoreboardItemViewModel!.GetTimer(false);
            }

            if (Scoreboard!.AutoStartBreakTimer)
            {
                if (_scoreboardItemViewModel.IsBreak)
                {
                    Task.Delay(1500).ContinueWith(t => StartStop(null));
                }
            }
        }

        public ScoreboardItemViewModel? Scoreboard
        {
            get
            {
                return _scoreboardItemViewModel;
            }
            set
            {
                _scoreboardItemViewModel = value;
                RaisePropertyChanged();
            }
        }

        public KeyboardSettingsItemViewModel? KeyboardSettings
        {
            get
            {
                return _keyboardSettingsItemViewModel;
            }
            set
            {
                _keyboardSettingsItemViewModel = value;
            }
        }

        public TimerViewModel? Timer
        {
            get => _timerViewModel;
            set
            {
                _timerViewModel = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand MinusCommand { get; }
        public DelegateCommand StartStopTimerCommand { get; }
        public DelegateCommand ResetTimerCommand { get; }
        public DelegateCommand UploadLogoCommand { get; }
        public DelegateCommand SwitchPossessionCommand { get; }
        public DelegateCommand PlayBuzzerCommand { get; }
        public DelegateCommand ExportCommand { get; }


        private void Add(object? team)
        {
            var a = team!.ToString();
            if (a == "home")
            {
                Scoreboard!.HomePoints++;
            }
            else if (a == "away")
            {
                Scoreboard!.AwayPoints++;
            }
            else if (a == "period")
            {
                Scoreboard!.Period++;
            }
        }

        private void Minus(object? team)
        {
            var a = team!.ToString();
            if (a == "home")
            {
                Scoreboard!.HomePoints--;
            }
            else if (a == "away")
            {
                Scoreboard!.AwayPoints--;
            }
            else if (a == "period")
            {
                Scoreboard!.Period--;
            }
        }

        private void StartStop(object? o)
        {
            if (Timer!.IsTimerStarted)
            {
                Timer!.StopTimer();
            }
            else
            {
                Timer!.StartTimer();
            }
        }

        public void Reset(object? o)
        {
            Timer!.StopTimer();
            Timer.Timer = Scoreboard!.GetTimer(true);
        }

        private void UploadLogo(object? team)
        {
            var a = team!.ToString();
            if (a == "home")
            {
                Scoreboard!.HomeLogo = LoadImagePath(a);
            }
            else if (a == "away")
            {
                Scoreboard!.AwayLogo = LoadImagePath(a);
            }
        }

        private string? LoadImagePath(string? team)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
                "|PNG Portable Network Graphics (*.png)|*.png" +
                "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
                "|BMP Windows Bitmap (*.bmp)|*.bmp" +
                "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
                "|GIF Graphics Interchange Format (*.gif)|*.gif";

            if (open.ShowDialog() == true)
                return open.FileName;

            return team == "home" ? Scoreboard!.HomeLogo : Scoreboard!.AwayLogo;
        }

        public void SwitchPossession(object? param)
        {
            Scoreboard!.HomePossession = !Scoreboard.HomePossession;
            Scoreboard!.AwayPossession = !Scoreboard.AwayPossession;
        }

        private void PlayBuzzer(object? parameter)
        {
            SoundPlayer player = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Sounds\\buzzer.wav");
            try
            {
                player.Play();
            }
            catch (Exception)
            {
                MessageBox.Show("Error playing sound. Sound file 'buzzer.wav' is not found or corrupted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Export(object? parameter)
        {
            bool? exportResult = _scoreboardItemViewModel!.ExportScoreData();

            if (exportResult != null)
            {
                if ((bool)exportResult)
                {
                    MessageBox.Show("Scores successfully exported to file: " + _scoreboardItemViewModel.ScoreDataFileName, "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to export scores", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
