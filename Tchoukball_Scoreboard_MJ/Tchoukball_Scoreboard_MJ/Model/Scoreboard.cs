using System.Diagnostics;

namespace Tchoukball_Scoreboard_MJ.Model
{
    public class Scoreboard
    {
        public string? MatchNo { get; set; }
        public int Period { get; set; }
        public TimeSpan PeriodTimer { get; set; }
        public TimeSpan BreakTimer { get; set; }
        public string? HomeName { get; set; }
        public string? AwayName { get; set; }
        public int HomePoints { get; set; }
        public int AwayPoints { get; set; }
        public string? HomeLogo { get; set; }
        public string? AwayLogo { get; set; }
        public bool HomePossession { get; set; }
        public bool AwayPossession { get; set; }

        public ObservableDictionary<int, PeriodScore> ScoreHistory { get; set; } = new();
        public TimeSpan PeriodTime { get; set; }
        public TimeSpan BreakTime { get; set; }

        public string? Category { get; set; }
        public string? CategoryColor { get; set; }
    }
}
