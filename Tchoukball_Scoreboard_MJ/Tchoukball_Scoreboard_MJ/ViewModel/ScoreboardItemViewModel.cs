using Tchoukball_Scoreboard_MJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;
using Tchoukball_Scoreboard_MJ.CustomEventArgs;
using System.Windows.Navigation;
using System.Media;
using System.Windows;
using System.Data;
using System.Data.Common;
using System.IO;
using ClosedXML.Excel;

namespace Tchoukball_Scoreboard_MJ.ViewModel
{
    public class ScoreboardItemViewModel : ValidationViewModelBase
    {
        private Scoreboard _model;
        private OtherSettingsItemViewModel _otherSettingsItemViewModel;
        public bool IsBreak = false;
        private DataSet ds;
        public string ScoreDataFileName;

        public ScoreboardItemViewModel(OtherSettingsItemViewModel otherSettingsItemViewModel)
        {
            _otherSettingsItemViewModel = otherSettingsItemViewModel;
            _model = new Scoreboard
            {
                Period = 1,
                HomePoints = 0,
                AwayPoints = 0,
                HomeLogo = null,
                AwayLogo = null,
                HomePossession = true,
                AwayPossession = false,
                PeriodTimer = _otherSettingsItemViewModel.PeriodTime,
                BreakTimer = _otherSettingsItemViewModel.BreakTime,
                HomeName = _otherSettingsItemViewModel.DefaultHomeName,
                AwayName = _otherSettingsItemViewModel.DefaultAwayName
            };
            DataTable dt = new DataTable(DateTime.Now.ToString("yyMMddHHmmss"));
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

        #region Other Settings Properties
        public bool EnableBreakTimerScreen => _otherSettingsItemViewModel.EnableBreakTimerScreen;
        public bool AutoIncrementPeriod => _otherSettingsItemViewModel.AutoIncrementPeriod;
        public bool SoundBuzzer => _otherSettingsItemViewModel.SoundBuzzer;
        public bool AutoSetBreakTimer => _otherSettingsItemViewModel.AutoSetBreakTimer;
        public bool AutoStartBreakTimer => _otherSettingsItemViewModel.AutoStartBreakTimer;
        public bool DisableTimerResetButtonWhileTimerIsStarted => _otherSettingsItemViewModel.DisableTimerResetButtonWhileTimerIsStarted;
        #endregion

        public void ResetScoreboard()
        {
            Period = 1;
            HomePoints = 0;
            AwayPoints = 0;
            HomeLogo = null;
            AwayLogo = null;
            HomePossession = true;
            AwayPossession = false;
            PeriodTimer = _otherSettingsItemViewModel.PeriodTime;
            BreakTimer = _otherSettingsItemViewModel.BreakTime;
            HomeName = _otherSettingsItemViewModel.DefaultHomeName;
            AwayName = _otherSettingsItemViewModel.DefaultAwayName;

            IsBreak = false;
        }

        public int Period
        {
            get => _model.Period;
            set
            {
                if (value >= 0)
                {
                    _model.Period = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TimeSpan PeriodTimer
        {
            get => _model.PeriodTimer;
            set
            {
                _model.PeriodTimer = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan BreakTimer
        {
            get => _model.BreakTimer;
            set
            {
                _model.BreakTimer = value;
                RaisePropertyChanged();
            }
        }

        public string? HomeName
        {
            get => _model.HomeName;
            set
            {
                _model.HomeName = value;
                RaisePropertyChanged();
            }
        }

        public string? AwayName
        {
            get => _model.AwayName;
            set
            {
                _model.AwayName = value;
                RaisePropertyChanged();
            }
        }

        public int HomePoints
        {
            get => _model.HomePoints;
            set
            {
                if (value >= 0)
                {
                    _model.HomePoints = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int AwayPoints
        {
            get => _model.AwayPoints;
            set
            {
                if (value >= 0)
                {
                    _model.AwayPoints = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string? HomeLogo
        {
            get => _model.HomeLogo;
            set
            {
                _model.HomeLogo = value;
                RaisePropertyChanged();
            }
        }

        public string? AwayLogo
        {
            get => _model.AwayLogo;
            set
            {
                _model.AwayLogo = value;
                RaisePropertyChanged();
            }
        }

        public bool HomePossession
        {
            get => _model.HomePossession;
            set
            {
                _model.HomePossession = value;
                RaisePropertyChanged();
            }
        }

        public bool AwayPossession
        {
            get => _model.AwayPossession;
            set
            {
                _model.AwayPossession = value;
                RaisePropertyChanged();
            }
        }

        public void RecordScore()
        {
            if (Period == 1)
            {
                ds.Tables[0].Rows.Add("", "", "");
                ds.Tables[0].Rows.Add(HomeName, "VS", AwayName);
                ds.Tables[0].Rows.Add(HomePoints, ":", AwayPoints);
            }
            else
            {
                ds.Tables[0].Rows.Add(HomePoints, ":", AwayPoints);
            }
        }

        public bool? ExportScoreData()
        {
            if (ds.Tables[0].Rows.Count > 1)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    try
                    {
                        DataTable dt = ds.Tables[0];
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

        public TimeSpan GetTimer(bool IsReset)
        {
            if (IsReset)
            {
                return IsBreak ? _otherSettingsItemViewModel.BreakTime : _otherSettingsItemViewModel.PeriodTime;
            }
            else
            {
                if (_otherSettingsItemViewModel.AutoSetBreakTimer)
                {
                    if (!IsBreak)
                    {
                        if (AutoIncrementPeriod)
                        {
                            Period++;
                        }
                        IsBreak = true;
                        return _otherSettingsItemViewModel.BreakTime;
                    }
                }
                IsBreak = false;
                return _otherSettingsItemViewModel.PeriodTime;
            }
        }
    }
}
