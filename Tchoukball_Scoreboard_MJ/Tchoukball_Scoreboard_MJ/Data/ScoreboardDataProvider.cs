using Tchoukball_Scoreboard_MJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tchoukball_Scoreboard_MJ.Data
{
    public interface IScoreboardDataProvider
    {
        Task<Scoreboard?> GetAsync();
    }
    public class ScoreboardDataProvider : IScoreboardDataProvider
    {
        public async Task<Scoreboard?> GetAsync()
        {
            await Task.Delay(100);

            return new Scoreboard
            {
                Period = 0,
                HomeName = "Home",
                AwayName = "Away",
                HomePoints = 0,
                AwayPoints = 0,
                HomeLogo=null,
                AwayLogo=null,
                HomePossession=true,
                AwayPossession=false
            };
        }
    }
}
