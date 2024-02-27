using Tchoukball_Scoreboard_MJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Tchoukball_Scoreboard_MJ.CustomEnum;
using System.Globalization;
using System.Drawing;
using System.Diagnostics;

namespace Tchoukball_Scoreboard_MJ.Data
{
    public interface IScoreboardDataProvider
    {
        Task<IEnumerable<Scoreboard>?> GetAsync();
    }
    public class ScoreboardDataProvider : IScoreboardDataProvider
    {
        private string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

        public async Task<IEnumerable<Scoreboard>?> GetAsync()
        {
            List<Scoreboard> matchList = new();
            string fileName = BaseDirectoryPath + "Resources\\Template.xlsx";
            try
            {
                var workbook = new XLWorkbook(fileName);
                var ws1 = workbook.Worksheet(1);
                var rows = ws1.RowsUsed().Skip(1);  //skip first header row

                foreach (var row in rows)
                {
                    if (!row.IsEmpty())
                    {
                        string matchNo = row.Cell((int)ColumnEnum.MatchNo).GetValue<String>();
                        string home = row.Cell((int)ColumnEnum.Home).GetValue<String>();
                        string away = row.Cell((int)ColumnEnum.Away).GetValue<String>();
                        string periodTimeStr = row.Cell((int)ColumnEnum.PeriodTime).GetValue<String>();
                        string breakTimeStr = row.Cell((int)ColumnEnum.BreakTime).GetValue<String>();
                        string category = row.Cell((int)ColumnEnum.Category).GetValue<String>();
                        string categoryColor = GetColor(row.Cell((int)ColumnEnum.CategoryColor), workbook) ?? "#FFD3D3D3";

                        TimeSpan periodTime = new TimeSpan();
                        TimeSpan breakTime = new TimeSpan();
                        TimeSpan.TryParseExact(periodTimeStr, "m\\:ss", CultureInfo.InvariantCulture, out periodTime);
                        TimeSpan.TryParseExact(breakTimeStr, "m\\:ss", CultureInfo.InvariantCulture, out breakTime);

                        matchList.Add(new Scoreboard
                        {
                            MatchNo = matchNo,
                            HomeName = home,
                            AwayName = away,
                            HomeLogo = BaseDirectoryPath + "TeamLogos\\" + home + ".jpg",
                            AwayLogo = BaseDirectoryPath + "TeamLogos\\" + away + ".jpg",
                            HomePossession = true,
                            AwayPossession = false,
                            Period = 1,
                            PeriodTimer = periodTime,
                            BreakTimer = breakTime,
                            PeriodTime = periodTime,
                            BreakTime = breakTime,
                            Category = category,
                            CategoryColor = "#" + categoryColor
                        });
                    }
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                matchList.Add(new Scoreboard
                {
                    HomeName = "Home",
                    AwayName = "Away",
                    HomePossession = true,
                    AwayPossession = false,
                    Period = 1,
                    PeriodTimer = new TimeSpan(0, 0, 0),
                    BreakTimer = new TimeSpan(0, 0, 0),
                    PeriodTime = new TimeSpan(0, 0, 0),
                    BreakTime = new TimeSpan(0, 0, 0),
                    CategoryColor = "#FFD3D3D3"
                });
            }

            return matchList;
        }

        private string? GetColor(IXLCell cell, XLWorkbook wb)
        {
            string? color = null;
            System.Drawing.Color? xlColor = null;

            switch (cell.Style.Fill.BackgroundColor.ColorType)
            {
                case XLColorType.Color:
                    xlColor = cell.Style.Fill.BackgroundColor.Color;
                    break;
                case XLColorType.Theme:
                    switch (cell.Style.Fill.BackgroundColor.ThemeColor)
                    {
                        case XLThemeColor.Background1:
                            xlColor = wb.Theme.Background1.Color;
                            break;
                        case XLThemeColor.Text1:
                            xlColor = wb.Theme.Text1.Color;
                            break;
                        case XLThemeColor.Background2:
                            xlColor = wb.Theme.Background2.Color;
                            break;
                        case XLThemeColor.Text2:
                            xlColor = wb.Theme.Text2.Color;
                            break;
                        case XLThemeColor.Accent1:
                            xlColor = wb.Theme.Accent1.Color;
                            break;
                        case XLThemeColor.Accent2:
                            xlColor = wb.Theme.Accent2.Color;
                            break;
                        case XLThemeColor.Accent3:
                            xlColor = wb.Theme.Accent3.Color;
                            break;
                        case XLThemeColor.Accent4:
                            xlColor = wb.Theme.Accent4.Color;
                            break;
                        case XLThemeColor.Accent5:
                            xlColor = wb.Theme.Accent5.Color;
                            break;
                        case XLThemeColor.Accent6:
                            xlColor = wb.Theme.Accent6.Color;
                            break;
                        case XLThemeColor.Hyperlink:
                            xlColor = wb.Theme.Hyperlink.Color;
                            break;
                        case XLThemeColor.FollowedHyperlink:
                            xlColor = wb.Theme.FollowedHyperlink.Color;
                            break;
                        default:
                            break;
                    }
                    break;
                case XLColorType.Indexed:
                    break;
                default:
                    break;
            }

            try
            {
                if (xlColor != null)
                {
                    color = xlColor?.ToArgb().ToString("X8");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return color;
        }
    }
}
