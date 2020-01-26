using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoder.Core.Models;

namespace Recoder.Helpers {
    class MatchHelper {
        
        public string GenerateCountText(int CountA, int CountB, string type = "SoftTennis") {
            if (type == "Base") {
                if (CountA >= 10) {
                    return CountA.ToString() + " - 0" + CountB.ToString();
                }
                else if (CountB >= 10) {
                    return "0" + CountA.ToString() + " - " + CountB.ToString();
                }
            }
            else if (type == "SoftTennis") {
                if (CountA > CountB && CountA >= 4) {
                    return "Adv - " + CountB.ToString();
                }
                else if (CountB > CountA && CountB >= 4) {
                    return CountA.ToString() + " - Adv";
                }
            }
            else if (type == "SoftTennis_Final") {
                if (CountA > CountB && CountA >= 6) {
                    return "Adv - " + CountB.ToString();
                }
                else if (CountB > CountA && CountB >= 6) {
                    return CountA.ToString() + " - Adv";
                }
            }
            return CountA.ToString() + " - " + CountB.ToString();
        }
    }
}
