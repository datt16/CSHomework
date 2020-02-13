using System;
using System.Collections.Generic;
using System.Text;

namespace Recoder.Core.Models {
    public class MatchData {
        public string Title { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int GamesCount { get; set; }

        public bool IsFinalDisable { get; set; } = false;

        public bool IsDueceDisable { get; set; } = false;

        public int TeamA_GamePoint { get; set; } = 0;

        public int TeamB_GamePoint { get; set; } = 0;

        public List<Game> Games { get; set; }

        public List<Player> TeamAPlayers { get; set; }

        public List<Player> TeamBPlayers { get; set; }

        public string ShortDiscription { get; set; }

        public void SetDescription() {
            ShortDiscription = $"{TeamAName} vs {TeamBName} : {TeamA_GamePoint} - {TeamB_GamePoint}";
        }
    }

}
