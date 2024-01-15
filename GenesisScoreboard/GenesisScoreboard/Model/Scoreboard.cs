﻿using System.Diagnostics;

namespace GenesisScoreboard.Model
{
    public class Scoreboard
    {
        public int Period { get; set; }
        public TimeSpan Timer { get; set; }
        public string? HomeName { get; set; }
        public string? GuestName { get; set; }
        public int HomePoints { get; set; }
        public int GuestPoints { get; set; }
    }
}
