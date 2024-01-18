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
            await Task.Delay(10);
            return new Scoreboard
            {
                Period = 0,
                Timer = new TimeSpan(0, 15, 0),
                HomeName = "Home",
                GuestName = "Guest",
                HomePoints = 0,
                GuestPoints = 0
            };
        }
    }
}
