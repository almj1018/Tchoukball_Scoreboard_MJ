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
        public const int TimerStartStop = 59;
        public const int HomeAddPoint = 67;
        public const int HomeMinusPoint = 69;
        public const int AwayAddPoint = 144;
        public const int AwayMinusPoint = 142;
        public const int PeriodAdd = 24;
        public const int PeriodMinus = 26;
        public const int SwitchPossesion = 61;

        public const Key TimerStartStopKey = Key.P;
        public const Key HomeAddPointKey = Key.X;
        public const Key HomeMinusPointKey = Key.Z;
        public const Key AwayAddPointKey = Key.OemPeriod;
        public const Key AwayMinusPointKey = Key.OemComma;
        public const Key PeriodAddKey = Key.Up;
        public const Key PeriodMinusKey = Key.Down;
        public const Key SwitchPossesionKey = Key.R;
    }
}
