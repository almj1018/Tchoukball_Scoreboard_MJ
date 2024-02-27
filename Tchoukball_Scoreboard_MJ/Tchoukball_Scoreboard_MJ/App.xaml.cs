using Tchoukball_Scoreboard_MJ.Data;
using Tchoukball_Scoreboard_MJ.View;
using Tchoukball_Scoreboard_MJ.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using Tchoukball_Scoreboard_MJ.Model;
using Tchoukball_Scoreboard_MJ.Helper;

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
            base.OnStartup(e);
            
            var mainWindow = _serviceProvider.GetService<MainWindow>();

            mainWindow?.Show();
        }
    }

}
