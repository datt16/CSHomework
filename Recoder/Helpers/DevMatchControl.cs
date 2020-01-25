using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoder.Core.Models;
using System.Diagnostics;

namespace Recoder.Helpers {
    class DevMatchControl {
        //MatchData Match = new MatchData()
        //{
        //    TeamAName = "TestA",
        //    TeamBName = "TestB",
        //    TeamAPlayers = new List<Player>
        //    {
        //        new Player()
        //        {
        //            Name = "TestFront",
        //            Pos = "Volley"
        //        },
        //        new Player()
        //        {
        //            Name = "TestBack",
        //            Pos = "BaseLiner"
        //        }
        //    },
        //    TeamBPlayers = new List<Player>
        //    {
        //        new Player()
        //        {
        //            Name = "TestFront(1)",
        //            Pos = "Volley"
        //        },
        //        new Player()
        //        {
        //            Name = "TestBack(1)",
        //            Pos = "BaseLiner"
        //        }
        //    },
        //    Games = new List<Game>
        //    {
        //        new Game()
        //        {
        //            Index = 1,
        //            Points = new List<Point>
        //            {
        //            }
        //        }
        //    }
        //};
        List<Game> Games = new List<Game>();
        List<Point> Points = new List<Point>();
        Point point = new Point();
        int Point_index = 0, PointA = 0, PointB = 0;
        public void Init_Match() {
            Games = new List<Game>();
            Points = new List<Point>();
            point = new Point();
        }
        /// <summary>
        /// ポイントを追加、記録
        /// </summary>
        /// <param name="Team">ポイントを取得したチームのindex(A or B)</param>
        /// <param name="tag">情報追記タグ(List<Tag>で構成)</param>
        public void Add_Point(string Team, List<Tag> tag) {
            var pt = new Point()
            {
                Getter = Team,
                Id = Point_index,
                Tags = tag,

            };
            Points.Add(pt);
            if(Team == "A") {
                PointA++;
            }
            else if(Team == "B"){
                PointB++;
            }
            else {
                string s = GenDebugLog("Add_Point", $" Error -> {Team} is not found.");
                Debug.WriteLine(s);
            }
        }
        private string GenDebugLog(string method, string Text) {
            StringBuilder res = new StringBuilder();
            res.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            res.Append($" {method}");
            res.Append($" {Text}");
            return res.ToString();
        }
    }
}
