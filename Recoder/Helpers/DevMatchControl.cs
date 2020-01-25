using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoder.Core.Models;

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
        public void Init_Match() {
            Games = new List<Game>();
            Points = new List<Point>();
            point = new Point();
        }
        public void Add_Point(string Team) {

        }
    }
}
