using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoder.Core.Models;
using System.Diagnostics;

namespace Recoder.Helpers {
    class Match {
        MatchData data = new MatchData();
        List<Game> Games = new List<Game>();
        List<Point> Points = new List<Point>();
        Point point = new Point();
        public int Point_index = 1, PointA = 0, PointB = 0, Game_Index = 1, GamesCount = 3, GCountA = 0, GCountB = 0;
        public string Server = "A", ReServer = "B";

        public void Init_Match() {
            Games = new List<Game>();
            Points = new List<Point>();
            point = new Point();
        }

        public void Setup_Tester() {
            data = new MatchData()
            {
                GamesCount = 3,
                TeamAName = "TeamA",
                TeamBName = "TeamB",
                TeamAPlayers = new List<Player>(){
                    new Player()
                    {
                        Name = "Matt",
                        Pos = "volley"
                    },
                    new Player()
                    {
                        Name = "Jack",
                        Pos = "baseliner"
                    }
                },
                TeamBPlayers = new List<Player>(){
                    new Player()
                    {
                        Name = "Yuuri",
                        Pos = "volley"
                    },
                    new Player()
                    {
                        Name = "Mary",
                        Pos = "baseliner"
                    }
                },
            };
            this.GamesCount = data.GamesCount;
            Point_index = 1; Game_Index = 1;
            Server = "A";
        }

        public void ChangeServer() {
            if (Server == "A") { Server = "B"; ReServer = "A"; }
            else if (Server == "B") { Server = "A"; ReServer = "B"; }
        }

        /// <summary>
        /// ポイントを追加、記録
        /// </summary>
        public String Add_Point(string Team, List<Tag> tag, int rally = 0) {
            var pt = new Point()
            {
                Getter = Team,
                Id = Point_index,
                Tags = tag,
                Rally = rally
            };
            Points.Add(pt);
            if(Team == "A") PointA++;
            else if(Team == "B") PointB++;
            else {
                string s = GenDebugLog("Add_Point", $" Error -> {Team} is not found.");
                Debug.WriteLine(s);
            }
            if (Point_index > 6) {
                if (PointA > PointB + 1 || PointB > PointA + 1) {
                    End_Game();
                    return "EndGame";
                }
            }
            else if (PointA == 4 || PointB == 4) {
                End_Game();
                return "EndGame";
            }
            Point_index++;
            return "";
        }

        public void End_Game() {
            var gm = new Game()
            {
                Index = Game_Index,
                Points = this.Points,
                Server = Server
            };
            ChangeServer();
            Games.Add(gm);
            Game_Index++;
            if (PointA > PointB) GCountA++;
            else if (PointB > PointA) GCountB++;
            if (Game_Index == GamesCount) {
                // GoFinalGame;
            }
            else if (GCountA == GamesCount / 2 + 1 || GCountB == GamesCount / 2 + 1)  {
                // EndMatch;
            }
            (Point_index, PointA, PointB) = (0, 0, 0);
            List<Point> Points = new List<Point>();
        }

        public void Undo() {
            if (Points[Points.Count - 1].Getter == "A") PointA--;
            else if (Points[Points.Count - 1].Getter == "A") PointB--;
            Points.RemoveAt(Points.Count - 1);
            Point_index--;
        }

        private string GenDebugLog(string method, string Text) {
            StringBuilder res = new StringBuilder();
            res.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            res.Append($" {method}");
            res.Append($" {Text}");
            return res.ToString();
        }

        public void MakeMatchFromInput(string teamNameA, string teamNameB, List<Player> playersA, List<Player> playersB, int gamesCount) {
            data = new MatchData()
            {
                TeamAName = teamNameA,
                TeamBName = teamNameB,
                TeamAPlayers = playersA,
                TeamBPlayers = playersB,
                GamesCount = gamesCount
            };
            GenDebugLog("MMFI", $"マッチを作成 : {teamNameA} vs {teamNameB}");
            GenDebugLog("MMFI", $"{teamNameA} : {playersA[0].Name}, {playersA[1].Name}");
            GenDebugLog("MMFI", $"{teamNameB} : {playersB[0].Name}, {playersB[1].Name}");
            GenDebugLog("MMFI", $"ゲーム数 : {gamesCount}マッチ");
        }
    }
}
