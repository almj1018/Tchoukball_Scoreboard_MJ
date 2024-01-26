using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tchoukball_Scoreboard_MJ.Model;

public class OtherSettings
{
    public bool EnableBreakTimerScreen { get; set; }
    public bool SoundBuzzer { get; set; }
    public bool DisableTimerResetButtonWhileTimerIsStarted { get; set; }
    public bool AutoIncrementPeriod { get; set; }
    public bool AutoSetBreakTimer { get; set; }
    public bool AutoStartBreakTimer { get; set; }


    public TimeSpan PeriodTime { get; set; }
    public TimeSpan BreakTime { get; set; }
    public string? DefaultHomeName { get; set; }
    public string? DefaultAwayName { get; set; }
}
