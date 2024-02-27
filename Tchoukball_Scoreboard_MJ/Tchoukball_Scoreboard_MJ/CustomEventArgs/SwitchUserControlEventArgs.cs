using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tchoukball_Scoreboard_MJ.ViewModel;

namespace Tchoukball_Scoreboard_MJ.CustomEventArgs
{
    public class SwitchUserControlEventArgs
    {
        public ScoreboardItemViewModel MatchScoreboard { get; set; }
    }
}
