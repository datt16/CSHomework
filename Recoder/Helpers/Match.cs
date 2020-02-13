using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoder.Core.Models;
using System.Diagnostics;
using Recoder.Core.Services;
using Windows.UI.Popups;

namespace Recoder.Helpers {
    using static Control_Alias;
    public class Match {
        public MatchData data = new MatchData();
        List<Game> Games = new List<Game>();
        List<Point> Points = new List<Point>();
        Point point = new Point();
        public int Point_index = 1, PointA = 0, PointB = 0, Game_Index = 1, GamesCount = 3, GCountA = 0, GCountB = 0;
        public static string Server = TEAM_A, ReServer = TEAM_B;
        public static string Side_A = SIDE_LEFT, Side_B = SIDE_RIGHT;
        public static bool IsFinal = false, IsMatchEnd = false;
        public Game GameCache;

        public void Init_Match() {
            Games = new List<Game>();
            Points = new List<Point>();
            point = new Point();
        }

        public void ChangeServer() {
            if (Server == TEAM_A) {
                Server = TEAM_B;
                ReServer = TEAM_A;
            }
            else if (Server == TEAM_B) {
                Server = TEAM_A;
                ReServer = TEAM_B;
            }
        }

        public void ChangeSides() {
            // TODO:サイドが変わったとき、ポイントが分かりづらいのでどうにかして修正
            if (Side_A == SIDE_LEFT) Side_A = SIDE_RIGHT;
            else Side_A = SIDE_LEFT;
            if (Side_B == SIDE_LEFT) Side_B = SIDE_RIGHT;
            else Side_B = SIDE_LEFT;
        }

        /// <summary>
        /// ポイントを追加、記録
        /// </summary>
        public string Add_Point(string Team, List<Tag> tag, int rally = 0) {
            var pt = new Point()
            {
                Getter = Team,
                Id = Point_index,
                Tags = tag,
                Rally = rally
            };
            Points.Add(pt);
            if(Team == TEAM_A) PointA++;
            else if(Team == TEAM_B) PointB++;

            if (!IsFinal) {
                if (Point_index > 6) {
                    if (PointA > PointB + 1 || PointB > PointA + 1) {
                        End_Game();
                        if (IsMatchEnd) {
                            
                            return "MatchEnd";
                        }
                        return "EndGame";
                    }
                }
                else if (PointA == 4 || PointB == 4) {
                    End_Game();
                    if (IsMatchEnd) {
                        // await new MessageDialog($"{data.TeamAName} : {GCountA} - {GCountB} : {data.TeamBName}", $"試合終了").ShowAsync();
                        return "MatchEnd";
                    }
                    return "EndGame";
                }
                Point_index++;
            }
            else {
                if (Point_index > 12) {
                    if (PointA > PointB + 1 || PointB > PointA + 1) {
                        End_Game();
                        if (IsMatchEnd) {
                            return "MatchEnd";
                        }
                        return "EndGame";
                    }
                }
                else if (PointA == 7 || PointB == 7) {
                    End_Game();
                    if (IsMatchEnd) {
                        return "MatchEnd";
                    }
                    return "EndGame";
                }
                Point_index++;
            }
            
            return "";
        }

        public async void End_Game() {
            var gm = new Game()
            {
                Index = Game_Index,
                Points = this.Points,
                Server = Server
            };
            ChangeServer();
            if (Game_Index % 2 == 1) {
                ChangeSides();
            }
            Games.Add(gm);
            Game_Index++;
            if (PointA > PointB) GCountA++;
            else if (PointB > PointA) GCountB++;
            if (Game_Index == GamesCount)   {
                // GoFinalGame;
                IsFinal = true;
            }

            if (GCountA == GamesCount / 2 + 1 || GCountB == GamesCount / 2 + 1) {
                // EndMatch;
                data.Games = this.Games;
                IsMatchEnd = true;
                return;
            }
            int ptA = PointA, ptB = PointB;
            (Point_index, PointA, PointB) = (1, 0, 0);
            await new MessageDialog($"{data.TeamAName} : {ptA} - {ptB} : {data.TeamBName}", $"第{Game_Index - 1}ゲーム終了").ShowAsync();
            List<Point> Points = new List<Point>();
        }

        public void Undo() {
            if (Points.Last().Getter == "A") PointA--;
            else if (Points.Last().Getter == "B") PointB--;
            else Debug.WriteLine("Error: Undo");
            // ゲームを仮生成
            Points.RemoveAt(Points.Count() - 1);
            Point_index--;
            GameCache = new Game()
            {
                Index = Game_Index,
                Points = Points,
                Server = Server
            };
        }

        private string GenDebugLog(string method, string Text) {
            StringBuilder res = new StringBuilder();
            res.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            res.Append($" {method}");
            res.Append($" {Text}");
            return res.ToString();
        }

        public void MakeMatchFromInput() {
            
        }
    }
}
