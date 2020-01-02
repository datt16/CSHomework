using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judge.Classes {
    class Match {
        public string GenerateCountText(int CountA,int CountB) {
            if (CountA >= 10) {
                return CountA.ToString() + " - 0" + CountB.ToString();
            }else if(CountB >= 10) {
                return "0" + CountA.ToString() + " - " + CountB.ToString();
            }
            return CountA.ToString() + " - " + CountB.ToString();
        }
    }
}
