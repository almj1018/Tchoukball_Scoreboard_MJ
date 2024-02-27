using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchoukball_Scoreboard_MJ.Command;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using Tchoukball_Scoreboard_MJ.Data;
using Tchoukball_Scoreboard_MJ.Model;
using DataTable = System.Data.DataTable;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IScoreboardDataProvider _dataProvider;
        private List<ScoreboardItemViewModel>? _scoreboardCollection;
        private ScoreboardItemViewModel? _scoreboardItem;
        private OtherSettingsItemViewModel? _otherSettings;
        private TimerViewModel? _timerViewModel;
        private DataSet ds;
        public string ScoreDataFileName;

        public event EventHandler<SwitchUserControlEventArgs>? SwitchUC;

        public HomeViewModel(OtherSettingsItemViewModel settings, IScoreboardDataProvider scoreboardDataProvider)
        {
            _otherSettings = settings;

            _dataProvider = scoreboardDataProvider;
            Load();

            SelectedMatch = MatchList.FirstOrDefault();

            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);

            System.Data.DataTable dt = new DataTable(DateTime.Now.ToString("yyMMddHHmmss"));
            DataColumn Home = new DataColumn("Home", typeof(string));
            DataColumn dc = new DataColumn("VS", typeof(string));
            DataColumn Away = new DataColumn("Away", typeof(string));
            dt.Columns.Add(Home);
            dt.Columns.Add(dc);
            dt.Columns.Add(Away);

            ds = new DataSet();
            ds.Tables.Add(dt);

            int count = 1;
            string DateTimeToday = DateTime.Today.ToString("yyMMdd");
            ScoreDataFileName = string.Format("{0}{1}", DateTimeToday, ".xlsx");
            while (File.Exists(ScoreDataFileName))
            {
                ScoreDataFileName = string.Format("{0}({1}){2}", DateTimeToday, count, ".xlsx");
                count++;
            }
        }

        private void Load()
        {
            if (MatchList.Any())
            {
                return;
            }

            var matches = _dataProvider.GetAsync().Result;
            if (matches != null)
            {
                foreach (var match in matches)
                {
                    MatchList.Add(new ScoreboardItemViewModel(match, _otherSettings));
                }
            }
        }

        public ObservableCollection<ScoreboardItemViewModel> MatchList { get; set; } = new();

        public ScoreboardItemViewModel? SelectedMatch
        {
            get => _scoreboardItem;
            set
            {
                var eventArgs = new SwitchUserControlEventArgs { MatchScoreboard = _scoreboardItem };
                _scoreboardItem = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsMatchSelected));
                SwitchUC?.Invoke(this, eventArgs);
            }
        }

        public bool IsMatchSelected => SelectedMatch != null;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        private void Add(object? parameter)
        {
            var match = new Scoreboard 
            {
                Period = 1,
                HomePoints = 0,
                AwayPoints = 0,
                HomeLogo = null,
                AwayLogo = null,
                HomePossession = true,
                AwayPossession = false,
                PeriodTimer = _otherSettings.PeriodTime,
                BreakTimer = _otherSettings.BreakTime,
                HomeName = _otherSettings.DefaultHomeName,
                AwayName = _otherSettings.DefaultAwayName,
                PeriodTime = _otherSettings.PeriodTime,
                BreakTime = _otherSettings.BreakTime
            };
            var viewModel = new ScoreboardItemViewModel(match, _otherSettings);

            MatchList.Add(viewModel);
            SelectedMatch = viewModel;
        }

        private void Delete(object? parameter)
        {
            if (SelectedMatch != null)
            {
                MatchList.Remove(SelectedMatch);
                SelectedMatch = MatchList.FirstOrDefault();
            }
        }

        private bool CanDelete(object? parameter)
        {
            return SelectedMatch != null;
        }

        public bool? ExportScoreData()
        {
            bool anyRecords = false;

            foreach (var match in MatchList)
            {
                ds.Tables[0].Rows.Add("", "", "");
                ds.Tables[0].Rows.Add(match.HomeName, "VS", match.AwayName);

                var a = new List<int>(match.PeriodScores.Keys);

                for (int i = 0; i < match.PeriodScores.Count; i++)
                {
                    anyRecords = true;
                    if (match.PeriodScores.ContainsKey(a[i]))
                    {
                        ds.Tables[0].Rows.Add(match.PeriodScores[a[i]].HomeScore, a[i], match.PeriodScores[a[i]].AwayScore);
                    }
                }
            }
            if (anyRecords)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    try
                    {
                        System.Data.DataTable dt = ds.Tables[0];
                        string[] columnNames = (from dc in dt.Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();
                        // int Cell = 0;  

                        int count = columnNames.Length;
                        object[] array = new object[count];
                        dt.Rows.Add(array);
                        wb.Worksheets.Add(dt, ds.Tables[0].TableName);

                        wb.SaveAs(ScoreDataFileName);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {

                    }
                }
                return true;
            }
            return null;
        }

        public void Refresh()
        {
            ObservableCollection<ScoreboardItemViewModel> newMatchList = new();
            var matches = _dataProvider.GetAsync().Result;
            if (matches != null)
            {
                foreach (var match in matches)
                {
                    newMatchList.Add(new ScoreboardItemViewModel(match, _otherSettings));
                }
            }

            MatchList = newMatchList;

            SelectedMatch = MatchList.FirstOrDefault();
        }
    }
}
