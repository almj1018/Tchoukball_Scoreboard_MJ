using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tchoukball_Scoreboard_MJ
{
    public class DefaultSettings
    {
        //default keyboard settings
        public const Key TimerStartStopKey = Key.Space;
        public const Key HomeAddPointKey = Key.X;
        public const Key HomeMinusPointKey = Key.Z;
        public const Key AwayAddPointKey = Key.OemPeriod;
        public const Key AwayMinusPointKey = Key.OemComma;
        public const Key PeriodAddKey = Key.Up;
        public const Key PeriodMinusKey = Key.Down;
        public const Key SwitchPossesionKey = Key.R;

        //default other settings
        public const bool EnableBreakTimerScreen = false;
        public const bool DisableTimerResetButtonWhileTimerIsStarted = false;
        public const bool AutoIncrementPeriod = true;

        public static TimeSpan PeriodTime = new TimeSpan(0, 15, 0);
        public static TimeSpan BreakTime = new TimeSpan(0, 2, 0);
        public const string DefaultHomeName = "Home";
        public const string DefaultAwayName = "Away";
    }
}
