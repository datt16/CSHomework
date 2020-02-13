using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Recoder.Core.Models;
using Windows.Storage;

namespace Recoder.Core.Services {
    class SampleMatchService {
        private IEnumerable<MatchData> _allMatchDatas;

        private static IEnumerable<MatchData> SampleMatch() {
            return new List<MatchData>()
            {

            new MatchData()
            {
                Title = "SampleData",
                TeamAName = "TeamA",
                TeamBName = "TeamB",
                GamesCount = 3,
                TeamAPlayers = new List<Player>()
                {
                    new Player()
                    {
                        Name = "PlayerA",
                        Age = 16,
                        Pos = Player_Position.Volleyer
                    },
                    new Player()
                    {
                        Name = "PlayerB",
                        Age = 16,
                        Pos = Player_Position.Baseliner
                    }
                },
                TeamBPlayers = new List<Player>()
                {
                    new Player()
                    {
                        Name = "PlayerC",
                        Age = 16,
                        Pos = Player_Position.Volleyer
                    },
                    new Player()
                    {
                        Name = "PlayerD",
                        Age = 16,
                        Pos = Player_Position.Baseliner
                    }
                }
            }
            };
        }

        public static MatchData test = new MatchData()
        {
            Title = "SampleData",
            TeamAName = "TeamA",
            TeamBName = "TeamB",
            GamesCount = 3,
            TeamAPlayers = new List<Player>()
                {
                    new Player()
                    {
                        Name = "PlayerA",
                        Age = 16,
                        Pos = Player_Position.Volleyer
                    },
                    new Player()
                    {
                        Name = "PlayerB",
                        Age = 16,
                        Pos = Player_Position.Baseliner
                    }
                },
            TeamBPlayers = new List<Player>()
                {
                    new Player()
                    {
                        Name = "PlayerC",
                        Age = 16,
                        Pos = Player_Position.Volleyer
                    },
                    new Player()
                    {
                        Name = "PlayerD",
                        Age = 16,
                        Pos = Player_Position.Baseliner
                    }
                }
        };
    }
}
