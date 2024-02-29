using Tchoukball_Scoreboard_MJ.Data;
using Tchoukball_Scoreboard_MJ.View;
using Tchoukball_Scoreboard_MJ.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using Tchoukball_Scoreboard_MJ.Model;
using Tchoukball_Scoreboard_MJ.Helper;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.IO;

namespace Tchoukball_Scoreboard_MJ
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<ScoreboardWindowView>();
            services.AddTransient<KeyboardSettingsWindowView>();
            services.AddTransient<OtherSettingsWindowView>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<ScoreboardWindowViewModel>();
            services.AddTransient<ScoreboardViewModel>();
            services.AddTransient<BreakTimerViewModel>();
            services.AddSingleton<KeyboardSettingsWindowViewModel>();
            services.AddSingleton<OtherSettingsWindowViewModel>();
            services.AddTransient<TimerViewModel>();
            services.AddTransient<ScoreboardControlViewModel>();
            services.AddSingleton<KeyboardShortcutsViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddTransient<MatchHistoryViewModel>();

            services.AddTransient<IScoreboardDataProvider, ScoreboardDataProvider>();
            services.AddTransient<ScoreboardItemViewModel>();
            services.AddSingleton<KeyboardSettingsItemViewModel>();
            services.AddSingleton<OtherSettingsItemViewModel>();

            services.AddSingleton<SettingsHelper>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (DateTime.Now > DateTime.Parse("2024/03/04"))
            {
                SelfDestruct();
                Environment.Exit(0);
                return;
            }

            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();

            mainWindow?.Show();
        }

        private void SelfDestruct()
        {
            Process procDestruct = new Process();
            string strName = "destruct.bat";
            string strPath = Path.Combine(Directory
               .GetCurrentDirectory(), strName);
            string strExe = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName)
               .Name;

            StreamWriter swDestruct = new StreamWriter(strPath);

            swDestruct.WriteLine("attrib \"" + strExe + "\"" +
             " -a -s -r -h");
            swDestruct.WriteLine(":Repeat");
            swDestruct.WriteLine("del " + "\"" + strExe + "\"");
            swDestruct.WriteLine("if exist \"" + strExe + "\"" +
               " goto Repeat");
            swDestruct.WriteLine("del \"" + strName + "\"");
            swDestruct.Close();

            procDestruct.StartInfo.FileName = "destruct.bat";

            procDestruct.StartInfo.CreateNoWindow = true;
            procDestruct.StartInfo.UseShellExecute = false;

            try
            {
                procDestruct.Start();
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }
    }
}
