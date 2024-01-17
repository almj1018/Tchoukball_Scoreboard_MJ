using Tchoukball_Scoreboard_MJ.Data;
using Tchoukball_Scoreboard_MJ.View;
using Tchoukball_Scoreboard_MJ.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using Tchoukball_Scoreboard_MJ.Model;

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

            services.AddTransient<MainViewModel>();
            services.AddSingleton<ControlsViewModel>();
            services.AddSingleton<ScoreboardWindowViewModel>();
            services.AddSingleton<ScoreboardViewModel>();
            services.AddTransient<BreakTimerViewModel>();

            //services.AddTransient<IScoreboardDataProvider, ScoreboardDataProvider>();
            services.AddSingleton<ScoreboardItemViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();

            mainWindow?.Show();
        }
    }

}
