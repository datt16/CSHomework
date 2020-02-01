using System;
using System.Collections.Generic;
using System.Text;

namespace Recoder.Core.Models {
    public class Game {
        public int Index { get; set; }

        public string Server { get; set; }

        public List<Point> Points { get; set; }
    }
}
